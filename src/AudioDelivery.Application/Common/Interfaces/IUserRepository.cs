using AudioDelivery.Application.Users.DTOs;
using AudioDelivery.Domain.Entities;

namespace AudioDelivery.Application.Common.Interfaces;

/// <summary>
/// User-specific repository interface.
/// </summary>
public interface IUserRepository : IRepository<User>
{
    /// <summary>
    /// Asynchronously creates a new user account based on the specified registration details.
    /// </summary>
    /// <param name="createUserRequest">An object containing the information required to create the user, such as username, password, and profile data.
    /// Cannot be null.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a UserProfileDto with details of the
    /// newly created user.</returns>
    Task<UserProfileDto> CreateUserAsync(CreateUserRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously updates the user profile with the specified identifier using the provided update information.
    /// </summary>
    /// <param name="id">The unique identifier of the user to update.</param>
    /// <param name="updateUserRequest">An object containing the updated user information to apply. Cannot be null.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a UserProfileDto with the updated
    /// user information.</returns>
    Task<UserProfileDto?> UpdateUserAsync(Guid id, UpdateUserRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously deletes the user with the specified unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user to delete.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result is <see langword="true"/> if the user was
    /// successfully deleted; otherwise, <see langword="false"/>.</returns>
    Task<bool> DeleteUserAsync(Guid id, CancellationToken cancellationToken = default);
}
