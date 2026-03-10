using AudioDelivery.Application.Common.Interfaces;
using AudioDelivery.Domain.Entities;
using AudioDelivery.Infrastructure.Data;
using AutoMapper;

namespace AudioDelivery.Infrastructure.Repositories;

/// <summary>
/// Track-specific repository implementation.
/// </summary>
public class TrackRepository : Repository<Track>, ITrackRepository
{
    public TrackRepository(AppDbContext context, IMapper mapper) : base(context, mapper) { }
}
