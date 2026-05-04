using AudioDelivery.Domain.Entities;

namespace AudioDelivery.Domain.JoinTables;

/// <summary>
/// Join entity for the many-to-many relationship between Album and Image.
/// </summary>
public class AlbumImage
{
    public Guid AlbumId { get; set; }
    public Album Album { get; set; } = null!;

    public Guid ImageId { get; set; }
    public Image Image { get; set; } = null!;
}
