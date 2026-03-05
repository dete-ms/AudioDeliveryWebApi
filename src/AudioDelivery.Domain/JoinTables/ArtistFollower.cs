using AudioDelivery.Domain.Entities;

namespace AudioDelivery.Domain.JoinTables;

/// <summary>
/// Join entity for the many-to-many relationship between Artist and User (followers).
/// </summary>
public class ArtistFollower
{
    public Guid ArtistId { get; set; }
    public Artist Artist { get; set; } = null!;

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
}
