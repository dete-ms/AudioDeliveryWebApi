using AudioDelivery.Application.Common.Interfaces;
using AudioDelivery.Application.Users.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AudioDelivery.Application.Users;

/// <summary>
/// User service implementation.
///
/// TODO: Implement each method.
/// </summary>
public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public UserService(
        IUserRepository userRepository,
        IMapper mapper)
    {
        _repository = userRepository;
        _mapper = mapper;
    }

    // create user

    public Task<UserProfileDto?> GetCurrentUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return _repository.Query()
            .Where(u => u.Id == userId)
            .ProjectTo<UserProfileDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<PublicUserDto?> GetUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return _repository.Query()
            .Where(u => u.Id == userId)
            .ProjectTo<PublicUserDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }

    //update user
    //delete user
}
