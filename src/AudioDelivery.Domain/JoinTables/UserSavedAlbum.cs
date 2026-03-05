using AudioDelivery.Domain.Entities;

namespace AudioDelivery.Domain.JoinTables;

/// <summary>
/// Join entity for the many-to-many relationship between User and Album (saved albums).
/// </summary>
public class UserSavedAlbum
{
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public Guid AlbumId { get; set; }
    public Album Album { get; set; } = null!;
}
