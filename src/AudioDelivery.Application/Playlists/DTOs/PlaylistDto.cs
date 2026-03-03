using AudioDelivery.Application.Images.DTOs;
using AudioDelivery.Application.Users.DTOs;

namespace AudioDelivery.Application.Playlists.DTOs;

/// <summary>
/// Full playlist details returned by GET /api/v1/playlists/{id}.
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
    public PublicUserDto Owner { get; set; } = null!;
    public IList<PlaylistTrackDto> Tracks { get; set; } = null!;
    public IList<ImageDto> Images { get; set; } = null!;
}
