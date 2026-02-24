using AudioDelivery.Domain.Entities;
using AudioDelivery.Infrastructure.Data;

namespace AudioDelivery.Infrastructure.Repositories;

/// <summary>
/// Artist-specific repository implementation.
///
/// TODO: Implement the methods defined in IArtistRepository.
/// </summary>
public class ArtistRepository : Repository<Artist>, IArtistRepository
{
    public ArtistRepository(AppDbContext context) : base(context) { }

    // TODO: Implement artist-specific query methods
}
