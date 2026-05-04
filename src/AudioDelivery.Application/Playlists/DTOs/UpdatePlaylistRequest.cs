using Microsoft.AspNetCore.Http;

namespace AudioDelivery.Application.Playlists.DTOs;

/// <summary>
/// Request body for PATCH /api/v1/playlists/{id} (update playlist details).
/// </summary>
public class UpdatePlaylistRequest
{
    /// <summary>
    /// Gets or sets the display name associated with the playlist.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the playlist is publicly accessible.
    /// </summary>
    public bool? IsPublic { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether collaborative features are enabled.
    /// </summary>
    public bool? Collaborative { get; set; }

    /// <summary>
    /// Gets or sets the textual description associated with the playlist.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the uploaded cover image file for the playlist.
    /// </summary>
    public IFormFile? CoverImage { get; set; }
}
