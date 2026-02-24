using AudioDelivery.Application.Genres.DTOs;

namespace AudioDelivery.Application.Genres;

/// <summary>
/// Service interface for Genre operations.
/// </summary>
public interface IGenreService
{
    /// <summary>
    /// GET /recommendations/available-genre-seeds – List available genre seeds.
    /// </summary>
    Task<IReadOnlyList<GenreDto>> GetAvailableGenreSeedsAsync(CancellationToken cancellationToken = default);
}
