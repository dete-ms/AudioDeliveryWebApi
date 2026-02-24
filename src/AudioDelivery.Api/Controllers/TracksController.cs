using AudioDelivery.Application.Tracks;
using AudioDelivery.Application.Tracks.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AudioDelivery.Api.Controllers;

/// <summary>
/// Tracks API – mirrors Spotify's /tracks and /audio-features endpoints.
///
/// Endpoints:
///   GET /api/v1/tracks/{id}               → Get a track
///   GET /api/v1/tracks?ids=...            → Get several tracks
///   GET /api/v1/audio-features/{id}       → Get audio features for a track
///   GET /api/v1/audio-features?ids=...    → Get audio features for several tracks
///
/// See: https://developer.spotify.com/documentation/web-api/reference/get-track
/// </summary>
[ApiController]
[Route("api/v1")]
public class TracksController : ControllerBase
{
    private readonly ITrackService _trackService;

    public TracksController(ITrackService trackService)
    {
        _trackService = trackService;
    }

    /// <summary>
    /// Get catalog info for a single track.
    /// </summary>
    [HttpGet("tracks/{id:guid}")]
    [ProducesResponseType(typeof(TrackDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTrack(Guid id, [FromQuery] string? market = null)
    {
        var result = await _trackService.GetTrackAsync(id, market);
        if (result is null) return NotFound();
        return Ok(result);
    }

    /// <summary>
    /// Get catalog info for several tracks.
    /// </summary>
    [HttpGet("tracks")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSeveralTracks([FromQuery] string ids, [FromQuery] string? market = null)
    {
        var guidList = ids.Split(',').Select(s => Guid.Parse(s.Trim())).ToList();
        var result = await _trackService.GetSeveralTracksAsync(guidList, market);
        return Ok(new { tracks = result });
    }

    /// <summary>
    /// Get audio features for a single track.
    /// </summary>
    [HttpGet("audio-features/{id:guid}")]
    [ProducesResponseType(typeof(AudioFeaturesDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAudioFeatures(Guid id)
    {
        var result = await _trackService.GetAudioFeaturesAsync(id);
        if (result is null) return NotFound();
        return Ok(result);
    }

    /// <summary>
    /// Get audio features for several tracks.
    /// </summary>
    [HttpGet("audio-features")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSeveralAudioFeatures([FromQuery] string ids)
    {
        var guidList = ids.Split(',').Select(s => Guid.Parse(s.Trim())).ToList();
        var result = await _trackService.GetSeveralAudioFeaturesAsync(guidList);
        return Ok(new { audio_features = result });
    }
}
