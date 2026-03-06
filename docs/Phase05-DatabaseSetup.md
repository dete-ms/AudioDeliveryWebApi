# Phase 5 – Database Setup

> **Status:** 🔲 To Do

## Overview

In this phase you'll:
1. Complete the entity configurations (if not done in Phase 3)
2. Add reference data (`HasData()` in entity configurations) — Genres, Categories
3. Create your first EF Core migration (which will include the reference data as `InsertData` calls)
4. Apply the migration to create the database and insert reference data
5. Implement `DataSeeder` for fake demo data (development only)

## Prerequisites

- [Docker Desktop](https://www.docker.com/products/docker-desktop/) installed and running
- Connection string configured in `appsettings.Development.json` (see [section below](#connection-string))

---

## SQL Server Setup with Docker

Running SQL Server in Docker means **zero local SQL Server installation**, and the identical setup works on every PC you own. The `docker-compose.yml` at the repo root defines the container so spinning it up is a single command.

### Why Docker for SQL Server?

| Concern | Answer |
|---------|--------|
| Cross-platform | Runs on Windows, macOS (Intel & ARM), Linux |
| No local install | No SQL Server edition to license or manage |
| Reproducible | Everyone on the project gets the exact same version |
| Persistent data | Named volume survives restarts and image updates |
| Portable | One `docker-compose up -d` on any machine with Docker |

---

### Option A — docker-compose (Recommended)

A `docker-compose.yml` is already included in the repo root. Use it on every PC you work from.

**Start the SQL Server container:**
```bash
# From the repo root (d:\Coding Projects\AudioDeliveryWebApi)
docker-compose up -d
```

**What the compose file creates:**

| Setting | Value |
|---------|-------|
| Image | `mcr.microsoft.com/mssql/server:2022-latest` |
| Container name | `audiodelivery-sql` |
| SA password | `YourStrong!Pass123` |
| Port | `1433:1433` (host:container) |
| Data volume | `audiodelivery-sqldata` (named, persists data) |
| Restart policy | `unless-stopped` (auto-starts with Docker) |

**Other useful compose commands:**

```bash
# Check status
docker-compose ps

# View SQL Server logs
docker-compose logs -f sqlserver

# Stop the container (data preserved in named volume)
docker-compose stop

# Stop and remove the container (data still preserved in named volume)
docker-compose down

# ⚠️ Stop, remove container AND delete all data (full reset)
docker-compose down -v
```

---

### Option B — docker run (Manual, One-Off)

If you prefer not to use the compose file, run this command directly:

```bash
docker run \
  --name audiodelivery-sql \
  -e "ACCEPT_EULA=Y" \
  -e "SA_PASSWORD=YourStrong!Pass123" \
  -e "MSSQL_PID=Developer" \
  -p 1433:1433 \
  -v audiodelivery-sqldata:/var/opt/mssql \
  --restart unless-stopped \
  -d mcr.microsoft.com/mssql/server:2022-latest
```

> The `-v audiodelivery-sqldata:/var/opt/mssql` flag attaches the same named volume used by the
> compose file, so the data persists between container restarts and removals.

**Manage the container:**

```bash
docker stop audiodelivery-sql    # Stop (preserves data)
docker start audiodelivery-sql   # Restart
docker rm audiodelivery-sql      # Remove container (data still in volume)
```

---

### Verify SQL Server is Running

**1. Check the container is up:**
```bash
docker ps --filter "name=audiodelivery-sql"
```

Expected output includes `STATUS: Up ... (healthy)` once the health check passes (~30 s after first start).

**2. Check SQL Server logs:**
```bash
docker logs audiodelivery-sql
```

Look for:
```
SQL Server is now ready for client connections. This is an informational message...
```

**3. Test connectivity from inside the container:**
```bash
docker exec -it audiodelivery-sql \
  /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "YourStrong!Pass123" -Q "SELECT @@VERSION" -No -C
```

You should receive the SQL Server version string as output.

---

### Named Volume — Data Persistence

Docker volumes keep your database data alive across container lifecycles.

```bash
# List all volumes
docker volume ls

# Inspect the volume (shows where Docker stores the data on your host)
docker volume inspect audiodelivery-sqldata
```

| Command | Container | Volume (data) |
|---------|-----------|---------------|
| `docker-compose stop` | Stopped | ✅ Preserved |
| `docker-compose down` | Removed | ✅ Preserved |
| `docker-compose down -v` | Removed | ❌ **Deleted** |

---

### Connection String {#connection-string}

Docker SQL Server uses **SQL authentication** (SA login), not Windows Authentication.
Update `src/AudioDelivery.Api/appsettings.Development.json` to:

```json
"DefaultConnection": "Server=localhost;Database=AudioDeliveryDb_Dev;User Id=sa;Password=YourStrong!Pass123;TrustServerCertificate=True;"
```

> ⚠️ The existing value uses `Trusted_Connection=True` (Windows Auth), which **will not work**
> with a Docker SQL Server container. Replace the entire connection string with the SA-password
> version above.

---

### Troubleshooting

**Port 1433 already in use**

Another SQL Server instance (local install or another container) is bound to port 1433. Either stop the conflicting instance, or map to a different host port in `docker-compose.yml`:

```yaml
ports:
  - "1434:1433"   # host port 1434 → container port 1433
```

Then update your connection string to use port 1434:
```
Server=localhost,1434;Database=AudioDeliveryDb_Dev;...
```

**Password complexity error**

SQL Server requires the SA password to contain uppercase, lowercase, digits, and special characters, and be at least 8 characters. `YourStrong!Pass123` satisfies this. If you choose your own password, ensure it meets these requirements.

**ARM64 / Apple Silicon (M-series Macs)**

The `mcr.microsoft.com/mssql/server` image does not support ARM64. Use the Azure SQL Edge image instead:

```yaml
image: mcr.microsoft.com/azure-sql-edge:latest
```

This is a compatible lightweight SQL Server variant that runs natively on ARM64. The connection string and behaviour are identical.

**Container exits immediately**

Run `docker logs audiodelivery-sql` — the most common cause is a password that doesn't meet complexity requirements or a port conflict logged at startup.

---

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
- `InsertData` calls appear for Genres and Categories (from `HasData()` — see 5.3.1)

## 5.3.1 Seed Reference Data via `HasData()`

**Do this before running `migrations add`** so the inserts are baked into the initial migration.

Reference data is data the app **cannot function without** in any environment — Genres and Categories.
Instead of a runtime seeder, use EF Core's `HasData()` inside the entity configuration.
EF Core converts this directly into `migrationBuilder.InsertData(...)` calls in the migration's `Up()` method.

> ⚠️ IDs **must be hardcoded** `Guid.Parse("...")` — never `Guid.NewGuid()`.
> `Guid.NewGuid()` generates a different value every time `migrations add` runs,
> causing phantom diffs and duplicate insert attempts on every migration.

**File:** `src/AudioDelivery.Infrastructure/Data/Configurations/GenreConfiguration.cs`

Add at the end of the `Configure` method:

```csharp
builder.HasData(
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000001"), Name = "rock" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000002"), Name = "pop" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000003"), Name = "hip-hop" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000004"), Name = "rap" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000005"), Name = "r-n-b" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000006"), Name = "soul" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000007"), Name = "funk" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000008"), Name = "jazz" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000009"), Name = "blues" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000010"), Name = "country" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000011"), Name = "folk" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000012"), Name = "classical" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000013"), Name = "opera" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000014"), Name = "electronic" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000015"), Name = "house" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000016"), Name = "techno" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000017"), Name = "trance" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000018"), Name = "dubstep" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000019"), Name = "drum-and-bass" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000020"), Name = "reggae" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000021"), Name = "ska" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000022"), Name = "dancehall" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000023"), Name = "metal" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000024"), Name = "heavy-metal" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000025"), Name = "thrash-metal" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000026"), Name = "death-metal" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000027"), Name = "black-metal" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000028"), Name = "power-metal" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000029"), Name = "progressive-metal" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000030"), Name = "punk-rock" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000031"), Name = "hardcore-punk" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000032"), Name = "post-punk" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000033"), Name = "alternative-rock" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000034"), Name = "indie-rock" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000035"), Name = "grunge" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000036"), Name = "shoegaze" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000037"), Name = "britpop" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000038"), Name = "synth-pop" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000039"), Name = "new-wave" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000040"), Name = "disco" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000041"), Name = "gospel" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000042"), Name = "christian-rock" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000043"), Name = "latin-pop" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000044"), Name = "salsa" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000045"), Name = "bachata" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000046"), Name = "merengue" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000047"), Name = "reggaeton" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000048"), Name = "flamenco" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000049"), Name = "bossa-nova" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000050"), Name = "samba" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000051"), Name = "afrobeat" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000052"), Name = "highlife" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000053"), Name = "k-pop" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000054"), Name = "j-pop" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000055"), Name = "mandopop" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000056"), Name = "bollywood" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000057"), Name = "soundtrack" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000058"), Name = "ambient" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000059"), Name = "chillout" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000060"), Name = "lo-fi" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000061"), Name = "trip-hop" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000062"), Name = "industrial" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000063"), Name = "ebm" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000064"), Name = "synthwave" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000065"), Name = "vaporwave" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000066"), Name = "hardstyle" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000067"), Name = "gabber" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000068"), Name = "bluegrass" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000069"), Name = "americana" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000070"), Name = "cajun" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000071"), Name = "zydeco" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000072"), Name = "tango" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000073"), Name = "polka" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000074"), Name = "chanson" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000075"), Name = "schlager" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000076"), Name = "world-music" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000077"), Name = "ethno" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000078"), Name = "minimalism" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000079"), Name = "baroque" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000080"), Name = "romantic" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000081"), Name = "impressionism" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000082"), Name = "contemporary-classical" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000083"), Name = "avant-garde" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000084"), Name = "experimental" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000085"), Name = "trap" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000086"), Name = "drill" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000087"), Name = "uk-garage" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000088"), Name = "grime" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000089"), Name = "electro" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000090"), Name = "electro-swing" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000091"), Name = "breakbeat" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000092"), Name = "idm" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000093"), Name = "hardcore" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000094"), Name = "emo" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000095"), Name = "math-rock" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000096"), Name = "post-rock" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000097"), Name = "neo-soul" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000098"), Name = "dance-pop" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000099"), Name = "art-rock" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000100"), Name = "symphonic-metal" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000101"), Name = "progressive-rock" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000102"), Name = "psychedelic-rock" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000103"), Name = "surf-rock" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000104"), Name = "gothic-rock" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000105"), Name = "stoner-rock" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000106"), Name = "doom-metal" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000107"), Name = "folk-metal" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000108"), Name = "melodic-death-metal" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000109"), Name = "metalcore" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000110"), Name = "post-hardcore" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000111"), Name = "ska-punk" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000112"), Name = "crunk" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000113"), Name = "boom-bap" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000114"), Name = "g-funk" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000115"), Name = "acid-house" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000116"), Name = "deep-house" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000117"), Name = "progressive-house" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000118"), Name = "psytrance" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000119"), Name = "future-bass" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000120"), Name = "glitch-hop" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000121"), Name = "new-jack-swing" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000122"), Name = "afropop" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000123"), Name = "rai" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000124"), Name = "qawwali" },
    new Genre { Id = Guid.Parse("a1000000-0000-0000-0000-000000000125"), Name = "cumbia" }
);
```

**File:** `src/AudioDelivery.Infrastructure/Data/Configurations/CategoryConfiguration.cs`

Add at the end of the `Configure` method:

```csharp
builder.HasData(
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000001"), Name = "Music",             Slug = "music" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000002"), Name = "Podcasts",           Slug = "podcasts" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000003"), Name = "Live Events",        Slug = "live-events" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000004"), Name = "Made For You",       Slug = "made-for-you" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000005"), Name = "Upcoming Releases",  Slug = "upcoming-releases" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000006"), Name = "New Releases",       Slug = "new-releases" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000007"), Name = "Pop",                Slug = "pop" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000008"), Name = "Hip-Hop",            Slug = "hip-hop" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000009"), Name = "Rock",               Slug = "rock" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000010"), Name = "Latin",              Slug = "latin" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000011"), Name = "Charts",             Slug = "charts" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000012"), Name = "Podcast Charts",     Slug = "podcast-charts" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000013"), Name = "Educational",        Slug = "educational" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000014"), Name = "Documentary",        Slug = "documentary" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000015"), Name = "Comedy",             Slug = "comedy" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000016"), Name = "Dance/Electronic",   Slug = "dance-electronic" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000017"), Name = "Mood",               Slug = "mood" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000018"), Name = "Indie",              Slug = "indie" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000019"), Name = "Workout",            Slug = "workout" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000020"), Name = "Discover",           Slug = "discover" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000021"), Name = "Radio",              Slug = "radio" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000022"), Name = "Country",            Slug = "country" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000023"), Name = "R&B",                Slug = "r-and-b" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000024"), Name = "K-pop",              Slug = "k-pop" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000025"), Name = "Chill",              Slug = "chill" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000026"), Name = "Sleep",              Slug = "sleep" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000027"), Name = "Party",              Slug = "party" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000028"), Name = "At Home",            Slug = "at-home" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000029"), Name = "Decades",            Slug = "decades" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000030"), Name = "Love",               Slug = "love" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000031"), Name = "Metal",              Slug = "metal" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000032"), Name = "Jazz",               Slug = "jazz" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000033"), Name = "Trending",           Slug = "trending" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000034"), Name = "Wellness",           Slug = "wellness" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000035"), Name = "Anime",              Slug = "anime" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000036"), Name = "Gaming",             Slug = "gaming" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000037"), Name = "Folk & Acoustic",    Slug = "folk-and-acoustic" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000038"), Name = "Focus",              Slug = "focus" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000039"), Name = "Soul",               Slug = "soul" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000040"), Name = "Kids & Family",      Slug = "kids-and-family" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000041"), Name = "Classical",          Slug = "classical" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000042"), Name = "TV & Movies",        Slug = "tv-and-movies" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000043"), Name = "Instrumental",       Slug = "instrumental" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000044"), Name = "Punk",               Slug = "punk" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000045"), Name = "Ambient",            Slug = "ambient" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000046"), Name = "Netflix",            Slug = "netflix" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000047"), Name = "Blues",              Slug = "blues" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000048"), Name = "Cooking & Dining",   Slug = "cooking-and-dining" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000049"), Name = "Alternative",        Slug = "alternative" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000050"), Name = "Travel",             Slug = "travel" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000051"), Name = "Caribbean",          Slug = "caribbean" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000052"), Name = "Afro",               Slug = "afro" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000053"), Name = "Songwriters",        Slug = "songwriters" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000054"), Name = "Nature & Noise",     Slug = "nature-and-noise" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000055"), Name = "Funk & Disco",       Slug = "funk-and-disco" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000056"), Name = "Spotify Singles",    Slug = "spotify-singles" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000057"), Name = "Summer",             Slug = "summer" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000058"), Name = "EQUAL",              Slug = "equal" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000059"), Name = "RADAR",              Slug = "radar" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000060"), Name = "Fresh Finds",        Slug = "fresh-finds" },
    new Category { Id = Guid.Parse("b1000000-0000-0000-0000-000000000061"), Name = "Mixed By",           Slug = "mixed-by" }
);
```

> **Why hardcoded-looking GUIDs?**
> Using a consistent prefix pattern (`a100...001`, `a100...002`) makes them human-readable
> in the database while still being valid, deterministic GUIDs.
> Use whatever GUIDs you prefer — the only rule is they must never change after the first migration.

## 5.4 Apply the Migration

```bash
dotnet ef database update \
  --project src/AudioDelivery.Infrastructure \
  --startup-project src/AudioDelivery.Api
```

This creates the database and all tables defined in the migration.

## 5.5 Implement Data Seeder

**File:** `src/AudioDelivery.Infrastructure/Seeders/DataSeeder.cs`

`DataSeeder` is for **fake demo data only** — realistic but fictional artists, albums, and tracks
that make the API usable during development without needing a real Spotify import.

> Genres and Categories are **not** seeded here — they were inserted by the migration
> via `HasData()` in section 5.3.1 and exist in all environments automatically.

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
        // Guard against re-seeding — genres always exist (seeded by migration),
        // so check Artists instead to detect whether fake data has been inserted.
        if (await _context.Artists.AnyAsync()) return;

        // Fetch genres seeded by migration so we can assign them to artists
        var rockGenre = await _context.Genres.FirstAsync(g => g.Name == "rock");
        var popGenre  = await _context.Genres.FirstAsync(g => g.Name == "pop");

        // --- Fake Artists ---
        var artist1 = new Artist
        {
            Id          = Guid.NewGuid(),
            Name        = "The Midnight",
            Popularity  = 78,
            Uri         = "spotify:artist:fake1",
            ExternalUrl = "https://open.spotify.com/artist/fake1",
        };
        var artist2 = new Artist
        {
            Id          = Guid.NewGuid(),
            Name        = "Neon Horizon",
            Popularity  = 62,
            Uri         = "spotify:artist:fake2",
            ExternalUrl = "https://open.spotify.com/artist/fake2",
        };
        _context.Artists.AddRange(artist1, artist2);

        // --- Fake Albums ---
        var album1 = new Album
        {
            Id          = Guid.NewGuid(),
            Name        = "Endless Summer",
            AlbumType   = AlbumType.Album,
            ReleaseDate = new DateOnly(2023, 6, 15),
            Popularity  = 74,
            Label       = "Indie Records",
            Uri         = "spotify:album:fake1",
            ExternalUrl = "https://open.spotify.com/album/fake1",
        };
        _context.Albums.Add(album1);

        // --- Fake Tracks ---
        var track1 = new Track
        {
            Id          = Guid.NewGuid(),
            Name        = "Sunset Drive",
            TrackNumber = 1,
            DiscNumber  = 1,
            DurationMs  = 214000,
            Explicit    = false,
            Popularity  = 71,
            Uri         = "spotify:track:fake1",
            ExternalUrl = "https://open.spotify.com/track/fake1",
            AlbumId     = album1.Id,
        };
        var track2 = new Track
        {
            Id          = Guid.NewGuid(),
            Name        = "Neon Lights",
            TrackNumber = 2,
            DiscNumber  = 1,
            DurationMs  = 198000,
            Explicit    = false,
            Popularity  = 68,
            Uri         = "spotify:track:fake2",
            ExternalUrl = "https://open.spotify.com/track/fake2",
            AlbumId     = album1.Id,
        };
        _context.Tracks.AddRange(track1, track2);

        await _context.SaveChangesAsync();
    }
}
```

> Expand `SeedAsync()` with more fake artists, albums, and tracks as needed during development.
> Keep IDs as `Guid.NewGuid()` here — fake data does not need to be deterministic.

## 5.6 Call Seeder on Startup

Add to `Program.cs` after `var app = builder.Build();`:

```csharp
// Apply any pending migrations on startup (all environments)
// This also inserts HasData() reference data (genres, categories) if the migration hasn't run yet.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.MigrateAsync();
}

