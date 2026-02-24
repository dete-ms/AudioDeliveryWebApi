using AudioDelivery.Application.Markets;
using Microsoft.AspNetCore.Mvc;

namespace AudioDelivery.Api.Controllers;

/// <summary>
/// Markets API – mirrors Spotify's /markets endpoint.
///
/// Endpoints:
///   GET /api/v1/markets → Get available markets
///
/// See: https://developer.spotify.com/documentation/web-api/reference/get-available-markets
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class MarketsController : ControllerBase
{
    private readonly IMarketService _marketService;

    public MarketsController(IMarketService marketService)
    {
        _marketService = marketService;
    }

    /// <summary>
    /// Get the list of markets where Spotify is available.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAvailableMarkets()
    {
        var result = await _marketService.GetAvailableMarketsAsync();
        return Ok(new { markets = result });
    }
}
