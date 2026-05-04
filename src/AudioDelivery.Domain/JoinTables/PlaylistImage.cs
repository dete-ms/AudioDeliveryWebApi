using AudioDelivery.Domain.Entities;

namespace AudioDelivery.Domain.JoinTables;

/// <summary>
/// Join entity for the many-to-many relationship between Playlist and Image.
/// </summary>
public class PlaylistImage
{
    public Guid PlaylistId { get; set; }
    public Playlist Playlist { get; set; } = null!;

    public Guid ImageId { get; set; }
    public Image Image { get; set; } = null!;
}
