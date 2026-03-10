using AudioDelivery.Application.Common.Extensions;
using AudioDelivery.Application.Common.Interfaces;
using AudioDelivery.Application.Common.Models;
using AudioDelivery.Application.Playlists.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AudioDelivery.Application.Playlists;

/// <summary>
/// Playlist service implementation.
///
/// TODO: Implement each method.
/// </summary>
public class PlaylistService : IPlaylistService
{
    private readonly IPlaylistRepository _repository;
    private readonly IMapper _mapper;

    public PlaylistService(
        IPlaylistRepository playlistRepository, 
        IMapper mapper)
    {
        _repository = playlistRepository;
        _mapper = mapper;
    }

    public Task<PlaylistDto?> CreatePlaylistAsync(Guid userId, CreatePlaylistRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException("Implement in Phase 6");
    }

    public Task<PlaylistDto?> GetPlaylistAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _repository.Query()
            .Where(p => p.Id == id)
            .ProjectTo<PlaylistDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<bool> UpdatePlaylistAsync(Guid id, UpdatePlaylistRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException("Implement in Phase 6");
    }

    public Task<string?> AddItemsToPlaylistAsync(Guid playlistId, AddItemsRequest request, CancellationToken cancellationToken = default)
    {
        // TODO: Return the new snapshot_id after adding items
        throw new NotImplementedException("Implement in Phase 6");
    }

    public Task<PaginatedResult<PlaylistSummaryDto>> GetUserPlaylistsAsync(Guid userId, int offset = 0, int limit = 50, CancellationToken cancellationToken = default)
    {
        return _repository.Query()
            .Where(p => p.OwnerId == userId)
            .ProjectTo<PlaylistSummaryDto>(_mapper.ConfigurationProvider)
            .ToPaginatedResultAsync(offset, limit, this.GetHref(offset, limit), cancellationToken);
    }

    public Task<PaginatedResult<PlaylistSummaryDto>> GetPlaylistsByCategoryAsync(Guid categoryId, int offset = 0, int limit = 50, CancellationToken cancellationToken = default)
    {
        return _repository.Query()
            .Where(p => p.Categories.Any(c => c.Id == categoryId))
            .ProjectTo<PlaylistSummaryDto>(_mapper.ConfigurationProvider)
            .ToPaginatedResultAsync(offset, limit, this.GetHref(offset, limit), cancellationToken);
    }

    private string GetHref(int offset, int limit) => $"/api/v1/playlists?offset={offset}&limit={limit}";
}
