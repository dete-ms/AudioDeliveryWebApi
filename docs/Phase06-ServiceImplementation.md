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

Now that AutoMapper profiles are set up, use `ProjectTo<TDto>()` instead of `.Include()` + manual mapping. This is simpler **and** faster — AutoMapper translates your profile into a SQL `SELECT` that fetches only the columns your DTO needs, with JOINs generated automatically from your navigation property mappings. No `Include()` calls, no in-memory mapping loop, no hand-written `MapToDto` private methods.

### Prerequisite: Fix AlbumProfile before using ProjectTo

> **Action required in `AlbumProfile.cs` and the two Album DTOs before implementing any service.**
>
> `ProjectTo<TDto>()` runs entirely at the SQL level. Any `ForMember` config that calls a C# method EF Core can't translate to SQL will throw a runtime exception. `AlbumProfile` currently has two such configs:
>
> ```csharp
> opt => opt.MapFrom(src => src.AlbumType.ToString().ToLowerInvariant())         // ⚠️ Not SQL-translatable
> opt => opt.MapFrom(src => src.ReleaseDatePrecision.ToString().ToLowerInvariant()) // ⚠️ Not SQL-translatable
> ```
>
> **To fix:**
>
> **Step 1 — Change the DTO property types** in `AlbumDto.cs` and `AlbumSummaryDto.cs`:
> ```csharp
> // Before
> public string AlbumType { get; set; } = string.Empty;
> public string ReleaseDatePrecision { get; set; } = string.Empty;
>
> // After
> public AlbumType AlbumType { get; set; }               // using AudioDelivery.Domain.Enums;
> public ReleaseDatePrecision ReleaseDatePrecision { get; set; }
> ```
>
> **Step 2 — Remove the two broken `ForMember` configs** from `AlbumProfile`. AutoMapper maps `enum → enum` with the same name automatically — no `ForMember` needed. Keep the `TotalTracks` one.
>
> **Step 3 — Add `JsonStringEnumConverter` in `Program.cs`** so the JSON responses still serialize enum values as lowercase strings:
> ```csharp
> builder.Services.AddControllers()
>     .AddJsonOptions(o =>
>     {
>         o.JsonSerializerOptions.Converters.Add(
>             new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
>     });
> ```
> Add `using System.Text.Json.Serialization;` at the top of `Program.cs`. This formats `AlbumType.Compilation` as `"compilation"` in JSON — matching the original string behaviour.

### The ProjectTo pattern

Every read service method follows this workflow:

```csharp
// Single entity
public async Task<AlbumDto?> GetAlbumAsync(Guid id, CancellationToken ct = default)
{
    return await _context.Albums
        .Where(a => a.Id == id)
        .ProjectTo<AlbumDto>(_mapper.ConfigurationProvider)
        .FirstOrDefaultAsync(ct);
    // Returns null automatically when not found — no null-check needed.
    // AutoMapper generates the JOINs for Artists, Images, Tracks from the profile.
    // No Include() calls required.
}

// Multiple entities by ID list
public async Task<IReadOnlyList<AlbumDto>> GetSeveralAlbumsAsync(
    IEnumerable<Guid> ids, CancellationToken ct = default)
{
    return await _context.Albums
        .Where(a => ids.Contains(a.Id))
        .ProjectTo<AlbumDto>(_mapper.ConfigurationProvider)
        .ToListAsync(ct);
}
```

**Why no `Include()`?** AutoMapper reads your profile at startup and builds an expression tree that translates directly to SQL. Navigation properties referenced in the profile (e.g. `Artists`, `Images`) become `LEFT JOIN` clauses in the generated query. EF Core never loads full entity graphs into memory.

## 6.2 Service Constructor Pattern

All services should inject **`AppDbContext`** and **`IMapper`**. Here is the standard constructor you will write for every read service:

```csharp
public class AlbumService : IAlbumService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public AlbumService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // Read: ProjectTo handles JOINs and mapping in one SQL query
    public async Task<AlbumDto?> GetAlbumAsync(Guid id, CancellationToken ct = default)
    {
        return await _context.Albums
            .Where(a => a.Id == id)
            .ProjectTo<AlbumDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(ct);
    }

    // Write: fetch a tracked entity, modify it, save
    public async Task<bool> UpdateAlbumAsync(Guid id, ...)
    {
        var album = await _context.Albums.FindAsync(id);  // tracked
        if (album is null) return false;
        album.Name = ...;
        await _context.SaveChangesAsync();
        return true;
    }
}
```

**Why inject `AppDbContext` instead of the repositories?**

`ProjectTo<TDto>()` is an extension method on `IQueryable<T>`. It needs direct access to a `DbSet<T>` (which is an `IQueryable<T>`) to build its SQL expression. The existing repositories return `Task<T?>` or `Task<IReadOnlyList<T>>` — materialized results, not queryables — so `ProjectTo` can't be called through them.

| | Repositories (`IAlbumRepository`) | DbContext directly |
|---|---|---|
| ProjectTo support | ✗ (returns materialized results) | ✓ (DbSet is IQueryable) |
| Write operations | ✓ | ✓ (`FindAsync` + `SaveChangesAsync`) |
| Unit-testable reads | ✓ (mockable interface) | Harder (requires in-memory DB) |
| Phase 6 scope | Unnecessary for reads | **Use this** |

Repositories remain in the project for future testability and write operations, but for reads in Phase 6, inject `AppDbContext` directly.

