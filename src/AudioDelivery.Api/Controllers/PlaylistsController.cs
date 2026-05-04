using AudioDelivery.Application.Playlists.DTOs;
using AudioDelivery.Application.Playlists;
using AudioDelivery.Application.Tracks;
using Microsoft.AspNetCore.Mvc;

namespace AudioDelivery.Api.Controllers;

/// <summary>
/// Provides API endpoints for managing user playlists, including creating, retrieving, updating, and deleting
/// playlists, as well as adding or removing playlist items.
/// </summary>
/// <remarks>This controller exposes RESTful endpoints for playlist operations, following standard HTTP
/// conventions. All routes are prefixed with 'api/v1'. Methods support pagination where applicable and return
/// appropriate HTTP status codes for success and error conditions. Access to these endpoints may require user
/// authentication and authorization, depending on application configuration.</remarks>
[ApiController]
[Route("api/v1")]
public class PlaylistsController : ControllerBase
{
    private readonly IPlaylistService _playlistService;
    private readonly ITrackService _trackService;

    public PlaylistsController(IPlaylistService playlistService, ITrackService trackService)
    {
        _playlistService = playlistService;
        _trackService = trackService;
    }

    /// <summary>
    /// Create a playlist for a user.
    /// </summary>
    [HttpPost("users/{userId:guid}/playlists")]
    [ProducesResponseType(typeof(PlaylistDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreatePlaylist(Guid userId, [FromBody] CreatePlaylistRequest request)
    {
        var result = await _playlistService.CreatePlaylistAsync(userId, request);
        return CreatedAtAction(nameof(GetPlaylist), new { id = result?.Id }, result);
    }

    /// <summary>
    /// Add one or more items to a user's playlist.
    /// </summary>
    [HttpPut("playlists/{id:guid}/items")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> AddItemsToPlaylist(Guid id, [FromBody] AddItemsRequest request)
    {
        var snapshotId = await _playlistService.AddItemsToPlaylistAsync(id, request);
        return Created(string.Empty, new { snapshot_id = snapshotId });
    }

    /// <summary>
    /// Get a playlist owned by a Spotify user.
    /// </summary>
    [HttpGet("playlists/{id:guid}")]
    [ProducesResponseType(typeof(PlaylistDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPlaylist(Guid id)
    {
        var result = await _playlistService.GetPlaylistAsync(id);
        if (result is null) return NotFound();
        return Ok(result);
    }

    /// <summary>
    /// Get full details of the items of a playlist.
    /// </summary>
    [HttpGet("playlists/{id:guid}/items")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPlaylistTracks(Guid id, [FromQuery] int offset = 0, [FromQuery] int limit = 100)
    {
        var result = await _trackService.GetTracksInPlaylistAsync(id, offset, limit);
        return Ok(result);
    }

    /// <summary>
    /// Get a list of the playlists owned or followed by a user.
    /// </summary>
    [HttpGet("users/{userId:guid}/playlists")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserPlaylists(Guid userId, [FromQuery] int offset = 0, [FromQuery] int limit = 50)
    {
        var result = await _playlistService.GetPublicPlaylistsByUserAsync(userId, offset, limit);
        return Ok(result);
    }

    /// <summary>
    /// Change a playlist's name, description, and public/private state.
    /// </summary>
    [HttpPut("playlists/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdatePlaylist(Guid id, [FromBody] UpdatePlaylistRequest request)
    {
        var success = (await _playlistService.UpdatePlaylistAsync(id, request)) != null;
        if (!success) return NotFound();
        return Ok();
    }

    [HttpDelete("playlists/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePlaylist(Guid id)
    {
        var success = await _playlistService.DeletePlaylistAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpDelete("playlists/{id:guid}/items")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveItemsFromPlaylist(Guid id, [FromBody] RemoveItemsRequest request)
    {
        var snapshotId = await _playlistService.RemoveItemsFromPlaylistAsync(id, request);
        if (snapshotId is null) return NotFound();
        return NoContent();
    }
}