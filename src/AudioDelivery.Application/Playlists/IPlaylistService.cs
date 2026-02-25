using AudioDelivery.Application.Common.Models;
using AudioDelivery.Application.Playlists.DTOs;
using AudioDelivery.Application.Tracks.DTOs;

namespace AudioDelivery.Application.Playlists;

/// <summary>
/// Service interface for Playlist business logic.
/// </summary>
public interface IPlaylistService
{
    /// <summary>
    /// GET /playlists/{id}
    /// </summary>
    Task<PlaylistDto?> GetPlaylistAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// PUT /playlists/{id}
    /// </summary>
    Task<bool> UpdatePlaylistAsync(Guid id, UpdatePlaylistRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// GET /playlists/{id}/items
    /// </summary>
    Task<PaginatedResult<TrackDto>> GetPlaylistTracksAsync(Guid playlistId, int offset = 0, int limit = 100, CancellationToken cancellationToken = default);

    /// <summary>
    /// POST /playlists/{id}/items
    /// </summary>
    Task<string?> AddItemsToPlaylistAsync(Guid playlistId, AddItemsRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// GET /users/{userId}/playlists
    /// </summary>
    Task<PaginatedResult<PlaylistSummaryDto>> GetUserPlaylistsAsync(Guid userId, int offset = 0, int limit = 20, CancellationToken cancellationToken = default);

    /// <summary>
    /// POST /users/{userId}/playlists
    /// </summary>
    Task<PlaylistDto?> CreatePlaylistAsync(Guid userId, CreatePlaylistRequest request, CancellationToken cancellationToken = default);

    // TODO: Add methods for:
    //   - RemovePlaylistItemsAsync(...)
    //   - GetPlaylistCoverImageAsync(...)
    //   - UploadPlaylistCoverImageAsync(...)
    //   - GetCurrentUserPlaylistsAsync(...)
}
