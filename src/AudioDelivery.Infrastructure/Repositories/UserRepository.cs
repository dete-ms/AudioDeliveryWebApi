using AudioDelivery.Domain.Entities;
using AudioDelivery.Infrastructure.Data;

namespace AudioDelivery.Infrastructure.Repositories;

/// <summary>
/// User-specific repository implementation.
///
/// TODO: Implement the methods defined in IUserRepository.
/// </summary>
public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context) { }

    // TODO: Implement user-specific query methods
}
