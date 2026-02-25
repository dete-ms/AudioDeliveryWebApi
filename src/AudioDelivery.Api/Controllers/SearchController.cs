using AudioDelivery.Application.Search;
using AudioDelivery.Application.Search.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AudioDelivery.Api.Controllers;

/// <summary>
/// Search API – mirrors Spotify's /search endpoint.
///
/// Endpoints:
///   GET /api/v1/search?q=...&amp;type=...  → Search for items
///
/// Supported types: album, artist, track, playlist
///
/// See: https://developer.spotify.com/documentation/web-api/reference/search
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class SearchController : ControllerBase
{
    private readonly ISearchService _searchService;

    public SearchController(ISearchService searchService)
    {
        _searchService = searchService;
    }

    /// <summary>
    /// Search for albums, artists, tracks, and/or playlists matching a keyword string.
    /// </summary>
    /// <param name="q">The search query.</param>
    /// <param name="type">Comma-separated list of types: album, artist, track, playlist.</param>
    /// <param name="limit">Max results per type (default 20, max 50).</param>
    /// <param name="offset">Pagination offset (default 0).</param>
    [HttpGet]
    [ProducesResponseType(typeof(SearchResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Search(
        [FromQuery] string q,
        [FromQuery] string type,
        [FromQuery] int limit = 20,
        [FromQuery] int offset = 0)
    {
        if (string.IsNullOrWhiteSpace(q) || string.IsNullOrWhiteSpace(type))
            return BadRequest(new { error = "Both 'q' and 'type' query parameters are required." });

        var result = await _searchService.SearchAsync(q, type, limit, offset);
        return Ok(result);
    }
}
