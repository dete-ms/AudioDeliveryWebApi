using AudioDelivery.Domain.Entities;

namespace AudioDelivery.Infrastructure.Repositories;

/// <summary>
/// Track-specific repository interface.
///
/// TODO: Add methods like:
///   - GetTrackWithArtistsAsync(Guid id) – eager-load artists and album
///   - GetTracksByAlbumAsync(Guid albumId, int offset, int limit)
///   - SearchTracksAsync(string query, int offset, int limit)
/// </summary>
public interface ITrackRepository : IRepository<Track>
{
    // TODO: Define track-specific query method signatures here
}
