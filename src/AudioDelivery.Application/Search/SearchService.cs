using AudioDelivery.Application.Common.Interfaces;
using AudioDelivery.Application.Search.DTOs;
using AudioDelivery.Domain.Entities;

namespace AudioDelivery.Application.Search;

/// <summary>
/// Search service implementation.
/// </summary>
public class SearchService : ISearchService
{
    private readonly IAlbumRepository _albumRepository;
    private readonly IArtistRepository _artistRepository;
    private readonly ITrackRepository _trackRepository;
    private readonly IPlaylistRepository _playlistRepository;
    private readonly Dictionary<string, ISearchableEntityRepository> _repositoryMap;

    public SearchService(
        IAlbumRepository albumRepository,
        IArtistRepository artistRepository,
        ITrackRepository trackRepository,
        IPlaylistRepository playlistRepository)
    {
        _albumRepository = albumRepository;
        _artistRepository = artistRepository;
        _trackRepository = trackRepository;
        _playlistRepository = playlistRepository;
        _repositoryMap = new()
        {
            { nameof(Album).ToLower(), _albumRepository},
            { nameof(Artist).ToLower(), _artistRepository},
            { nameof(Track).ToLower(), _trackRepository},
            { nameof(Playlist).ToLower(), _playlistRepository},
        };
    }

    public async Task<SearchResultDto> SearchAsync(string query, string types, int limit = 50, int offset = 0, CancellationToken cancellationToken = default)
    {
        var typeList = types.Split(',').Select(t => t.Trim().ToLower());

        var result = new SearchResultDto();

        foreach (var type in typeList)
        {
            if (_repositoryMap.TryGetValue(type, out var repository))
            {
                var currentSearchResult = await repository.SearchAsync(query, limit, offset, cancellationToken);
                result = SearchResultDto.CopyFilled(currentSearchResult, result);
            }
        }

        return result;
    }
}
