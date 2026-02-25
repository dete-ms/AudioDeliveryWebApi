# Phase 2 – Domain Entities

> **Status:** ✅ Complete – all entities and enums are implemented.
> This guide explains the design decisions behind each entity.

## Overview

The Domain layer is the heart of Clean Architecture. It contains pure C# classes that model real-world concepts from Spotify's data model. These classes have **zero dependencies** on frameworks like EF Core or ASP.NET – they're plain old CLR objects (POCOs).

## 2.1 BaseEntity

**File:** `src/AudioDelivery.Domain/Common/BaseEntity.cs`

Every entity inherits from `BaseEntity`, which provides:

```csharp
public abstract class BaseEntity
{
    public Guid Id { get; set; }           // Primary key
    public DateTime CreatedAt { get; set; } // Set automatically on insert
    public DateTime UpdatedAt { get; set; } // Updated automatically on save
}
```

**Why Guid instead of int?**
- GUIDs don't leak information (sequential IDs reveal record count)
- Safe for distributed systems (no sequence coordination needed)
- Can be generated client-side before saving

**Why timestamps in the base class?**
- Every record should track creation and modification times
- Handled automatically in `AppDbContext.SaveChangesAsync()` override

## 2.2 Enums

**Location:** `src/AudioDelivery.Domain/Enums/`

| Enum | Values | Used By |
|------|--------|---------|
| `AlbumType` | Album, Single, Compilation | `Album.AlbumType` |
| `ReleaseDatePrecision` | Year, Month, Day | `Album.ReleaseDatePrecision` |
| `RepeatMode` | Off, Track, Context | Reserved for player feature |

**Why enums?**
- Type safety – compiler prevents invalid values
- Self-documenting – code reads `AlbumType.Single` instead of magic string `"single"`
- EF Core stores them as integers by default (efficient storage)

## 2.3 Entity Map (Spotify → Our Domain)

| Spotify Concept | Our Entity | Key Properties |
|----------------|-----------|----------------|
| Album | `Album.cs` | Name, AlbumType, ReleaseDate, TotalTracks, Label |
| Artist | `Artist.cs` | Name, Popularity, ExternalUrl, Uri |
| Track | `Track.cs` | Name, DurationMs, TrackNumber, Explicit, Popularity |
| Audio Features | `AudioFeatures.cs` | Danceability, Energy, Key, Tempo, Valence |
| Playlist | `Playlist.cs` | Name, Description, Public, Collaborative |
| Playlist Track | `PlaylistTrack.cs` | Position, AddedAt (join entity) |
| User | `User.cs` | DisplayName, Email, Country |
| Genre | `Genre.cs` | Name |
| Category | `Category.cs` | Name, Slug |
| Image | `Image.cs` | Url, Height, Width (polymorphic) |

## 2.4 Relationships

### Many-to-Many Relationships

```
Album ←→ Artist     (an album has multiple artists, an artist has multiple albums)
Track ←→ Artist     (a track features multiple artists)
Artist ←→ Genre     (an artist belongs to multiple genres)
Category ←→ Playlist (a category contains multiple playlists)
```

EF Core handles these with **skip navigations** (implicit join tables) configured in Phase 3.

### One-to-Many Relationships

```
Album → Track       (an album contains multiple tracks)
Album → Image       (an album has cover art)
User → Playlist     (a user owns multiple playlists)
Track → AudioFeatures (a track has one set of audio features)
```

### Join Entity (Explicit)

`PlaylistTrack` is an **explicit join entity** because it carries extra data:
- `Position` – track order in the playlist
- `AddedAt` – when the track was added
- `AddedByUserId` – who added it

## 2.5 Polymorphic Image Entity

The `Image` entity uses **nullable foreign keys** to associate with multiple entity types:

```csharp
public Guid? AlbumId { get; set; }
public Guid? ArtistId { get; set; }
public Guid? PlaylistId { get; set; }
public Guid? UserId { get; set; }
public Guid? CategoryId { get; set; }
```

Only one FK is set per image row. This is a simple approach for a learning project. In production, you might use:
- **Table-per-type** (separate AlbumImage, ArtistImage tables)
- **Owned types** or value objects

## Key Concepts

### Navigation Properties

```csharp
public ICollection<Track> Tracks { get; set; } = new List<Track>();
```

Navigation properties tell EF Core about relationships. Initializing with `new List<>()` prevents null reference exceptions when accessing the collection before loading data.

### Foreign Keys

```csharp
public Guid AlbumId { get; set; }   // FK property
public Album Album { get; set; }    // Navigation property
```

EF Core uses the FK property to store the relationship in the database and the navigation property to load the related entity in C#.

## Verify

```bash
dotnet build src/AudioDelivery.Domain
# Should compile with 0 errors and 0 warnings
```

## Next Phase

→ [Phase 3: Infrastructure Layer](Phase03-InfrastructureLayer.md)
