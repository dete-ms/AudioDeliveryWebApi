using AudioDelivery.Application.Albums.DTOs;
using AudioDelivery.Application.Common.Models;
using AudioDelivery.Domain.Entities;

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
    /// Creates a new album using the specified album creation request.
    /// </summary>
    /// <param name="createAlbumRequest">An object containing the details required to create the album. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an AlbumDto representing the created
    /// album, or null if the album could not be created.</returns>
    Task<AlbumDto?> CreateAlbum(CreateAlbumRequest createAlbumRequest);

    /// <summary>
    /// GET /albums/{id} – Get a single album by its ID.
    /// </summary>
    Task<AlbumDto?> GetAlbumAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// GET /albums?ids=... – Get several albums by their IDs.
    /// </summary>
    Task<List<AlbumDto>> GetSeveralAlbumsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    /// <summary>
    /// GET /artists/{id}/albums
    /// </summary>
    Task<PaginatedResult<AlbumSummaryDto>> GetAlbumsByArtistAsync(Guid artistId, int offset = 0, int limit = 50, CancellationToken cancellationToken = default);

    /// <summary>
    /// GET /browse/new-releases – Get new album releases.
    /// </summary>
    Task<PaginatedResult<AlbumSummaryDto>> GetNewReleasesAsync(int offset = 0, int limit = 50, string? country = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// GET /search?q=...&type=... – Search for albums by name.
    /// </summary>
    /// <returns></returns>
    Task<PaginatedResult<AlbumSummaryDto>> SearchAlbumsAsync(string query, int offset = 0, int limit = 50, CancellationToken cancellationToken = default);

    /// <summary>
    /// GET /me/albums – Get a list of the albums saved in the current user's library.
    /// </summary>
    /// <returns></returns>
    Task<PaginatedResult<AlbumSummaryDto>> GetSavedAlbums(Guid userId, int offset = 0, int limit = 50, CancellationToken cancellationToken = default);

    /// <summary>
    /// PATCH /albums/{id} Updates the album with the specified identifier using the provided update information.
    /// </summary>
    /// <param name="id">The unique identifier of the album to update.</param>
    /// <param name="updateAlbumRequest">An object containing the updated album information. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="AlbumDto"/> with the
    /// updated album data if the update is successful; otherwise, <see langword="null"/> if the album is not found.</returns>
    Task<AlbumDto?> UpdateAlbum(Guid id, UpdateAlbumRequest updateAlbumRequest);
}
