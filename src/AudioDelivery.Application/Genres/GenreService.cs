using AudioDelivery.Application.Common.Interfaces;
using AudioDelivery.Application.Genres.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

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

    public Task<List<GenreDto>> GetAllGenresAsync(CancellationToken cancellationToken = default)
    {
        return _repository.Query()
            .ProjectTo<GenreDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }

    public Task<GenreDto?> GetGenreAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _repository.Query()
            .Where(g => g.Id == id)
            .ProjectTo<GenreDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
