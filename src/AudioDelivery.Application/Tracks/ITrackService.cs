using AudioDelivery.Application.Tracks.DTOs;

namespace AudioDelivery.Application.Tracks;

/// <summary>
/// Service interface for Track business logic.
/// </summary>
public interface ITrackService
{
    /// <summary>
    /// GET /tracks/{id}
    /// </summary>
    Task<TrackDto?> GetTrackAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// GET /tracks?ids=...
    /// </summary>
    Task<IReadOnlyList<TrackDto>> GetSeveralTracksAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
}
