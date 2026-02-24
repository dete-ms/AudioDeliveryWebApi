using AudioDelivery.Domain.Entities;

namespace AudioDelivery.Infrastructure.Repositories;

/// <summary>
/// User-specific repository interface.
///
/// TODO: Add methods like:
///   - GetUserWithPlaylistsAsync(Guid id)
///   - GetUserByEmailAsync(string email)
///   - GetUserSavedAlbumsAsync(Guid userId, int offset, int limit) [future]
///   - GetUserSavedTracksAsync(Guid userId, int offset, int limit) [future]
/// </summary>
public interface IUserRepository : IRepository<User>
{
    // TODO: Define user-specific query method signatures here
}
