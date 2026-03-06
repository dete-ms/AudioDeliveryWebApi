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
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(connectionString, sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
            });

            // Fallback: InMemory database for quick testing without SQL Server
            // options.UseInMemoryDatabase("AudioDeliveryDb");
        });

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

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
