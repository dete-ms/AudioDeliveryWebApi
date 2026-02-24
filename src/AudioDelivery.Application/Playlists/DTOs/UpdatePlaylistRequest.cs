namespace AudioDelivery.Application.Playlists.DTOs;

/// <summary>
/// Request body for PUT /api/v1/playlists/{id} (update playlist details).
/// All fields are optional – only provided fields are updated.
///
/// See: https://developer.spotify.com/documentation/web-api/reference/change-playlist-details
/// </summary>
public class UpdatePlaylistRequest
{
    public string? Name { get; set; }
    public bool? IsPublic { get; set; }
    public bool? Collaborative { get; set; }
    public string? Description { get; set; }
}
