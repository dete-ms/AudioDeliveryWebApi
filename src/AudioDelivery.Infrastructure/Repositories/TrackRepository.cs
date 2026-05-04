using AudioDelivery.Application.Common.Extensions;
using AudioDelivery.Application.Common.Interfaces;
using AudioDelivery.Application.Common.Models;
using AudioDelivery.Application.Search.DTOs;
using AudioDelivery.Application.Tracks.DTOs;
using AudioDelivery.Infrastructure.Data;
using AudioDelivery.Domain.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AudioDelivery.Infrastructure.Repositories;

/// <summary>
/// Track-specific repository implementation.
/// </summary>
public class TrackRepository : Repository<Track>, ITrackRepository
{
    private readonly IUriGenerationService _uriGenerationService;
    private readonly IHrefGenerationService _hrefGenerationService;

    public TrackRepository(
        AppDbContext context, 
        IMapper mapper,
        IUriGenerationService uriGenerationService,
        IHrefGenerationService hrefGenerationService) : base(context, mapper) 
    {
        _uriGenerationService = uriGenerationService;
        _hrefGenerationService = hrefGenerationService;
    }

    public async Task<TrackDto> CreateTrackAsync(CreateTrackRequest createTrackRequest, CancellationToken cancellationToken = default)
    {
        var track = _mapper.Map<CreateTrackRequest, Track>(createTrackRequest);

        if (track == null)
        {
            throw new InvalidOperationException($"Failed to map {nameof(CreateTrackRequest)} to {nameof(Track)}.");
        }

        track.Id = Guid.NewGuid();
        track.Uri = _uriGenerationService.GenerateUri(Domain.Enums.EntityType.Track, track.Id);

        await base.AddAsync(track, cancellationToken);

        return _mapper.Map<TrackDto>(track);
    }

    public async Task<IList<TrackDto>> CreateSeveralTracks(
        IEnumerable<CreateTrackRequest> createTrackRequests, 
        Guid albumId,
        IList<Guid> artistIds,
        CancellationToken cancellationToken = default)
    {
        var createdTracks = new List<TrackDto>();

        foreach (var track in createTrackRequests)
        {
            if (track.AlbumId == null)
            {
                track.AlbumId = albumId;
            }

            if (track.ArtistIds == null || track.ArtistIds.Count == 0)
            {
                track.ArtistIds = artistIds;
            }

            createdTracks.Add(await this.CreateTrackAsync(track, cancellationToken));
        }

        return createdTracks;
    }

    public Task<PaginatedResult<TrackDto>> GetTracksInPlaylistAsync(Guid playlistId, int offset = 0, int limit = 50, CancellationToken cancellationToken = default)
    {
        return _context.PlaylistTracks
            .Where(pt => pt.PlaylistId == playlistId)
            .OrderByDescending(pt => pt.Position)
            .Select(pt => pt.Track)
            .ProjectTo<TrackDto>(_mapper.ConfigurationProvider)
            .ToPaginatedResultAsync(offset, limit, 
                _hrefGenerationService.GeneratePaginatedHref(Domain.Enums.EntityType.Track, offset, limit), cancellationToken);
    }

    public async Task<TrackDto?> UpdateTrackAsync(Guid id, UpdateTrackRequest updateTrackRequest, CancellationToken cancellationToken = default)
    {
        var oldTrack = await _dbSet
                .Include(a => a.Artists)
                .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

        if (oldTrack == null)
        {
            throw new KeyNotFoundException($"Track with ID {id} not found.");
        }

        var newTrack = _mapper.Map(updateTrackRequest, oldTrack);

        if (updateTrackRequest.ArtistIds != null && updateTrackRequest.ArtistIds.Count > 0)
        {
            newTrack.Artists = await _context.Artists
                .Where(a => updateTrackRequest.ArtistIds.Contains(a.Id))
                .ToListAsync(cancellationToken);
        }
            
        base.Update(newTrack);
        await base.SaveChangesAsync(cancellationToken);

        return _mapper.Map<TrackDto>(newTrack);
    }

    public async Task<bool> DeleteTrackAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var track = await _dbSet.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

        if (track == null) return false;

        base.Delete(track);

        return true;
    }

    public async Task<SearchResultDto> SearchAsync(string query, int limit = 50, int offset = 0, CancellationToken cancellationToken = default)
    {
        var searchQuery = base.Query()
            .Where(a => a.Name.Contains(query))
            .OrderBy(a => a.Name)
            .ProjectTo<TrackDto>(_mapper.ConfigurationProvider);

        var searchDto = new SearchResultDto
        {
            Tracks = await searchQuery.ToPaginatedResultAsync(limit, offset,
                    _hrefGenerationService.GeneratePaginatedHref(Domain.Enums.EntityType.Track, limit, offset), cancellationToken)
        };

        return searchDto;
    }
}
