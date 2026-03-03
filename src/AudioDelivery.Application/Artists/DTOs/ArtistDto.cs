using AudioDelivery.Application.Images.DTOs;

namespace AudioDelivery.Application.Artists.DTOs;

/// <summary>
/// Full artist details returned by GET /api/v1/artists/{id}.
/// </summary>
public class ArtistDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Popularity { get; set; }
    public int FollowerCount { get; set; }
    public string Uri { get; set; } = string.Empty;
    public string? ExternalUrl { get; set; }
    public IList<string> Genres { get; set; } = new List<string>();
    public IList<ImageDto> Images { get; set; } = new List<ImageDto>();
}
