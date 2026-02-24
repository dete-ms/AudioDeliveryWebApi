namespace AudioDelivery.Application.Playlists.DTOs;

/// <summary>
/// Simplified playlist used in lists (e.g., GET /browse/categories/{id}/playlists).
/// Maps to Spotify's SimplifiedPlaylistObject.
/// </summary>
public class PlaylistSummaryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool? IsPublic { get; set; }
    public bool Collaborative { get; set; }
    public string Uri { get; set; } = string.Empty;
    public string? ExternalUrl { get; set; }

    // TODO: Add Owner (PublicUserDto), Images
}
