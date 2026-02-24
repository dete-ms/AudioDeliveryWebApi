using AudioDelivery.Application.Albums;
using AudioDelivery.Application.Albums.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AudioDelivery.Api.Controllers;

/// <summary>
/// Albums API – mirrors Spotify's /albums endpoints.
///
/// Endpoints:
///   GET    /api/v1/albums/{id}           → Get an album
///   GET    /api/v1/albums?ids=...        → Get several albums
///   GET    /api/v1/albums/{id}/tracks    → Get an album's tracks
///   GET    /api/v1/browse/new-releases   → Get new releases (routed here for organization)
///
/// See: https://developer.spotify.com/documentation/web-api/reference/get-an-album
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class AlbumsController : ControllerBase
{
    private readonly IAlbumService _albumService;

    public AlbumsController(IAlbumService albumService)
    {
        _albumService = albumService;
    }

    /// <summary>
    /// Get Spotify catalog information for a single album.
    /// </summary>
    /// <param name="id">The album ID.</param>
    /// <param name="market">Optional ISO 3166-1 alpha-2 country code.</param>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(AlbumDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAlbum(Guid id, [FromQuery] string? market = null)
    {
        // TODO: Call _albumService.GetAlbumAsync(id, market)
        //       Return Ok(result) if found, NotFound() if null
        var result = await _albumService.GetAlbumAsync(id, market);
        if (result is null) return NotFound();
        return Ok(result);
    }

    /// <summary>
    /// Get Spotify catalog information for multiple albums by their IDs.
    /// </summary>
    /// <param name="ids">Comma-separated list of album IDs (max 20).</param>
    /// <param name="market">Optional market filter.</param>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<AlbumDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSeveralAlbums([FromQuery] string ids, [FromQuery] string? market = null)
    {
        // TODO: Parse comma-separated ids string into Guid list
        //       Call _albumService.GetSeveralAlbumsAsync(guidList, market)
        var guidList = ids.Split(',').Select(s => Guid.Parse(s.Trim())).ToList();
        var result = await _albumService.GetSeveralAlbumsAsync(guidList, market);
        return Ok(new { albums = result });
    }

    /// <summary>
    /// Get Spotify catalog information about an album's tracks.
    /// </summary>
    [HttpGet("{id:guid}/tracks")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAlbumTracks(Guid id, [FromQuery] int offset = 0, [FromQuery] int limit = 20, [FromQuery] string? market = null)
    {
        var result = await _albumService.GetAlbumTracksAsync(id, offset, limit, market);
        return Ok(result);
    }

    /// <summary>
    /// Get a list of new album releases featured in Spotify.
    /// </summary>
    [HttpGet("/api/v1/browse/new-releases")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetNewReleases([FromQuery] int offset = 0, [FromQuery] int limit = 20, [FromQuery] string? country = null)
    {
        var result = await _albumService.GetNewReleasesAsync(offset, limit, country);
        return Ok(new { albums = result });
    }
}
