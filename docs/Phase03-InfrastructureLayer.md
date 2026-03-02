# Phase 3 – Infrastructure Layer

> **Status:** 🔶 Partially complete – scaffolding done, entity configurations need implementation.

## Overview

The Infrastructure layer is responsible for **data access**. It contains:
- `AppDbContext` – EF Core database context (✅ complete)
- Entity Configurations – Fluent API rules for each entity (🔲 bodies are stubbed)
- Repositories – Data access abstraction (✅ complete)
- Data Seeder – Initial seed data (🔲 stubbed)
- DI Extensions – Service registration (✅ complete)

## 3.1 AppDbContext

**File:** `src/AudioDelivery.Infrastructure/Data/AppDbContext.cs`

The `AppDbContext` class:
1. Declares a `DbSet<T>` for each entity (EF Core uses these to generate tables)
2. Overrides `OnModelCreating` to apply Fluent API configurations
3. Overrides `SaveChangesAsync` to automatically set `CreatedAt` / `UpdatedAt` timestamps

```csharp
// How auto-timestamping works:
public override async Task<int> SaveChangesAsync(CancellationToken ct = default)
{
    foreach (var entry in ChangeTracker.Entries<BaseEntity>())
    {
        if (entry.State == EntityState.Added)
            entry.Entity.CreatedAt = DateTime.UtcNow;
        
        if (entry.State == EntityState.Modified)
            entry.Entity.UpdatedAt = DateTime.UtcNow;
    }
    return await base.SaveChangesAsync(ct);
}
```

## 3.2 Entity Configurations – YOUR TASK

**Location:** `src/AudioDelivery.Infrastructure/Data/Configurations/`

Each configuration file implements `IEntityTypeConfiguration<T>` and uses the **Fluent API** to define:
- Table names
- Primary keys
- Property constraints (max length, required, etc.)
- Relationships (one-to-many, many-to-many)
- Indexes

### Why Fluent API over Data Annotations?

| Feature | Data Annotations | Fluent API |
|---------|-----------------|------------|
| Location | On entity classes | In configuration files |
| Complexity | Simple constraints | Any EF Core feature |
| Domain purity | Adds EF dependency to Domain | Keeps Domain clean |
| Organization | Scattered across entities | Centralized per entity |

We chose Fluent API to keep the Domain layer free of EF Core dependencies.

### How to Implement Each Configuration

Here's the pattern for `AlbumConfiguration.cs` as an example:

```csharp
public void Configure(EntityTypeBuilder<Album> builder)
{
    // 1. TABLE NAME
    builder.ToTable("Albums");

    // 2. PRIMARY KEY
    builder.HasKey(a => a.Id);

    // 3. PROPERTY CONSTRAINTS
    builder.Property(a => a.Name)
        .IsRequired()
        .HasMaxLength(200);

    builder.Property(a => a.AlbumType)
        .IsRequired()
        .HasConversion<string>();  // Store enum as string for readability

    builder.Property(a => a.ReleaseDate)
        .IsRequired()
        .HasMaxLength(10);  // "YYYY-MM-DD" format

    builder.Property(a => a.Label)
        .HasMaxLength(200);

    builder.Property(a => a.ExternalUrl)
        .HasMaxLength(500);

    builder.Property(a => a.Uri)
        .HasMaxLength(200);

    // 4. RELATIONSHIPS
    // One Album → Many Tracks
    builder.HasMany(a => a.Tracks)
        .WithOne(t => t.Album)
        .HasForeignKey(t => t.AlbumId)
        .OnDelete(DeleteBehavior.Cascade);

    // Many Albums ↔ Many Artists (EF Core creates join table)
    builder.HasMany(a => a.Artists)
        .WithMany(ar => ar.Albums);

    // One Album → Many Images
    builder.HasMany(a => a.Images)
        .WithOne()
        .HasForeignKey(i => i.AlbumId)
        .OnDelete(DeleteBehavior.Cascade);

    // 5. INDEXES (optional, improves query performance)
    builder.HasIndex(a => a.Name);
    builder.HasIndex(a => a.ReleaseDate);
}
```

