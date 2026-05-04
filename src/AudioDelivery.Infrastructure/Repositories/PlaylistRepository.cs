using AudioDelivery.Application.Common.Extensions;
using AudioDelivery.Application.Common.Interfaces;
using AudioDelivery.Application.Playlists.DTOs;
using AudioDelivery.Application.Search.DTOs;
using AudioDelivery.Infrastructure.Data;
using AudioDelivery.Domain.Entities;
using AudioDelivery.Domain.Events;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AudioDelivery.Infrastructure.Repositories;

/// <summary>
/// Playlist-specific repository implementation.
/// </summary>
public class PlaylistRepository : Repository<Playlist>, IPlaylistRepository
{
    private readonly IUriGenerationService _uriGenerationService;
    private readonly IHrefGenerationService _hrefGenerationService;

    public PlaylistRepository(
        AppDbContext context, 
        IMapper mapper,
        IUriGenerationService uriGenerationService,
        IHrefGenerationService hrefGenerationService) : base(context, mapper) 
    {
        _uriGenerationService = uriGenerationService;
        _hrefGenerationService = hrefGenerationService;
    }

    public async Task<Playlist> CreatePlaylistAsync(CreatePlaylistRequest createPlaylistRequest, IList<Image> coverArtInDifferentSizes, CancellationToken cancellationToken = default)
    {
        var playlist = _mapper.Map<Playlist>(createPlaylistRequest);

        if (playlist == null)
        {
            throw new InvalidOperationException($"Failed to map {nameof(CreatePlaylistRequest)} to {nameof(Playlist)}.");
        }

        playlist.Id = Guid.NewGuid();
        playlist.Uri = _uriGenerationService.GenerateUri(Domain.Enums.EntityType.Playlist, playlist.Id);
        playlist.Images = coverArtInDifferentSizes;

        await base.AddAsync(playlist);

        return playlist;
    }

    public async Task<PlaylistDto?> UpdatePlaylistAsync(Guid id, UpdatePlaylistRequest updatePlaylistRequest, IList<Image>? newCoverArtInDifferentSizes = null, CancellationToken cancellationToken = default)
    {
        var oldPlaylist = await base.QueryTracked()
            .Include(p => p.Images)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (oldPlaylist == null)
            return null;

        var updatedPlaylist = _mapper.Map(updatePlaylistRequest, oldPlaylist);

        if (newCoverArtInDifferentSizes != null && newCoverArtInDifferentSizes.Count > 0)
        {
            updatedPlaylist.Images = newCoverArtInDifferentSizes;
        }

        base.Update(updatedPlaylist);
        await base.SaveChangesAsync(cancellationToken);

        return _mapper.Map<PlaylistDto>(updatedPlaylist);
    }

    public async Task<PlaylistDto?> AddTracksToPlaylistAsync(Guid playlistId, AddItemsRequest addItemsRequest, CancellationToken cancellationToken = default)
    {
        var playlist = await base.QueryTracked()
            .Include(p => p.PlaylistTracks)
            .FirstOrDefaultAsync(p => p.Id == playlistId);

        if (playlist == null)
            return null;

        foreach (var trackId in addItemsRequest.Ids)
        {
            if (addItemsRequest.AllowDuplicates || !playlist.PlaylistTracks.Any(pt => pt.TrackId == trackId))
            {
                playlist.PlaylistTracks.Add(new PlaylistTrack
                {
                    PlaylistId = playlistId,
                    TrackId = trackId,
                    Position = addItemsRequest.Position ?? playlist.PlaylistTracks.Count
                });
            }
        }

        await base.SaveChangesAsync(cancellationToken);
        return _mapper.Map<PlaylistDto>(playlist);
    }

    public async Task<PlaylistDto?> RemoveTracksFromPlaylistAsync(Guid playlistId, RemoveItemsRequest request, CancellationToken cancellationToken = default)
    {
        var playlist = await base.QueryTracked()
            .Include(p => p.PlaylistTracks)
            .FirstOrDefaultAsync(p => p.Id == playlistId, cancellationToken);

        if (playlist == null)
            return null;

        var itemsToRemove = playlist.PlaylistTracks
            .Where(pt => request.Ids.Contains(pt.TrackId))
            .ToList();

        foreach (var item in itemsToRemove)
        {
            playlist.PlaylistTracks.Remove(item); // Tracked removal
        }

        var counter = 0;
        foreach (var playlistTrack in playlist.PlaylistTracks.OrderBy(pt => pt.Position))
        {
            playlistTrack.Position = counter++;
        }

        await base.SaveChangesAsync(cancellationToken);

        return _mapper.Map<PlaylistDto>(playlist);
    }

    public async Task<bool> DeletePlaylistAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var playlist = await base.QueryTracked()
            .Include(p => p.Images)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        if (playlist == null) return false;

        var playlistImages = playlist.Images;

        if (playlistImages.Any())
        {
            playlist.RaiseDomainEvent(new EntityDeletedEvent(
                playlistImages.Select(i => i.Id).ToList(),
                playlistImages.Select(i => i.Url).ToList()
            ));
        }

        // TODO: Check if the owner is the one requesting the deletion

        base.Delete(playlist);
        await base.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<SearchResultDto> SearchAsync(string query, int limit = 50, int offset = 0, CancellationToken cancellationToken = default)
    {
        var searchQuery = base.Query()
            .Where(a => a.Name.Contains(query))
            .OrderBy(a => a.Name)
            .ProjectTo<PlaylistSummaryDto>(_mapper.ConfigurationProvider);

        var searchDto = new SearchResultDto
        {
            Playlists = await searchQuery.ToPaginatedResultAsync(limit, offset,
                    _hrefGenerationService.GeneratePaginatedHref(Domain.Enums.EntityType.Playlist, limit, offset), cancellationToken)
        };

        return searchDto;
    }
}
