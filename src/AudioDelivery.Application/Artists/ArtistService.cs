using AudioDelivery.Application.Common.Extensions;
using AudioDelivery.Application.Common.Interfaces;
using AudioDelivery.Application.Common.Models;
using AudioDelivery.Application.Artists.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AudioDelivery.Application.Artists;

/// <inheritdoc />
public class ArtistService : IArtistService
{
    private readonly IArtistRepository _repository;
    private readonly IUserRepository _userRepository;
    private readonly IUserLibraryRepository _userLibraryRepository;
    private readonly IHrefGenerationService _hrefGenerationService;
    private readonly IMapper _mapper;

    public ArtistService(
        IArtistRepository artistRepository,
        IUserRepository userRepository,
        IUserLibraryRepository userLibraryRepository,
        IHrefGenerationService hrefGenerationService,
        IMapper mapper)
    {
        _repository = artistRepository;
        _userRepository = userRepository;
        _userLibraryRepository = userLibraryRepository;
        _hrefGenerationService = hrefGenerationService;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public Task<ArtistDto> CreateArtistAsync(CreateArtistRequest createArtistRequest, CancellationToken cancellationToken = default)
    {
        return _repository.CreateArtistAsync(createArtistRequest, cancellationToken);
    }

    /// <inheritdoc />
    public Task<ArtistDto?> GetArtistAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _repository.FindFirstAsync<ArtistDto>(a => a.Id == id);
    }

    /// <inheritdoc />
    public Task<PaginatedResult<ArtistDto>> GetSeveralArtistsAsync(
        IEnumerable<Guid> ids, 
        int offset = 0,
        int limit = 50,
        CancellationToken cancellationToken = default)
    {
        return _repository.Query()
            .Where(a => ids.Contains(a.Id))
            .ProjectTo<ArtistDto>(_mapper.ConfigurationProvider)
            .ToPaginatedResultAsync(offset, limit, 
                _hrefGenerationService.GeneratePaginatedHref(Domain.Enums.EntityType.Artist, offset, limit), cancellationToken);
    }

    /// <inheritdoc />
    public async Task<PaginatedResult<ArtistDto>> GetRelatedArtistsAsync(
        Guid artistId, 
        int offset = 0,
        int limit = 50, CancellationToken cancellationToken = default)
    {
        var artist = await _repository.Query()
            .Include(a => a.Genres)
            .FirstOrDefaultAsync(a => a.Id == artistId);

        if (artist == null)
        {
            return new PaginatedResult<ArtistDto>
            {
                Href = _hrefGenerationService.GeneratePaginatedHref(Domain.Enums.EntityType.Artist, offset, limit),
                Items = Array.Empty<ArtistDto>(),
                Limit = limit,
                Offset = offset,
                Total = 0
            };
        }

        return await _repository.Query()
            .Where(a => a.Genres.Any(g => artist.Genres.Contains(g)))
            .ProjectTo<ArtistDto>(_mapper.ConfigurationProvider)
            .ToPaginatedResultAsync(offset, limit, 
                _hrefGenerationService.GeneratePaginatedHref(Domain.Enums.EntityType.Artist, offset, limit), cancellationToken);
    }

    /// <inheritdoc />
    public Task<PaginatedResult<ArtistSummaryDto>> GetSavedArtistsAsync(Guid userId, int offset = 0, int limit = 50, CancellationToken cancellationToken = default)
    {
        return _userLibraryRepository.Query()
            .Where(ul => ul.UserId == userId && ul.Artist != null)
            .Select(ul => ul.Artist)
            .ProjectTo<ArtistSummaryDto>(_mapper.ConfigurationProvider)
            .ToPaginatedResultAsync(offset, limit,
                _hrefGenerationService.GeneratePaginatedHref(Domain.Enums.EntityType.Artist, offset, limit), cancellationToken);
    }

    /// <inheritdoc />
    public Task<PaginatedResult<ArtistSummaryDto>> GetFollowedArtistsAsync(Guid userId, int offset = 0, int limit = 50, CancellationToken cancellationToken = default)
    {
        return _userRepository.Query()
            .Where(u => u.Id == userId)
            .Include(u => u.FollowedArtists)
            .SelectMany(u => u.FollowedArtists)
            .ProjectTo<ArtistSummaryDto>(_mapper.ConfigurationProvider)
            .ToPaginatedResultAsync(offset, limit,
                _hrefGenerationService.GeneratePaginatedHref(Domain.Enums.EntityType.Artist, offset, limit), cancellationToken);
    }

    /// <inheritdoc />
    public Task<ArtistDto?> UpdateArtistAsync(Guid id, UpdateArtistRequest updateArtistRequest, CancellationToken cancellationToken = default)
    {
        return _repository.UpdateArtistAsync(id, updateArtistRequest, cancellationToken);
    }

    /// <inheritdoc />
    public Task<bool> DeleteArtistAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _repository.DeleteArtistAsync(id, cancellationToken);
    }
}
