using AudioDelivery.Application.Common.Models;
using AudioDelivery.Application.Common.DTOs;
using AudioDelivery.Application.Users.DTOs;
using AudioDelivery.Application.Users;
using Microsoft.AspNetCore.Mvc;

namespace AudioDelivery.Api.Controllers;

/// <summary>
/// Users API – mirrors Spotify's /me and /users endpoints.
/// </summary>
[ApiController]
[Route("api/v1/users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    #region CRUD operations

    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <returns><see cref="StatusCodes.Status201Created"/> response if the user was successfully created; otherwise, a <see cref="StatusCodes.Status400BadRequest"/> response if the request is invalid.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(PublicUserDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest createUserRequest)
    {
        var result = await _userService.CreateUserAsync(createUserRequest);
        return CreatedAtAction(nameof(GetUser), new { userId = result.Id }, result);
    }

    /// <summary>
    /// Get public profile information about a user.
    /// </summary>
    [HttpGet("{userId:guid}")]
    [ProducesResponseType(typeof(PublicUserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUser(Guid userId)
    {
        var result = await _userService.GetUserAsync(userId);
        if (result is null) return NotFound();
        return Ok(result);
    }

    /// <summary>
    /// Updates the user with the specified unique identifier.
    /// </summary>
    /// <returns><see cref="StatusCodes.Status200OK"/> response if the user was successfully updated; otherwise, a <see cref="StatusCodes.Status404NotFound"/> response if the user
    /// does not exist.</returns>
    [HttpPatch("{userId:guid}")]
    [ProducesResponseType(typeof(PublicUserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateUser(Guid userId, [FromBody] UpdateUserRequest updateUserRequest)
    {
        var result = await _userService.UpdateUserAsync(userId, updateUserRequest);
        if (result is null) return NotFound();
        return Ok(result);
    }

    /// <summary>
    /// Deletes the user with the specified unique identifier.
    /// </summary>
    /// <returns><see cref="StatusCodes.Status204NoContent"/> response if the user was successfully deleted; otherwise, a <see cref="StatusCodes.Status404NotFound"/> response if the user
    /// does not exist.</returns>
    [HttpDelete("{userId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUser(Guid userId)
    {
        var result = await _userService.DeleteUserAsync(userId);
        if (!result) return NotFound();
        return NoContent();
    }

    #endregion

    #region Follow User Operations

    /// <summary>
    /// Follow a user.
    /// </summary>
    /// <returns><see cref="StatusCodes.Status204NoContent"/> response if the user was successfully followed.</returns>
    [HttpPut("{userId:guid}/following")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> FollowUser(Guid userId, [FromQuery] Guid userIdToFollow)
    {
        await _userService.FollowUserAsync(userId, userIdToFollow);
        return NoContent();
    }

    /// <summary>
    /// Unfollow a user.
    /// </summary>
    /// <returns><see cref="StatusCodes.Status204NoContent"/> response if the user was successfully unfollowed.</returns>
    [HttpDelete("{userId:guid}/following/{userIdToUnfollow:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UnFollowUser(Guid userId, Guid userIdToUnfollow)
    {
        await _userService.UnFollowUserAsync(userId, userIdToUnfollow);
        return NoContent();
    }

    /// <summary>
    /// Get a paginated list of users followed by the specified user.
    /// </summary>
    [HttpGet("{userId:guid}/following")]
    [ProducesResponseType(typeof(PaginatedResult<PublicUserDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetFollowedUsers(Guid userId, [FromQuery] int offset = 0, [FromQuery] int limit = 50)
    {
        var result = await _userService.GetFollowedUsersAsync(userId, offset, limit);
        return Ok(result);
    }

    /// <summary>
    /// Get a paginated list of users who follow the specified user.
    /// </summary>
    [HttpGet("{userId:guid}/followers")]
    [ProducesResponseType(typeof(PaginatedResult<PublicUserDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetFollowers(Guid userId, [FromQuery] int offset = 0, [FromQuery] int limit = 50)
    {
        var result = await _userService.GetFollowersAsync(userId, offset, limit);
        return Ok(result);
    }

    /// <summary>
    /// Check if a user follows multiple users.
    /// </summary>
    [HttpGet("{userId:guid}/following/contains")]
    [ProducesResponseType(typeof(ItemCheckResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> CheckIfUserFollowsUsers(Guid userId, [FromQuery] List<Guid> userIds)
    {
        var result = await _userService.CheckIfUserFollowsUsersAsync(userId, userIds);
        return Ok(result);
    }

    #endregion

    #region Follow Artist Operations

    /// <summary>
    /// Follow an artist.
    /// </summary>
    /// <returns><see cref="StatusCodes.Status204NoContent"/> response if the artist was successfully followed.</returns>
    [HttpPut("{userId:guid}/following/artists")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> FollowArtist(Guid userId, [FromQuery] Guid artistIdToFollow)
    {
        await _userService.FollowArtistAsync(userId, artistIdToFollow);
        return NoContent();
    }

    /// <summary>
    /// Unfollow an artist.
    /// </summary>
    /// <returns><see cref="StatusCodes.Status204NoContent"/> response if the artist was successfully unfollowed.</returns>
    [HttpDelete("{userId:guid}/following/artists/{artistIdToUnfollow:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UnFollowArtist(Guid userId, Guid artistIdToUnfollow)
    {
        await _userService.UnFollowArtistAsync(userId, artistIdToUnfollow);
        return NoContent();
    }

    /// <summary>
    /// Check if a user follows multiple artists.
    /// </summary>
    [HttpGet("{userId:guid}/following/artists/contains")]
    [ProducesResponseType(typeof(ItemCheckResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> CheckIfUserFollowsArtists(Guid userId, [FromQuery] List<Guid> artistIds)
    {
        var result = await _userService.CheckIfUserFollowsArtistsAsync(userId, artistIds);
        return Ok(result);
    }

    #endregion
}