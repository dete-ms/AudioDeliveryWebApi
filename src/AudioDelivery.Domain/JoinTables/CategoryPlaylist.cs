using AudioDelivery.Domain.Entities;

namespace AudioDelivery.Domain.JoinTables;

/// <summary>
/// Join entity for the many-to-many relationship between Category and Playlist.
/// </summary>
public class CategoryPlaylist
{
    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    public Guid PlaylistId { get; set; }
    public Playlist Playlist { get; set; } = null!;
}
