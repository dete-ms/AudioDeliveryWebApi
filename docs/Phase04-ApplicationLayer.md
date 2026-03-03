# Phase 4 – Application Layer

> **Status:** ✅ Complete – DTOs, service interfaces, stubbed implementations, and AutoMapper profiles are in place.

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
    Task<AlbumDto?> GetAlbumAsync(Guid id);
    Task<IReadOnlyList<AlbumDto>> GetSeveralAlbumsAsync(IEnumerable<Guid> ids);
    Task<PaginatedResult<TrackDto>> GetAlbumTracksAsync(Guid albumId, int offset, int limit);
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
public async Task<AlbumDto?> GetAlbumAsync(Guid id)
{
    // TODO: Implement in Phase 6
    // 1. Use _albumRepository to fetch the album with includes
    // 2. Map Album entity to AlbumDto
    // 3. Return the DTO (or null if not found)
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
| Tracks | `ITrackService` | `TrackService` | `TrackDto` |
| Playlists | `IPlaylistService` | `PlaylistService` | `PlaylistDto`, `PlaylistSummaryDto`, `CreatePlaylistRequest`, `UpdatePlaylistRequest`, `AddItemsRequest` |
| Search | `ISearchService` | `SearchService` | `SearchResultDto` |
| Users | `IUserService` | `UserService` | `UserProfileDto`, `PublicUserDto` |
| Genres | `IGenreService` | `GenreService` | `GenreDto` |
| Categories | `ICategoryService` | `CategoryService` | `CategoryDto` |
| Library | `ILibraryService` | `LibraryService` | `LibraryItemRequest`, `LibraryCheckResult` |

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

## 4.7 AutoMapper Profiles

**Do this before Phase 5.** Services (Phase 6) will inject `IMapper` and use `ProjectTo<T>()` for SQL-translated queries — loading only the columns each DTO actually needs.

### Why AutoMapper?

| Feature | Extension Methods | AutoMapper |
|---------|------------------|------------|
| Compile-time safety | ✅ Full | ✅ Full (with tests) |
| Debuggable | ✅ Easy | 🔶 Slightly harder |
| SQL projection (`ProjectTo<T>()`) | ❌ Loads full entity first | ✅ Translates map to SQL |
| Reuse across layers | 🔶 Static methods | ✅ `IMapper` injectable anywhere |
| Boilerplate | Medium | Low |

`ProjectTo<T>()` is the decisive advantage: AutoMapper translates your profile's `CreateMap` configuration directly into a SQL `SELECT` with only the columns the DTO uses — no full entity load into memory.

### Install AutoMapper

Since **AutoMapper v13.0**, `AddAutoMapper` is built into the core package — the old
`AutoMapper.Extensions.Microsoft.DependencyInjection` package is deprecated and no longer needed.
Add the single package to both projects:

```bash
dotnet add src/AudioDelivery.Application package AutoMapper
dotnet add src/AudioDelivery.Api package AutoMapper
```

### DI Registration

In `ServiceCollectionExtensions.cs`, scan the Application assembly for all `Profile` subclasses using a
marker type. The first argument is a configuration lambda (leave it empty unless you need extra setup
such as a license key):

```csharp
// src/AudioDelivery.Api/Extensions/ServiceCollectionExtensions.cs
using AudioDelivery.Application.Albums.Profiles; // any type from the Application assembly

// Pass the marker type — AutoMapper scans its assembly for all Profile subclasses
services.AddAutoMapper(cfg => { }, typeof(AlbumProfile));
```

This registers `IMapper` as a **Singleton** and discovers every class that inherits `AutoMapper.Profile`
in the Application project. You never need to list profiles individually.

### File Layout

Create a `Profiles/` folder inside each domain area, alongside `DTOs/`:

```
src/AudioDelivery.Application/
├── Albums/
│   ├── DTOs/
│   └── Profiles/
│       └── AlbumProfile.cs
├── Artists/
│   ├── DTOs/
│   └── Profiles/
│       └── ArtistProfile.cs
├── Tracks/
│   ├── DTOs/
│   └── Profiles/
│       └── TrackProfile.cs
├── Playlists/
│   ├── DTOs/
│   └── Profiles/
│       └── PlaylistProfile.cs
├── Users/
│   ├── DTOs/
│   └── Profiles/
│       └── UserProfile.cs
├── Genres/
│   ├── DTOs/
│   └── Profiles/
│       └── GenreProfile.cs
├── Categories/
│   ├── DTOs/
│   └── Profiles/
│       └── CategoryProfile.cs
└── Common/
    ├── Models/
    │   └── ImageDto.cs
    └── Profiles/
        └── ImageProfile.cs
```

### Pattern

Each file inherits `AutoMapper.Profile` and declares maps in its constructor.
Properties with identical names and compatible types are mapped **automatically** — only mismatches and computed values need `ForMember`.

```csharp
// Albums/Profiles/AlbumProfile.cs
using AudioDelivery.Application.Albums.DTOs;
using AudioDelivery.Domain.Entities;
using AutoMapper;

namespace AudioDelivery.Application.Albums.Profiles;

public class AlbumProfile : Profile
{
    public AlbumProfile()
    {
        CreateMap<Album, AlbumDto>()
            // Enum → lowercase string
            .ForMember(dest => dest.AlbumType,
                opt => opt.MapFrom(src => src.AlbumType.ToString().ToLowerInvariant()))
            .ForMember(dest => dest.ReleaseDatePrecision,
                opt => opt.MapFrom(src => src.ReleaseDatePrecision.ToString().ToLowerInvariant()))
            // Computed: Tracks.Count (ProjectTo translates this to SQL COUNT)
            .ForMember(dest => dest.TotalTracks,
                opt => opt.MapFrom(src => src.Tracks.Count));
        // Id, Name, Popularity, Label, Uri, ExternalUrl, ReleaseDate,
        // Artists (List<Artist> → List<ArtistSummaryDto>) and
        // Images  (List<Image>  → List<ImageDto>)
        // are all resolved automatically via their own profiles.

        CreateMap<Album, AlbumSummaryDto>()
            .ForMember(dest => dest.AlbumType,
                opt => opt.MapFrom(src => src.AlbumType.ToString().ToLowerInvariant()))
            .ForMember(dest => dest.TotalTracks,
                opt => opt.MapFrom(src => src.Tracks.Count));
    }
}
```

> **How AutoMapper resolves nested collections:**
> When `AlbumDto` has a `List<ArtistSummaryDto> Artists` property and `Album` has
> `ICollection<Artist> Artists`, AutoMapper finds `CreateMap<Artist, ArtistSummaryDto>()`
> in `ArtistProfile` and applies it element-by-element automatically.
> No explicit `ForMember` is needed for the collection itself.

### All Profiles to Create

#### `Artists/Profiles/ArtistProfile.cs`

```csharp
public class ArtistProfile : Profile
{
    public ArtistProfile()
    {
        CreateMap<Artist, ArtistDto>()
            // Genres is ICollection<Genre> → List<string>: must project manually
            .ForMember(dest => dest.Genres,
                opt => opt.MapFrom(src => src.Genres.Select(g => g.Name).ToList()));
        // Id, Name, Popularity, FollowerCount, Uri, ExternalUrl, Images → auto

        CreateMap<Artist, ArtistSummaryDto>();
        // Id, Name, Uri, ExternalUrl → all auto
    }
}
```

#### `Tracks/Profiles/TrackProfile.cs`

```csharp
public class TrackProfile : Profile
{
    public TrackProfile()
    {
        CreateMap<Track, TrackDto>();
        // Id, Name, DiscNumber, TrackNumber, DurationMs, Explicit, Popularity,
        // PreviewUrl, IsLocal, Uri, ExternalUrl → auto
        // Album (Track → AlbumSummaryDto) resolved via AlbumProfile
        // Artists (List<Artist> → List<ArtistSummaryDto>) resolved via ArtistProfile
    }
}
```

> ⚠️ `TrackDto.Album` will be `null` if `Album` was not eagerly loaded.
> In Phase 6, `GetTrackAsync` must call `.Include(t => t.Album)` before mapping.
> `ProjectTo<TrackDto>()` avoids this — it generates a SQL JOIN automatically.

#### `Playlists/Profiles/PlaylistProfile.cs`

```csharp
public class PlaylistProfile : Profile
{
    public PlaylistProfile()
    {
        CreateMap<Playlist, PlaylistDto>();
        // Owner (User → PublicUserDto) resolved via UserProfile
        // Images → auto via ImageProfile

        CreateMap<Playlist, PlaylistSummaryDto>();
    }
}
```

#### `Users/Profiles/UserProfile.cs`

```csharp
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, PublicUserDto>()
            // FollowerCount = Followers.Count — not a stored column
            .ForMember(dest => dest.FollowerCount,
                opt => opt.MapFrom(src => src.Followers.Count));
        // Id, DisplayName, Uri, ExternalUrl, Images → auto
    }
}
```

#### `Genres/Profiles/GenreProfile.cs`

```csharp
public class GenreProfile : Profile
{
    public GenreProfile()
    {
        CreateMap<Genre, GenreDto>();
        // Id, Name → auto
    }
}
```

#### `Categories/Profiles/CategoryProfile.cs`

```csharp
public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryDto>();
        // Id, Name, Images → auto via ImageProfile
    }
}
```

### Image DTO and Profile

Images appear across Albums, Artists, Playlists, Users, and Categories.
Create a **shared** DTO and profile in `Common/`:

```csharp
// Common/Models/ImageDto.cs
namespace AudioDelivery.Application.Common.Models;

public class ImageDto
{
    public string Url { get; set; } = string.Empty;
    public int? Height { get; set; }
    public int? Width { get; set; }
}
```

```csharp
// Common/Profiles/ImageProfile.cs
using AudioDelivery.Application.Common.Models;
using AudioDelivery.Domain.Entities;
using AutoMapper;

namespace AudioDelivery.Application.Common.Profiles;

public class ImageProfile : Profile
{
    public ImageProfile()
    {
        CreateMap<Image, ImageDto>();
        // Url, Height, Width → all auto
    }
}
```

Because `services.AddAutoMapper(typeof(AlbumProfile).Assembly)` scans the entire assembly, `ImageProfile` is discovered automatically — no extra registration needed.

### Using IMapper in Services (Phase 6 preview)

Services inject `IMapper` via the constructor and use it in two ways:

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

    // Option A – Map after loading (use for single-entity lookups)
    public async Task<AlbumDto?> GetAlbumAsync(Guid id)
    {
        var album = await _context.Albums
            .Include(a => a.Artists)
            .Include(a => a.Images)
            .Include(a => a.Tracks)
            .FirstOrDefaultAsync(a => a.Id == id);

        return album is null ? null : _mapper.Map<AlbumDto>(album);
    }

    // Option B – ProjectTo (use for lists/pagination — only SELECTs DTO columns)
    public async Task<PaginatedResult<AlbumDto>> GetNewReleasesAsync(int offset, int limit)
    {
        var query = _context.Albums
            .OrderByDescending(a => a.ReleaseDate)
            .ProjectTo<AlbumDto>(_mapper.ConfigurationProvider);

        var total = await query.CountAsync();
        var items = await query.Skip(offset).Take(limit).ToListAsync();

        return new PaginatedResult<AlbumDto> { Items = items, Total = total, Offset = offset, Limit = limit };
    }
}
```

`ProjectTo<T>()` generates SQL that SELECTs only the columns `AlbumDto` uses — no `Include()` calls needed for the SQL path. The ORM resolves joins from your `CreateMap` configuration.

### Checklist

- [ ] `dotnet add src/AudioDelivery.Application package AutoMapper`
- [ ] `dotnet add src/AudioDelivery.Api package AutoMapper`
- [ ] `services.AddAutoMapper(cfg => { }, typeof(AlbumProfile))` in `ServiceCollectionExtensions.cs`
- [ ] `Common/Models/ImageDto.cs`
- [ ] `Common/Profiles/ImageProfile.cs`
- [ ] `Albums/Profiles/AlbumProfile.cs` – `Album → AlbumDto`, `Album → AlbumSummaryDto`
- [ ] `Artists/Profiles/ArtistProfile.cs` – `Artist → ArtistDto`, `Artist → ArtistSummaryDto`
- [ ] `Tracks/Profiles/TrackProfile.cs` – `Track → TrackDto`
- [ ] `Playlists/Profiles/PlaylistProfile.cs` – `Playlist → PlaylistDto`, `Playlist → PlaylistSummaryDto`
- [ ] `Users/Profiles/UserProfile.cs` – `User → PublicUserDto`
- [ ] `Genres/Profiles/GenreProfile.cs` – `Genre → GenreDto`
- [ ] `Categories/Profiles/CategoryProfile.cs` – `Category → CategoryDto`

## Key Concepts

### AutoMapper Profiles

We use **AutoMapper** (see section 4.7) for entity-to-DTO mapping.
The decisive reason is `ProjectTo<T>()`: AutoMapper translates your `CreateMap` configuration into a SQL SELECT that fetches only the columns each DTO uses — the EF Core equivalent of writing hand-crafted inline `.Select(a => new AlbumDto { ... })` projections, but without the boilerplate.
See section 4.7 for the full profile layout, `ForMember` examples, and DI registration.

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
