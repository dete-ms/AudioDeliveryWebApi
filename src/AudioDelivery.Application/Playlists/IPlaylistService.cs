using AudioDelivery.Application.Common.Models;
using AudioDelivery.Application.Playlists.DTOs;

namespace AudioDelivery.Application.Playlists;

/// <summary>
/// Service interface for Playlist business logic.
/// </summary>
public interface IPlaylistService
{
    /// <summary>
    /// POST /users/{userId}/playlists - Creates a new playlist for the specified user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user for whom the playlist will be created.</param>
    /// <param name="request">An object containing the details of the playlist to create, such as name and initial tracks. Cannot be null.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="PlaylistDto"/>
    /// representing the created playlist, or <see langword="null"/> if the playlist could not be created.</returns>
    Task<PlaylistDto?> CreatePlaylistAsync(Guid userId, CreatePlaylistRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// GET /playlists/{id} - Asynchronously retrieves the playlist with the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the playlist to retrieve.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="PlaylistDto"/> if the
    /// playlist is found; otherwise, <see langword="null"/>.</returns>
    Task<PlaylistDto?> GetPlaylistAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// GET me/playlists - Asynchronously retrieves the playlists belonging to the currently authenticated user.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="PlaylistDto"/> with the
    /// user's playlists, or <see langword="null"/> if the user has no playlists or is not authenticated.</returns>
    Task<PlaylistDto?> GetCurrentUserPlaylistsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// PUT /playlists/{id}/items - Adds one or more items to the specified playlist. The items to be added are identified by their unique identifiers provided in the request body.
    /// </summary>
    /// <param name="playlistId">The unique identifier of the playlist to which items will be added.</param>
    /// <param name="request">An object containing the details of the items to add to the playlist. Cannot be null.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="PlaylistDto"/>
    /// representing the updated playlist if the operation succeeds; otherwise, <see langword="null"/> if the playlist
    /// does not exist.</returns>
    public Task<PlaylistDto?> AddItemsToPlaylistAsync(Guid playlistId, AddItemsRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// DELETE /playlists/{id}/items - Removes one or more items from the specified playlist. The items to be removed are identified by their unique identifiers provided in the request body.
    /// </summary>
    /// <param name="playlistId">The unique identifier of the playlist from which items will be removed.</param>
    /// <param name="request">An object containing the details of the items to remove from the playlist. Cannot be null.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="PlaylistDto"/>
    /// representing the updated playlist if the operation succeeds; otherwise, <see langword="null"/> if the playlist
    /// does not exist.</returns>
    Task<PlaylistDto?> RemoveItemsFromPlaylistAsync(Guid playlistId, RemoveItemsRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// GET /users/{userId}/playlists - Asynchronously retrieves a paginated list of playlist summaries belonging to the specified user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user whose playlists are to be retrieved.</param>
    /// <param name="offset">The zero-based index of the first playlist to retrieve. Must be greater than or equal to 0.</param>
    /// <param name="limit">The maximum number of playlists to retrieve. Must be greater than 0.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a paginated result of playlist
    /// summaries for the specified user. If the user has no playlists, the result will contain an empty collection.</returns>
    Task<PaginatedResult<PlaylistSummaryDto>> GetPublicPlaylistsByUserAsync(Guid userId, int offset = 0, int limit = 50, CancellationToken cancellationToken = default);

    /// <summary>
    /// GET /browse/categories/{categoryId}/playlists – Asynchronously retrieves a paginated list of playlists that belong to the specified category.
    /// </summary>
    /// <param name="categoryId">The unique identifier of the category for which to retrieve playlists.</param>
    /// <param name="offset">The zero-based index of the first playlist to retrieve. Must be greater than or equal to 0.</param>
    /// <param name="limit">The maximum number of playlists to retrieve. Must be greater than 0.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a paginated result of playlist
    /// summaries for the specified category. If no playlists are found, the result contains an empty collection.</returns>
    Task<PaginatedResult<PlaylistSummaryDto>> GetPlaylistsByCategoryAsync(Guid categoryId, int offset = 0, int limit = 50, CancellationToken cancellationToken = default);

    /// <summary>
    /// GET /me/playlists - Asynchronously retrieves a paginated list of playlists saved by the specified user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user whose saved playlists are to be retrieved.</param>
    /// <param name="offset">The zero-based index of the first playlist to retrieve. Must be greater than or equal to 0. Defaults to 0.</param>
    /// <param name="limit">The maximum number of playlists to retrieve. Must be greater than 0. Defaults to 50.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the update operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see
    /// cref="PaginatedResult{PlaylistSummaryDto}"/> with the user's saved playlists for the specified page. If no
    /// playlists are found, the result contains an empty collection.</returns>
    Task<PaginatedResult<PlaylistSummaryDto>> GetSavedPlaylistsAsync(Guid userId, int offset = 0, int limit = 50, CancellationToken cancellationToken = default);

    /// <summary>
    /// PATCH /playlists/{id} - Updates the specified playlist's details. Only the fields provided in the request will be updated.
    /// </summary>
    /// <param name="id">The unique identifier of the playlist to update.</param>
    /// <param name="request">An object containing the updated playlist information. Cannot be null.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the update operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="PlaylistDto"/> with the
    /// updated playlist details if the update is successful; otherwise, <see langword="null"/> if the playlist does not
    /// exist.</returns>
    Task<PlaylistDto?> UpdatePlaylistAsync(Guid id, UpdatePlaylistRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// DELETE /playlists/{id} - Deletes the specified playlist. The playlist will be removed from all users' libraries and will no longer be accessible.
    /// </summary>
    /// <param name="id">The unique identifier of the playlist to delete.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the delete operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result is <see langword="true"/> if the playlist was
    /// successfully deleted; otherwise, <see langword="false"/>.</returns>
    Task<bool> DeletePlaylistAsync(Guid id, CancellationToken cancellationToken = default);
}
