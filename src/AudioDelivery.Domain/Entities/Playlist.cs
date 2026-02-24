using AudioDelivery.Domain.Common;

namespace AudioDelivery.Domain.Entities;

/// <summary>
/// Represents a playlist – a user-curated collection of tracks.
/// </summary>
public class Playlist : BaseEntity
{
    /// <summary>
    /// The name of the playlist.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The playlist description (can contain Markdown/HTML in Spotify).
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Whether the playlist is public (visible on the user's profile).
    /// null = status is not relevant.
    /// </summary>
    public bool? IsPublic { get; set; }

    /// <summary>
    /// Whether the owner allows other users to modify the playlist.
    /// </summary>
    public bool Collaborative { get; set; }

    /// <summary>
    /// Version identifier for the current playlist state.
    /// Changes each time the playlist is modified. Useful for conflict detection.
    /// </summary>
    public string? SnapshotId { get; set; }

    /// <summary>
    /// The Spotify-style URI (e.g., "spotify:playlist:{id}").
    /// </summary>
    public string Uri { get; set; } = string.Empty;

    /// <summary>
    /// External URL – e.g., the Spotify web player link.
    /// </summary>
    public string? ExternalUrl { get; set; }

    // ── Foreign Key ──

    /// <summary>
    /// FK to the user who owns this playlist.
    /// </summary>
    public Guid OwnerId { get; set; }
    public User Owner { get; set; } = null!;

    // ── Navigation Properties ──

    /// <summary>
    /// Tracks in this playlist, with metadata like added_at (via PlaylistTrack).
    /// </summary>
    public ICollection<PlaylistTrack> PlaylistTracks { get; set; } = new List<PlaylistTrack>();

    /// <summary>
    /// Playlist cover images (one-to-many).
    /// </summary>
    public ICollection<Image> Images { get; set; } = new List<Image>();
}
