using AudioDelivery.Domain.Common;

namespace AudioDelivery.Domain.Entities;

/// <summary>
/// Represents a user profile.
/// NOTE: Security/auth fields (password hash, tokens, etc.) are intentionally
/// omitted for now. They will be added in a later phase.
/// </summary>
public class User : BaseEntity
{
    /// <summary>
    /// The name displayed on the user's profile.
    /// </summary>
    public string? DisplayName { get; set; }

    /// <summary>
    /// The user's email address (unverified per Spotify docs).
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// ISO 3166-1 alpha-2 country code of the user's account.
    /// </summary>
    public string? Country { get; set; }

    /// <summary>
    /// The Spotify-style URI (e.g., "spotify:user:{id}").
    /// </summary>
    public string Uri { get; set; } = string.Empty;

    /// <summary>
    /// External URL – e.g., the public Spotify profile link.
    /// </summary>
    public string? ExternalUrl { get; set; }

    /// <summary>
    /// Total number of followers this user has.
    /// </summary>
    public int FollowerCount { get; set; }

    // ── Navigation Properties ──

    /// <summary>
    /// Playlists owned by this user (one-to-many).
    /// </summary>
    public ICollection<Playlist> Playlists { get; set; } = new List<Playlist>();

    /// <summary>
    /// Profile images (one-to-many).
    /// </summary>
    public ICollection<Image> Images { get; set; } = new List<Image>();

    // TODO: Phase 2 – Add saved albums, saved tracks, and followed artists
    //       as many-to-many navigation properties with dedicated join entities.
}
