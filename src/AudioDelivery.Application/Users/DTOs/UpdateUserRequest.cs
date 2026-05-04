using Microsoft.AspNetCore.Http;

namespace AudioDelivery.Application.Users.DTOs;

public class UpdateUserRequest
{
    /// <summary>
    /// The name displayed on the user's profile.
    /// </summary>
    public string? DisplayName { get; set; }

    /// <summary>
    /// The user's email address.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// ISO 3166-1 alpha-2 country code of the user's account.
    /// </summary>
    public string? Country { get; set; }

    /// <summary>
    /// Gets or sets the uploaded profile image file for the user.
    /// </summary>
    public IFormFile? ProfileImage { get; set; }
}
