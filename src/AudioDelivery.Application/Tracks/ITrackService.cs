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
    Task<TrackDto?> GetTrackAsync(Guid id, string? market = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// GET /tracks?ids=...
    /// </summary>
    Task<IReadOnlyList<TrackDto>> GetSeveralTracksAsync(IEnumerable<Guid> ids, string? market = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// GET /audio-features/{id}
    /// </summary>
    Task<AudioFeaturesDto?> GetAudioFeaturesAsync(Guid trackId, CancellationToken cancellationToken = default);

    /// <summary>
    /// GET /audio-features?ids=...
    /// </summary>
    Task<IReadOnlyList<AudioFeaturesDto>> GetSeveralAudioFeaturesAsync(IEnumerable<Guid> trackIds, CancellationToken cancellationToken = default);

    // TODO: Add methods for user's saved tracks:
    //   - GetUserSavedTracksAsync(...)
    //   - SaveTracksAsync(...)
    //   - RemoveTracksAsync(...)
    //   - CheckSavedTracksAsync(...)
}
