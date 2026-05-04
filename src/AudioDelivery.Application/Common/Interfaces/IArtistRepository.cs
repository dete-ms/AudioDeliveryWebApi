using AudioDelivery.Application.Artists.DTOs;
using AudioDelivery.Domain.Entities;

namespace AudioDelivery.Application.Common.Interfaces;

/// <summary>
/// Artist-specific repository interface.
/// </summary>
public interface IArtistRepository : IRepository<Artist>, ISearchableEntityRepository
{
    /// <summary>
    /// Asynchronously creates a new artist using the specified request data.
    /// </summary>
    /// <param name="createArtistRequest">The details of the artist to create. Cannot be null.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an ArtistDto representing the newly
    /// created artist.</returns>
    Task<ArtistDto> CreateArtistAsync(CreateArtistRequest createArtistRequest, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously updates the details of an existing artist with the specified values.
    /// </summary>
    /// <param name="id">The unique identifier of the artist to update.</param>
    /// <param name="updateArtistRequest">An object containing the updated artist information to apply. Cannot be null.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="ArtistDto"/> with the
    /// updated artist details if the update is successful; otherwise, <see langword="null"/> if the artist is not
    /// found.</returns>
    Task<ArtistDto?> UpdateArtistAsync(Guid id, UpdateArtistRequest updateArtistRequest, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously deletes the artist with the specified unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the artist to delete.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result is <see langword="true"/> if the artist was
    /// successfully deleted; otherwise, <see langword="false"/>.</returns>
    Task<bool> DeleteArtistAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously adds the specified user as a follower of the given artist.
    /// </summary>
    /// <param name="id">The unique identifier of the artist to be followed.</param>
    /// <param name="user">The user who will follow the artist.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the update operation.</param>
    /// <returns>A task that represents the asynchronous add follower operation.</returns>
    Task AddFollowerAsync(Guid id, User user, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously removes the specified user as a follower of the given artist.
    /// </summary>
    /// <param name="id">The unique identifier of the artist from whom the follower will be removed.</param>
    /// <param name="user">The user to remove as a follower.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the update operation.</param>
    /// <returns>A task that represents the asynchronous remove operation.</returns>
    Task RemoveFollowerAsync(Guid id, User user, CancellationToken cancellationToken = default);
}
