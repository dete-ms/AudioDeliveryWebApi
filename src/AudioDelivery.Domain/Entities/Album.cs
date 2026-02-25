using AudioDelivery.Domain.Common;
using AudioDelivery.Domain.Enums;

namespace AudioDelivery.Domain.Entities;

/// <summary>
/// Represents a music album (LP, single, or compilation).
/// </summary>
public class Album : BaseEntity
{
    /// <summary>
    /// The name of the album.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The type: Album, Single, or Compilation.
    /// </summary>
    public AlbumType AlbumType { get; set; }

    /// <summary>
    /// The total number of tracks on the album.
    /// </summary>
    public int TotalTracks => this.Tracks.Count;

    /// <summary>
    /// The release date as a string (e.g., "1981", "1981-12", "1981-12-15").
    /// The precision is indicated by <see cref="ReleaseDatePrecision"/>.
    /// </summary>
    public string ReleaseDate { get; set; } = string.Empty;

    /// <summary>
    /// How precise the release date is: Year, Month, or Day. Default is Year if not specified.
    /// </summary>
    public ReleaseDatePrecision ReleaseDatePrecision { get; set; } = ReleaseDatePrecision.Year;

    /// <summary>
    /// Popularity score (0–100). Higher = more popular.
    /// Derived from the popularity of the album's tracks.
    /// </summary>
    public int Popularity { get; set; }

    /// <summary>
    /// The record label that released the album.
    /// </summary>
    public string? Label { get; set; }

    /// <summary>
    /// The Spotify-style URI (e.g., "spotify:album:{id}").
    /// </summary>
    public string Uri { get; set; } = string.Empty;

    /// <summary>
    /// External URL – e.g., the Spotify web player link.
    /// </summary>
    public string? ExternalUrl { get; set; }

    // ── Navigation Properties ──

    /// <summary>
    /// Artists who created this album (many-to-many).
    /// </summary>
    public ICollection<Artist> Artists { get; set; } = new List<Artist>();

    /// <summary>
    /// Tracks on this album (one-to-many).
    /// </summary>
    public ICollection<Track> Tracks { get; set; } = new List<Track>();

    /// <summary>
    /// Cover art images in various sizes (one-to-many).
    /// </summary>
    public ICollection<Image> Images { get; set; } = new List<Image>();

    /// <summary>
    /// Gets or sets the collection of users who have saved this item (many-to-many).
    /// </summary>
    public ICollection<User> SavedByUsers { get; set; } = new List<User>();
}
