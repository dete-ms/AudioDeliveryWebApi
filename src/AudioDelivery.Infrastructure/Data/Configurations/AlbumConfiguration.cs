using AudioDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AudioDelivery.Infrastructure.Data.Configurations;

/// <summary>
/// EF Core configuration for the Album entity.
///
/// TODO: Complete the configuration by defining:
///   - Table name, primary key, property constraints (max lengths, required fields)
///   - Many-to-many relationship with Artist (via implicit join table "ArtistAlbum")
///   - One-to-many relationship with Track
///   - One-to-many relationship with Copyright
///   - One-to-many relationship with Image (filtered to AlbumId)
///   - Many-to-many relationship with Market (via implicit join table "AlbumMarket")
///   - Index on Name for search performance
///   - Enum conversion for AlbumType and ReleaseDatePrecision (store as string)
/// </summary>
public class AlbumConfiguration : IEntityTypeConfiguration<Album>
{
    public void Configure(EntityTypeBuilder<Album> builder)
    {
        builder.ToTable("Albums");
        builder.HasKey(a => a.Id);

        // TODO: Configure properties – Name (required, max length), ReleaseDate, Label, Uri, ExternalUrl
        // TODO: Configure AlbumType enum → store as string using .HasConversion<string>()
        // TODO: Configure ReleaseDatePrecision enum → store as string
        // TODO: Configure relationships (Artists, Tracks, Copyrights, Images, AvailableMarkets)
        // TODO: Add indexes (e.g., on Name, AlbumType)
    }
}
