using AudioDelivery.Domain.Common;

namespace AudioDelivery.Domain.Entities;

/// <summary>
/// Audio feature analysis data for a specific track.
/// </summary>
public class AudioFeatures : BaseEntity
{
    /// <summary>
    /// Danceability: 0.0 to 1.0 – how suitable a track is for dancing.
    /// </summary>
    public float Danceability { get; set; }

    /// <summary>
    /// Energy: 0.0 to 1.0 – perceptual measure of intensity and activity.
    /// </summary>
    public float Energy { get; set; }

    /// <summary>
    /// Key: integer using standard Pitch Class notation.
    /// 0 = C, 1 = C♯/D♭, 2 = D, … 11 = B. -1 if no key detected.
    /// </summary>
    public int Key { get; set; }

    /// <summary>
    /// Loudness: typical range -60 to 0 dB.
    /// </summary>
    public float Loudness { get; set; }

    /// <summary>
    /// Mode: 1 = major, 0 = minor.
    /// </summary>
    public int Mode { get; set; }

    /// <summary>
    /// Speechiness: 0.0 to 1.0 – presence of spoken words.
    /// </summary>
    public float Speechiness { get; set; }

    /// <summary>
    /// Acousticness: 0.0 to 1.0 – confidence the track is acoustic.
    /// </summary>
    public float Acousticness { get; set; }

    /// <summary>
    /// Instrumentalness: 0.0 to 1.0 – predicts whether a track has no vocals.
    /// </summary>
    public float Instrumentalness { get; set; }

    /// <summary>
    /// Liveness: 0.0 to 1.0 – detects the presence of a live audience.
    /// </summary>
    public float Liveness { get; set; }

    /// <summary>
    /// Valence: 0.0 to 1.0 – musical positiveness (happy vs. sad).
    /// </summary>
    public float Valence { get; set; }

    /// <summary>
    /// Tempo: BPM (beats per minute).
    /// </summary>
    public float Tempo { get; set; }

    /// <summary>
    /// Time signature: estimated number of beats per bar (e.g., 3, 4, 5, 6, 7).
    /// </summary>
    public int TimeSignature { get; set; }

    /// <summary>
    /// Track duration in milliseconds (should match Track.DurationMs).
    /// </summary>
    public int DurationMs { get; set; }

    // ── Foreign Key ──

    /// <summary>
    /// FK to the Track this analysis belongs to (one-to-one).
    /// </summary>
    public Guid TrackId { get; set; }
    public Track Track { get; set; } = null!;
}
