using AudioDelivery.Application.Common.Extensions;
using AudioDelivery.Application.Common.Interfaces;
using AudioDelivery.Application.Artists.DTOs;
using AudioDelivery.Application.Search.DTOs;
using AudioDelivery.Infrastructure.Data;
using AudioDelivery.Domain.Entities;
using AudioDelivery.Domain.Events;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AudioDelivery.Infrastructure.Repositories;

/// <summary>
/// Artist-specific repository implementation.
/// </summary>
public class ArtistRepository : Repository<Artist>, IArtistRepository
{
    private readonly IImageRepository _imageRepository;
    private readonly IUriGenerationService _uriGenerationService;
    private readonly IHrefGenerationService _hrefGenerationService;

    public ArtistRepository(
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

    public async Task<ArtistDto> CreateArtistAsync(CreateArtistRequest createArtistRequest, CancellationToken cancellationToken = default)
    {
        var images = await _imageRepository.CreateSizedImagesAsync(createArtistRequest.Image!, Domain.Enums.ImageType.Artist);

        var artist = _mapper.Map<CreateArtistRequest, Artist>(createArtistRequest);

        if (artist == null)
        {
            throw new InvalidOperationException($"Failed to map {nameof(CreateArtistRequest)} to {nameof(Artist)}.");
        }

        artist.Id = Guid.NewGuid();
        artist.Images = images;
        artist.Uri = _uriGenerationService.GenerateUri(Domain.Enums.EntityType.Artist, artist.Id);

        await base.AddAsync(artist, cancellationToken);
        await base.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ArtistDto>(artist);
    }

    public async Task<ArtistDto?> UpdateArtistAsync(Guid id, UpdateArtistRequest updateArtistRequest, CancellationToken cancellationToken = default)
    {
        var oldArtist = await _dbSet
            .Include(a => a.Images)
            .Include(a => a.Genres)
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

        if (oldArtist == null)
        {
            throw new KeyNotFoundException($"Artist with ID {id} not found.");
        }

        var imageFile = updateArtistRequest.Image;
        var newArtist = _mapper.Map(updateArtistRequest, oldArtist);

        if (imageFile != null && imageFile.Length > 0)
        {
            var newImages = await _imageRepository.CreateSizedImagesAsync(imageFile, Domain.Enums.ImageType.Artist, cancellationToken);
            newArtist.Images = newImages;
        }

        if (newArtist.Genres != null && newArtist.Genres.Any())
        {
            var existingGenres = await _context
                .Genres
                .Where(g => newArtist.Genres.Select(ng => ng.Name).Contains(g.Name))
                .ToListAsync(cancellationToken);

            var newGenres = newArtist
                .Genres
                .Where(ng => !existingGenres.Any(eg => eg.Name == ng.Name))
                .ToList();

            if (newGenres.Any())
            {
                await _context.Genres.AddRangeAsync(newGenres, cancellationToken);
                existingGenres.AddRange(newGenres);
            }

            newArtist.Genres = existingGenres;
        }

        base.Update(newArtist);
        await base.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ArtistDto>(newArtist);
    }

    public async Task<bool> DeleteArtistAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var artist = await _context
            .Artists
            .Include(a => a.Images)
            .Include(a => a.Albums)
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

        if (artist == null) return false;

        if (artist.Albums.Any()) 
        {
            artist.RaiseDomainEvent(new ArtistDeletedEvent(
                artist.Albums.Select(a => a.Id).ToList()
            ));
        }

        base.Delete(artist);
        await base.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<SearchResultDto> SearchAsync(string query, int limit = 50, int offset = 0, CancellationToken cancellationToken = default)
    {
        var searchQuery = base.Query()
                    .Where(a => a.Name.Contains(query))
                    .OrderBy(a => a.Name)
                    .ProjectTo<ArtistSummaryDto>(_mapper.ConfigurationProvider);

        var searchDto = new SearchResultDto
        {
            Artists = await searchQuery.ToPaginatedResultAsync(limit, offset,
                    _hrefGenerationService.GeneratePaginatedHref(Domain.Enums.EntityType.Artist, limit, offset), cancellationToken)
        };

        return searchDto;
    }

    public async Task AddFollowerAsync(Guid id, User user, CancellationToken cancellationToken = default)
    {
        var artist = await base.QueryTracked()
            .Include(a => a.Followers)
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

        if (artist == null)
        {
            throw new KeyNotFoundException($"Artist with ID {id} not found.");
        }

        if (artist.Followers.Any(f => f.Id == user.Id))
        {
            throw new InvalidOperationException($"User with ID {user.Id} is already a follower of artist with ID {id}.");
        }

        artist.Followers.Add(user);
    }

    public async Task RemoveFollowerAsync(Guid id, User user, CancellationToken cancellationToken = default)
    {
        var artist = await base.QueryTracked()
            .Include(a => a.Followers)
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

        if (artist == null)
        {
            throw new KeyNotFoundException($"Artist with ID {id} not found.");
        }

        if (!artist.Followers.Any(f => f.Id == user.Id))
        {
            throw new InvalidOperationException($"User with ID {user.Id} is not a follower of artist with ID {id}.");
        }

        artist.Followers.Remove(user);
    }
}
