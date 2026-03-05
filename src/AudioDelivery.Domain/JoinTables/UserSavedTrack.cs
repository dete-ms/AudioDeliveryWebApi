using AudioDelivery.Domain.Entities;

namespace AudioDelivery.Domain.JoinTables;

/// <summary>
/// Join entity for the many-to-many relationship between User and Track (saved tracks).
/// </summary>
public class UserSavedTrack
{
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public Guid TrackId { get; set; }
    public Track Track { get; set; } = null!;
}
