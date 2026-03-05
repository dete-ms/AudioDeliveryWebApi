using AudioDelivery.Domain.Entities;

namespace AudioDelivery.Domain.JoinTables;

/// <summary>
/// Join entity for the many-to-many relationship between Artist and Track.
/// </summary>
public class ArtistTrack
{
    public Guid ArtistId { get; set; }
    public Artist Artist { get; set; } = null!;

    public Guid TrackId { get; set; }
    public Track Track { get; set; } = null!;
}
