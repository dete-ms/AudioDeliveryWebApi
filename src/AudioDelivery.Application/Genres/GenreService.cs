using AudioDelivery.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace AudioDelivery.Application.Genres;

/// <summary>
/// Genre service implementation.
/// </summary>
public class GenreService : IGenreService
{
    private readonly IGenreRepository _repository;
    private readonly IMapper _mapper;

    public GenreService(
        IGenreRepository genreRepository,
        IMapper mapper)
    {
        _repository = genreRepository;
        _mapper = mapper;
    }

    public Task<List<string>> GetAllGenreNamesAsync(CancellationToken cancellationToken = default)
    {
        return _repository.Query()
            .Select(g => g.Name)
            .ToListAsync(cancellationToken);
    }
}