### Configuration Checklist

Implement the `Configure` method body for each file:

- [x] `AlbumConfiguration.cs` – Table, properties, relationships to Artists/Tracks/Images
- [x] `ArtistConfiguration.cs` – Table, properties, relationships to Genres/Images
- [x] `TrackConfiguration.cs` – Table, properties, FK to Album, relationship to Artists
- [x] `PlaylistConfiguration.cs` – Table, properties, FK to User (Owner), relationship to Images/PlaylistTracks
- [x] `UserConfiguration.cs` – Table, properties, relationships to Playlists/Images
- [x] `GenreConfiguration.cs` – Table, unique index on Name
- [x] `CategoryConfiguration.cs` – Table, properties, relationships to Images/Playlists
- [ ] `PlaylistTrackConfiguration.cs` – Composite index on (PlaylistId, Position), SetNull on AddedByUser FK (see 3.5.1)

### Tips

- **Cascading deletes:** Use `DeleteBehavior.Cascade` when child records should be deleted with the parent (e.g., Album → Tracks).
- **Restrict deletes:** Use `DeleteBehavior.Restrict` when you want to prevent accidental deletion of referenced records.
- **String lengths:** Always set `HasMaxLength()` for string properties – unbounded strings use `nvarchar(max)` which is wasteful.
- **Enum storage:** Use `.HasConversion<string>()` to store enums as readable strings instead of integers.

## 3.3 Repositories

**Generic Repository:** `IRepository<T>` / `Repository<T>` – provides CRUD operations for any entity.

**Domain-specific Repositories:** Each domain area has an interface and implementation that extends the generic repository with specialized queries.

```csharp
// Example: IAlbumRepository adds album-specific queries
public interface IAlbumRepository : IRepository<Album>
{
    // TODO: Add methods like:
    // Task<Album?> GetAlbumWithTracksAsync(Guid id);
    // Task<IReadOnlyList<Album>> GetNewReleasesAsync(int offset, int limit);
    // Task<IReadOnlyList<Album>> GetArtistAlbumsAsync(Guid artistId, int offset, int limit);
}
```

Implement these methods when you reach Phase 6 (Service Implementation).

## 3.4 DI Registration

**File:** `src/AudioDelivery.Infrastructure/Extensions/InfrastructureServiceExtensions.cs`

This extension method registers all Infrastructure services:
```csharp
services.AddDbContext<AppDbContext>(options => options.UseSqlServer(...));
services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
services.AddScoped<IAlbumRepository, AlbumRepository>();
// ... etc.
```

Called from `Program.cs` as `builder.Services.AddInfrastructure(builder.Configuration)`.

## 3.5 Code Review & Design Decisions

### 3.5.1 Configuration Corrections

Three issues were found after completing the entity configurations:

**1. `Album.TotalTracks` — computed property, must be Ignored**

`Album.TotalTracks` is defined as `=> this.Tracks.Count` (getter-only, no setter).
EF Core **cannot** persist a property with no setter to a column. Calling `builder.Property()` on it will throw at runtime when materializing entities from a database query.

```csharp
// ✅ Correct
builder.Ignore(a => a.TotalTracks);

// ❌ Wrong — throws at runtime
builder.Property(a => a.TotalTracks).HasDefaultValue(0);
```

The same rule applies to `User.FollowerCount` (`=> this.Followers.Count`). However, since it is **not** explicitly configured in `UserConfiguration`, EF Core skips it automatically (no setter = not mapped by convention). You only need `Ignore()` if you accidentally call `builder.Property()` on it.

**2. `User.Country` — nullable in the entity but `IsRequired()` in the config**

`User.cs` declares `public string? Country { get; set; }` (nullable).  
`UserConfiguration` was calling `.IsRequired()`, which generates a `NOT NULL` database column. These contradict each other: inserts would fail whenever `Country` is not provided.

```csharp
// ✅ Correct — matches the nullable entity declaration
builder.Property(u => u.Country).HasMaxLength(10);

// ❌ Wrong — creates NOT NULL column, throws on insert when Country is null
builder.Property(u => u.Country).IsRequired().HasMaxLength(10);
```

