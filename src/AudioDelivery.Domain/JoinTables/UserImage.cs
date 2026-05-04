using AudioDelivery.Domain.Entities;

namespace AudioDelivery.Domain.JoinTables;

/// <summary>
/// Join entity for the many-to-many relationship between User and Image.
/// </summary>
public class UserImage
{
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public Guid ImageId { get; set; }
    public Image Image { get; set; } = null!;
}