**Updating the service stubs:** The existing stubs inject `IAlbumRepository`, `IArtistRepository`, etc. Before implementing each service, update the constructor to inject `AppDbContext` and `IMapper` instead. You'll also need to update `ServiceCollectionExtensions.cs` — currently it registers services before the DI container knows about `AppDbContext`. Make sure `AddApplicationServices()` is called **after** `AddDbContext<AppDbContext>(...)` in `Program.cs`; that order is already correct since `AddDbContext` is called in the Infrastructure registration step.

## 6.3 Service Implementation Checklist

### Albums (`AlbumService.cs`)

> Constructor: `AppDbContext context, IMapper mapper`. Remember to fix `AlbumProfile` + DTOs first (section 6.1 prerequisite).

- [ ] `GetAlbumAsync` – `ProjectTo<AlbumDto>()` filtered by `Id`. Returns null if not found.
- [ ] `GetSeveralAlbumsAsync` – `Where(a => ids.Contains(a.Id))` + `ProjectTo<AlbumDto>()`.
- [ ] `GetAlbumTracksAsync` – `Where(t => t.AlbumId == albumId)` on `_context.Tracks` + `ProjectTo<TrackDto>()` + paginate.
- [ ] `GetNewReleasesAsync` – `OrderByDescending(a => a.ReleaseDate)` + `ProjectTo<AlbumSummaryDto>()` + paginate.

### Artists (`ArtistService.cs`)

> Constructor: `AppDbContext context, IMapper mapper`.

- [ ] `GetArtistAsync` – `ProjectTo<ArtistDto>()` filtered by `Id`.
- [ ] `GetSeveralArtistsAsync` – `Where(a => ids.Contains(a.Id))` + `ProjectTo<ArtistDto>()`.
- [ ] `GetArtistAlbumsAsync` – Join albums via `_context.Albums.Where(a => a.Artists.Any(ar => ar.Id == artistId))` + `ProjectTo<AlbumSummaryDto>()` + paginate.
- [ ] `GetArtistTopTracksAsync` – `Where(t => t.Artists.Any(a => a.Id == artistId)).OrderByDescending(t => t.Popularity).Take(10)` + `ProjectTo<TrackDto>()`.
- [ ] `GetRelatedArtistsAsync` – Find genres of the artist, then `Where(a => a.Genres.Any(g => genreIds.Contains(g.Id)) && a.Id != artistId)` + `ProjectTo<ArtistDto>()`.

### Tracks (`TrackService.cs`)

> Constructor: `AppDbContext context, IMapper mapper`.

- [ ] `GetTrackAsync` – `ProjectTo<TrackDto>()` filtered by `Id`.
- [ ] `GetSeveralTracksAsync` – `Where(t => ids.Contains(t.Id))` + `ProjectTo<TrackDto>()`.
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

### Loading Related Data with ProjectTo

You do **not** need `Include()` when using `ProjectTo<TDto>()`. AutoMapper generates the necessary JOINs from your profiles.

```csharp
// ✅ Correct — ProjectTo generates JOINs from the AlbumProfile mapping
var album = await _context.Albums
    .Where(a => a.Id == id)
    .ProjectTo<AlbumDto>(_mapper.ConfigurationProvider)
    .FirstOrDefaultAsync(ct);

// ✅ Correct — nested navigation properties (e.g. PlaylistTracks → Track → Artists)
// are handled by the PlaylistProfile + PlaylistTrackProfile + TrackProfile chain.
// AutoMapper resolves the full JOIN chain automatically.
var playlist = await _context.Playlists
    .Where(p => p.Id == id)
    .ProjectTo<PlaylistDto>(_mapper.ConfigurationProvider)
    .FirstOrDefaultAsync(ct);

// ❌ Avoid — Include() with ProjectTo is redundant and can cause unexpected behaviour.
// EF Core may ignore the Include() or double-join. Don't mix them.
```

> **When is `Include()` still valid?** Only when you need a fully tracked entity graph for a write operation (e.g. loading a playlist with its tracks in order to add/remove a track). For all read-only responses that map to a DTO, use `ProjectTo`.

### Pagination

```csharp
// Build the base query (no .ToList() yet — still IQueryable)
var query = _context.Albums
    .Where(...)                          // optional filters
    .OrderByDescending(a => a.ReleaseDate)
    .ProjectTo<AlbumSummaryDto>(_mapper.ConfigurationProvider);

// COUNT runs as a separate SELECT COUNT(*) — same filters, no data fetched
var total = await query.CountAsync(ct);

// Data fetch — single query with OFFSET/FETCH NEXT
var items = await query
    .Skip(offset)
    .Take(limit)
    .ToListAsync(ct);

return new PaginatedResult<AlbumSummaryDto>
{
    Items = items,      // already List<AlbumSummaryDto> — no .Select() needed
    Total = total,
    Offset = offset,
    Limit = limit,
    Next = offset + limit < total ? $"/api/v1/albums/new-releases?offset={offset + limit}&limit={limit}" : null,
    Previous = offset > 0 ? $"/api/v1/albums/new-releases?offset={Math.Max(0, offset - limit)}&limit={limit}" : null
};
```

> `ProjectTo` is called **before** `Skip`/`Take`, so the SQL includes both the column projection and the pagination in a single query. The `CountAsync()` call re-runs the query as `SELECT COUNT(*)` — no data is fetched twice.

### Search

```csharp
// Case-insensitive search (SQL Server default collation is CI)
// ProjectTo works the same way — add the Where() before ProjectTo
var albums = await _context.Albums
    .Where(a => a.Name.Contains(searchQuery))
    .ProjectTo<AlbumSummaryDto>(_mapper.ConfigurationProvider)
    .Take(limit)
    .ToListAsync(ct);
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
