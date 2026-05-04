using AudioDelivery.Domain.Entities;

namespace AudioDelivery.Domain.JoinTables;

/// <summary>
/// Join entity for the many-to-many relationship between Artist and Image.
/// </summary>
public class ArtistImage
{
    public Guid ArtistId { get; set; }
    public Artist Artist { get; set; } = null!;

    public Guid ImageId { get; set; }
    public Image Image { get; set; } = null!;
}
