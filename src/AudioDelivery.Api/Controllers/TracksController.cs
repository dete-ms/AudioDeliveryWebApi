using AudioDelivery.Application.Tracks.DTOs;
using AudioDelivery.Application.Tracks;
using Microsoft.AspNetCore.Mvc;

namespace AudioDelivery.Api.Controllers;

/// <summary>
/// Tracks API
/// </summary>
[ApiController]
[Route("api/v1/tracks")]
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
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(TrackDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTrack(Guid id)
    {
        var result = await _trackService.GetTrackAsync(id);

        if (result is null) 
            return NotFound();

        return Ok(result);
    }

    /// <summary>
    /// Get catalog info for several tracks.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSeveralTracks([FromQuery] string ids)
    {
        var guidList = ids.Split(',').Select(s => Guid.Parse(s.Trim())).ToList();

        var result = await _trackService.GetSeveralTracksAsync(guidList);

        return Ok(new { tracks = result });
    }

    [HttpPatch("{id:guid}")]
    [ProducesResponseType(typeof(TrackDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateTrack(Guid id, [FromBody] UpdateTrackRequest request)
    {
        if (request == null)
            return BadRequest(new { error = "Request body cannot be null." });

        var updatedTrack = await _trackService.UpdateTrackAsync(id, request);

        if (updatedTrack == null)
            return NotFound(new { error = $"Track with ID {id} not found." });

        return Ok(updatedTrack);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTrack(Guid id)
    {
        var success = await _trackService.DeleteTrackAsync(id);

        if (!success)
            return NotFound(new { error = $"Track with ID {id} not found." });

        return NoContent();
    }
}
