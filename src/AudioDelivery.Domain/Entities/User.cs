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
    /// The user's email address.
    /// </summary>
    public string Email { get; set; } = string.Empty;

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
    public int FollowerCount => this.Followers.Count;

    // ── Navigation Properties ──

    /// <summary>
    /// Playlists owned by this user (one-to-many).
    /// </summary>
    public ICollection<Playlist> Playlists { get; set; } = new List<Playlist>();

    /// <summary>
    /// Profile images (one-to-many).
    /// </summary>
    public ICollection<Image> Images { get; set; } = new List<Image>();

    /// <summary>
    /// Gets or sets the collection of artists that the user is currently following (many-to-many).
    /// </summary>
    public ICollection<Artist> FollowedArtists { get; set; } = new List<Artist>();

    /// <summary>
    /// Gets or sets the collection of albums that have been saved by the user (many-to-many).
    /// </summary>
    public ICollection<Album> SavedAlbums { get; set; } = new List<Album>();

    /// <summary>
    /// Gets or sets the collection of tracks that have been saved by the user (many-to-many).
    /// </summary>
    public ICollection<Track> SavedTracks { get; set; } = new List<Track>();

    /// <summary>
    /// Gets or sets the collection of users who follow this user (many-to-many).
    /// </summary>
    public ICollection<User> Followers { get; set; } = new List<User>();

    /// <summary>
    /// Gets or sets the collection of users that are currently followed by this user (many-to-many).
    /// </summary>
    public ICollection<User> FollowedUsers { get; set; } = new List<User>();
}
