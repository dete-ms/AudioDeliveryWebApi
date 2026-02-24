using AudioDelivery.Domain.Entities;

namespace AudioDelivery.Infrastructure.Repositories;

/// <summary>
/// Album-specific repository interface.
/// Extends IRepository&lt;Album&gt; with queries specific to the Albums domain.
///
/// TODO: Add methods like:
///   - GetAlbumWithTracksAsync(Guid id) – eager-load tracks
///   - GetAlbumsByArtistAsync(Guid artistId, int offset, int limit)
///   - GetNewReleasesAsync(int offset, int limit) – ordered by release date desc
///   - SearchAlbumsAsync(string query, int offset, int limit)
/// </summary>
public interface IAlbumRepository : IRepository<Album>
{
    // TODO: Define album-specific query method signatures here
}
