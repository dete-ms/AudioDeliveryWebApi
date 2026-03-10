using AudioDelivery.Application.Common.Interfaces;
using AudioDelivery.Application.Search.DTOs;

namespace AudioDelivery.Application.Search;

/// <summary>
/// Search service implementation.
///
/// TODO: Implement SearchAsync by:
///   1. Parse the "types" string into individual types
///   2. For each requested type, query the corresponding repository with LIKE/Contains
///   3. Return paginated results for each type
///
/// TIP: Use IQueryable.Where(x => x.Name.Contains(query)) for basic search.
///      Later, consider full-text search for better performance.
/// </summary>
public class SearchService : ISearchService
{
    private readonly IAlbumRepository _albumRepository;
    private readonly IArtistRepository _artistRepository;
    private readonly ITrackRepository _trackRepository;
    private readonly IPlaylistRepository _playlistRepository;

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
    }

    public async Task<SearchResultDto> SearchAsync(string query, string types, int limit = 50, int offset = 0, CancellationToken cancellationToken = default)
    {
        // TODO: Implement search logic
        // 1. Split types by comma: var typeList = types.Split(',').Select(t => t.Trim().ToLower())
        // 2. For each type in typeList, run a search query:
        //    - "album" → search albums by name
        //    - "artist" → search artists by name
        //    - "track" → search tracks by name
        //    - "playlist" → search playlists by name
        // 3. Map results to DTOs and wrap in PaginatedResult<T>
        // 4. Populate only the requested types in SearchResultDto
        throw new NotImplementedException("Implement in Phase 6");
    }
}
