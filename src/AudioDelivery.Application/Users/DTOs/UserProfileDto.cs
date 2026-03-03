using AudioDelivery.Application.Images.DTOs;

namespace AudioDelivery.Application.Users.DTOs;

/// <summary>
/// Full user profile returned by GET /api/v1/me (current user).
/// </summary>
public class UserProfileDto
{
    public Guid Id { get; set; }
    public string? DisplayName { get; set; }
    public string? Email { get; set; }
    public string? Country { get; set; }
    public string Uri { get; set; } = string.Empty;
    public string? ExternalUrl { get; set; }
    public int FollowerCount { get; set; }
    public IList<ImageDto> Images { get; set; } = null!;
}
