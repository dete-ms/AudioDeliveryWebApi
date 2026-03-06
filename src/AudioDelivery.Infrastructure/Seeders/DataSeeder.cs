using AudioDelivery.Domain.Entities;
using AudioDelivery.Domain.Enums;
using AudioDelivery.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Xml.Linq;

namespace AudioDelivery.Infrastructure.Seeders;

/// <summary>
/// Seeds the database with initial reference data.
///
/// This class populates:
///   - Genres:     Loaded from Seeders/SeedData/genres.xml     (embedded resource)
///   - Categories: Loaded from Seeders/SeedData/categories.xml (embedded resource)
///
/// HOW TO USE:
///   Call DataSeeder.SeedAsync(dbContext) from Program.cs during app startup
///   (typically wrapped behind an if-development check).
///
/// WHEN TO RUN:
///   - First time the database is created
///   - After adding new reference data
///   - In development for quick testing with Swagger
///
/// IDEMPOTENT: Each seed step checks whether the table already has rows before inserting.
/// </summary>
public class DataSeeder
{
    private static readonly Assembly _assembly = typeof(DataSeeder).Assembly;
    private readonly AppDbContext _context;

    public DataSeeder(AppDbContext context)
    {
        _context = context;
    }


    public async Task SeedRealDataAsync()
    {
        await SeedGenresAsync();
        await SeedCategoriesAsync();
    }

    public async Task SeedTestDataAsync()
    {
        if (await _context.Artists.AnyAsync()) return;

        var rockGenre = await _context.Genres.FirstAsync(g => g.Name == "rock");
        var popGenre = await _context.Genres.FirstAsync(g => g.Name == "pop");

        // --- Fake Artists ---
        var artist1 = new Artist
        {
            Id = Guid.NewGuid(),
            Name = "The Midnight",
            Popularity = 78,
            Uri = "spotify:artist:fake1",
            ExternalUrl = "https://open.spotify.com/artist/fake1",
        };
        var artist2 = new Artist
        {
            Id = Guid.NewGuid(),
            Name = "Neon Horizon",
            Popularity = 62,
            Uri = "spotify:artist:fake2",
            ExternalUrl = "https://open.spotify.com/artist/fake2",
        };
        _context.Artists.AddRange(artist1, artist2);

        // --- Fake Albums ---
        var album1 = new Album
        {
            Id = Guid.NewGuid(),
            Name = "Endless Summer",
            AlbumType = AlbumType.Album,
            ReleaseDate = new DateOnly(2023, 6, 15).ToString(),
            Popularity = 74,
            Label = "Indie Records",
            Uri = "spotify:album:fake1",
            ExternalUrl = "https://open.spotify.com/album/fake1",
        };
        _context.Albums.Add(album1);

        // --- Fake Tracks ---
        var track1 = new Track
        {
            Id = Guid.NewGuid(),
            Name = "Sunset Drive",
            TrackNumber = 1,
            DiscNumber = 1,
            DurationMs = 214000,
            Explicit = false,
            Popularity = 71,
            Uri = "spotify:track:fake1",
            ExternalUrl = "https://open.spotify.com/track/fake1",
            AlbumId = album1.Id,
        };
        var track2 = new Track
        {
            Id = Guid.NewGuid(),
            Name = "Neon Lights",
            TrackNumber = 2,
            DiscNumber = 1,
            DurationMs = 198000,
            Explicit = false,
            Popularity = 68,
            Uri = "spotify:track:fake2",
            ExternalUrl = "https://open.spotify.com/track/fake2",
            AlbumId = album1.Id,
        };
        _context.Tracks.AddRange(track1, track2);

        await _context.SaveChangesAsync();
    }


    /// <summary>
    /// Reads an embedded XML resource from the SeedData folder and returns
    /// the value of the <c>name</c> attribute for each element matching <paramref name="elementName"/>.
    /// </summary>
    public static IEnumerable<string> LoadNamesFromXml(string fileName, string elementName)
    {
        var resourceName = $"AudioDelivery.Infrastructure.Seeders.SeedData.{fileName}";

        using var stream = _assembly.GetManifestResourceStream(resourceName)
            ?? throw new InvalidOperationException($"Embedded resource '{resourceName}' not found.");

        return XDocument.Load(stream)
            .Descendants(elementName)
            .Select(e => (string?)e.Attribute("name"))
            .Where(name => !string.IsNullOrWhiteSpace(name))
            .Select(name => name!)
            .ToList();
    }

    private async Task SeedGenresAsync()
    {
        if (await _context.Genres.AnyAsync())
            return;

        var names = LoadNamesFromXml("genres.xml", "Genre");
        var now = DateTime.UtcNow;

        var genres = new List<Genre>();

        foreach (var name in names)
        {
            var genreNumber = (genres.Count + 1).ToString("D12");

            genres.Add(new Genre
            {
                Id = Guid.Parse($"a1000000-0000-0000-0000-{genreNumber}"),
                Name = name,
                CreatedAt = now,
                UpdatedAt = now
            });
        }

        await _context.Genres.AddRangeAsync(genres);
        await _context.SaveChangesAsync();
    }

    private async Task SeedCategoriesAsync()
    {
        if (await _context.Categories.AnyAsync())
            return;

        var names = LoadNamesFromXml("categories.xml", "Category");
        var now = DateTime.UtcNow;

        var categories = new List<Category>();

        foreach (var name in names)
        {
            var categoryNumber = (categories.Count + 1).ToString("D12");
            categories.Add(new Category
            {
                Id = Guid.Parse($"b1000000-0000-0000-0000-{categoryNumber}"),
                Name = name,
                CreatedAt = now,
                UpdatedAt = now
            });
        }

        await _context.Categories.AddRangeAsync(categories);
        await _context.SaveChangesAsync();
    }
}
