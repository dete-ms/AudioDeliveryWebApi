using AudioDelivery.Application.Albums.DTOs;
using AudioDelivery.Application.Common.Models;
using AudioDelivery.Infrastructure.Repositories;

namespace AudioDelivery.Application.Albums;

/// <summary>
/// Album service implementation.
///
/// TODO: Implement each method by:
///   1. Calling the appropriate repository method to fetch data
///   2. Mapping domain entities to DTOs (manual mapping for now)
///   3. Building the paginated response where applicable
///   4. Handling not-found cases (return null or throw)
///
/// LEARNING NOTES:
///   - Start with GetAlbumAsync – it's the simplest (single entity fetch + map)
///   - Then add GetSeveralAlbumsAsync (batch fetch)
///   - Then tackle pagination with GetAlbumTracksAsync
///   - Manual mapping is intentional – you'll understand the shape of data
///     before introducing AutoMapper later
/// </summary>
public class AlbumService : IAlbumService
{
    private readonly IAlbumRepository _albumRepository;

    public AlbumService(IAlbumRepository albumRepository)
    {
        _albumRepository = albumRepository;
    }

    public async Task<AlbumDto?> GetAlbumAsync(Guid id, CancellationToken cancellationToken = default)
    {
        // TODO: Implement
        // 1. Fetch album from repository (with includes for artists, images, etc.)
        // 2. If not found, return null
        // 3. Map Album entity → AlbumDto
        throw new NotImplementedException("Implement in Phase 6 – see docs/Phase06-ServiceLayer.md");
    }

    public async Task<IReadOnlyList<AlbumDto>> GetSeveralAlbumsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        // TODO: Implement – fetch multiple albums by ID list, map to DTOs
        throw new NotImplementedException("Implement in Phase 6");
    }

    public async Task<PaginatedResult<AlbumSummaryDto>> GetAlbumTracksAsync(Guid albumId, int offset = 0, int limit = 20, CancellationToken cancellationToken = default)
    {
        // TODO: Implement – fetch tracks for a specific album with pagination
        throw new NotImplementedException("Implement in Phase 6");
    }

    public async Task<PaginatedResult<AlbumSummaryDto>> GetNewReleasesAsync(int offset = 0, int limit = 20, string? country = null, CancellationToken cancellationToken = default)
    {
        // TODO: Implement – fetch recently released albums, ordered by release date desc
        throw new NotImplementedException("Implement in Phase 6");
    }
}
