using AudioDelivery.Application.Genres;
using Microsoft.AspNetCore.Mvc;

namespace AudioDelivery.Api.Controllers;

/// <summary>
/// Genres API – mirrors Spotify's /recommendations/available-genre-seeds endpoint.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class GenresController : ControllerBase
{
    private readonly IGenreService _genreService;

    public GenresController(IGenreService genreService)
    {
        _genreService = genreService;
    }

    /// <summary>
    /// Retrieve a list of available genres seed parameter values for recommendations.
    /// </summary>
    [HttpGet("seeds")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAvailableGenreSeeds()
    {
        var result = await _genreService.GetAllGenreNamesAsync();
        return Ok(new { genres = result });
    }
}
