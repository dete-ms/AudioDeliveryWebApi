using AudioDelivery.Domain.Entities;
using AudioDelivery.Infrastructure.Data;

namespace AudioDelivery.Infrastructure.Repositories;

/// <summary>
/// Track-specific repository implementation.
///
/// TODO: Implement the methods defined in ITrackRepository.
/// </summary>
public class TrackRepository : Repository<Track>, ITrackRepository
{
    public TrackRepository(AppDbContext context) : base(context) { }

    // TODO: Implement track-specific query methods
}
