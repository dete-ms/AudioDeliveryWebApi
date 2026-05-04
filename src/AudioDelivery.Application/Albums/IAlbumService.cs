using AudioDelivery.Application.Albums.DTOs;
using AudioDelivery.Application.Common.Models;
using AudioDelivery.Application.Tracks.DTOs;

namespace AudioDelivery.Application.Albums;

/// <summary>
/// Service interface for Album business logic.
/// Each method maps to a Spotify Albums API endpoint.
/// </summary>
public interface IAlbumService
{
    /// <summary>
    /// POST /albums - Creates a new album with the provided information and returns the created album details.
    /// </summary>
    /// <param name="createAlbumRequest">An object containing the information required to create the album. Cannot be null.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="AlbumDto"/> representing the newly
    /// created album.</returns>
    Task<AlbumDto> CreateAlbumAsync(CreateAlbumRequest createAlbumRequest, CancellationToken cancellationToken = default);

    /// <summary>
    /// POST /albums/{id}/tracks - Adds one or more tracks to the album identified by the given album ID.
    /// </summary>
    /// <remarks>If the album does not exist, the operation may fail. The order of tracks in the list
    /// determines their order in the album. This method does not return the created tracks; to retrieve them, query the
    /// album after completion.</remarks>
    /// <param name="albumId">The unique identifier of the album to which the tracks will be added.</param>
    /// <param name="tracksToAdd">A list of track creation requests representing the tracks to add to the album. Cannot be null or contain null
    /// elements.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous add operation.</returns>
    Task AddTracksToAlbumAsync(Guid albumId, List<CreateTrackRequest> tracksToAdd, CancellationToken cancellationToken = default);

    /// <summary>
    /// DELETE /albums/{id}/tracks - Removes one or more tracks from the album identified by the given album ID.
    /// </summary>
    /// <remarks>If any of the specified tracks are not present in the album, they are ignored. The operation
    /// completes successfully even if none of the provided track IDs are found in the album.</remarks>
    /// <param name="albumId">The unique identifier of the album from which tracks will be removed.</param>
    /// <param name="trackIdsToRemove">A list of unique identifiers for the tracks to remove from the album. Cannot be null or empty.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous remove operation.</returns>
    Task RemoveTracksFromAlbumAsync(Guid albumId, List<Guid> trackIdsToRemove, CancellationToken cancellationToken = default);

    /// <summary>
    /// GET /albums/{id} – Get a single album by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the album to retrieve.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="AlbumDto"/>
    /// representing the album if found; otherwise, <see langword="null"/>.</returns>
    Task<AlbumDto?> GetAlbumAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// GET /albums?ids=... – Get several albums by their IDs.
    /// </summary>
    /// <param name="ids">A collection of album IDs for which to retrieve album details. Cannot be null or contain duplicate values.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="AlbumDto"> data transfer
    /// objects for the specified IDs. The list will be empty if no matching albums are found.</returns>
    Task<List<AlbumDto>> GetSeveralAlbumsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    /// <summary>
    /// GET /artists/{id}/albums - Get an artist's albums.
    /// </summary>
    /// <param name="artistId">The unique identifier of the artist whose albums are to be retrieved.</param>
    /// <param name="offset">The zero-based index of the first album to retrieve. Must be greater than or equal to 0.</param>
    /// <param name="limit">The maximum number of albums to retrieve. Must be greater than 0.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a paginated result of <see cref="AlbumSummaryDto">
    /// summaries for the specified artist. If the artist has no albums, the result contains an empty collection.</returns>
    Task<PaginatedResult<AlbumSummaryDto>> GetAlbumsByArtistAsync(Guid artistId, int offset = 0, int limit = 50, CancellationToken cancellationToken = default);

    /// <summary>
    /// GET /browse/new-releases – Get new album releases.
    /// </summary>
    /// <param name="offset">The zero-based index of the first album to return. Must be greater than or equal to 0.</param>
    /// <param name="limit">The maximum number of albums to return. Must be greater than 0.</param>
    /// <param name="country">An optional ISO 3166-1 alpha-2 country code to filter releases by market. If null, releases are not filtered by
    /// country.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a paginated result of <see cref="AlbumSummaryDto">
    /// summaries for the new releases.</returns>
    Task<PaginatedResult<AlbumSummaryDto>> GetNewReleasesAsync(int offset = 0, int limit = 50, string? country = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// GET /me/library/albums – Get a list of the albums saved in the current user's library.
    /// </summary>
    /// <param name="userId">The unique identifier of the user whose saved albums are to be retrieved.</param>
    /// <param name="offset">The zero-based index of the first album to return. Must be greater than or equal to 0.</param>
    /// <param name="limit">The maximum number of albums to return in the result. Must be greater than 0.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a paginated result of <see cref="AlbumSummaryDto">
    /// summaries saved by the user. If the user has no saved albums, the result will contain an empty collection.</returns>
    Task<PaginatedResult<AlbumSummaryDto>> GetSavedAlbumsAsync(Guid userId, int offset = 0, int limit = 50, CancellationToken cancellationToken = default);

    /// <summary>
    /// PATCH /albums/{id} - Updates the album with the specified identifier using the provided update information.
    /// </summary>
    /// <param name="id">The unique identifier of the album to update.</param>
    /// <param name="updateAlbumRequest">An object containing the updated album information. Cannot be null.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an AlbumDto with the updated <see cref="AlbumDto">
    /// details if the update is successful; otherwise, null if the album is not found.</returns>
    Task<AlbumDto?> UpdateAlbumAsync(Guid id, UpdateAlbumRequest updateAlbumRequest, CancellationToken cancellationToken = default);

    /// <summary>
    /// DELETE /albums/{id} - Deletes the album with the specified identifier from the system.
    /// </summary>
    /// <param name="id">The unique identifier of the album to delete.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result is <see langword="true"/> if the album was
    /// successfully deleted; otherwise, <see langword="false"/>.</returns>
    Task<bool> DeleteAlbumAsync(Guid id, CancellationToken cancellationToken = default);
}
