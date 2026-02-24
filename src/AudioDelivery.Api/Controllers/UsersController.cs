using AudioDelivery.Application.Users;
using AudioDelivery.Application.Users.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AudioDelivery.Api.Controllers;

/// <summary>
/// Users API – mirrors Spotify's /me and /users endpoints.
///
/// Endpoints:
///   GET /api/v1/me                 → Get current user's profile
///   GET /api/v1/users/{userId}     → Get a user's public profile
///
/// NOTE: The /me endpoint normally requires authentication to know which user
/// is "current". Since security is deferred, we'll accept a userId query parameter
/// as a temporary workaround. Replace with auth-based user identification later.
///
/// See: https://developer.spotify.com/documentation/web-api/reference/get-current-users-profile
/// </summary>
[ApiController]
[Route("api/v1")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Get detailed profile information about the current user.
    /// Temporarily accepts userId as a query parameter until auth is implemented.
    /// </summary>
    [HttpGet("me")]
    [ProducesResponseType(typeof(UserProfileDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCurrentUser([FromQuery] Guid userId)
    {
        // TODO: Replace userId query param with auth-based current user (from JWT claims)
        var result = await _userService.GetCurrentUserAsync(userId);
        if (result is null) return NotFound();
        return Ok(result);
    }

    /// <summary>
    /// Get public profile information about a user.
    /// </summary>
    [HttpGet("users/{userId:guid}")]
    [ProducesResponseType(typeof(PublicUserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUser(Guid userId)
    {
        var result = await _userService.GetUserAsync(userId);
        if (result is null) return NotFound();
        return Ok(result);
    }
}
