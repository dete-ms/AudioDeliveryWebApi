using AudioDelivery.Application.Artists.DTOs;
using AudioDelivery.Application.Common.Extensions;
using AudioDelivery.Application.Common.Interfaces;
using AudioDelivery.Application.Common.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace AudioDelivery.Application.Artists;

/// <summary>
/// Provides methods for retrieving artist information and related artists from the underlying data store.
/// </summary>
/// <remarks>The ArtistService is responsible for accessing artist data and exposing operations to fetch
/// individual artists, multiple artists, and related artists. All methods are asynchronous and support cancellation via
/// a CancellationToken. This service is typically used in application layers to abstract data access and business logic
/// related to artists.</remarks>
public class ArtistService : IArtistService
{
    private readonly IArtistRepository _repository;
    private readonly IMapper _mapper;

    public ArtistService(
        IArtistRepository artistRepository,
        IMapper mapper)
    {
        _repository = artistRepository;
        _mapper = mapper;
    }

    public Task<ArtistDto?> CreateArtist(CreateArtistRequest createArtistRequest)
    {
        return _repository.CreateArtistAsync(createArtistRequest);
    }

    public Task<ArtistDto?> GetArtistAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _repository.Query()
            .Where(a => a.Id == id)
            .ProjectTo<ArtistDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<PaginatedResult<ArtistDto>> GetSeveralArtistsAsync(
        IEnumerable<Guid> ids, 
        int offset = 0,
        int limit = 50,
        CancellationToken cancellationToken = default)
    {
        return _repository.Query()
            .Where(a => ids.Contains(a.Id))
            .ProjectTo<ArtistDto>(_mapper.ConfigurationProvider)
            .ToPaginatedResultAsync(offset, limit, this.GetHref(offset, limit), cancellationToken);
    }

    public Task<PaginatedResult<ArtistDto>> GetRelatedArtistsAsync(
        Guid artistId, 
        int offset = 0,
        int limit = 50, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    // update
    // delete

    private string GetHref(int offset, int limit) => $"/api/v1/artists/?offset={offset}&limit={limit}";
}
