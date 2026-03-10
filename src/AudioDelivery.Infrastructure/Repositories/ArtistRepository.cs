using AudioDelivery.Application.Common.Interfaces;
using AudioDelivery.Application.Artists.DTOs;
using AudioDelivery.Domain.Entities;
using AudioDelivery.Infrastructure.Data;
using AutoMapper;

namespace AudioDelivery.Infrastructure.Repositories;

/// <summary>
/// Artist-specific repository implementation.
/// </summary>
public class ArtistRepository : Repository<Artist>, IArtistRepository
{
    public ArtistRepository(AppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<ArtistDto?> CreateArtistAsync(CreateArtistRequest createArtistRequest)
    {
        var artist = _mapper.Map<CreateArtistRequest, Artist>(createArtistRequest);

        if (artist == null)
        {
            throw new InvalidOperationException("Failed to map CreateArtistRequest to Artist.");
        }

        artist.Id = Guid.NewGuid();
        await base.AddAsync(artist);
        await base.SaveChangesAsync();

        return await base.GetByIdAsync<ArtistDto>(artist.Id);
    }
}
