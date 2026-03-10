using AudioDelivery.Application.Artists.DTOs;
using AudioDelivery.Application.Images.DTOs;
using AudioDelivery.Domain.Enums;

namespace AudioDelivery.Application.Albums.DTOs;

/// <summary>
/// Simplified album representation used when albums appear inside other responses
/// (e.g., in a Track response, the album field uses this simplified form).
/// </summary>
public class AlbumSummaryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public AlbumType AlbumType { get; set; }
    public bool IsPublic { get; set; }
    public int TotalTracks { get; set; }
    public string ReleaseDate { get; set; } = string.Empty;
    public string Uri { get; set; } = string.Empty;
    public IList<ImageDto> Images { get; set; } = null!;
    public IList<ArtistSummaryDto> Artists { get; set; } = null!;
}
