# Phase 11 – Testing

> **Status:** 🔲 To Do

## Overview

This phase establishes a testing strategy:
1. **Unit tests** for services (business logic)
2. **Integration tests** for API endpoints (full pipeline)
3. **Repository tests** with in-memory database

## 11.1 Create Test Project

```bash
dotnet new xunit -n AudioDelivery.Tests -o tests/AudioDelivery.Tests --framework net9.0
dotnet sln add tests/AudioDelivery.Tests

dotnet add tests/AudioDelivery.Tests reference src/AudioDelivery.Api
dotnet add tests/AudioDelivery.Tests reference src/AudioDelivery.Application
dotnet add tests/AudioDelivery.Tests reference src/AudioDelivery.Infrastructure
dotnet add tests/AudioDelivery.Tests reference src/AudioDelivery.Domain
```

Install testing packages:
```bash
dotnet add tests/AudioDelivery.Tests package Moq
dotnet add tests/AudioDelivery.Tests package FluentAssertions
dotnet add tests/AudioDelivery.Tests package Microsoft.AspNetCore.Mvc.Testing
dotnet add tests/AudioDelivery.Tests package Microsoft.EntityFrameworkCore.InMemory
```

## 11.2 Unit Testing Services

Test business logic in isolation by mocking dependencies:

```csharp
// tests/AudioDelivery.Tests/Services/GenreServiceTests.cs
public class GenreServiceTests
{
    private readonly AppDbContext _context;
    private readonly GenreService _service;

    public GenreServiceTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new AppDbContext(options);
        _service = new GenreService(_context);

        // Seed test data
        _context.Genres.AddRange(
            new Genre { Id = Guid.NewGuid(), Name = "rock" },
            new Genre { Id = Guid.NewGuid(), Name = "pop" });
        _context.SaveChanges();
    }

    [Fact]
    public async Task GetAvailableGenreSeeds_ReturnsAllGenres()
    {
        // Act
        var result = await _service.GetAvailableGenreSeedsAsync();

        // Assert
        result.Should().HaveCount(2);
        result.Select(g => g.Name).Should().Contain("rock");
        result.Select(g => g.Name).Should().Contain("pop");
    }
}
```

## 11.3 Integration Testing with WebApplicationFactory

Test the full HTTP pipeline (controllers → services → database):

```csharp
// tests/AudioDelivery.Tests/Integration/AlbumsControllerTests.cs
public class AlbumsControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public AlbumsControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                // Replace SQL Server with In-Memory DB for testing
                services.RemoveAll<DbContextOptions<AppDbContext>>();
                services.AddDbContext<AppDbContext>(options =>
                    options.UseInMemoryDatabase("TestDb"));
            });
        }).CreateClient();
    }

    [Fact]
    public async Task GetAlbum_ReturnsNotFound_WhenAlbumDoesNotExist()
    {
        var response = await _client.GetAsync($"/api/v1/albums/{Guid.NewGuid()}");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetGenreSeeds_ReturnsOk()
    {
        var response = await _client.GetAsync("/api/v1/genres/seeds");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
```

> **Note:** For `WebApplicationFactory<Program>` to work, add this to the Api project's `Program.cs`:
> ```csharp
> // At the very end of Program.cs
> public partial class Program { } // Needed for WebApplicationFactory
> ```

## 11.4 Testing with Moq

```csharp
// Mock a repository
var mockRepo = new Mock<IAlbumRepository>();
mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
    .ReturnsAsync(new Album { Id = Guid.NewGuid(), Name = "Test Album" });

var service = new AlbumService(mockRepo.Object);
var result = await service.GetAlbumAsync(Guid.NewGuid());

result.Should().NotBeNull();
result!.Name.Should().Be("Test Album");
```

## 11.5 Test Organization

```
tests/AudioDelivery.Tests/
├── Unit/
│   ├── Services/
│   │   ├── AlbumServiceTests.cs
│   │   ├── ArtistServiceTests.cs
│   │   ├── TrackServiceTests.cs
│   │   └── ...
│   └── Mapping/
│       └── DtoMappingTests.cs
├── Integration/
│   ├── AlbumsControllerTests.cs
│   ├── ArtistsControllerTests.cs
│   └── ...
└── Helpers/
    ├── TestDbContextFactory.cs
    └── TestDataBuilder.cs
```

## 11.6 Test Data Builder Pattern

```csharp
public class TestDataBuilder
{
    public static Album CreateAlbum(string name = "Test Album", int trackCount = 10)
    {
        var album = new Album
        {
            Id = Guid.NewGuid(),
            Name = name,
            AlbumType = AlbumType.Album,
            TotalTracks = trackCount,
            ReleaseDate = "2024-01-01",
            ReleaseDatePrecision = ReleaseDatePrecision.Day,
            CreatedAt = DateTime.UtcNow
        };

        for (int i = 0; i < trackCount; i++)
        {
            album.Tracks.Add(new Track
            {
                Id = Guid.NewGuid(),
                Name = $"Track {i + 1}",
                TrackNumber = i + 1,
                DurationMs = 200000,
                AlbumId = album.Id
            });
        }

        return album;
    }
}
```

## 11.7 Run Tests

```bash
dotnet test
dotnet test --verbosity normal
dotnet test --filter "FullyQualifiedName~AlbumsControllerTests"
```

## Key Concepts

### Test Pyramid

```
          /  E2E  \         ← Few: test critical user flows
         /  Integr. \       ← Medium: test API endpoints
        / Unit Tests  \     ← Many: test business logic
```

### AAA Pattern

```csharp
[Fact]
public async Task MethodName_Scenario_ExpectedResult()
{
    // Arrange – set up test data and dependencies
    // Act – call the method under test
    // Assert – verify the result
}
```

## Verify

```bash
dotnet test --verbosity normal
# All tests should pass
```

## Next Phase

→ [Phase 12: Deployment & Next Steps](Phase12-DeploymentNextSteps.md)
