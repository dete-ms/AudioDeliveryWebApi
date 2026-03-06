using AudioDelivery.Domain.Common;
using AudioDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AudioDelivery.Infrastructure.Data;

/// <summary>
/// The central EF Core DbContext for the AudioDelivery application.
///
/// This class:
///   1. Declares a DbSet&lt;T&gt; for every domain entity → each becomes a table.
///   2. Applies entity configurations from the Configurations/ folder.
///   3. Overrides SaveChangesAsync to auto-set CreatedAt/UpdatedAt timestamps.
///
/// HOW TO USE:
///   - Register in DI via InfrastructureServiceExtensions.AddInfrastructure()
///   - Inject into repositories, never into controllers or services directly.
///
/// MIGRATIONS:
///   After modifying entities or configurations, create a migration:
///     dotnet ef migrations add MigrationName --project src/AudioDelivery.Infrastructure --startup-project src/AudioDelivery.Api
///   Then apply it:
///     dotnet ef database update --project src/AudioDelivery.Infrastructure --startup-project src/AudioDelivery.Api
/// </summary>
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Album> Albums => Set<Album>();
    public DbSet<Artist> Artists => Set<Artist>();
    public DbSet<Track> Tracks => Set<Track>();
    public DbSet<Playlist> Playlists => Set<Playlist>();
    public DbSet<PlaylistTrack> PlaylistTracks => Set<PlaylistTrack>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Image> Images => Set<Image>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // This keeps the DbContext clean — each entity's config lives in its own file.
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
