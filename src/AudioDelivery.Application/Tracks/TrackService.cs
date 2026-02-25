using AudioDelivery.Application.Tracks.DTOs;
using AudioDelivery.Infrastructure.Repositories;

namespace AudioDelivery.Application.Tracks;

/// <summary>
/// Track service implementation.
///
/// TODO: Implement each method.
/// </summary>
public class TrackService : ITrackService
{
    private readonly ITrackRepository _trackRepository;

    public TrackService(ITrackRepository trackRepository)
    {
        _trackRepository = trackRepository;
    }

    public async Task<TrackDto?> GetTrackAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException("Implement in Phase 6");
    }

    public async Task<IReadOnlyList<TrackDto>> GetSeveralTracksAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException("Implement in Phase 6");
    }
}
