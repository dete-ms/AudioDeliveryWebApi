using AudioDelivery.Domain.Common;

namespace AudioDelivery.Domain.Entities;

/// <summary>
/// Represents a playlist – a user-curated collection of tracks.
/// </summary>
public class Playlist : BaseEntity
{
    /// <summary>
    /// Gets or sets the name of the playlist.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the playlist description (can contain Markdown/HTML in Spotify).
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the whether the playlist is public (visible on the user's profile).
    /// </summary>
    public bool IsPublic { get; set; } = false;

    /// <summary>
    /// Gets or sets the whether the owner allows other users to modify the playlist.
    /// </summary>
    public bool Collaborative { get; set; } = false;

    /// <summary>
    /// Gets or sets the Version identifier for the current playlist state.
    /// Changes each time the playlist is modified. Useful for conflict detection.
    /// </summary>
    public string? SnapshotId { get; set; }

    /// <summary>
    /// Gets or sets the Spotify-style URI (e.g., "spotify:playlist:{id}").
    /// </summary>
    public string Uri { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the external URL – e.g., the Spotify web player link.
    /// </summary>
    public string? ExternalUrl { get; set; }

    // ── Foreign Key ──

    /// <summary>
    /// Gets or sets the FK to the user who owns this playlist.
    /// </summary>
    public Guid OwnerId { get; set; }
    public User Owner { get; set; } = null!;

    // ── Navigation Properties ──

    /// <summary>
    /// Gets or sets the tracks in this playlist, with metadata like added_at (via PlaylistTrack).
    /// </summary>
    public ICollection<PlaylistTrack> PlaylistTracks { get; set; } = new List<PlaylistTrack>();

    /// <summary>
    /// Gets or sets the playlist cover images (one-to-many).
    /// </summary>
    public ICollection<Image> Images { get; set; } = new List<Image>();

    /// <summary>
    /// Gets or sets the collection of categories associated with this entity (many-to-many).
    /// </summary>
    public ICollection<Category> Categories { get; set; } = new List<Category>();
}
