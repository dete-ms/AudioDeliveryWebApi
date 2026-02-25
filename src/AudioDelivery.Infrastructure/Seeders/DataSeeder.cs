using AudioDelivery.Infrastructure.Data;

namespace AudioDelivery.Infrastructure.Seeders;

/// <summary>
/// Seeds the database with initial reference data.
///
/// This class populates:
///   - Genres: The standard Spotify genre seeds (e.g., "rock", "pop", "hip-hop", ...)
///   - Categories: Browse categories (e.g., "Top Lists", "Mood", "Workout")
///   - (Optional) Sample artists, albums, tracks for development/testing
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
/// TODO: Implement SeedAsync with actual genre data.
///       Use context.Genres.AnyAsync() to check if already seeded (idempotent).
/// </summary>
public static class DataSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        // TODO: Implement seeding logic
        //
        // Step 1: Seed Genres (if Genres table is empty)
        //   - Create Genre entities for common genres like:
        //     "acoustic", "alt-rock", "alternative", "ambient", "blues",
        //     "classical", "country", "dance", "electronic", "folk",
        //     "funk", "grunge", "hip-hop", "indie", "jazz", "k-pop",
        //     "latin", "metal", "pop", "punk", "r-n-b", "reggae",
        //     "rock", "soul", "techno", "world-music"
        //   - context.Genres.AddRange(genres);
        //
        // Step 2: (Optional) Seed sample artists, albums, tracks for dev
        //
        // Step 3: Save all changes
        //   await context.SaveChangesAsync();

        await Task.CompletedTask; // Remove this once implemented
    }
}
