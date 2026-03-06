using AudioDelivery.Domain.Common;
using AudioDelivery.Domain.Entities;
using AudioDelivery.Infrastructure.Data;
using AudioDelivery.Infrastructure.Data.Configurations;
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
public static class DataSeeder
{
    private static readonly Assembly _assembly = typeof(DataSeeder).Assembly;

    public static async Task SeedRealDataAsync(AppDbContext context)
    {
        await SeedGenresAsync(context);
        await SeedCategoriesAsync(context);
    }

    public static async Task SeedTestDataAsync(AppDbContext context)
    {
        // test data for albums, artists, tracks, playlists, users, etc. can be added here
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

    private static async Task SeedGenresAsync(AppDbContext context)
    {
        if (await context.Genres.AnyAsync())
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

        await context.Genres.AddRangeAsync(genres);
        await context.SaveChangesAsync();
    }

    private static async Task SeedCategoriesAsync(AppDbContext context)
    {
        if (await context.Categories.AnyAsync())
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

        await context.Categories.AddRangeAsync(categories);
        await context.SaveChangesAsync();
    }
}
