using AudioDelivery.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace AudioDelivery.Application.Albums.DTOs;

public class UpdateAlbumRequest
{
    /// <summary>
    /// Gets or sets the name associated with the album.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether the album is publicly accessible.
    /// </summary>
    public bool IsPublic { get; set; }

    /// <summary>
    /// Gets or sets the label associated with this instance.
    /// </summary>
    public string? Label { get; set; }

    /// <summary>
    /// Gets or sets the collection of unique identifiers for the associated artists.
    /// </summary>
    public List<Guid> ArtistIds { get; set; } = [];

    /// <summary>
    /// Gets or sets the uploaded image file associated with the request.
    /// </summary>
    public IFormFile ImageFile { get; set; } = null!;
}
