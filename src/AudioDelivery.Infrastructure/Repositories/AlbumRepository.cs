using AudioDelivery.Application.Common.Extensions;
using AudioDelivery.Application.Common.Interfaces;
using AudioDelivery.Application.Albums.DTOs;
using AudioDelivery.Application.Search.DTOs;
using AudioDelivery.Infrastructure.Data;
using AudioDelivery.Domain.Entities;
using AudioDelivery.Domain.Events;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AudioDelivery.Infrastructure.Repositories;

/// <summary>
/// Album-specific repository implementation.
/// </summary>
public class AlbumRepository : Repository<Album>, IAlbumRepository
{
    private readonly IImageRepository _imageRepository;
    private readonly IUriGenerationService _uriGenerationService;
    private readonly IHrefGenerationService _hrefGenerationService;

    public AlbumRepository(
        AppDbContext context, 
        IMapper mapper,         
        IImageRepository imageRepository,
        IUriGenerationService uriGenerationService,
        IHrefGenerationService hrefGenerationService) : base(context, mapper)
    {
        _imageRepository = imageRepository;
        _uriGenerationService = uriGenerationService;
        _hrefGenerationService = hrefGenerationService;
    }

    public async Task<AlbumDto> CreateAlbumAsync(CreateAlbumRequest createAlbumRequest, CancellationToken cancellationToken = default)
    {
        var images = await _imageRepository.CreateSizedImagesAsync(createAlbumRequest.Image!, Domain.Enums.ImageType.Album);

        var album = _mapper.Map<CreateAlbumRequest, Album>(createAlbumRequest);

        if (album == null)
        {
            throw new InvalidOperationException($"Failed to map {nameof(CreateAlbumRequest)} to {nameof(Album)}.");
        }

        var artists = await _context.Artists
            .Where(a => createAlbumRequest.ArtistIds.Contains(a.Id))
            .ToListAsync(cancellationToken);

        album.Id = Guid.NewGuid();
        album.Artists = artists;
        album.Images = images;
        album.Uri = _uriGenerationService.GenerateUri(Domain.Enums.EntityType.Album, album.Id);

        await base.AddAsync(album, cancellationToken);
        await base.SaveChangesAsync(cancellationToken);

        return _mapper.Map<AlbumDto>(album);
    }

    public async Task<AlbumDto?> UpdateAlbumAsync(Guid id, UpdateAlbumRequest updateAlbumRequest, CancellationToken cancellationToken = default)
    {
        var album = await _dbSet
            .Include(a => a.Artists)
            .Include(a => a.Images)
            .Include(a => a.Tracks)
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

        if (album == null)
        {
            throw new KeyNotFoundException($"Album with ID {id} not found.");
        }

        var newAlbum = _mapper.Map(updateAlbumRequest, album);

        if (updateAlbumRequest.ArtistIds != null && updateAlbumRequest.ArtistIds.Count > 0)
        {
            newAlbum.Artists = await _context.Artists
                .Where(a => updateAlbumRequest.ArtistIds.Contains(a.Id))
                .ToListAsync(cancellationToken);
        }

        var oldImageIds = album.Images.Select(i => i.Id).ToList();
        var newImages = await _imageRepository.ReplaceSizedImagesAsync(
            oldImageIds, 
            updateAlbumRequest.ImageFile, 
            Domain.Enums.ImageType.Album, 
            cancellationToken
        );

        if (newImages != null && newImages.Count > 0)
            newAlbum.Images = newImages;

        base.Update(newAlbum);
        await base.SaveChangesAsync(cancellationToken);

        return _mapper.Map<AlbumDto>(newAlbum);
    }

    public async Task<bool> DeleteAlbumAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var album = await _dbSet
            .Include(a => a.Images)
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

        if (album == null) return false;

         if (album.Images.Any())
        {
            album.RaiseDomainEvent(new EntityDeletedEvent(
                album.Images.Select(i => i.Id).ToList(),
                album.Images.Select(i => i.Url).ToList()
            ));
        }

        base.Delete(album);
        await base.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> HasAnyArtistsAsync(Guid albumId, CancellationToken cancellationToken = default)
    {
        return await _context.ArtistAlbums
            .AnyAsync(aa => aa.AlbumId == albumId, cancellationToken);
    }

    public async Task<SearchResultDto> SearchAsync(string query, int limit = 50, int offset = 0, CancellationToken cancellationToken = default)
    {
        var searchQuery = base.Query()
            .Where(a => a.Name.Contains(query))
            .OrderBy(a => a.Name)
            .ProjectTo<AlbumSummaryDto>(_mapper.ConfigurationProvider);
            
        var searchDto = new SearchResultDto
        {
            Albums = await searchQuery.ToPaginatedResultAsync(limit, offset,
                    _hrefGenerationService.GeneratePaginatedHref(Domain.Enums.EntityType.Album, limit, offset), cancellationToken)
        };

        return searchDto;
    }
}
