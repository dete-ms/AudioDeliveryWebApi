using AudioDelivery.Application.Playlists;
using AudioDelivery.Application.Artists;
using AudioDelivery.Application.Library;
using AudioDelivery.Application.Albums;
using AudioDelivery.Application.Tracks;
using AudioDelivery.Application.Playlists.DTOs;
using AudioDelivery.Application.Common.Models;
using AudioDelivery.Application.Artists.DTOs;
using AudioDelivery.Application.Library.DTOs;
using AudioDelivery.Application.Albums.DTOs;
using AudioDelivery.Application.Tracks.DTOs;
using AudioDelivery.Application.Common.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AudioDelivery.Api.Controllers;

/// <summary>
/// Library API – unified endpoint for saving, removing, and checking items.
/// </summary>
[ApiController]
[Route("api/v1/me/library")]
public class LibraryController : ControllerBase
{
    private readonly IUserLibraryService _userLibraryService;
    private readonly IPlaylistService _playlistService;
    private readonly IArtistService _artistService;
    private readonly IAlbumService _albumService;
    private readonly ITrackService _trackService;

    public LibraryController(
        IUserLibraryService libraryService,
        IPlaylistService playlistService,
        IArtistService artistService,
        IAlbumService albumService,
        ITrackService trackService)
    {
        _userLibraryService = libraryService;
        _playlistService = playlistService;
        _artistService = artistService;
        _albumService = albumService;
        _trackService = trackService;
    }

    /// <summary>
    /// Save one or more items to the current user's library.
    /// </summary>
    /// <param name="userId">The current user's ID (will be resolved from auth token in Phase 8).</param>
    /// <param name="request">Comma-separated Spotify URIs to save (max 40).</param>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SaveItems([FromQuery] Guid userId, [FromBody] LibraryItemRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Uris))
            return BadRequest(new { error = "Request body must contain 'uris'." });

        // TODO: Replace userId query param with ClaimsPrincipal resolution in Phase 8
        await _userLibraryService.SaveItemsAsync(userId, request);
        return Ok();
    }

    /// <summary>
    /// Remove one or more items from the current user's library.
    /// </summary>
    /// <param name="userId">The current user's ID (will be resolved from auth token in Phase 8).</param>
    /// <param name="request">Comma-separated Spotify URIs to remove (max 40).</param>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RemoveItems([FromQuery] Guid userId, [FromBody] LibraryItemRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Uris))
            return BadRequest(new { error = "Request body must contain 'uris'." });

        // TODO: Replace userId query param with ClaimsPrincipal resolution in Phase 8
        await _userLibraryService.RemoveItemsAsync(userId, request);
        return Ok();
    }

    /// <summary>
    /// Check if one or more items are saved in the current user's library.
    /// </summary>
    /// <param name="userId">The current user's ID (will be resolved from auth token in Phase 8).</param>
    /// <param name="uris">Comma-separated Spotify URIs to check (max 40).</param>
    [HttpGet("contains")]
    [ProducesResponseType(typeof(ItemCheckResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CheckItems([FromQuery] Guid userId, [FromQuery] string uris)
    {
        if (string.IsNullOrWhiteSpace(uris))
            return BadRequest(new { error = "The 'uris' query parameter is required." });

        // TODO: Replace userId query param with ClaimsPrincipal resolution in Phase 8
        var request = new LibraryItemRequest { Uris = uris };
        var result = await _userLibraryService.CheckItemsAsync(userId, request);
        return Ok(result);
    }

    [HttpGet("albums")]
    [ProducesResponseType(typeof(PaginatedResult<AlbumSummaryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetSavedAlbums([FromQuery] Guid userId)
    {
        // TODO: Replace userId query param with ClaimsPrincipal resolution in Phase 8
        var result = await _albumService.GetSavedAlbumsAsync(userId);
        return Ok(result);
    }

    [HttpGet("tracks")]
    [ProducesResponseType(typeof(PaginatedResult<TrackDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetSavedTracks([FromQuery] Guid userId)
    {
        // TODO: Replace userId query param with ClaimsPrincipal resolution in Phase 8
        var result = await _trackService.GetSavedTracksAsync(userId);
        return Ok(result);
    }

    [HttpGet("artists")]
    [ProducesResponseType(typeof(PaginatedResult<ArtistSummaryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetSavedArtists([FromQuery] Guid userId)
    {
        // TODO: Replace userId query param with ClaimsPrincipal resolution in Phase 8
        var result = await _artistService.GetSavedArtistsAsync(userId);
        return Ok(result);
    }

    [HttpGet("playlists")]
    [ProducesResponseType(typeof(PaginatedResult<PlaylistSummaryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetSavedPlaylists([FromQuery] Guid userId)
    {
        // TODO: Replace userId query param with ClaimsPrincipal resolution in Phase 8
        var result = await _playlistService.GetSavedPlaylistsAsync(userId);
        return Ok(result);
    }
}