using AudioDelivery.Domain.Entities;

namespace AudioDelivery.Infrastructure.Repositories;

/// <summary>
/// Playlist-specific repository interface.
///
/// TODO: Add methods like:
///   - GetPlaylistWithTracksAsync(Guid id, int offset, int limit)
///   - GetPlaylistsByUserAsync(Guid userId, int offset, int limit)
///   - SearchPlaylistsAsync(string query, int offset, int limit)
/// </summary>
public interface IPlaylistRepository : IRepository<Playlist>
{
    // TODO: Define playlist-specific query method signatures here
}
