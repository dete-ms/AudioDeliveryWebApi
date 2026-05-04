using AudioDelivery.Application.Playlists.DTOs;
using AudioDelivery.Domain.Entities;

namespace AudioDelivery.Application.Common.Interfaces;

/// <summary>
/// Playlist-specific repository interface.
/// </summary>
public interface IPlaylistRepository : IRepository<Playlist>, ISearchableEntityRepository
{
    /// <summary>
    /// Creates a new playlist asynchronously using the specified playlist details.
    /// </summary>
    /// <param name="createPlaylistRequest">An object containing the information required to create the playlist. Cannot be null.</param>
    /// <param name="coverArtInDifferentSizes">A collection of <see cref="Image"/> objects representing the cover art for the playlist in different sizes. Cannot be null.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="Playlist"/> representing the newly
    /// created playlist.</returns>
    Task<Playlist> CreatePlaylistAsync(CreatePlaylistRequest createPlaylistRequest, IList<Image> coverArtInDifferentSizes, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously updates the details of an existing playlist with the specified values.
    /// </summary>
    /// <param name="id">The unique identifier of the playlist to update.</param>
    /// <param name="updatePlaylistRequest">An object containing the updated playlist information. Cannot be null.</param>
    /// <param name="newCoverArtInDifferentSizes">An optional collection of <see cref="Image"/> objects representing the new cover art for the playlist in different sizes. 
    /// If provided, it will replace the existing cover art; if null, the existing cover art will remain unchanged.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="PlaylistDto"/>
    /// representing the updated playlist if the update is successful; otherwise, <see langword="null"/> if the playlist
    /// does not exist.</returns>
    Task<PlaylistDto?> UpdatePlaylistAsync(Guid id, UpdatePlaylistRequest updatePlaylistRequest, IList<Image>? newCoverArtInDifferentSizes = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously deletes the playlist with the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the playlist to delete.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result is <see langword="true"/> if the playlist was
    /// successfully deleted; otherwise, <see langword="false"/>.</returns>
    Task<bool> DeletePlaylistAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously adds the specified tracks to the playlist identified by the given playlist ID.
    /// </summary>
    /// <remarks>If any of the specified tracks are already present in the playlist, they may be ignored or
    /// reordered according to implementation. The order of tracks in the collection may determine their insertion order
    /// in the playlist.</remarks>
    /// <param name="playlistId">The unique identifier of the playlist to which the tracks will be added.</param>
    /// <param name="addItemsRequest">An object containing the details of the tracks to add to the playlist.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="PlaylistDto"/>
    /// representing the updated playlist if the update is successful; otherwise, <see langword="null"/> if the playlist
    /// does not exist.</returns>
    Task<PlaylistDto?> AddTracksToPlaylistAsync(Guid playlistId, AddItemsRequest addItemsRequest, CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes the specified tracks from the playlist with the given identifier asynchronously.
    /// </summary>
    /// <param name="playlistId">The unique identifier of the playlist from which items will be removed.</param>
    /// <param name="request">An object containing the details of the tracks to remove from the playlist. Cannot be null.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="PlaylistDto"/>
    /// representing the updated playlist if the operation succeeds; otherwise, <see langword="null"/> if the playlist
    /// does not exist.</returns>
    Task<PlaylistDto?> RemoveTracksFromPlaylistAsync(Guid playlistId, RemoveItemsRequest request, CancellationToken cancellationToken = default);

}
