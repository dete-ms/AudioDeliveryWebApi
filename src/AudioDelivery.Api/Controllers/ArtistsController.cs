using AudioDelivery.Application.Artists;
using AudioDelivery.Application.Artists.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AudioDelivery.Api.Controllers;

/// <summary>
/// Artists API – mirrors Spotify's /artists endpoints.
///
/// Endpoints:
///   GET /api/v1/artists/{id}                 → Get an artist
///   GET /api/v1/artists?ids=...              → Get several artists
///   GET /api/v1/artists/{id}/albums          → Get an artist's albums
///   GET /api/v1/artists/{id}/top-tracks      → Get an artist's top tracks
///   GET /api/v1/artists/{id}/related-artists → Get related artists
///
/// See: https://developer.spotify.com/documentation/web-api/reference/get-an-artist
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class ArtistsController : ControllerBase
{
    private readonly IArtistService _artistService;

    public ArtistsController(IArtistService artistService)
    {
        _artistService = artistService;
    }

    /// <summary>
    /// Get catalog info for a single artist.
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ArtistDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetArtist(Guid id)
    {
        var result = await _artistService.GetArtistAsync(id);
        if (result is null) return NotFound();
        return Ok(result);
    }

    /// <summary>
    /// Get catalog info for several artists.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSeveralArtists([FromQuery] string ids)
    {
        var guidList = ids.Split(',').Select(s => Guid.Parse(s.Trim())).ToList();
        var result = await _artistService.GetSeveralArtistsAsync(guidList);
        return Ok(new { artists = result });
    }

    /// <summary>
    /// Get an artist's albums.
    /// </summary>
    [HttpGet("{id:guid}/albums")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetArtistAlbums(Guid id, [FromQuery] int offset = 0, [FromQuery] int limit = 20)
    {
        var result = await _artistService.GetArtistAlbumsAsync(id, offset, limit);
        return Ok(result);
    }

    /// <summary>
    /// Get an artist's top tracks.
    /// </summary>
    [HttpGet("{id:guid}/top-tracks")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetArtistTopTracks(Guid id)
    {
        var result = await _artistService.GetArtistTopTracksAsync(id);
        return Ok(new { tracks = result });
    }

    /// <summary>
    /// Get artists related to a given artist.
    /// </summary>
    [HttpGet("{id:guid}/related-artists")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRelatedArtists(Guid id)
    {
        var result = await _artistService.GetRelatedArtistsAsync(id);
        return Ok(new { artists = result });
    }
}
