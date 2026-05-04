using AudioDelivery.Application.Common.Interfaces;
using AudioDelivery.Domain.Entities;
using AudioDelivery.Domain.Enums;
using AudioDelivery.Infrastructure.Data;
using AudioDelivery.Infrastructure.Repositories;
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
    private readonly IImageRepository _imageRepository;

    public DataSeeder(AppDbContext context, IImageRepository imageRepository)
    {
        _context = context;
        _imageRepository = imageRepository;
    }

    /// <summary>
    /// Asynchronously seeds the database with real data, including genres, categories, and default images.
    /// </summary>
    public async Task SeedRealDataAsync(CancellationToken cancellationToken = default)
    {
        await SeedGenresAsync(cancellationToken);
        await SeedCategoriesAsync(cancellationToken);
        await SeedDefaultImagesAsync(cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task SeedTestDataAsync(CancellationToken cancellationToken = default)
    {
        await SeedDefaultImagesAsync(cancellationToken);

        if (await _context.Artists.AnyAsync(cancellationToken)) return;

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

        await _context.Tracks.AddRangeAsync(new Track[] { track1, track2 }, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
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

    public static Guid GetGenreGuidFormat(int genreNumber)
    {
        var formatedGenreNumber = (genreNumber).ToString("D12");
        return Guid.Parse($"a1000000-0000-0000-0000-{formatedGenreNumber}");
    }

    public static Guid GetCategoryGuidFormat(int categoryNumber)
    {
        var formatedCategoryNumber = (categoryNumber).ToString("D12");
        return Guid.Parse($"b1000000-0000-0000-0000-{formatedCategoryNumber}");
    }


    private async Task SeedGenresAsync(CancellationToken cancellationToken = default)
    {
        if (await _context.Genres.AnyAsync(cancellationToken))
            return;

        var names = LoadNamesFromXml("genres.xml", "Genre");
        var now = DateTime.UtcNow;

        var genres = new List<Genre>();

        foreach (var name in names)
        {

            genres.Add(new Genre
            {
                Id = GetGenreGuidFormat(genres.Count + 1),
                Name = name,
                CreatedAt = now,
                UpdatedAt = now
            });
        }

        await _context.Genres.AddRangeAsync(genres, cancellationToken);
    }

    private async Task SeedCategoriesAsync(CancellationToken cancellationToken = default)
    {
        if (await _context.Categories.AnyAsync(cancellationToken))
            return;

        var names = LoadNamesFromXml("categories.xml", "Category");
        var now = DateTime.UtcNow;

        var categories = new List<Category>();

        foreach (var name in names)
        {
            categories.Add(new Category
            {
                Id = GetCategoryGuidFormat(categories.Count + 1),
                Name = name,
                CreatedAt = now,
                UpdatedAt = now
            });
        }

        await _context.Categories.AddRangeAsync(categories, cancellationToken);
    }

    private async Task SeedDefaultImagesAsync(CancellationToken cancellationToken = default)
    {
        if (await _context.Images.AnyAsync(cancellationToken))
            return;

        var defaultStream = ImageRepository.LoadDefaultImageStream();
        await _imageRepository.CreateSizedImagesAsync(imageStream:defaultStream, Domain.Enums.ImageType.Album, cancellationToken, true);
        await _imageRepository.SaveChangesAsync(cancellationToken);
    }
}
