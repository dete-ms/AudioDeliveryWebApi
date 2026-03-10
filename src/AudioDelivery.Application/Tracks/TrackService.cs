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
    private readonly ITrackRepository _repository;
    private readonly IMapper _mapper;

    public TrackService(
        ITrackRepository trackRepository,
        IMapper mapper)
    {
        _repository = trackRepository;
        _mapper = mapper;
    }

    public Task<TrackDto?> GetTrackAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _repository.Query()
            .Where(t => t.Id == id)
            .ProjectTo<TrackDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<List<TrackDto>> GetSeveralTracksAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        return _repository.Query()
            .Where(t => ids.Contains(t.Id))
            .ProjectTo<TrackDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }

    public Task<List<TrackDto>> GetTopTracksOfArtistAsync(Guid artistId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<PaginatedResult<TrackDto>> GetTracksInAlbumAsync(Guid albumId, int offset = 0, int limit = 50, CancellationToken cancellationToken = default)
    {
        return _repository.Query()
            .Where(t => t.AlbumId == albumId)
            .Select(t => t.Album.Tracks)
            .ProjectTo<TrackDto>(_mapper.ConfigurationProvider)
            .ToPaginatedResultAsync(offset, limit, this.GetHref(offset, limit), cancellationToken);
    }

    public Task<PaginatedResult<TrackDto>> GetTracksInPlaylistAsync(Guid playlistId, int offset = 0, int limit = 50, CancellationToken cancellationToken = default)
    {
        return _repository.Query()
            .Where(t => t.PlaylistTracks.Any(pt => pt.PlaylistId == playlistId))
            .ProjectTo<TrackDto>(_mapper.ConfigurationProvider)
            .ToPaginatedResultAsync(offset, limit, this.GetHref(offset, limit), cancellationToken);
    }

    private string GetHref(int offset, int limit) => $"/api/v1/tracks?offset={offset}&limit={limit}";
}
