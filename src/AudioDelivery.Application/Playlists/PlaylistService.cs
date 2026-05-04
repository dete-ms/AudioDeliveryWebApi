using AudioDelivery.Application.Common.Extensions;
using AudioDelivery.Application.Common.Interfaces;
using AudioDelivery.Application.Playlists.DTOs;
using AudioDelivery.Application.Common.Models;
using AudioDelivery.Domain.Enums;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AudioDelivery.Application.Playlists;

/// <summary>
/// Playlist service implementation.
/// </summary>
public class PlaylistService : IPlaylistService
{
    private readonly IPlaylistRepository _playlistRepository;
    private readonly IImageRepository _imageRepository;
    private readonly IUserLibraryRepository _userLibraryRepository;
    private readonly IHrefGenerationService _hrefGenerationService;
    private readonly IMapper _mapper;

    public PlaylistService(
        IPlaylistRepository playlistRepository, 
        IImageRepository imageRepository,
        IUserLibraryRepository userLibraryRepository,
        IHrefGenerationService hrefGenerationService,
        IMapper mapper)
    {
        _playlistRepository = playlistRepository;
        _imageRepository = imageRepository;
        _userLibraryRepository = userLibraryRepository;
        _hrefGenerationService = hrefGenerationService;
        _mapper = mapper;
    }

    public async Task<PlaylistDto?> CreatePlaylistAsync(Guid userId, CreatePlaylistRequest request, CancellationToken cancellationToken = default)
    {
        var images = await _imageRepository.CreateSizedImagesAsync(request.CoverImage, ImageType.Playlist, cancellationToken);
        var playlist = await _playlistRepository.CreatePlaylistAsync(request, images, cancellationToken);

        playlist.OwnerId = userId;

        await _playlistRepository.SaveChangesAsync(cancellationToken);

        return _mapper.Map<PlaylistDto>(playlist);
    }

    public Task<PlaylistDto?> GetPlaylistAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _playlistRepository.Query()
            .Where(p => p.Id == id)
            .ProjectTo<PlaylistDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<PlaylistDto?> GetCurrentUserPlaylistsAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<PaginatedResult<PlaylistSummaryDto>> GetPublicPlaylistsByUserAsync(Guid userId, int offset = 0, int limit = 50, CancellationToken cancellationToken = default)
    {
        return _playlistRepository.Query()
            .Where(p => p.OwnerId == userId && p.IsPublic)
            .ProjectTo<PlaylistSummaryDto>(_mapper.ConfigurationProvider)
            .ToPaginatedResultAsync(offset, limit,
                _hrefGenerationService.GeneratePaginatedHref(EntityType.Playlist, offset, limit), cancellationToken);
    }

    public Task<PaginatedResult<PlaylistSummaryDto>> GetSavedPlaylistsAsync(Guid userId, int offset = 0, int limit = 50, CancellationToken cancellationToken = default)
    {
        return _userLibraryRepository.Query()
            .Where(ul => ul.UserId == userId && ul.Playlist != null)
            .Select(ul => ul.Playlist)
            .ProjectTo<PlaylistSummaryDto>(_mapper.ConfigurationProvider)
            .ToPaginatedResultAsync(offset, limit,
                _hrefGenerationService.GeneratePaginatedHref(EntityType.Playlist, offset, limit), cancellationToken);
    }

    public Task<PaginatedResult<PlaylistSummaryDto>> GetPlaylistsByCategoryAsync(Guid categoryId, int offset = 0, int limit = 50, CancellationToken cancellationToken = default)
    {
        return _playlistRepository.Query()
            .Where(p => p.Categories.Any(c => c.Id == categoryId))
            .ProjectTo<PlaylistSummaryDto>(_mapper.ConfigurationProvider)
            .ToPaginatedResultAsync(offset, limit, 
                _hrefGenerationService.GeneratePaginatedHref(EntityType.Playlist, offset, limit), cancellationToken);
    }

    public async Task<PlaylistDto?> UpdatePlaylistAsync(Guid id, UpdatePlaylistRequest request, CancellationToken cancellationToken = default)
    {
        if (request.CoverImage != null)
        {
            var currentPlaylistImageIds = await _playlistRepository.Query()
                .Where(p => p.Id == id)
                .SelectMany(p => p.Images.Select(i => i.Id))
                .ToListAsync(cancellationToken);

            var images = await _imageRepository.ReplaceSizedImagesAsync(currentPlaylistImageIds, request.CoverImage, ImageType.Playlist, cancellationToken);

            return await _playlistRepository.UpdatePlaylistAsync(id, request, images, cancellationToken);
        }
        else
        {
            return await _playlistRepository.UpdatePlaylistAsync(id, request, null, cancellationToken);
        }
    }

    public Task<bool> DeletePlaylistAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _playlistRepository.DeletePlaylistAsync(id, cancellationToken);
    }

    public Task<PlaylistDto?> AddItemsToPlaylistAsync(Guid playlistId, AddItemsRequest request, CancellationToken cancellationToken = default)
    {
        return _playlistRepository.AddTracksToPlaylistAsync(playlistId, request, cancellationToken);
    }

    public Task<PlaylistDto?> RemoveItemsFromPlaylistAsync(Guid playlistId, RemoveItemsRequest request, CancellationToken cancellationToken = default)
    {
        return _playlistRepository.RemoveTracksFromPlaylistAsync(playlistId, request, cancellationToken);
    }
}