// Seed fake demo data (Development only — never runs in Staging or Production)
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var seeder = new DataSeeder(scope.ServiceProvider.GetRequiredService<AppDbContext>());
    await seeder.SeedAsync();
}
```

> `MigrateAsync()` is idempotent — it checks the `__EFMigrationsHistory` table and only
> runs migrations that haven't been applied yet. Running it on every startup is safe and means
> you never have to manually run `dotnet ef database update` in deployed environments.

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

### Two Kinds of Seed Data

| Kind | Mechanism | Environments | Idempotency |
|------|-----------|-------------|-------------|
| Reference data (Genres, Categories) | `HasData()` in entity configs → baked into migration | All | EF Core tracks applied migrations in `__EFMigrationsHistory` — never re-runs |
| Fake demo data (Artists, Albums, Tracks) | `DataSeeder.SeedAsync()` at startup | Development only | Manual `if (await _context.Artists.AnyAsync()) return;` guard |

**Why `HasData()` IDs must be hardcoded:**
EF Core compares `HasData()` entries between migrations by their primary key.
If you use `Guid.NewGuid()`, every `migrations add` sees a "new" row and generates a duplicate `InsertData`.
Hardcode the GUIDs once and never change them.

## Verify

```bash
# Run the app – migrations apply automatically, fake data seeded in Development
dotnet run --project src/AudioDelivery.Api
```

Then check the database (SSMS, Azure Data Studio, or `sqlcmd`):

```sql
-- Reference data — present in all environments (inserted by migration)
SELECT COUNT(*) FROM Genres;      -- Should be 125
SELECT COUNT(*) FROM Categories;  -- Should be 61

-- Fake demo data — present only in Development
SELECT COUNT(*) FROM Artists;     -- Should be 2 (or more if you expanded SeedAsync)
SELECT COUNT(*) FROM Albums;      -- Should be 1+
SELECT COUNT(*) FROM Tracks;      -- Should be 2+

-- Confirm the migration ran
SELECT * FROM __EFMigrationsHistory;
```

## Next Phase

→ [Phase 6: Service Implementation](Phase06-ServiceImplementation.md)
