using AudioDelivery.Application.Categories;
using AudioDelivery.Application.Categories.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AudioDelivery.Api.Controllers;

/// <summary>
/// Categories API – mirrors Spotify's /browse/categories endpoints.
///
/// Endpoints:
///   GET /api/v1/browse/categories             → Get several browse categories
///   GET /api/v1/browse/categories/{id}        → Get a single category
///   GET /api/v1/browse/categories/{id}/playlists → Get a category's playlists
///
/// See: https://developer.spotify.com/documentation/web-api/reference/get-categories
/// </summary>
[ApiController]
[Route("api/v1/browse/categories")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    /// <summary>
    /// Get a list of categories used to tag items in Spotify.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCategories(
        [FromQuery] string? country = null,
        [FromQuery] string? locale = null,
        [FromQuery] int offset = 0,
        [FromQuery] int limit = 20)
    {
        var result = await _categoryService.GetCategoriesAsync(country, locale, offset, limit);
        return Ok(new { categories = result });
    }

    /// <summary>
    /// Get a single category used to tag items in Spotify.
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCategory(Guid id, [FromQuery] string? country = null, [FromQuery] string? locale = null)
    {
        var result = await _categoryService.GetCategoryAsync(id, country, locale);
        if (result is null) return NotFound();
        return Ok(result);
    }

    /// <summary>
    /// Get a list of Spotify playlists tagged with a particular category.
    /// </summary>
    [HttpGet("{id:guid}/playlists")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCategoryPlaylists(Guid id, [FromQuery] string? country = null, [FromQuery] int offset = 0, [FromQuery] int limit = 20)
    {
        var result = await _categoryService.GetCategoryPlaylistsAsync(id, country, offset, limit);
        return Ok(new { playlists = result });
    }
}
