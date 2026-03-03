namespace AudioDelivery.Application.Artists.DTOs;

/// <summary>
/// Simplified artist representation used inside Album and Track responses.
/// </summary>
public class ArtistSummaryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Uri { get; set; } = string.Empty;
    public string? ExternalUrl { get; set; }
}
