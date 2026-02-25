using AudioDelivery.Application.Common.Models;
using AudioDelivery.Application.Playlists.DTOs;
using AudioDelivery.Application.Tracks.DTOs;
using AudioDelivery.Infrastructure.Repositories;

namespace AudioDelivery.Application.Playlists;

/// <summary>
/// Playlist service implementation.
///
/// TODO: Implement each method.
/// </summary>
public class PlaylistService : IPlaylistService
{
    private readonly IPlaylistRepository _playlistRepository;
    private readonly IUserRepository _userRepository;

    public PlaylistService(IPlaylistRepository playlistRepository, IUserRepository userRepository)
    {
        _playlistRepository = playlistRepository;
        _userRepository = userRepository;
    }

    public async Task<PlaylistDto?> GetPlaylistAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException("Implement in Phase 6");
    }

    public async Task<bool> UpdatePlaylistAsync(Guid id, UpdatePlaylistRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException("Implement in Phase 6");
    }

    public async Task<PaginatedResult<TrackDto>> GetPlaylistTracksAsync(Guid playlistId, int offset = 0, int limit = 100, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException("Implement in Phase 6");
    }

    public async Task<string?> AddItemsToPlaylistAsync(Guid playlistId, AddItemsRequest request, CancellationToken cancellationToken = default)
    {
        // TODO: Return the new snapshot_id after adding items
        throw new NotImplementedException("Implement in Phase 6");
    }

    public async Task<PaginatedResult<PlaylistSummaryDto>> GetUserPlaylistsAsync(Guid userId, int offset = 0, int limit = 20, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException("Implement in Phase 6");
    }

    public async Task<PlaylistDto?> CreatePlaylistAsync(Guid userId, CreatePlaylistRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException("Implement in Phase 6");
    }
}
