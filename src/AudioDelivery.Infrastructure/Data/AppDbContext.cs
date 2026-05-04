using AudioDelivery.Domain.JoinTables;
using AudioDelivery.Domain.Entities;
using AudioDelivery.Domain.Common;
using AudioDelivery.Domain.Events;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace AudioDelivery.Infrastructure.Data;

/// <summary>
/// The central EF Core DbContext for the AudioDelivery application.
///
/// This class:
///   1. Declares a DbSet<T> for every domain entity → each becomes a table.
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
    private readonly IMediator _mediator;

    public AppDbContext(
        DbContextOptions<AppDbContext> options,
        IMediator mediator) : base(options) 
    {
        _mediator = mediator;
    }

    #region DbSets
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Playlist> Playlists => Set<Playlist>();
    public DbSet<Artist> Artists => Set<Artist>();
    public DbSet<Image> Images => Set<Image>();
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<Album> Albums => Set<Album>();
    public DbSet<Track> Tracks => Set<Track>();
    public DbSet<User> Users => Set<User>();
    public DbSet<UserLibraryItem> UserLibraryItems => Set<UserLibraryItem>();
    public DbSet<UserFollowedUser> UserFollowedUsers => Set<UserFollowedUser>();
    public DbSet<PlaylistTrack> PlaylistTracks => Set<PlaylistTrack>();
    public DbSet<PlaylistImage> PlaylistImages => Set<PlaylistImage>();
    public DbSet<CategoryImage> CategoryImages => Set<CategoryImage>();
    public DbSet<ArtistImage> ArtistImages => Set<ArtistImage>();
    public DbSet<ArtistAlbum> ArtistAlbums => Set<ArtistAlbum>();
    public DbSet<AlbumImage> AlbumImages => Set<AlbumImage>();
    public DbSet<UserImage> UserImages => Set<UserImage>();
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // This keeps the DbContext clean — each entity's config lives in its own file.
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var domainEvents = new List<IDomainEvent>();

        foreach (var entry in base.ChangeTracker.Entries<BaseEntity>())
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

            var currentEntity = entry.Entity;
            domainEvents.AddRange(currentEntity.DomainEvents);
            currentEntity.ClearDomainEvents();
        }

        var result = await base.SaveChangesAsync(cancellationToken);

        foreach (var domainEvent in domainEvents)
        {
            await _mediator.Publish(domainEvent, cancellationToken);
        }

        return result;
    }
}
