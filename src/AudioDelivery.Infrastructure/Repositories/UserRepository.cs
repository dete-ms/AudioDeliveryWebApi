using AudioDelivery.Application.Common.Interfaces;
using AudioDelivery.Domain.Entities;
using AudioDelivery.Infrastructure.Data;
using AutoMapper;

namespace AudioDelivery.Infrastructure.Repositories;

/// <summary>
/// User-specific repository implementation.
/// </summary>
public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(AppDbContext context, IMapper mapper) : base(context, mapper) { }
}
