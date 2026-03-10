using AudioDelivery.Application.Artists.DTOs;
using AudioDelivery.Application.Common.Models;

namespace AudioDelivery.Application.Artists;

/// <summary>
/// Service interface for Artist business logic.
/// </summary>
public interface IArtistService
{
    /// <summary>
    /// GET /artists/{id}
    /// </summary>
    Task<ArtistDto?> GetArtistAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// GET /artists?ids=...
    /// </summary>
    Task<PaginatedResult<ArtistDto>> GetSeveralArtistsAsync(IEnumerable<Guid> ids, int offset = 0, int limit = 50, CancellationToken cancellationToken = default);

    /// <summary>
    /// GET /artists/{id}/related-artists
    /// </summary>
    Task<PaginatedResult<ArtistDto>> GetRelatedArtistsAsync(Guid artistId, int offset = 0, int limit = 50, CancellationToken cancellationToken = default);

    /// <summary>
    /// POST /artists/new. Creates a new artist using the specified request data.
    /// </summary>
    Task<ArtistDto?> CreateArtist(CreateArtistRequest createArtistRequest);
}
