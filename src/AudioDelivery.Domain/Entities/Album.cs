using AudioDelivery.Domain.Common;
using AudioDelivery.Domain.Enums;

namespace AudioDelivery.Domain.Entities;

/// <summary>
/// Represents a music album (LP, single, or compilation).
/// </summary>
public class Album : BaseEntity
{
    /// <summary>
    /// Gets or sets the name of the album.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The type: Album, Single, or Compilation.
    /// </summary>
    public AlbumType AlbumType { get; set; }

    /// <summary>
    /// Gets or sets the boolean value indicating whether the album is public (available to all users) or private (restricted access).
    /// </summary>
    public bool IsPublic { get; set; }

    /// <summary>
    /// Gets the total number of tracks on the album.
    /// </summary>
    public int TotalTracks => this.Tracks.Count;

    /// <summary>
    /// Gets or sets the release date as a string (e.g., "1981", "1981-12", "1981-12-15").
    /// Gets or sets the precision is indicated by <see cref="ReleaseDatePrecision"/>.
    /// </summary>
    public string ReleaseDate { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets how precise the release date is: Year, Month, or Day. Default is Year if not specified.
    /// </summary>
    public ReleaseDatePrecision ReleaseDatePrecision { get; set; } = ReleaseDatePrecision.Year;

    /// <summary>
    /// Gets or sets the popularity score (0–100). Higher = more popular.
    /// Derived from the popularity of the album's tracks.
    /// </summary>
    public int Popularity { get; set; }

    /// <summary>
    /// Gets or sets the record label that released the album.
    /// </summary>
    public string? Label { get; set; }

    /// <summary>
    /// Gets or sets the Spotify-style URI (e.g., "spotify:album:{id}").
    /// </summary>
    public string Uri { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the external URL – e.g., the Spotify web player link.
    /// </summary>
    public string? ExternalUrl { get; set; }

    // ── Navigation Properties ──

    /// <summary>
    /// Gets or sets the artists who created this album (many-to-many).
    /// </summary>
    public ICollection<Artist> Artists { get; set; } = new List<Artist>();

    /// <summary>
    /// Gets or sets the tracks on this album (one-to-many).
    /// </summary>
    public ICollection<Track> Tracks { get; set; } = new List<Track>();

    /// <summary>
    /// Gets or sets the cover art images in various sizes (one-to-many).
    /// </summary>
    public ICollection<Image> Images { get; set; } = new List<Image>();

    /// <summary>
    /// Gets or sets the collection of users who have saved this item (many-to-many).
    /// </summary>
    public ICollection<User> SavedByUsers { get; set; } = new List<User>();
}
