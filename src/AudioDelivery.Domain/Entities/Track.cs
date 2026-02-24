using AudioDelivery.Domain.Common;

namespace AudioDelivery.Domain.Entities;

/// <summary>
/// Represents a single music track.
/// </summary>
public class Track : BaseEntity
{
    /// <summary>
    /// The name of the track.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The disc number (usually 1 unless multi-disc album).
    /// </summary>
    public int DiscNumber { get; set; } = 1;

    /// <summary>
    /// The track's position number on its disc.
    /// </summary>
    public int TrackNumber { get; set; }

    /// <summary>
    /// The track duration in milliseconds.
    /// </summary>
    public int DurationMs { get; set; }

    /// <summary>
    /// Whether the track has explicit lyrics.
    /// </summary>
    public bool Explicit { get; set; }

    /// <summary>
    /// Popularity score (0–100). Higher = more popular.
    /// </summary>
    public int Popularity { get; set; }

    /// <summary>
    /// URL to a 30-second preview MP3 clip (nullable).
    /// </summary>
    public string? PreviewUrl { get; set; }

    /// <summary>
    /// Whether the track is from a local file.
    /// </summary>
    public bool IsLocal { get; set; }

    /// <summary>
    /// The Spotify-style URI (e.g., "spotify:track:{id}").
    /// </summary>
    public string Uri { get; set; } = string.Empty;

    /// <summary>
    /// External URL – e.g., the Spotify web player link.
    /// </summary>
    public string? ExternalUrl { get; set; }

    /// <summary>
    /// International Standard Recording Code.
    /// A unique identifier for recordings, used for rights management.
    /// </summary>
    public string? Isrc { get; set; }

    // ── Foreign Keys ──

    /// <summary>
    /// FK to the album this track belongs to.
    /// </summary>
    public Guid AlbumId { get; set; }
    public Album Album { get; set; } = null!;

    // ── Navigation Properties ──

    /// <summary>
    /// Artists who performed this track (many-to-many).
    /// </summary>
    public ICollection<Artist> Artists { get; set; } = new List<Artist>();

    /// <summary>
    /// Markets where this track is available (many-to-many).
    /// </summary>
    public ICollection<Market> AvailableMarkets { get; set; } = new List<Market>();

    /// <summary>
    /// Playlists containing this track (many-to-many via PlaylistTrack).
    /// </summary>
    public ICollection<PlaylistTrack> PlaylistTracks { get; set; } = new List<PlaylistTrack>();

}
