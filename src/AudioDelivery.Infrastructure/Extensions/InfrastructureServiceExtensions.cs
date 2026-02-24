using AudioDelivery.Infrastructure.Data;
using AudioDelivery.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AudioDelivery.Infrastructure.Extensions;

/// <summary>
/// Extension methods for registering Infrastructure layer services in DI.
///
/// This keeps the DI registration logic organized and out of Program.cs.
/// Call builder.Services.AddInfrastructure(builder.Configuration) from Program.cs.
/// </summary>
public static class InfrastructureServiceExtensions
{
    /// <summary>
    /// Registers the AppDbContext (with SQL Server) and all repository implementations.
    /// </summary>
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // ── Register DbContext ──
        // Uses SQL Server if a connection string is configured,
        // otherwise falls back to InMemory for development/testing.
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(options =>
        {
            if (!string.IsNullOrEmpty(connectionString) 
                && !connectionString.Contains("PLACEHOLDER", System.StringComparison.OrdinalIgnoreCase))
            {
                options.UseSqlServer(connectionString, sqlOptions =>
                {
                    // Tell EF Core that migrations live in the Infrastructure assembly
                    sqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                });
            }
            else
            {
                // Fallback: InMemory database for quick testing without SQL Server
                // TODO Phase 5: Set up your SQL Server and update the connection string
                options.UseInMemoryDatabase("AudioDeliveryDb");
            }
        });

        // ── Register Generic Repository ──
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        // ── Register Domain-Specific Repositories ──
        services.AddScoped<IAlbumRepository, AlbumRepository>();
        services.AddScoped<IArtistRepository, ArtistRepository>();
        services.AddScoped<ITrackRepository, TrackRepository>();
        services.AddScoped<IPlaylistRepository, PlaylistRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        return services;
    }
}
