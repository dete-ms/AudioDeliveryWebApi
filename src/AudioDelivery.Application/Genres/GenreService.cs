using AudioDelivery.Application.Genres.DTOs;
using AudioDelivery.Infrastructure.Repositories;

namespace AudioDelivery.Application.Genres;

/// <summary>
/// Genre service implementation.
/// </summary>
public class GenreService : IGenreService
{
    private readonly IGenreRepository _genreRepository;

    public GenreService(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }

    public async Task<IReadOnlyList<GenreDto>> GetAvailableGenreSeedsAsync(CancellationToken cancellationToken = default)
    {
        // TODO: Implement – fetch all genres, map to GenreDto
        throw new NotImplementedException("Implement in Phase 6");
    }
}
