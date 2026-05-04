using AudioDelivery.Application.Common.DTOs;
using AudioDelivery.Application.Common.Extensions;
using AudioDelivery.Application.Common.Interfaces;
using AudioDelivery.Application.Common.Models;
using AudioDelivery.Application.Users.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AudioDelivery.Application.Users;

/// <inheritdoc />
public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IArtistRepository _artistRepository;
    private readonly IHrefGenerationService _hrefGenerationService;
    private readonly IMapper _mapper;

    public UserService(
        IUserRepository userRepository,
        IArtistRepository artistRepository,
        IHrefGenerationService hrefGenerationService,
        IMapper mapper)
    {
        _repository = userRepository;
        _artistRepository = artistRepository;
        _hrefGenerationService = hrefGenerationService;
        _mapper = mapper;
    }

    #region CRUD operations

    /// <inheritdoc />
    public Task<UserProfileDto> CreateUserAsync(CreateUserRequest createUserRequest, CancellationToken cancellationToken = default)
    {
        return _repository.CreateUserAsync(createUserRequest, cancellationToken);
    }

    /// <inheritdoc />
    public Task<UserProfileDto?> GetCurrentUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return _repository.Query()
            .Where(u => u.Id == userId)
            .ProjectTo<UserProfileDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }

    /// <inheritdoc />
    public Task<PublicUserDto?> GetUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return _repository.Query()
            .Where(u => u.Id == userId)
            .ProjectTo<PublicUserDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }

    /// <inheritdoc />
    public Task<UserProfileDto?> UpdateUserAsync(Guid id, UpdateUserRequest updateUserRequest, CancellationToken cancellationToken = default)
    {
        return _repository.UpdateUserAsync(id, updateUserRequest, cancellationToken);
    }

    /// <inheritdoc />
    public Task<bool> DeleteUserAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _repository.DeleteUserAsync(id, cancellationToken);
    }

    #endregion

    #region Follow operations
    /// <inheritdoc />
    public async Task FollowUserAsync(Guid id, Guid userIdToFollow, CancellationToken cancellationToken = default)
    {
        var user = await _repository.QueryTracked()
            .Include(u => u.FollowedUsers)
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

        if (user == null)
        {
            throw new KeyNotFoundException($"User with id {id} not found.");
        }

        var userToFollow = await _repository.QueryTracked()
            .Include(u => u.Followers)
            .FirstOrDefaultAsync(u => u.Id == userIdToFollow, cancellationToken);

        if (userToFollow == null)
        {
            throw new KeyNotFoundException($"User with id {userIdToFollow} not found.");
        }

        if (!user.FollowedUsers.Contains(userToFollow))
        {
            user.FollowedUsers.Add(userToFollow);
            userToFollow.Followers.Add(user);
            await _repository.SaveChangesAsync(cancellationToken);
        }
    }

    /// <inheritdoc />
    public async Task UnFollowUserAsync(Guid id, Guid userIdToUnfollow, CancellationToken cancellationToken = default)
    {
        var user = await _repository.QueryTracked()
                    .Include(u => u.FollowedUsers)
                    .Include(u => u.Followers)
                    .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

        if (user == null)
        {
            throw new KeyNotFoundException($"User with id {id} not found.");
        }

        var userToUnfollow = user.Followers.FirstOrDefault(u => u.Id == userIdToUnfollow);

        if (userToUnfollow == null)
        {
            throw new KeyNotFoundException($"User does not follow user with id {userIdToUnfollow}.");
        }

        if (!user.FollowedUsers.Contains(userToUnfollow))
        {
            user.FollowedUsers.Remove(userToUnfollow);
            userToUnfollow.Followers.Remove(user);
            await _repository.SaveChangesAsync(cancellationToken);
        }
    }

    /// <inheritdoc />
    public Task<PaginatedResult<PublicUserDto>> GetFollowedUsersAsync(Guid id, int offset = 0, int limit = 50, CancellationToken cancellationToken = default)
    {
        return _repository.Query()
            .Where(u => u.Id == id)
            .Include(u => u.FollowedUsers)
            .SelectMany(u => u.FollowedUsers)
            .ProjectTo<PublicUserDto>(_mapper.ConfigurationProvider)
            .ToPaginatedResultAsync(offset, limit, 
                _hrefGenerationService.GeneratePaginatedHref(Domain.Enums.EntityType.User, offset, limit), cancellationToken);
    }

    /// <inheritdoc />
    public Task<PaginatedResult<PublicUserDto>> GetFollowersAsync(Guid id, int offset = 0, int limit = 50, CancellationToken cancellationToken = default)
    {
        return _repository.Query()
            .Where(u => u.Id == id)
            .Include(u => u.Followers)
            .SelectMany(u => u.Followers)
            .ProjectTo<PublicUserDto>(_mapper.ConfigurationProvider)
            .ToPaginatedResultAsync(offset, limit, 
                _hrefGenerationService.GeneratePaginatedHref(Domain.Enums.EntityType.User, offset, limit), cancellationToken);
    }

    /// <inheritdoc />
    public Task<PaginatedResult<PublicUserDto>> GetArtistFollowersAsync(Guid artistId, int offset = 0, int limit = 50, CancellationToken cancellationToken = default)
    {
        return _artistRepository.Query()
            .Where(a => a.Id == artistId)
            .Include(a => a.Followers)
            .SelectMany(a => a.Followers)
            .ProjectTo<PublicUserDto>(_mapper.ConfigurationProvider)
            .ToPaginatedResultAsync(offset, limit,
                _hrefGenerationService.GeneratePaginatedHref(Domain.Enums.EntityType.User, offset, limit), cancellationToken);
    }

    /// <inheritdoc />
    public async Task<ItemCheckResult> CheckIfUserFollowsUsersAsync(Guid id, IList<Guid> userIds, CancellationToken cancellationToken = default)
    {
        var followedUserIds = await _repository.Query()
            .Where(u => u.Id == id)
            .Include(u => u.FollowedUsers)
            .SelectMany(u => u.FollowedUsers)
            .Select(fu => fu.Id)
            .ToListAsync(cancellationToken);

        var results = new List<bool>();

        foreach (var userId in userIds)
        {
            if (followedUserIds.Contains(userId))
            {
                results.Add(true);
            }
            else
            {
                results.Add(false);
            }
        }

        return new ItemCheckResult { Results = results };
    }

    /// <inheritdoc />
    public async Task FollowArtistAsync(Guid id, Guid artistIdToFollow, CancellationToken cancellationToken = default)
    {
        var user = await _repository.QueryTracked()
            .Include(u => u.FollowedArtists)
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

        if (user == null)
        {
            throw new KeyNotFoundException($"User with id {id} not found.");
        }

        var artistToFollow = await _artistRepository.QueryTracked()
            .FirstOrDefaultAsync(a => a.Id == artistIdToFollow, cancellationToken);

        if (artistToFollow == null)
        {
            throw new KeyNotFoundException($"Artist with id {artistIdToFollow} not found.");
        }

        await _artistRepository.AddFollowerAsync(artistIdToFollow, user, cancellationToken);

        if (!user.FollowedArtists.Contains(artistToFollow))
        {
            user.FollowedArtists.Add(artistToFollow);
            await _repository.SaveChangesAsync(cancellationToken);
        }
    }

    /// <inheritdoc />
    public async Task UnFollowArtistAsync(Guid id, Guid artistIdToUnfollow, CancellationToken cancellationToken = default)
    {
        var user = await _repository.QueryTracked()
                    .Include(u => u.FollowedArtists)
                    .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

        if (user == null)
        {
            throw new KeyNotFoundException($"User with id {id} not found.");
        }

        var artistToUnfollow = await _artistRepository.QueryTracked()
            .FirstOrDefaultAsync(a => a.Id == artistIdToUnfollow, cancellationToken);

        if (artistToUnfollow == null)
        {
            throw new KeyNotFoundException($"Artist with id {artistIdToUnfollow} not found.");
        }

        await _artistRepository.RemoveFollowerAsync(artistIdToUnfollow, user, cancellationToken);

        if (user.FollowedArtists.Contains(artistToUnfollow))
        {
            user.FollowedArtists.Remove(artistToUnfollow);
            await _repository.SaveChangesAsync(cancellationToken);
        }
    }

    /// <inheritdoc />
    public async Task<ItemCheckResult> CheckIfUserFollowsArtistsAsync(Guid id, IList<Guid> artistIds, CancellationToken cancellationToken = default)
    {
        var followedArtistIds = await _repository.Query()
                    .Where(u => u.Id == id)
                    .Include(u => u.FollowedArtists)
                    .SelectMany(u => u.FollowedArtists)
                    .Select(fu => fu.Id)
                    .ToListAsync(cancellationToken);

        var results = new List<bool>();

        foreach (var artistId in artistIds)
        {
            if (followedArtistIds.Contains(artistId))
            {
                results.Add(true);
            }
            else
            {
                results.Add(false);
            }
        }

        return new ItemCheckResult { Results = results };
    }
    #endregion
}
