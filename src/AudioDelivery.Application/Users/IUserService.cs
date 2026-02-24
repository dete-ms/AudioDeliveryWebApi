using AudioDelivery.Application.Users.DTOs;

namespace AudioDelivery.Application.Users;

/// <summary>
/// Service interface for User business logic.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// GET /me – Get current user's profile.
    /// </summary>
    Task<UserProfileDto?> GetCurrentUserAsync(Guid userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// GET /users/{userId} – Get a user's public profile.
    /// </summary>
    Task<PublicUserDto?> GetUserAsync(Guid userId, CancellationToken cancellationToken = default);

    // TODO: Add methods for:
    //   - GetUserTopItemsAsync(...)  → GET /me/top/{type}
    //   - FollowArtistsAsync(...)    → PUT /me/following
    //   - UnfollowArtistsAsync(...)  → DELETE /me/following
    //   - GetFollowedArtistsAsync(...) → GET /me/following
    //   - CheckFollowingAsync(...)   → GET /me/following/contains
}
