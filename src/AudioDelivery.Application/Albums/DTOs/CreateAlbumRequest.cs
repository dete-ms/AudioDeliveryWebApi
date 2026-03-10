using AudioDelivery.Application.Images.DTOs;
using AudioDelivery.Domain.Entities;
using AudioDelivery.Domain.Enums;

namespace AudioDelivery.Application.Albums.DTOs;

/// <summary>
/// Represents a request to create a new album, including album details, associated artists, and related images.
/// </summary>
/// <remarks>Use this type to supply all necessary information when creating an album, such as its name, type,
/// release date, associated artists, and optional metadata like label, popularity, and images. All required and
/// optional fields should be populated according to the album's intended characteristics.</remarks>
public class CreateAlbumRequest
{
    /// <summary>
    /// Gets or sets the name associated with the album.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the type of the album.
    /// </summary>
    public AlbumType AlbumType { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the album is publicly accessible.
    /// </summary>
    public bool IsPublic { get; set; }

    /// <summary>
    /// Gets or sets the release date.
    /// </summary>
    public DateOnly ReleaseDate { get; set; }

    /// <summary>
    /// Gets or sets the level of precision used to represent the release date.
    /// </summary>
    public ReleaseDatePrecision ReleaseDatePrecision { get; set; } = ReleaseDatePrecision.Year;

    /// <summary>
    /// Gets or sets the label associated with this instance.
    /// </summary>
    public string? Label { get; set; }

    /// <summary>
    /// Gets or sets the popularity score associated with the item.
    /// </summary>
    public int? Popularity { get; set; }

    /// <summary>
    /// Gets or sets the collection of image creation requests to be processed.
    /// </summary>
    /// <remarks>Each item in the collection represents a separate image creation operation.</remarks>
    public List<CreateImageRequest> Images { get; set; } = [];

    /// <summary>
    /// Gets or sets the collection of unique identifiers for the associated artists.
    /// </summary>
    public List<Guid> ArtistIds { get; set; } = [];

    /// <summary>
    /// Gets or sets the collection of tracks associated with this album.
    /// </summary>
    /// <remarks>The returned list is never null, but it may be empty if no tracks have been added.</remarks>
    public List<Guid> TrackIds { get; set; } = [];
}
