using AudioDelivery.Application.Albums.DTOs;
using AudioDelivery.Application.Artists.DTOs;
using AudioDelivery.Application.Common.Models;
using AudioDelivery.Application.Tracks.DTOs;
using AudioDelivery.Infrastructure.Repositories;

namespace AudioDelivery.Application.Artists;

/// <summary>
/// Artist service implementation.
///
/// TODO: Implement each method following the same pattern as AlbumService:
///   1. Fetch from repository  2. Map to DTO  3. Return
/// </summary>
public class ArtistService : IArtistService
{
    private readonly IArtistRepository _artistRepository;

    public ArtistService(IArtistRepository artistRepository)
    {
        _artistRepository = artistRepository;
    }

    public async Task<ArtistDto?> GetArtistAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException("Implement in Phase 6");
    }

    public async Task<IReadOnlyList<ArtistDto>> GetSeveralArtistsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException("Implement in Phase 6");
    }

    public async Task<PaginatedResult<AlbumSummaryDto>> GetArtistAlbumsAsync(Guid artistId, int offset = 0, int limit = 20, string? market = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException("Implement in Phase 6");
    }

    public async Task<IReadOnlyList<TrackDto>> GetArtistTopTracksAsync(Guid artistId, string? market = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException("Implement in Phase 6");
    }

    public async Task<IReadOnlyList<ArtistDto>> GetRelatedArtistsAsync(Guid artistId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException("Implement in Phase 6");
    }
}
