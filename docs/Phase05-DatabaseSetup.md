# Phase 5 – Database Setup

> **Status:** 🔲 To Do

## Overview

In this phase you'll:
1. Complete the entity configurations (if not done in Phase 3)
2. Create your first EF Core migration
3. Apply the migration to create the database
4. Seed initial data (genres, categories)

## Prerequisites

- SQL Server instance running (local, Docker, or Azure SQL)
- Connection string configured in `appsettings.Development.json`

### Quick SQL Server Setup with Docker

```bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=YourStrong!Pass123" \
  -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
```

Then update your connection string:
```json
"DefaultConnection": "Server=localhost;Database=AudioDeliveryDb_Dev;User Id=sa;Password=YourStrong!Pass123;TrustServerCertificate=True;"
```

### Alternative: In-Memory Database (No SQL Server Needed)

For quick testing without SQL Server, modify `InfrastructureServiceExtensions.cs`:

```csharp
// Replace:
services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(...));

// With:
services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("AudioDeliveryDb"));
```

> ⚠️ In-Memory database doesn't support migrations, transactions, or raw SQL.
> Switch back to SQL Server before Phase 6.

## 5.1 Verify Entity Configurations

Ensure all configuration files in `src/AudioDelivery.Infrastructure/Data/Configurations/` have their `Configure` method implemented (see Phase 3 guide for the pattern).

## 5.2 Install EF Core Tools

```bash
# Install the dotnet-ef CLI tool globally (one-time setup)
dotnet tool install --global dotnet-ef

# Verify installation
dotnet ef --version
```

## 5.3 Create Initial Migration

Run from the solution root:

```bash
dotnet ef migrations add InitialCreate \
  --project src/AudioDelivery.Infrastructure \
  --startup-project src/AudioDelivery.Api \
  --output-dir Data/Migrations
```

**What this does:**
- Analyzes your `AppDbContext` and entity configurations
- Generates a migration file in `src/AudioDelivery.Infrastructure/Data/Migrations/`
- The migration contains `Up()` (create tables) and `Down()` (drop tables) methods

**Inspect the migration:**
Open the generated `*_InitialCreate.cs` file and verify:
- All expected tables are created
- Column types and constraints look correct
- Relationships and foreign keys are properly defined
- Indexes are created where expected

## 5.4 Apply the Migration

```bash
dotnet ef database update \
  --project src/AudioDelivery.Infrastructure \
  --startup-project src/AudioDelivery.Api
```

This creates the database and all tables defined in the migration.

## 5.5 Implement Data Seeder

**File:** `src/AudioDelivery.Infrastructure/Seeders/DataSeeder.cs`

Implement the `SeedAsync` method to populate reference data:

```csharp
public class DataSeeder
{
    private readonly AppDbContext _context;

    public DataSeeder(AppDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        if (await _context.Genres.AnyAsync()) return; // Already seeded

        // Seed Genres (Spotify has ~125 genre seeds)
        var genres = new List<Genre>
        {
            new() { Id = Guid.NewGuid(), Name = "rock" },
            new() { Id = Guid.NewGuid(), Name = "pop" },
            new() { Id = Guid.NewGuid(), Name = "hip-hop" },
            new() { Id = Guid.NewGuid(), Name = "jazz" },
            new() { Id = Guid.NewGuid(), Name = "electronic" },
            new() { Id = Guid.NewGuid(), Name = "classical" },
            new() { Id = Guid.NewGuid(), Name = "r-n-b" },
            new() { Id = Guid.NewGuid(), Name = "country" },
            new() { Id = Guid.NewGuid(), Name = "metal" },
            new() { Id = Guid.NewGuid(), Name = "indie" },
            // Add more as needed...
        };
        _context.Genres.AddRange(genres);

        // Seed Categories
        var categories = new List<Category>
        {
            new() { Id = Guid.NewGuid(), Name = "Top Lists", Slug = "toplists" },
            new() { Id = Guid.NewGuid(), Name = "Pop", Slug = "pop" },
            new() { Id = Guid.NewGuid(), Name = "Hip-Hop", Slug = "hiphop" },
            new() { Id = Guid.NewGuid(), Name = "Rock", Slug = "rock" },
            new() { Id = Guid.NewGuid(), Name = "Mood", Slug = "mood" },
        };
        _context.Categories.AddRange(categories);

        await _context.SaveChangesAsync();
    }
}
```

## 5.6 Call Seeder on Startup

Add to `Program.cs` after `var app = builder.Build();`:

```csharp
// Seed the database on startup (Development only)
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var seeder = new DataSeeder(scope.ServiceProvider.GetRequiredService<AppDbContext>());
    await seeder.SeedAsync();
}
```

## 5.7 Useful EF Core Commands

```bash
# List all migrations
dotnet ef migrations list --project src/AudioDelivery.Infrastructure --startup-project src/AudioDelivery.Api

# Remove last migration (if not applied)
dotnet ef migrations remove --project src/AudioDelivery.Infrastructure --startup-project src/AudioDelivery.Api

# Generate SQL script (for review or production deployment)
dotnet ef migrations script --project src/AudioDelivery.Infrastructure --startup-project src/AudioDelivery.Api

# Drop database (careful!)
dotnet ef database drop --project src/AudioDelivery.Infrastructure --startup-project src/AudioDelivery.Api
```

## Key Concepts

### What is a Migration?

A migration is a versioned snapshot of your database schema. EF Core compares your current model with the last migration to generate change scripts. Think of it as "version control for your database."

### Migration Naming

Use descriptive names: `InitialCreate`, `AddAudioFeaturesTable`, `AddIndexOnAlbumName`

### Idempotent Seeding

Always check `if (await _context.Genres.AnyAsync()) return;` before seeding to prevent duplicate data on subsequent app starts.

## Verify

```bash
# Run the app – should connect to the database and seed data
dotnet run --project src/AudioDelivery.Api

# Check the database (use SSMS, Azure Data Studio, or SQL command line)
SELECT COUNT(*) FROM Genres;      -- Should show seeded genres
SELECT COUNT(*) FROM Categories;  -- Should show seeded categories
```

## Next Phase

→ [Phase 6: Service Implementation](Phase06-ServiceImplementation.md)
