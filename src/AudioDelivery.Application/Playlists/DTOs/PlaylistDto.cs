namespace AudioDelivery.Application.Playlists.DTOs;

/// <summary>
/// Full playlist details returned by GET /api/v1/playlists/{id}.
/// Maps to Spotify's PlaylistObject.
///
/// See: https://developer.spotify.com/documentation/web-api/reference/get-playlist
/// </summary>
public class PlaylistDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool? IsPublic { get; set; }
    public bool Collaborative { get; set; }
    public string? SnapshotId { get; set; }
    public string Uri { get; set; } = string.Empty;
    public string? ExternalUrl { get; set; }

    // TODO: Add Owner (PublicUserDto), Images, Tracks (PaginatedResult<PlaylistTrackDto>)
}
