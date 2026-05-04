using AudioDelivery.Domain.Common;

namespace AudioDelivery.Domain.Entities;

/// <summary>
/// Represents a single music track.
/// </summary>
public class Track : BaseEntity
{
    /// <summary>
    /// Gets or sets the name of the track.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the disc number (usually 1 unless multi-disc album).
    /// </summary>
    public int DiscNumber { get; set; } = 1;

    /// <summary>
    /// Gets or sets the track's position number on its disc.
    /// </summary>
    public int TrackNumber { get; set; }

    /// <summary>
    /// Gets or sets the track duration in milliseconds.
    /// </summary>
    public int DurationMs { get; set; }

    /// <summary>
    /// Gets or sets the number of times the track has been played.
    /// </summary>
    public long PlayCount { get; set; }

    /// <summary>
    /// Gets or sets whether the track has explicit lyrics.
    /// </summary>
    public bool Explicit { get; set; }

    /// <summary>
    /// Gets or sets the popularity score (0–100). Higher = more popular.
    /// </summary>
    public int Popularity { get; set; }

    /// <summary>
    /// Gets or sets the URL to a 30-second preview MP3 clip (nullable).
    /// </summary>
    public string? PreviewUrl { get; set; }

    /// <summary>
    /// Gets or sets whether the track is from a local file.
    /// </summary>
    public bool IsLocal { get; set; }

    /// <summary>
    /// Gets or sets the Spotify-style URI (e.g., "spotify:track:{id}").
    /// </summary>
    public string Uri { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the external URL – e.g., the Spotify web player link.
    /// </summary>
    public string? ExternalUrl { get; set; }

    // ── Foreign Keys ──

    /// <summary>
    /// Gets or sets the FK to the album this track belongs to.
    /// </summary>
    public Guid AlbumId { get; set; }
    public Album Album { get; set; } = null!;

    // ── Navigation Properties ──

    /// <summary>
    /// Gets or sets the artists who performed this track (many-to-many).
    /// </summary>
    public ICollection<Artist> Artists { get; set; } = new List<Artist>();

    /// <summary>
    /// Gets or sets the playlists containing this track (many-to-many via PlaylistTrack).
    /// </summary>
    public ICollection<PlaylistTrack> PlaylistTracks { get; set; } = new List<PlaylistTrack>();
}
