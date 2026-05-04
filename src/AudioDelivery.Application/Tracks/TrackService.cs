using AudioDelivery.Application.Common.Extensions;
using AudioDelivery.Application.Common.Interfaces;
using AudioDelivery.Application.Common.Models;
using AudioDelivery.Application.Tracks.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AudioDelivery.Application.Tracks;

/// <summary>
/// Track service implementation.
/// </summary>
public class TrackService : ITrackService
{
    private readonly ITrackRepository _trackRepository;
    private readonly IArtistRepository _artistRepository;
    private readonly IUserLibraryRepository _userLibraryRepository;
    private readonly IHrefGenerationService _hrefGenerationService;
    private readonly IMapper _mapper;

    /// <inheritdoc />
    public TrackService(
        ITrackRepository trackRepository,
        IArtistRepository artistRepository,
        IUserLibraryRepository userLibraryRepository,
        IHrefGenerationService hrefGenerationService,
        IMapper mapper)
    {
        _trackRepository = trackRepository;
        _artistRepository = artistRepository;
        _userLibraryRepository = userLibraryRepository;
        _hrefGenerationService = hrefGenerationService;
        _mapper = mapper;
    }
    // TODO: incrememnt play count when track is played
    // Do it with Interlocked.Decrement(ref _playCount);
    /// <inheritdoc />
    public async Task<TrackDto> CreateTrackAsync(CreateTrackRequest createTrackRequest, CancellationToken cancellationToken = default)
    {
        var trackDto = await _trackRepository.CreateTrackAsync(createTrackRequest, cancellationToken);
        await _trackRepository.SaveChangesAsync(cancellationToken);
        return trackDto;
    }

    /// <inheritdoc />
    public Task<TrackDto?> GetTrackAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _trackRepository.Query()
            .Where(t => t.Id == id)
            .ProjectTo<TrackDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }

    /// <inheritdoc />
    public Task<List<TrackDto>> GetSeveralTracksAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        return _trackRepository.Query()
            .Where(t => ids.Contains(t.Id))
            .ProjectTo<TrackDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public Task<List<TrackDto>> GetTopTracksOfArtistAsync(Guid artistId, CancellationToken cancellationToken = default)
    {
        return _artistRepository.Query()
            .Where(a => a.Id == artistId)
            .SelectMany(a => a.Tracks)
            .OrderByDescending(t => t.PlayCount)
            .Take(10)
            .ProjectTo<TrackDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public Task<PaginatedResult<TrackDto>> GetSavedTracksAsync(Guid userId, int offset = 0, int limit = 50, CancellationToken cancellationToken = default)
    {
        return _userLibraryRepository.Query()
            .Where(ul => ul.UserId == userId && ul.Track != null)
            .Select(ul => ul.Track)
            .ProjectTo<TrackDto>(_mapper.ConfigurationProvider)
            .ToPaginatedResultAsync(offset, limit,
                _hrefGenerationService.GeneratePaginatedHref(Domain.Enums.EntityType.Track, offset, limit), cancellationToken);
    }

    /// <inheritdoc />
    public Task<PaginatedResult<TrackDto>> GetTracksInAlbumAsync(Guid albumId, int offset = 0, int limit = 50, CancellationToken cancellationToken = default)
    {
        return _trackRepository.Query()
            .Where(t => t.AlbumId == albumId)
            .Select(t => t.Album.Tracks)
            .ProjectTo<TrackDto>(_mapper.ConfigurationProvider)
            .ToPaginatedResultAsync(offset, limit, 
                _hrefGenerationService.GeneratePaginatedHref(Domain.Enums.EntityType.Track, offset, limit), cancellationToken);
    }

    /// <inheritdoc />
    public Task<PaginatedResult<TrackDto>> GetTracksInPlaylistAsync(Guid playlistId, int offset = 0, int limit = 50, CancellationToken cancellationToken = default)
    {
        return _trackRepository.GetTracksInPlaylistAsync(playlistId, offset, limit, cancellationToken);
    }

    /// <inheritdoc />
    public Task<TrackDto?> UpdateTrackAsync(Guid id, UpdateTrackRequest updateTrackRequest, CancellationToken cancellationToken = default)
    {
        return _trackRepository.UpdateTrackAsync(id, updateTrackRequest, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteTrackAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var isDeleted = await _trackRepository.DeleteTrackAsync(id, cancellationToken);
        await _trackRepository.SaveChangesAsync(cancellationToken);
        return isDeleted;
    }
}
