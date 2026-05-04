using AudioDelivery.Domain.Common;

namespace AudioDelivery.Domain.Entities;

/// <summary>
/// Represents a reusable image (cover art, profile picture, etc.).
/// </summary>
public class Image : BaseEntity
{
    /// <summary>
    /// The source URL of the image.
    /// </summary>
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// The image height in pixels.
    /// </summary>
    public int Height { get; set; }

    /// <summary>
    /// The image width in pixels.
    /// </summary>
    public int Width { get; set; }

    // ── Many-to-many reverse navigations ──
    public ICollection<Album> Albums { get; set; } = new List<Album>();
    public ICollection<Artist> Artists { get; set; } = new List<Artist>();
    public ICollection<Playlist> Playlists { get; set; } = new List<Playlist>();
    public ICollection<User> Users { get; set; } = new List<User>();
    public ICollection<Category> Categories { get; set; } = new List<Category>();
}
