using AudioDelivery.Application.Albums.DTOs;
using AudioDelivery.Application.Common.Models;
using AudioDelivery.Application.Tracks.DTOs;

namespace AudioDelivery.Application.Tracks;

/// <summary>
/// Service interface for Track business logic.
/// </summary>
public interface ITrackService
{
    /// <summary>
    /// GET /tracks/{id}
    /// </summary>
    Task<TrackDto?> GetTrackAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// GET /tracks?ids=...
    /// </summary>
    Task<List<TrackDto>> GetSeveralTracksAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    /// <summary>
    /// GET /artists/{id}/top-tracks
    /// </summary>
    Task<List<TrackDto>> GetTopTracksOfArtistAsync(Guid artistId, CancellationToken cancellationToken = default);

    /// <summary>
    /// GET /albums/{id}/tracks – Get tracks for an album (paginated).
    /// </summary>
    Task<PaginatedResult<TrackDto>> GetTracksInAlbumAsync(Guid albumId, int offset = 0, int limit = 50, CancellationToken cancellationToken = default);

    /// <summary>
    /// GET /playlists/{id}/items - Get tracks in a playlist (paginated).
    /// </summary>
    Task<PaginatedResult<TrackDto>> GetTracksInPlaylistAsync(Guid playlistId, int offset = 0, int limit = 100, CancellationToken cancellationToken = default);
}
