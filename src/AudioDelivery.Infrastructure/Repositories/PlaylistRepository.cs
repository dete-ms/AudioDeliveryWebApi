using AudioDelivery.Domain.Entities;
using AudioDelivery.Infrastructure.Data;

namespace AudioDelivery.Infrastructure.Repositories;

/// <summary>
/// Playlist-specific repository implementation.
///
/// TODO: Implement the methods defined in IPlaylistRepository.
/// </summary>
public class PlaylistRepository : Repository<Playlist>, IPlaylistRepository
{
    public PlaylistRepository(AppDbContext context) : base(context) { }

    // TODO: Implement playlist-specific query methods
}
