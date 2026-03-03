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

    // ── Foreign Keys ──
    // Only ONE of these will be set per image row (polymorphic ownership).
    // EF Core configuration will set up optional relationships for each.

    /// <summary>
    /// FK to the Album this image belongs to (null if not an album image).
    /// </summary>
    public Guid? AlbumId { get; set; }
    public Album? Album { get; set; }

    /// <summary>
    /// FK to the Artist this image belongs to (null if not an artist image).
    /// </summary>
    public Guid? ArtistId { get; set; }
    public Artist? Artist { get; set; }

    /// <summary>
    /// FK to the Playlist this image belongs to.
    /// </summary>
    public Guid? PlaylistId { get; set; }
    public Playlist? Playlist { get; set; }

    /// <summary>
    /// FK to the User this image belongs to.
    /// </summary>
    public Guid? UserId { get; set; }
    public User? User { get; set; }

    /// <summary>
    /// FK to the Category this image belongs to.
    /// </summary>
    public Guid? CategoryId { get; set; }
    public Category? Category { get; set; }
}
