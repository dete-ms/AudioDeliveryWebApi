using AudioDelivery.Application.Images.DTOs;

namespace AudioDelivery.Application.Users.DTOs;

/// <summary>
/// Public user profile returned by GET /api/v1/users/{userId}.
/// </summary>
public class PublicUserDto
{
    public Guid Id { get; set; }
    public string? DisplayName { get; set; }
    public string Uri { get; set; } = string.Empty;
    public string? ExternalUrl { get; set; }
    public int FollowerCount { get; set; }
    public IList<ImageDto> Images { get; set; } = null!;
}
