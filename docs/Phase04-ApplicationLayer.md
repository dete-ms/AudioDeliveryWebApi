# Phase 4 – Application Layer

> **Status:** ✅ Complete – all DTOs, service interfaces, and stubbed implementations are in place.

## Overview

The Application layer contains **business logic**. It sits between the API controllers and the Infrastructure repositories, providing:

- **DTOs** (Data Transfer Objects) – shaped for API responses
- **Service Interfaces** – define what operations are available
- **Service Implementations** – orchestrate repository calls and map data (stubbed for now)

## 4.1 Why DTOs?

Domain entities contain:
- Database-specific properties (IDs, navigation properties, timestamps)
- Relationships that could cause circular serialization
- More data than the client needs

DTOs solve this by defining **exactly what the API returns**:

```csharp
// Entity (internal, rich model)
public class Album : BaseEntity
{
    public string Name { get; set; }
    public Guid? AlbumId { get; set; }          // FK – not useful to clients
    public ICollection<Track> Tracks { get; set; }  // Could cause circular ref
    public DateTime CreatedAt { get; set; }      // Internal metadata
}

// DTO (external, API response shape)
public class AlbumDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string AlbumType { get; set; }        // String, not enum
    public int TotalTracks { get; set; }
    public List<ArtistSummaryDto> Artists { get; set; }  // Simplified artist info
}
```

### DTO Naming Convention

| Suffix | Purpose | Example |
|--------|---------|---------|
| `Dto` | Full response object | `AlbumDto`, `TrackDto` |
| `SummaryDto` | Abbreviated version (used in lists) | `AlbumSummaryDto`, `ArtistSummaryDto` |
| `Request` | Incoming data from clients | `CreatePlaylistRequest`, `AddItemsRequest` |

## 4.2 Service Interfaces

Each domain area has a service interface that defines available operations:

```csharp
public interface IAlbumService
{
    Task<AlbumDto?> GetAlbumAsync(Guid id, string? market = null);
    Task<IReadOnlyList<AlbumDto>> GetSeveralAlbumsAsync(IEnumerable<Guid> ids, string? market = null);
    Task<PaginatedResult<TrackDto>> GetAlbumTracksAsync(Guid albumId, int offset, int limit, string? market = null);
    Task<PaginatedResult<AlbumSummaryDto>> GetNewReleasesAsync(int offset, int limit, string? country = null);
}
```

**Why interfaces?**
1. **Testability** – Replace real services with mocks in unit tests
2. **Loose coupling** – Controllers depend on the interface, not the concrete class
3. **Dependency Injection** – ASP.NET Core resolves interfaces to implementations automatically

## 4.3 Service Implementations (Stubbed)

All service methods currently throw `NotImplementedException`:

```csharp
public async Task<AlbumDto?> GetAlbumAsync(Guid id, string? market = null)
{
    // TODO: Implement in Phase 6
    // 1. Use _albumRepository to fetch the album with includes
    // 2. Apply market filtering if provided
    // 3. Map Album entity to AlbumDto
    // 4. Return the DTO (or null if not found)
    throw new NotImplementedException("Implement in Phase 6 – see docs/Phase06-ServiceImplementation.md");
}
```

This is intentional – it lets the project **compile and launch** while clearly marking what needs implementation.

## 4.4 PaginatedResult\<T\>

**File:** `src/AudioDelivery.Application/Common/Models/PaginatedResult.cs`

Mirrors Spotify's paginated response format:

```json
{
  "href": "https://api.example.com/v1/albums?offset=0&limit=20",
  "items": [ ... ],
  "limit": 20,
  "offset": 0,
  "next": "https://api.example.com/v1/albums?offset=20&limit=20",
  "previous": null,
  "total": 50
}
```

## 4.5 Domain Service Map

| Area | Interface | Implementation | DTOs |
|------|-----------|---------------|------|
| Albums | `IAlbumService` | `AlbumService` | `AlbumDto`, `AlbumSummaryDto`, `SaveAlbumRequest` |
| Artists | `IArtistService` | `ArtistService` | `ArtistDto`, `ArtistSummaryDto` |
| Tracks | `ITrackService` | `TrackService` | `TrackDto`, `AudioFeaturesDto` |
| Playlists | `IPlaylistService` | `PlaylistService` | `PlaylistDto`, `PlaylistSummaryDto`, `CreatePlaylistRequest`, `UpdatePlaylistRequest`, `AddItemsRequest` |
| Search | `ISearchService` | `SearchService` | `SearchResultDto` |
| Users | `IUserService` | `UserService` | `UserProfileDto`, `PublicUserDto` |
| Genres | `IGenreService` | `GenreService` | `GenreDto` |
| Markets | `IMarketService` | `MarketService` | `MarketDto` |
| Categories | `ICategoryService` | `CategoryService` | `CategoryDto` |

## 4.6 DI Registration

**File:** `src/AudioDelivery.Api/Extensions/ServiceCollectionExtensions.cs`

```csharp
services.AddScoped<IAlbumService, AlbumService>();
services.AddScoped<IArtistService, ArtistService>();
// ... all 9 services registered as Scoped
```

**Why Scoped?**
- One service instance per HTTP request
- Matches DbContext's lifetime (also Scoped)
- Ensures each request gets its own database connection and change tracker

## Key Concepts

### Manual Mapping vs AutoMapper

We use **manual mapping** (entity → DTO) intentionally:
- Understand exactly how data flows through the system
- No hidden magic or configuration surprises
- Better performance (no reflection overhead)
- AutoMapper can be added later as an optimization

### Dependency Flow

```
Controller → IAlbumService → IAlbumRepository → AppDbContext → SQL Server
     ↑            ↑                ↑
  (DI resolves interfaces to concrete types)
```

## Verify

```bash
dotnet build src/AudioDelivery.Application
# Should compile (warnings about async methods are expected – they'll go away in Phase 6)
```

## Next Phase

→ [Phase 5: Database Setup](Phase05-DatabaseSetup.md)
