using AudioDelivery.Application.Common.Interfaces;
using AudioDelivery.Application.Albums.DTOs;
using AudioDelivery.Infrastructure.Data;
using AudioDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace AudioDelivery.Infrastructure.Repositories;

/// <summary>
/// Album-specific repository implementation.
/// </summary>
public class AlbumRepository : Repository<Album>, IAlbumRepository
{
    public AlbumRepository(AppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<AlbumDto?> CreateAlbum(CreateAlbumRequest createAlbumRequest)
    {
        // TODO: Create images from list
        throw new NotImplementedException();
        var album = _mapper.Map<CreateAlbumRequest, Album>(createAlbumRequest);

        if (album == null)
        {
            throw new InvalidOperationException("Failed to map CreateAlbumRequest to Album.");
        }

        album.Id = Guid.NewGuid();

        var artists = await _context.Artists
            .Where(a => createAlbumRequest.ArtistIds.Contains(a.Id))
            .ToListAsync();

        album.Artists = artists;

        await base.AddAsync(album);
        await base.SaveChangesAsync();

        return await base.GetByIdAsync<AlbumDto>(album.Id);
    }

    public async Task<AlbumDto?> UpdateAlbum(Guid id, UpdateAlbumRequest updateAlbumRequest)
    {
        var album = await base.GetByIdAsync(id);

        if (album == null)
        {
            throw new KeyNotFoundException($"Album with ID {id} not found.");
        }

        var newAlbum = _mapper.Map(updateAlbumRequest, album);

        if (updateAlbumRequest.ArtistIds != null && updateAlbumRequest.ArtistIds.Count > 0)
        {
            newAlbum.Artists = await _context.Artists
                .Where(a => updateAlbumRequest.ArtistIds.Contains(a.Id))
                .ToListAsync();
        }

        base.Update(newAlbum);
        await base.SaveChangesAsync();

        return await base.GetByIdAsync<AlbumDto>(id);
    }

    public async Task<bool> DeleteAlbum(Guid id)
    {
        var album = await base.GetByIdAsync(id);
        if (album == null)
        {
            return false;
        }

        base.Delete(album);
        await base.SaveChangesAsync();

        return true;
    }
}