**3. `PlaylistTrack` — no configuration file**

`PlaylistTrack` is an explicit join entity with extra data (`Position`, `AddedAt`, `AddedByUserId`). Without a dedicated `PlaylistTrackConfiguration.cs`, two things are missing:

- **Sort index:** `(PlaylistId, Position)` — needed for efficiently fetching tracks in playlist order.
- **Delete behavior on `AddedByUserId`:** without explicit config, EF Core uses `ClientSetNull` for the optional FK. You should set `OnDelete(DeleteBehavior.SetNull)` explicitly to make the intent clear: if the user who added the track is deleted, the track remains in the playlist but the "added by" attribution is nullified.

```csharp
// PlaylistTrackConfiguration.cs
builder.HasIndex(pt => new { pt.PlaylistId, pt.Position });

builder.HasOne(pt => pt.AddedByUser)
    .WithMany()
    .HasForeignKey(pt => pt.AddedByUserId)
    .OnDelete(DeleteBehavior.SetNull);
```

---

### 3.5.2 Should Artist Inherit from User?

**No — keep them separate.**

Both `Artist` and `User` happen to have `Uri`, `ExternalUrl`, and a follower collection, but these are coincidental naming overlaps between two fundamentally different domain concepts:

| Concern | `Artist` | `User` |
|---------|----------|--------|
| **Purpose** | Music catalog entity | Account / security principal |
| **Unique fields** | Popularity, Genres, Albums, Tracks | Email, Country, Playlists, SavedAlbums, SavedTracks |
| **Followers** | Users who follow this act | Users who follow this person |
| **Can be a band?** | Yes — multi-person or historical | No — 1:1 with an account |
| **Must have login?** | No | Yes |
| **Spotify API resource** | `/artists/{id}` | `/users/{id}` |

**EF Core inheritance problems:**

- **Table Per Hierarchy (TPH):** all Artist + User columns land in one `Users` table, mostly null depending on the row type.
- **Table Per Type (TPT):** every artist query requires a JOIN to the parent Users table.
- **Navigation type collision:** `User.FollowedArtists` would become `ICollection<User>` if `Artist` IS a `User` — you lose compile-time type safety and semantic clarity.

**The right model:** if an artist can log in to the service, add a nullable `UserId?` foreign key on `Artist` that links to their account. That link is an authentication concern — added in Phase 8, not as inheritance.

---

### 3.5.3 Why Services Are Registered as Scoped, Not Singleton

```csharp
// In ServiceCollectionExtensions.cs
services.AddScoped<IAlbumService, AlbumService>();
```

| Lifetime | Instances | Use when |
|----------|-----------|----------|
| **Singleton** | One for the entire app | Stateless, thread-safe, no scoped dependencies |
| **Scoped** | One per HTTP request | Depends on `DbContext` or other request-scoped services |
| **Transient** | New instance every resolve | Lightweight, no shared state needed |

**Services must be Scoped because they depend on `AppDbContext`.**

EF Core registers `AppDbContext` as Scoped by default — one context per HTTP request. `DbContext` is **not** thread-safe and is designed to be short-lived.

If you register a service that takes `AppDbContext` as **Singleton**, ASP.NET Core throws at startup:
> `InvalidOperationException: Cannot consume scoped service 'AppDbContext' from singleton 'IAlbumService'.`

This error is called a **captive dependency** — a longer-lived object incorrectly holding a reference to a shorter-lived one. The Scoped lifetime avoids this:

```
HTTP Request arrives
  → Controller (Transient, created by MVC)
      → AlbumService (Scoped)
          → AppDbContext (Scoped)
  ← All three are disposed together when the request ends
```

Using Transient for services would also satisfy the constraint (Transient can consume Scoped), but each resolve would create a separate service instance within the same request, which means you'd lose any in-request state and potentially create multiple DbContext instances unnecessarily.

## Verify

After implementing the configurations:
```bash
dotnet build src/AudioDelivery.Infrastructure
# Should compile with 0 errors
```

## Next Phase

→ [Phase 4: Application Layer](Phase04-ApplicationLayer.md)
