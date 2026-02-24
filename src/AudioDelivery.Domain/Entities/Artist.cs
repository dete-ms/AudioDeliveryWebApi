using AudioDelivery.Domain.Common;

namespace AudioDelivery.Domain.Entities;

/// <summary>
/// Represents a music artist.
/// </summary>
public class Artist : BaseEntity
{
    /// <summary>
    /// The name of the artist.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The popularity of the artist (0–100). 
    /// Calculated from the popularity of all the artist's tracks.
    /// </summary>
    public int Popularity { get; set; }

    /// <summary>
    /// Total number of followers.
    /// </summary>
    public int FollowerCount { get; set; }

    /// <summary>
    /// The Spotify-style URI (e.g., "spotify:artist:{id}").
    /// </summary>
    public string Uri { get; set; } = string.Empty;

    /// <summary>
    /// External URL – e.g., the Spotify web player link.
    /// </summary>
    public string? ExternalUrl { get; set; }

    // ── Navigation Properties ──

    /// <summary>
    /// Albums this artist appears on (many-to-many).
    /// </summary>
    public ICollection<Album> Albums { get; set; } = new List<Album>();

    /// <summary>
    /// Tracks this artist performed (many-to-many).
    /// </summary>
    public ICollection<Track> Tracks { get; set; } = new List<Track>();

    /// <summary>
    /// Genres associated with this artist (many-to-many).
    /// </summary>
    public ICollection<Genre> Genres { get; set; } = new List<Genre>();

    /// <summary>
    /// Profile/promo images for the artist (one-to-many).
    /// </summary>
    public ICollection<Image> Images { get; set; } = new List<Image>();
}
