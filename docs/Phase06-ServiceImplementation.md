# Phase 6 – Service Implementation

> **Status:** 🔲 To Do

## Overview

This is the largest phase. You'll implement all the service methods that currently throw `NotImplementedException`. Each method follows the same pattern:

1. **Query** the database via repositories
2. **Map** entities to DTOs
3. **Return** the DTO (or paginated result)

## Prerequisites

- Phase 5 complete (database created with seed data)
- Entity configurations implemented (Phase 3)

## 6.1 Implementation Pattern

Every service method follows this workflow:

```csharp
public async Task<AlbumDto?> GetAlbumAsync(Guid id)
{
    // Step 1: Query the database
    var album = await _context.Albums
        .Include(a => a.Artists)
        .Include(a => a.Images)
        .Include(a => a.Tracks)
        .FirstOrDefaultAsync(a => a.Id == id);

    if (album is null) return null;

    // Step 2: Map entity to DTO
    return MapToAlbumDto(album);
}

private static AlbumDto MapToAlbumDto(Album album)
{
    return new AlbumDto
    {
        Id = album.Id,
        Name = album.Name,
        AlbumType = album.AlbumType.ToString().ToLowerInvariant(),
        TotalTracks = album.TotalTracks,
        ReleaseDate = album.ReleaseDate,
        ReleaseDatePrecision = album.ReleaseDatePrecision.ToString().ToLowerInvariant(),
        Label = album.Label,
        Popularity = album.Popularity,
        ExternalUrl = album.ExternalUrl,
        Uri = album.Uri,
        Artists = album.Artists.Select(a => new ArtistSummaryDto
        {
            Id = a.Id,
            Name = a.Name,
            ExternalUrl = a.ExternalUrl,
            Uri = a.Uri
        }).ToList(),
        Images = album.Images.Select(i => new ImageDto
        {
            Url = i.Url,
            Height = i.Height,
            Width = i.Width
        }).ToList()
    };
}
```

## 6.2 Choosing Repository vs DbContext Directly

You have two options for data access in services:

### Option A: Use Domain-Specific Repositories

```csharp
public class AlbumService : IAlbumService
{
    private readonly IAlbumRepository _albumRepository;

    public AlbumService(IAlbumRepository albumRepository)
    {
        _albumRepository = albumRepository;
    }

    public async Task<AlbumDto?> GetAlbumAsync(Guid id)
    {
        var album = await _albumRepository.GetAlbumWithDetailsAsync(id);
        // ...
    }
}
```

**Pros:** Cleaner services, reusable queries, testable
**Cons:** More files, more abstraction

### Option B: Inject DbContext Directly

```csharp
public class AlbumService : IAlbumService
{
    private readonly AppDbContext _context;

    public AlbumService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<AlbumDto?> GetAlbumAsync(Guid id)
    {
        var album = await _context.Albums
            .Include(a => a.Artists)
            .FirstOrDefaultAsync(a => a.Id == id);
        // ...
    }
}
```

**Pros:** Simpler, fewer files, full EF Core API access
**Cons:** Harder to unit test, queries scattered across services

**Recommendation:** Start with Option B for simplicity. Move complex/reusable queries to repositories later.

## 6.3 Service Implementation Checklist

### Albums (`AlbumService.cs`)

- [ ] `GetAlbumAsync` – Fetch album with includes. Map to `AlbumDto`.
- [ ] `GetSeveralAlbumsAsync` – Fetch multiple albums by ID list. Return list of `AlbumDto`.
- [ ] `GetAlbumTracksAsync` – Query tracks for an album with pagination. Return `PaginatedResult<TrackDto>`.
- [ ] `GetNewReleasesAsync` – Query albums ordered by release date desc. Return `PaginatedResult<AlbumSummaryDto>`.

### Artists (`ArtistService.cs`)

