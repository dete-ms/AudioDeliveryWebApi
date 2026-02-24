namespace AudioDelivery.Application.Users.DTOs;

/// <summary>
/// Public user profile returned by GET /api/v1/users/{userId}.
/// Maps to Spotify's PublicUserObject (fewer fields than private profile).
///
/// See: https://developer.spotify.com/documentation/web-api/reference/get-users-profile
/// </summary>
public class PublicUserDto
{
    public Guid Id { get; set; }
    public string? DisplayName { get; set; }
    public string Uri { get; set; } = string.Empty;
    public string? ExternalUrl { get; set; }
    public int FollowerCount { get; set; }

    // TODO: Add Images list
}
