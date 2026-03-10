using AudioDelivery.Application.Artists.DTOs;
using AudioDelivery.Domain.Entities;

namespace AudioDelivery.Application.Common.Interfaces;

/// <summary>
/// Artist-specific repository interface.
/// </summary>
public interface IArtistRepository : IRepository<Artist>
{
    /// <summary>
    /// Asynchronously creates a new artist using the specified creation request.
    /// </summary>
    Task<ArtistDto?> CreateArtistAsync(CreateArtistRequest createArtistRequest);
}
