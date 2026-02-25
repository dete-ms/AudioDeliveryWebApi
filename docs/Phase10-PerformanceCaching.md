# Phase 10 – Performance & Caching

> **Status:** 🔲 To Do

## Overview

This phase optimizes API performance:
1. **Response caching** for read-heavy endpoints
2. **CORS** configuration
3. **Pagination best practices** hardening
4. **Query optimization** with projection
5. **Response compression**

## 10.1 In-Memory Caching

```bash
# Built into ASP.NET Core – no extra package needed
```

```csharp
// Program.cs
builder.Services.AddMemoryCache();

// In a service
public class GenreService : IGenreService
{
    private readonly IMemoryCache _cache;
    private readonly AppDbContext _context;

    public GenreService(AppDbContext context, IMemoryCache cache)
    {
        _context = context;
        _cache = cache;
    }

    public async Task<IReadOnlyList<GenreDto>> GetAvailableGenreSeedsAsync()
    {
        return await _cache.GetOrCreateAsync("genre-seeds", async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24);
            return await _context.Genres
                .Select(g => new GenreDto { Name = g.Name })
                .ToListAsync();
        }) ?? [];
    }
}
```

### What to Cache

| Endpoint | TTL | Reason |
|----------|-----|--------|
| Genre seeds | 24 hours | Rarely changes |
| Categories | 1 hour | Changes occasionally |
| Album details | 5 minutes | May update (popularity) |
| Search results | No cache | Too variable |

## 10.2 Response Caching Headers

```csharp
// Program.cs
builder.Services.AddResponseCaching();

// Middleware pipeline
app.UseResponseCaching();

// On controller actions
[HttpGet("seeds")]
[ResponseCache(Duration = 86400)]  // 24 hours in seconds
public async Task<IActionResult> GetAvailableGenreSeeds() { }
```

## 10.3 CORS Configuration

```csharp
// Program.cs – service registration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "https://yourfrontend.com")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });

    // Development: allow all origins
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Program.cs – middleware pipeline (before UseAuthorization)
if (app.Environment.IsDevelopment())
    app.UseCors("AllowAll");
else
    app.UseCors("AllowFrontend");
```

## 10.4 Query Optimization with Projection

Instead of loading full entities and mapping, project directly to DTOs:

```csharp
// ❌ SLOW: Loads entire entity graph, then maps
var album = await _context.Albums
    .Include(a => a.Artists)
    .Include(a => a.Images)
    .Include(a => a.Tracks)
    .FirstOrDefaultAsync(a => a.Id == id);
return MapToDto(album);

// ✅ FAST: Projects directly to DTO (only fetches needed columns)
var dto = await _context.Albums
    .Where(a => a.Id == id)
    .Select(a => new AlbumDto
    {
        Id = a.Id,
        Name = a.Name,
        TotalTracks = a.TotalTracks,
        Artists = a.Artists.Select(ar => new ArtistSummaryDto
        {
            Id = ar.Id,
            Name = ar.Name
        }).ToList()
    })
    .FirstOrDefaultAsync();
```

Projection generates a targeted SQL query that only fetches the columns you need.

## 10.5 AsNoTracking for Read-Only Queries

```csharp
// Add to queries that don't modify data
var albums = await _context.Albums
    .AsNoTracking()  // Skip change tracking = faster reads
    .Where(a => a.Artists.Any(ar => ar.Id == artistId))
    .ToListAsync();
```

## 10.6 Response Compression

```csharp
// Program.cs
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
});

// Middleware pipeline (at the top, before other middleware)
app.UseResponseCompression();
```

## 10.7 Pagination Guard

Prevent clients from requesting too many records:

```csharp
// In controller
[HttpGet]
public async Task<IActionResult> GetCategories(
    [FromQuery] int offset = 0,
    [FromQuery] int limit = 20)
{
    limit = Math.Clamp(limit, 1, 50); // Enforce max 50
    offset = Math.Max(0, offset);       // No negative offsets
    // ...
}
```

## Key Concepts

### Cache Invalidation

"There are only two hard things in Computer Science: cache invalidation and naming things."

Strategies:
1. **Time-based expiration** – simplest, good for most cases
2. **Event-based invalidation** – clear cache when data changes
3. **Cache tags** – group related cache entries for bulk invalidation

### ETag Support (Advanced)

```csharp
// Return ETag header with responses
var etag = $"\"{album.UpdatedAt.Ticks}\"";
Response.Headers.ETag = etag;

// Check If-None-Match header
if (Request.Headers.IfNoneMatch == etag)
    return StatusCode(304); // Not Modified
```

## Verify

```bash
# Check response headers for caching
curl -v https://localhost:5001/api/v1/genres/seeds
# Should include: Cache-Control: public, max-age=86400

# Check CORS headers
curl -v -H "Origin: http://localhost:3000" https://localhost:5001/api/v1/categories
# Should include: Access-Control-Allow-Origin: http://localhost:3000
```

## Next Phase

→ [Phase 11: Testing](Phase11-Testing.md)
