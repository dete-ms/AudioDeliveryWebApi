using AudioDelivery.Application.Common.Extensions;
using AudioDelivery.Application.Common.Interfaces;
using AudioDelivery.Application.Common.Models;
using AudioDelivery.Application.Albums.DTOs;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using AudioDelivery.Application.Tracks.DTOs;

namespace AudioDelivery.Application.Albums;

/// <summary>
/// Album service implementation.
/// </summary>
public class AlbumService : IAlbumService
{
    private readonly IAlbumRepository _albumRepository;
    private readonly ITrackRepository _trackRepository;
    private readonly IUserLibraryRepository _userLibraryRepository;
    private readonly IHrefGenerationService _hrefGenerationService;
    private readonly IMapper _mapper;

    public AlbumService(
        IAlbumRepository albumRepository,
        ITrackRepository trackRepository,
        IUserLibraryRepository userLibraryRepository,
        IHrefGenerationService hrefGenerationService,
        IMapper mapper)
    {
        _albumRepository = albumRepository;
        _trackRepository = trackRepository;
        _userLibraryRepository = userLibraryRepository;
        _hrefGenerationService = hrefGenerationService;
        _mapper = mapper;
    }
    
    public async Task<AlbumDto> CreateAlbumAsync(CreateAlbumRequest createAlbumRequest, CancellationToken cancellationToken = default)
    {
        var albumDto = await _albumRepository.CreateAlbumAsync(createAlbumRequest, cancellationToken);

        var tracks = await _trackRepository.CreateSeveralTracks(
            createAlbumRequest.TracksToAdd, 
            albumDto.Id, 
            albumDto.Artists.Select(a => a.Id).ToList(), 
            cancellationToken);

        return albumDto;
    }

    public async Task AddTracksToAlbumAsync(Guid albumId, List<CreateTrackRequest> tracksToAdd, CancellationToken cancellationToken = default)
    {
        var album = await _albumRepository.GetByIdTrackedAsync(albumId, cancellationToken);

        if (album == null)
        {
            throw new KeyNotFoundException($"Album with ID {albumId} not found.");
        }

        var trackDtos = await _trackRepository.CreateSeveralTracks(
            tracksToAdd, 
            albumId, 
            album.Artists.Select(a => a.Id).ToList(), 
            cancellationToken);

        await _albumRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveTracksFromAlbumAsync(Guid albumId, List<Guid> trackIdsToRemove, CancellationToken cancellationToken = default)
    {
        var album = await _albumRepository.GetByIdTrackedAsync(albumId, cancellationToken);
        
        if (album == null)
        {
            throw new KeyNotFoundException($"Album with ID {albumId} not found.");
        }

        var tracksToRemove = album.Tracks.Where(t => trackIdsToRemove.Contains(t.Id)).ToList();

        if (!tracksToRemove.Any())
        {
            throw new KeyNotFoundException($"No tracks with the specified IDs were found in the album.");
        }

        foreach (var track in tracksToRemove)
        {
            album.Tracks.Remove(track);
        }

        await _albumRepository.SaveChangesAsync(cancellationToken);
    }

    public Task<AlbumDto?> GetAlbumAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _albumRepository.FindFirstAsync<AlbumDto>(a => a.Id == id);
    }

    public Task<List<AlbumDto>> GetSeveralAlbumsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        return _albumRepository.Query()
            .Where(a => ids.Contains(a.Id))
            .ProjectTo<AlbumDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }

    public Task<PaginatedResult<AlbumSummaryDto>> GetAlbumsByArtistAsync(Guid artistId, int offset = 0, int limit = 50, CancellationToken cancellationToken = default)
    {
        return _albumRepository.Query()
            .Where(a => a.Artists.Any(ar => ar.Id == artistId))
            .OrderByDescending(a => a.ReleaseDate)
            .ProjectTo<AlbumSummaryDto>(_mapper.ConfigurationProvider)
            .ToPaginatedResultAsync(offset, limit, 
                _hrefGenerationService.GeneratePaginatedHref(Domain.Enums.EntityType.Album, offset, limit), cancellationToken);
    }

    public Task<PaginatedResult<AlbumSummaryDto>> GetNewReleasesAsync(int offset = 0, int limit = 50, string? country = null, CancellationToken cancellationToken = default)
    {
        return _albumRepository.Query()
            .OrderByDescending(a => a.ReleaseDate)
            .ProjectTo<AlbumSummaryDto>(_mapper.ConfigurationProvider)
            .ToPaginatedResultAsync(offset, limit, 
                _hrefGenerationService.GeneratePaginatedHref(Domain.Enums.EntityType.Album, offset, limit), cancellationToken);
    }

    public Task<PaginatedResult<AlbumSummaryDto>> GetSavedAlbumsAsync(Guid userId, int offset = 0, int limit = 50, CancellationToken cancellationToken = default)
    {
        return _userLibraryRepository.Query()
            .Where(ul => ul.UserId == userId && ul.Album != null)
            .Select(ul => ul.Album)
            .ProjectTo<AlbumSummaryDto>(_mapper.ConfigurationProvider)
            .ToPaginatedResultAsync(offset, limit,
                _hrefGenerationService.GeneratePaginatedHref(Domain.Enums.EntityType.Album, offset, limit), cancellationToken);
    }
    
    public Task<AlbumDto?> UpdateAlbumAsync(Guid id, UpdateAlbumRequest updateAlbumRequest, CancellationToken cancellationToken = default)
    {
        return _albumRepository.UpdateAlbumAsync(id, updateAlbumRequest, cancellationToken);
    }

    public Task<bool> DeleteAlbumAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _albumRepository.DeleteAlbumAsync(id, cancellationToken);
    }
}
