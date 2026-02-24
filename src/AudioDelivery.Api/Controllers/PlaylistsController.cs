using AudioDelivery.Application.Playlists;
using AudioDelivery.Application.Playlists.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AudioDelivery.Api.Controllers;

/// <summary>
/// Playlists API – mirrors Spotify's /playlists and /users/{userId}/playlists endpoints.
///
/// Endpoints:
///   GET    /api/v1/playlists/{id}           → Get a playlist
///   PUT    /api/v1/playlists/{id}           → Update playlist details
///   GET    /api/v1/playlists/{id}/tracks    → Get playlist tracks
///   POST   /api/v1/playlists/{id}/tracks    → Add items to a playlist
///   GET    /api/v1/users/{userId}/playlists → Get a user's playlists
///   POST   /api/v1/users/{userId}/playlists → Create a playlist
///
/// See: https://developer.spotify.com/documentation/web-api/reference/get-playlist
/// </summary>
[ApiController]
[Route("api/v1")]
public class PlaylistsController : ControllerBase
{
    private readonly IPlaylistService _playlistService;

    public PlaylistsController(IPlaylistService playlistService)
    {
        _playlistService = playlistService;
    }

    /// <summary>
    /// Get a playlist owned by a Spotify user.
    /// </summary>
    [HttpGet("playlists/{id:guid}")]
    [ProducesResponseType(typeof(PlaylistDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPlaylist(Guid id, [FromQuery] string? market = null)
    {
        var result = await _playlistService.GetPlaylistAsync(id, market);
        if (result is null) return NotFound();
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
        var success = await _playlistService.UpdatePlaylistAsync(id, request);
        if (!success) return NotFound();
        return Ok();
    }

    /// <summary>
    /// Get full details of the items of a playlist.
    /// </summary>
    [HttpGet("playlists/{id:guid}/items")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPlaylistTracks(Guid id, [FromQuery] int offset = 0, [FromQuery] int limit = 100, [FromQuery] string? market = null)
    {
        var result = await _playlistService.GetPlaylistTracksAsync(id, offset, limit, market);
        return Ok(result);
    }

    /// <summary>
    /// Add one or more items to a user's playlist.
    /// </summary>
    [HttpPost("playlists/{id:guid}/items")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> AddItemsToPlaylist(Guid id, [FromBody] AddItemsRequest request)
    {
        var snapshotId = await _playlistService.AddItemsToPlaylistAsync(id, request);
        return Created(string.Empty, new { snapshot_id = snapshotId });
    }

    /// <summary>
    /// Get a list of the playlists owned or followed by a user.
    /// </summary>
    [HttpGet("users/{userId:guid}/playlists")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserPlaylists(Guid userId, [FromQuery] int offset = 0, [FromQuery] int limit = 20)
    {
        var result = await _playlistService.GetUserPlaylistsAsync(userId, offset, limit);
        return Ok(result);
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
}
