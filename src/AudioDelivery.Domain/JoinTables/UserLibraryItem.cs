using AudioDelivery.Domain.Common;
using AudioDelivery.Domain.Entities;

namespace AudioDelivery.Domain.JoinTables;

public class UserLibraryItem : BaseEntity
{
    public Guid UserId { get; set; }
    public User? User { get; set; }

    public Guid? TrackId { get; set; }
    public Guid? AlbumId { get; set; }
    public Guid? ArtistId { get; set; }
    public Guid? PlaylistId { get; set; }

    public Track? Track { get; set; }
    public Album? Album { get; set; }
    public Artist? Artist { get; set; }
    public Playlist? Playlist { get; set; }
}
