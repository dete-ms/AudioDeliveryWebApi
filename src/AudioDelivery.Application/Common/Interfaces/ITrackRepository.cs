using AudioDelivery.Application.Common.Models;
using AudioDelivery.Application.Tracks.DTOs;
using AudioDelivery.Domain.Entities;

namespace AudioDelivery.Application.Common.Interfaces;

/// <summary>
/// Track-specific repository interface.
/// </summary>
public interface ITrackRepository : IRepository<Track>, ISearchableEntityRepository
{
    /// <summary>
    /// Asynchronously creates a new track using the specified request data.
    /// </summary>
    /// <param name="createTrackRequest">The details of the track to create. Cannot be null.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="TrackDto"/> representing the created
    /// track.</returns>
    Task<TrackDto> CreateTrackAsync(CreateTrackRequest createTrackRequest, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates multiple tracks and associates them with the specified album and artists.
    /// </summary>
    /// <param name="createTrackRequests">A collection of requests containing the details for each track to be created. Each request specifies the
    /// metadata and properties for a new track.</param>
    /// <param name="albumId">The unique identifier of the album to which the new tracks will be added.</param>
    /// <param name="artistIds">A list of unique identifiers representing the artists to be associated with each created track.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="TrackDto"/>
    /// representing the created tracks.</returns>
    Task<IList<TrackDto>> CreateSeveralTracks(
        IEnumerable<CreateTrackRequest> createTrackRequests,
        Guid albumId,
        IList<Guid> artistIds,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously retrieves a paginated list of tracks contained in the specified playlist.
    /// </summary>
    /// <param name="playlistId">The unique identifier of the playlist whose tracks are to be retrieved.</param>
    /// <param name="offset">The zero-based index of the first track to retrieve. Must be greater than or equal to 0.</param>
    /// <param name="limit">The maximum number of tracks to retrieve. Must be greater than 0.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a paginated result of track data
    /// transfer objects for the specified playlist. If the playlist contains no tracks, the result will be empty.</returns>
    Task<PaginatedResult<TrackDto>> GetTracksInPlaylistAsync(
        Guid playlistId,
        int offset = 0,
        int limit = 50,
        CancellationToken cancellationToken = default);


    /// <summary>
    /// Asynchronously updates the details of an existing track with the specified values.
    /// </summary>
    /// <param name="id">The unique identifier of the track to update.</param>
    /// <param name="updateTrackRequest">An object containing the updated values for the track. All required fields must be provided.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="TrackDto"/> representing the updated
    /// track, or null if the track was not found.</returns>
    Task<TrackDto?> UpdateTrackAsync(Guid id, UpdateTrackRequest updateTrackRequest, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously deletes the track with the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the track to delete.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result is <see langword="true"/> if the track was
    /// successfully deleted; otherwise, <see langword="false"/>.</returns>
    Task<bool> DeleteTrackAsync(Guid id, CancellationToken cancellationToken = default);
}
