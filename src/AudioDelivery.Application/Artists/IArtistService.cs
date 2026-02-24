using AudioDelivery.Application.Albums.DTOs;
using AudioDelivery.Application.Artists.DTOs;
using AudioDelivery.Application.Common.Models;
using AudioDelivery.Application.Tracks.DTOs;

namespace AudioDelivery.Application.Artists;

/// <summary>
/// Service interface for Artist business logic.
/// </summary>
public interface IArtistService
{
    /// <summary>
    /// GET /artists/{id}
    /// </summary>
    Task<ArtistDto?> GetArtistAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// GET /artists?ids=...
    /// </summary>
    Task<IReadOnlyList<ArtistDto>> GetSeveralArtistsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    /// <summary>
    /// GET /artists/{id}/albums
    /// </summary>
    Task<PaginatedResult<AlbumSummaryDto>> GetArtistAlbumsAsync(Guid artistId, int offset = 0, int limit = 20, string? market = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// GET /artists/{id}/top-tracks
    /// </summary>
    Task<IReadOnlyList<TrackDto>> GetArtistTopTracksAsync(Guid artistId, string? market = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// GET /artists/{id}/related-artists
    /// </summary>
    Task<IReadOnlyList<ArtistDto>> GetRelatedArtistsAsync(Guid artistId, CancellationToken cancellationToken = default);
}
