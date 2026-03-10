using AudioDelivery.Application.Common.Extensions;
using AudioDelivery.Application.Common.Interfaces;
using AudioDelivery.Application.Common.Models;
using AudioDelivery.Application.Albums.DTOs;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace AudioDelivery.Application.Albums;

/// <summary>
/// Album service implementation.
/// </summary>
public class AlbumService : IAlbumService
{
    private readonly IAlbumRepository _repository;
    private readonly IMapper _mapper;

    public AlbumService(
        IAlbumRepository albumRepository,
        IMapper mapper)
    {
        _repository = albumRepository;
        _mapper = mapper;
    }
    
    public Task<AlbumDto?> CreateAlbum(CreateAlbumRequest createAlbumRequest)
    {
        return _repository.CreateAlbum(createAlbumRequest);
    }

    public Task<AlbumDto?> GetAlbumAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _repository.Query()
            .Where(a => a.Id == id)
            .ProjectTo<AlbumDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<List<AlbumDto>> GetSeveralAlbumsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        return _repository.Query()
            .Where(a => ids.Contains(a.Id))
            .ProjectTo<AlbumDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }

    public Task<PaginatedResult<AlbumSummaryDto>> GetAlbumsByArtistAsync(Guid artistId, int offset = 0, int limit = 50, CancellationToken cancellationToken = default)
    {
        return _repository.Query()
            .Where(a => a.Artists.Any(ar => ar.Id == artistId))
            .OrderByDescending(a => a.ReleaseDate)
            .ProjectTo<AlbumSummaryDto>(_mapper.ConfigurationProvider)
            .ToPaginatedResultAsync(offset, limit, this.GetHref(offset, limit), cancellationToken);
    }

    public Task<PaginatedResult<AlbumSummaryDto>> GetNewReleasesAsync(int offset = 0, int limit = 50, string? country = null, CancellationToken cancellationToken = default)
    {
        return _repository.Query()
            .OrderByDescending(a => a.ReleaseDate)
            .ProjectTo<AlbumSummaryDto>(_mapper.ConfigurationProvider)
            .ToPaginatedResultAsync(offset, limit, this.GetHref(offset, limit), cancellationToken);
    }

    public Task<PaginatedResult<AlbumSummaryDto>> SearchAlbumsAsync(string query, int offset = 0, int limit = 50, CancellationToken cancellationToken = default)
    {
        return _repository.Query()
            .Where(a => a.Name.Contains(query))
            .ProjectTo<AlbumSummaryDto>(_mapper.ConfigurationProvider)
            .ToPaginatedResultAsync(offset, limit, this.GetHref(offset, limit), cancellationToken);
    }

    public Task<PaginatedResult<AlbumSummaryDto>> GetSavedAlbums(Guid userId, int offset = 0, int limit = 50, CancellationToken cancellationToken = default)
    {
        return _repository.Query()
            .Where(a => a.SavedByUsers.Any(u => u.Id == userId))
            .ProjectTo<AlbumSummaryDto>(_mapper.ConfigurationProvider)
            .ToPaginatedResultAsync(offset, limit, this.GetHref(offset, limit), cancellationToken);
    }

    public Task<AlbumDto?> UpdateAlbum(Guid id, UpdateAlbumRequest updateAlbumRequest)
    {
        return _repository.UpdateAlbum(id, updateAlbumRequest);
    }

    // delete
    public Task<bool> DeleteAlbum(Guid id)
    {
        return _repository.DeleteAlbum(id);
    }

    private string GetHref(int offset, int limit) => $"/api/v1/albums?offset={offset}&limit={limit}";
}
