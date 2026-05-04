using AudioDelivery.Application.Common.Models;
using AudioDelivery.Application.Tracks.DTOs;

namespace AudioDelivery.Application.Tracks;

/// <summary>
/// Service interface for Track business logic.
/// </summary>
public interface ITrackService
{
    /// <summary>
    /// Post /tracks - Create a new track.
    /// </summary>
    /// <param name="createTrackRequest">The details of the track to create. Cannot be null.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="TrackDto"/>
    /// representing the newly created track.</returns>
    Task<TrackDto> CreateTrackAsync(CreateTrackRequest createTrackRequest, CancellationToken cancellationToken = default);

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

    /// <summary>
    /// GET /me/tracks - Get a list of the tracks saved in the current user's library.
    /// </summary>
    /// <param name="userId">The unique identifier of the user whose saved tracks are to be retrieved.</param>
    /// <param name="offset">The zero-based index of the first track to retrieve. Must be greater than or equal to 0.</param>
    /// <param name="limit">The maximum number of tracks to retrieve. Must be greater than 0.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the update operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="PaginatedResult{T}"/> of <see cref="TrackDto"/>
    /// for the user. If the user has no saved tracks, the result contains an empty collection.</returns>
    Task<PaginatedResult<TrackDto>> GetSavedTracksAsync(Guid userId, int offset = 0, int limit = 50, CancellationToken cancellationToken = default);

    /// <summary>
    /// PATCH /tracks/{id} - Update an existing track. Only the fields provided in the request will be updated; all other fields will remain unchanged.
    /// </summary>
    /// <param name="id">The unique identifier of the track to update.</param>
    /// <param name="updateTrackRequest">An object containing the updated values for the track. All required fields must be provided.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the update operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="TrackDto"/> with the
    /// updated track details if the update is successful; otherwise, <see langword="null"/> if the track does not
    /// exist.</returns>
    Task<TrackDto?> UpdateTrackAsync(Guid id, UpdateTrackRequest updateTrackRequest, CancellationToken cancellationToken = default);

    /// <summary>
    /// DELETE /tracks/{id} - Delete the track with the specified unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the track to delete.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the delete operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result is <see langword="true"/> if the track was
    /// successfully deleted; otherwise, <see langword="false"/>.</returns>
    Task<bool> DeleteTrackAsync(Guid id, CancellationToken cancellationToken = default);
}
