using AudioDelivery.Application.Genres;
using AudioDelivery.Application.Genres.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AudioDelivery.Api.Controllers;

/// <summary>
/// Genres API – mirrors Spotify's /recommendations/available-genre-seeds endpoint.
///
/// Endpoints:
///   GET /api/v1/genres/seeds → Get available genre seeds
///
/// See: https://developer.spotify.com/documentation/web-api/reference/get-recommendation-genres
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
        var result = await _genreService.GetAllGenresAsync();
        return Ok(new { genres = result });
    }
}
