namespace AudioDelivery.Application.Playlists.DTOs;

/// <summary>
/// Request body for POST /api/v1/users/{userId}/playlists (create a playlist).
/// </summary>
public class CreatePlaylistRequest
{
    /// <summary>
    /// The name for the new playlist (required).
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Whether the playlist is public. Default: true.
    /// </summary>
    public bool? IsPublic { get; set; } = true;

    /// <summary>
    /// Whether users other than the owner can modify it. Default: false.
    /// </summary>
    public bool Collaborative { get; set; }

    /// <summary>
    /// Description of the playlist.
    /// </summary>
    public string? Description { get; set; }
}
