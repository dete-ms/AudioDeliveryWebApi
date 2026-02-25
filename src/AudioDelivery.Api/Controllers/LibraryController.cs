using AudioDelivery.Application.Library;
using AudioDelivery.Application.Library.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AudioDelivery.Api.Controllers;

/// <summary>
/// Library API – unified endpoint for saving, removing, and checking items.
///
/// Endpoints:
///   PUT    /api/v1/me/library          → Save items to current user's library
///   DELETE /api/v1/me/library          → Remove items from current user's library
///   GET    /api/v1/me/library/contains → Check if items are in current user's library
///
/// Accepts Spotify URIs: spotify:{type}:{id}
/// Supported types: track, album, artist, playlist
///
/// See: https://developer.spotify.com/documentation/web-api/reference/save-tracks-user
/// </summary>
[ApiController]
[Route("api/v1/me/library")]
public class LibraryController : ControllerBase
{
    private readonly ILibraryService _libraryService;

    public LibraryController(ILibraryService libraryService)
    {
        _libraryService = libraryService;
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
        await _libraryService.SaveItemsAsync(userId, request);
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
        await _libraryService.RemoveItemsAsync(userId, request);
        return Ok();
    }

    /// <summary>
    /// Check if one or more items are saved in the current user's library.
    /// </summary>
    /// <param name="userId">The current user's ID (will be resolved from auth token in Phase 8).</param>
    /// <param name="uris">Comma-separated Spotify URIs to check (max 40).</param>
    [HttpGet("contains")]
    [ProducesResponseType(typeof(LibraryCheckResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CheckItems([FromQuery] Guid userId, [FromQuery] string uris)
    {
        if (string.IsNullOrWhiteSpace(uris))
            return BadRequest(new { error = "The 'uris' query parameter is required." });

        // TODO: Replace userId query param with ClaimsPrincipal resolution in Phase 8
        var result = await _libraryService.CheckItemsAsync(userId, uris);
        return Ok(result);
    }
}
