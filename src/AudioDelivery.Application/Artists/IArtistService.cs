using AudioDelivery.Application.Artists.DTOs;
using AudioDelivery.Application.Common.Models;

namespace AudioDelivery.Application.Artists;

/// <summary>
/// Service interface for Artist business logic.
/// </summary>
public interface IArtistService
{
    /// <summary>
    /// POST /artists - Creates a new artist using the specified request data.
    /// </summary>
    /// <param name="createArtistRequest">The details of the artist to create. Cannot be null.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="ArtistDto"/>
    /// representing the created artist, or <see langword="null"/> if the artist could not be created.</returns>
    Task<ArtistDto> CreateArtistAsync(CreateArtistRequest createArtistRequest, CancellationToken cancellationToken = default);

    /// <summary>
    /// GET /artists/{id}
    /// </summary>
    Task<ArtistDto?> GetArtistAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// GET /artists?ids=...
    /// </summary>
    Task<PaginatedResult<ArtistDto>> GetSeveralArtistsAsync(IEnumerable<Guid> ids, int offset = 0, int limit = 50, CancellationToken cancellationToken = default);

    /// <summary>
    /// GET /artists/{id}/related-artists
    /// </summary>
    Task<PaginatedResult<ArtistDto>> GetRelatedArtistsAsync(Guid artistId, int offset = 0, int limit = 50, CancellationToken cancellationToken = default);

    /// <summary>
    /// GET /me/artists - Asynchronously retrieves a paginated list of artists saved by the specified user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user whose saved artists are to be retrieved.</param>
    /// <param name="offset">The zero-based index of the first artist to retrieve. Must be greater than or equal to 0.</param>
    /// <param name="limit">The maximum number of artists to retrieve. Must be greater than 0.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the update operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="PaginatedResult{T}"/> of <see cref="ArtistSummaryDto"/>
    /// for the user. If the user has no saved artists, the result contains an empty collection.</returns>
    Task<PaginatedResult<ArtistSummaryDto>> GetSavedArtistsAsync(Guid userId, int offset = 0, int limit = 50, CancellationToken cancellationToken = default);

    /// <summary>
    /// PATCH /artists/{id} - Updates an existing artist with the specified ID using the provided request data.
    /// </summary>
    /// <param name="id">The unique identifier of the artist to update.</param>
    /// <param name="updateArtistRequest">An object containing the updated artist information. Cannot be null.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the update operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an updated <see cref="ArtistDto"/>
    /// if the artist is found and updated; otherwise, <see langword="null"/>.</returns>
    Task<ArtistDto?> UpdateArtistAsync(Guid id, UpdateArtistRequest updateArtistRequest, CancellationToken cancellationToken = default);

    /// <summary>
    /// DELETE /artists/{id} - Deletes the artist with the specified unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the artist to delete.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the delete operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result is <see langword="true"/> if the artist was
    /// successfully deleted; otherwise, <see langword="false"/>.</returns>
    Task<bool> DeleteArtistAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// GET /me/following/artists - Asynchronously retrieves a paginated list of artists followed by the specified user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user whose followed artists are to be retrieved.</param>
    /// <param name="offset">The zero-based index of the first artist to retrieve. Must be greater than or equal to 0. The default is 0.</param>
    /// <param name="limit">The maximum number of artists to retrieve. Must be greater than 0. The default is 50.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the update operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a  <see cref="PaginatedResult{T}"/> of <see cref="ArtistSummaryDto"/>
    /// If the user does not follow any artists, the result contains an empty collection.</returns>
    Task<PaginatedResult<ArtistSummaryDto>> GetFollowedArtistsAsync(Guid userId, int offset = 0, int limit = 50, CancellationToken cancellationToken = default);
}
