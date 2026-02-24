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

    // Many Albums ↔ Many Markets
    builder.HasMany(a => a.AvailableMarkets)
        .WithMany(m => m.Albums);

    // One Album → Many Images
    builder.HasMany(a => a.Images)
        .WithOne()
        .HasForeignKey(i => i.AlbumId)
        .OnDelete(DeleteBehavior.Cascade);

    // One Album → Many Copyrights
    builder.HasMany(a => a.Copyrights)
        .WithOne(c => c.Album)
        .HasForeignKey(c => c.AlbumId)
        .OnDelete(DeleteBehavior.Cascade);

    // 5. INDEXES (optional, improves query performance)
    builder.HasIndex(a => a.Name);
    builder.HasIndex(a => a.ReleaseDate);
}
```

### Configuration Checklist

Implement the `Configure` method body for each file:

- [ ] `AlbumConfiguration.cs` – Table, properties, relationships to Artists/Tracks/Images/Copyrights/Markets
- [ ] `ArtistConfiguration.cs` – Table, properties, relationships to Genres/Images
- [ ] `TrackConfiguration.cs` – Table, properties, FK to Album, relationships to Artists/Markets
- [ ] `AudioFeaturesConfiguration.cs` – Table, properties, one-to-one with Track
- [ ] `PlaylistConfiguration.cs` – Table, properties, FK to User (Owner), relationship to Images/PlaylistTracks
- [ ] `UserConfiguration.cs` – Table, properties, relationships to Playlists/Images
- [ ] `GenreConfiguration.cs` – Table, unique index on Name
- [ ] `MarketConfiguration.cs` – Table, unique index on Code
- [ ] `CategoryConfiguration.cs` – Table, properties, relationships to Images/Playlists

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

## Verify

After implementing the configurations:
```bash
dotnet build src/AudioDelivery.Infrastructure
# Should compile with 0 errors
```

## Next Phase

→ [Phase 4: Application Layer](Phase04-ApplicationLayer.md)
