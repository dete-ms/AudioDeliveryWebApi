using AudioDelivery.Application.Common.DTOs;
using AudioDelivery.Application.Common.Models;
using AudioDelivery.Application.Users.DTOs;

namespace AudioDelivery.Application.Users;

/// <summary>
/// Service interface for User business logic.
/// </summary>
public interface IUserService
{
    #region CRUD operations

    /// <summary>
    /// POST /users – Create a user.
    /// </summary>
    /// <param name="createUserRequest">An object containing the information required to create the user. Cannot be null.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="UserProfileDto"/> representing the
    /// newly created user.</returns>
    Task<UserProfileDto> CreateUserAsync(CreateUserRequest createUserRequest, CancellationToken cancellationToken = default);

    /// <summary>
    /// GET /me – Get current user's profile.
    /// </summary>
    Task<UserProfileDto?> GetCurrentUserAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// GET /users/{userId} – Get a user's public profile.
    /// </summary>
    Task<PublicUserDto?> GetUserAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// PATCH /me – Update the current user's profile.
    /// </summary>
    /// <param name="updateUserRequest">An object containing the information required to update the user. Cannot be null.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="UserProfileDto"/> representing the
    /// newly updated user.</returns>
    Task<UserProfileDto?> UpdateUserAsync(Guid id, UpdateUserRequest updateUserRequest, CancellationToken cancellationToken = default);

    /// <summary>
    /// DELETE /me – Delete the current user's profile.
    /// </summary>
    /// <param name="id">The unique identifier of the user to delete.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result is <see langword="true"/> if the user was
    /// successfully deleted; otherwise, <see langword="false"/>.</returns>
    Task<bool> DeleteUserAsync(Guid id, CancellationToken cancellationToken = default);

    #endregion

    #region Follow operations

    /// <summary>
    /// PUT /me/following - Asynchronously requests that the specified user follow another user.
    /// </summary>
    /// <param name="id">The unique identifier of the user who will perform the follow action.</param>
    /// <param name="userIdToFollow">The unique identifier of the user to be followed.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous follow operation.</returns>
    Task FollowUserAsync(Guid id, Guid userIdToFollow, CancellationToken cancellationToken = default);

    /// <summary>
    /// DELETE /me/following/{userId} - Asynchronously removes the specified user from the current user's following list.
    /// </summary>
    /// <remarks>If the user is not currently following the specified user, this operation has no effect. This
    /// method does not throw an exception if the user to unfollow is not found in the following list.</remarks>
    /// <param name="id">The unique identifier of the user performing the unfollow operation.</param>
    /// <param name="userIdToUnfollow">The unique identifier of the user to unfollow.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous unfollow operation.</returns>
    Task UnFollowUserAsync(Guid id, Guid userIdToUnfollow, CancellationToken cancellationToken = default);

    /// <summary>
    /// GET /me/following - Asynchronously retrieves a paginated list of users followed by the specified user.
    /// </summary>
    /// <param name="id">The unique identifier of the user whose followed users are to be retrieved.</param>
    /// <param name="offset">The zero-based index of the first followed user to retrieve. Must be greater than or equal to 0.</param>
    /// <param name="limit">The maximum number of followed users to retrieve. Must be greater than 0.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains <see cref="PaginatedResult{T}"/> of <see cref="PublicUserDto"/>. 
    /// If the user does not follow any users, the result contains an empty collection.</returns>
    Task<PaginatedResult<PublicUserDto>> GetFollowedUsersAsync(Guid id, int offset = 0, int limit = 50, CancellationToken cancellationToken = default);

    /// <summary>
    /// GET /me/followers - Asynchronously retrieves a paginated list of users who follow the specified user.
    /// </summary>
    /// <param name="id">The unique identifier of the user whose followers are to be retrieved.</param>
    /// <param name="offset">The zero-based index of the first follower to retrieve. Must be greater than or equal to 0.</param>
    /// <param name="limit">The maximum number of followers to retrieve. Must be greater than 0.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains <see cref="PaginatedResult{T}"/> of <see cref="PublicUserDto"/>. 
    /// If the user does not follow any users, the result contains an empty collection.</returns>
    Task<PaginatedResult<PublicUserDto>> GetFollowersAsync(Guid id, int offset = 0, int limit = 50, CancellationToken cancellationToken = default);

    /// <summary>
    /// GET /artists/followers - Asynchronously retrieves a paginated list of users who follow the specified artist.
    /// </summary>
    /// <param name="artistId">The unique identifier of the artist whose followers are to be retrieved.</param>
    /// <param name="offset">The zero-based index of the first follower to retrieve. Must be greater than or equal to 0.</param>
    /// <param name="limit">The maximum number of followers to retrieve. Must be greater than 0.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains <see cref="PaginatedResult{T}"/> of <see cref="PublicUserDto"/>. 
    /// If the artist does not have any followers, the result contains an empty collection.</returns>
    Task<PaginatedResult<PublicUserDto>> GetArtistFollowersAsync(Guid artistId, int offset = 0, int limit = 50, CancellationToken cancellationToken = default);

    /// <summary>
    /// Determines whether the specified user follows each user in the provided list asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the user whose following relationships are to be checked.</param>
    /// <param name="userIds">A list of user identifiers to check for a following relationship. Each identifier represents a user to test
    /// against the specified user.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="ItemCheckResult"/> indicating, for
    /// each user in the list, whether the specified user follows them.</returns>
    Task<ItemCheckResult> CheckIfUserFollowsUsersAsync(Guid id, IList<Guid> userIds, CancellationToken cancellationToken = default);

    /// <summary>
    /// PUT /me/following/artists - Asynchronously follows the specified artist on behalf of the user.
    /// </summary>
    /// <param name="id">The unique identifier of the user who is performing the follow operation.</param>
    /// <param name="artistIdToFollow">The unique identifier of the artist to follow.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous follow operation.</returns>
    Task FollowArtistAsync(Guid id, Guid artistIdToFollow, CancellationToken cancellationToken = default);

    /// <summary>
    /// DELETE /me/following/artists - Asynchronously removes the specified artist from the user's list of followed artists.
    /// </summary>
    /// <param name="id">The unique identifier of the user who is unfollowing the artist.</param>
    /// <param name="artistIdToUnfollow">The unique identifier of the artist to unfollow.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous unfollow operation.</returns>
    Task UnFollowArtistAsync(Guid id, Guid artistIdToUnfollow, CancellationToken cancellationToken = default);

    /// <summary>
    /// GET me/following/artists/contains - Determines whether the specified user follows each of the given artists.
    /// </summary>
    /// <param name="id">The unique identifier of the user whose followed artists are to be checked.</param>
    /// <param name="artistIds">A list of artist identifiers to check for follow status. Cannot be null or empty.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="ItemCheckResult"/> indicating, for
    /// each artist, whether the user follows them.</returns>
    Task<ItemCheckResult> CheckIfUserFollowsArtistsAsync(Guid id, IList<Guid> artistIds, CancellationToken cancellationToken = default);

    #endregion
}
