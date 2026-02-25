using AudioDelivery.Domain.Entities;

namespace AudioDelivery.Infrastructure.Repositories;

/// <summary>
/// Artist-specific repository interface.
///
/// TODO: Add methods like:
///   - GetArtistWithAlbumsAsync(Guid id)
///   - GetArtistTopTracksAsync(Guid id) – top tracks by popularity
///   - GetRelatedArtistsAsync(Guid id) – artists sharing genres
///   - SearchArtistsAsync(string query, int offset, int limit)
/// </summary>
public interface IArtistRepository : IRepository<Artist>
{
    // TODO: Define artist-specific query method signatures here
}
