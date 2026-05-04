using AudioDelivery.Application.Common.Interfaces;
using AudioDelivery.Application.Users.DTOs;
using AudioDelivery.Infrastructure.Data;
using AudioDelivery.Domain.Entities;
using AudioDelivery.Domain.Events;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AudioDelivery.Infrastructure.Repositories;

/// <summary>
/// User-specific repository implementation.
/// </summary>
public class UserRepository : Repository<User>, IUserRepository
{
    private readonly IImageRepository _imageRepository;
    private readonly IUriGenerationService _uriGenerationService;

    public UserRepository(
        IImageRepository imageRepository,
        AppDbContext context, 
        IMapper mapper,
        IUriGenerationService uriGenerationService) : base(context, mapper)
    {
        _imageRepository = imageRepository;
        _uriGenerationService = uriGenerationService;
    }

    public async Task<UserProfileDto> CreateUserAsync(CreateUserRequest request, CancellationToken cancellationToken = default)
    {
        var images = await _imageRepository.CreateSizedImagesAsync(request.ProfileImage!, Domain.Enums.ImageType.User);

        var user = _mapper.Map<User>(request);

        if (user == null)
        {
            throw new InvalidOperationException($"Failed to map {nameof(CreateUserRequest)} to {nameof(User)}.");
        }

        user.Id = Guid.NewGuid();
        user.Images = images;
        user.Uri = _uriGenerationService.GenerateUri(Domain.Enums.EntityType.User, user.Id);

        await base.AddAsync(user, cancellationToken);
        await base.SaveChangesAsync(cancellationToken);

        return _mapper.Map<UserProfileDto>(user);
    }

    public async Task<UserProfileDto?> UpdateUserAsync(Guid id, UpdateUserRequest request, CancellationToken cancellationToken = default)
    {
        var oldUser = await base.QueryTracked()
            .Include(u => u.Images)
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

        if (oldUser == null)
        {
            return null;
        }

        var newUser = _mapper.Map(request, oldUser);

        if (request.ProfileImage != null)
        {
            var newImages = await _imageRepository.ReplaceSizedImagesAsync(
                oldUser.Images.Select(i => i.Id).ToList(),
                request.ProfileImage,
                Domain.Enums.ImageType.User,
                cancellationToken
            );

            if (newImages != null && newImages.Count > 0)
                newUser.Images = newImages;
        }

        base.Update(newUser);
        await base.SaveChangesAsync(cancellationToken);

        return _mapper.Map<UserProfileDto>(newUser);
    }

    public async Task<bool> DeleteUserAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await _context.Users
            .Include(u => u.Images)
            .Include(u => u.Playlists)
                .ThenInclude(p => p.Images)
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

        if (user == null) return false;

        var userImages = user.Images;

        if (userImages.Any())
        {
            user.RaiseDomainEvent(new EntityDeletedEvent(
                userImages.Select(i => i.Id).ToList(),
                userImages.Select(i => i.Url).ToList()
            ));
        }

        var playlistImageUrls = user.Playlists
            .SelectMany(p => p.Images)
            .Select(i => i.Url)
            .ToList();

        var playlistImageIds = user.Playlists
            .SelectMany(p => p.Images)
            .Select(i => i.Id)
            .ToList();

        if (playlistImageUrls.Any())
        {
            user.RaiseDomainEvent(new UserDeletedEvent(
                playlistImageUrls,
                playlistImageIds
            ));
        }

        var followRows = await _context.UserFollowedUsers
            .Where(ufu => ufu.FollowerId == id || ufu.FollowedUserId == id)
            .ToListAsync(cancellationToken);

        _context.UserFollowedUsers.RemoveRange(followRows);

        base.Delete(user);
        await base.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}
