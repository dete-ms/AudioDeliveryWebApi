using AudioDelivery.Application.Users.DTOs;
using AudioDelivery.Infrastructure.Repositories;

namespace AudioDelivery.Application.Users;

/// <summary>
/// User service implementation.
///
/// TODO: Implement each method.
/// </summary>
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserProfileDto?> GetCurrentUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException("Implement in Phase 6");
    }

    public async Task<PublicUserDto?> GetUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException("Implement in Phase 6");
    }
}
