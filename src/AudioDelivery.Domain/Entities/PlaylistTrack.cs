using AudioDelivery.Domain.Common;

namespace AudioDelivery.Domain.Entities;

/// <summary>
/// Join entity for the many-to-many relationship between Playlist and Track.
/// Modeled as a first-class entity to store extra fields (added_at, added_by, position).
/// </summary>
public class PlaylistTrack : BaseEntity
{
    /// <summary>
    /// FK to the playlist.
    /// </summary>
    public Guid PlaylistId { get; set; }
    public Playlist Playlist { get; set; } = null!;

    /// <summary>
    /// FK to the track.
    /// </summary>
    public Guid TrackId { get; set; }
    public Track Track { get; set; } = null!;

    /// <summary>
    /// When this track was added to the playlist (UTC).
    /// </summary>
    public DateTime AddedAt { get; set; }

    /// <summary>
    /// FK to the user who added this track (nullable – could be the owner).
    /// </summary>
    public Guid? AddedByUserId { get; set; }
    public User? AddedByUser { get; set; }

    /// <summary>
    /// Zero-based position of this track in the playlist.
    /// Used to maintain the user-defined ordering of tracks.
    /// </summary>
    public int Position { get; set; }
}