- [ ] `GetArtistAsync` – Fetch artist with genres and images.
- [ ] `GetSeveralArtistsAsync` – Fetch multiple artists.
- [ ] `GetArtistAlbumsAsync` – Query albums for an artist with pagination.
- [ ] `GetArtistTopTracksAsync` – Query tracks by artist, ordered by popularity desc, limit 10.
- [ ] `GetRelatedArtistsAsync` – Query artists sharing genres with the given artist.

### Tracks (`TrackService.cs`)

- [ ] `GetTrackAsync` – Fetch track with album and artists.
- [ ] `GetSeveralTracksAsync` – Fetch multiple tracks.
- [ ] `GetAudioFeaturesAsync` – Fetch audio features for a track.
- [ ] `GetSeveralAudioFeaturesAsync` – Fetch audio features for multiple tracks.

### Playlists (`PlaylistService.cs`)

- [ ] `GetPlaylistAsync` – Fetch playlist with owner, tracks, and images.
- [ ] `UpdatePlaylistAsync` – Update name, description, public flag.
- [ ] `GetPlaylistTracksAsync` – Paginated tracks for a playlist.
- [ ] `AddItemsToPlaylistAsync` – Add tracks to playlist, return snapshot ID.
- [ ] `GetUserPlaylistsAsync` – Paginated playlists for a user.
- [ ] `CreatePlaylistAsync` – Create a new playlist for a user.

### Search (`SearchService.cs`)

- [ ] `SearchAsync` – Multi-type search across albums, artists, tracks, playlists.

### Users (`UserService.cs`)

- [ ] `GetCurrentUserAsync` – Fetch user profile with full details.
- [ ] `GetUserAsync` – Fetch public user profile.

### Genres (`GenreService.cs`)

- [ ] `GetAvailableGenreSeedsAsync` – Return all genre names.

### Library (`LibraryService.cs`)

- [ ] `SaveItemsAsync` – Save items by Spotify URI to user library.
- [ ] `RemoveItemsAsync` – Remove items by Spotify URI from user library.
- [ ] `CheckItemsAsync` – Return ordered bool array for each URI.

### Categories (`CategoryService.cs`)

- [ ] `GetCategoriesAsync` – Paginated categories.
- [ ] `GetCategoryAsync` – Single category with images.
- [ ] `GetCategoryPlaylistsAsync` – Playlists tagged with a category.

## 6.4 EF Core Query Tips

### Loading Related Data

```csharp
// Eager loading (loads in one query with JOINs)
var album = await _context.Albums
    .Include(a => a.Artists)
    .Include(a => a.Images)
    .FirstOrDefaultAsync(a => a.Id == id);

// Nested includes
var playlist = await _context.Playlists
    .Include(p => p.PlaylistTracks)
        .ThenInclude(pt => pt.Track)
            .ThenInclude(t => t.Artists)
    .FirstOrDefaultAsync(p => p.Id == id);
```

### Pagination

```csharp
var query = _context.Albums
    .OrderByDescending(a => a.ReleaseDate)
    .AsQueryable();

var total = await query.CountAsync();
var items = await query
    .Skip(offset)
    .Take(limit)
    .ToListAsync();

return new PaginatedResult<AlbumSummaryDto>
{
    Items = items.Select(MapToSummaryDto).ToList(),
    Total = total,
    Offset = offset,
    Limit = limit,
    Next = offset + limit < total ? $"...?offset={offset + limit}&limit={limit}" : null,
    Previous = offset > 0 ? $"...?offset={Math.Max(0, offset - limit)}&limit={limit}" : null
};
```

### Search

```csharp
// Case-insensitive search (SQL Server default collation is CI)
var albums = await _context.Albums
    .Where(a => a.Name.Contains(query))
    .Take(limit)
    .ToListAsync();
```

## Verify

After implementing each service:

```bash
dotnet build AudioDeliveryWebApi.sln
dotnet run --project src/AudioDelivery.Api

# Test endpoints via Swagger UI or curl:
curl https://localhost:5001/api/v1/genres/seeds
curl https://localhost:5001/api/v1/categories
```

## Next Phase

→ [Phase 7: Validation & Error Handling](Phase07-ValidationErrorHandling.md)
