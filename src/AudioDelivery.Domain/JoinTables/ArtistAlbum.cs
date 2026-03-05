using AudioDelivery.Domain.Entities;

namespace AudioDelivery.Domain.JoinTables;

/// <summary>
/// Join entity for the many-to-many relationship between Artist and Album.
/// </summary>
public class ArtistAlbum
{
    public Guid ArtistId { get; set; }
    public Artist Artist { get; set; } = null!;

    public Guid AlbumId { get; set; }
    public Album Album { get; set; } = null!;
}
