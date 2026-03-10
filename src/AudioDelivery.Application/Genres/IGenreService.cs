using AudioDelivery.Application.Genres.DTOs;

namespace AudioDelivery.Application.Genres;

/// <summary>
/// Service interface for Genre operations.
/// </summary>
public interface IGenreService
{
    /// <summary>
    /// GET /recommendations/genres – List available genres.
    /// </summary>
    Task<List<GenreDto>> GetAllGenresAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// GET /recommendations/genre/{id} – Get genre.
    /// </summary>
    Task<GenreDto?> GetGenreAsync(Guid id, CancellationToken cancellationToken = default);
}
