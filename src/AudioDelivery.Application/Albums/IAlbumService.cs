using AudioDelivery.Application.Albums.DTOs;
using AudioDelivery.Application.Common.Models;

namespace AudioDelivery.Application.Albums;

/// <summary>
/// Service interface for Album business logic.
/// Each method maps to a Spotify Albums API endpoint.
///
/// TODO: Implement all methods in AlbumService.cs
/// </summary>
public interface IAlbumService
{
    /// <summary>
    /// GET /albums/{id} – Get a single album by its ID.
    /// </summary>
    Task<AlbumDto?> GetAlbumAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// GET /albums?ids=... – Get several albums by their IDs.
    /// </summary>
    Task<IReadOnlyList<AlbumDto>> GetSeveralAlbumsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    /// <summary>
    /// GET /albums/{id}/tracks – Get tracks for an album (paginated).
    /// </summary>
    Task<PaginatedResult<AlbumSummaryDto>> GetAlbumTracksAsync(Guid albumId, int offset = 0, int limit = 20, CancellationToken cancellationToken = default);

    /// <summary>
    /// GET /browse/new-releases – Get new album releases.
    /// </summary>
    Task<PaginatedResult<AlbumSummaryDto>> GetNewReleasesAsync(int offset = 0, int limit = 20, string? country = null, CancellationToken cancellationToken = default);

    // TODO: Add methods for user's saved albums (handled by LibraryService).
}
