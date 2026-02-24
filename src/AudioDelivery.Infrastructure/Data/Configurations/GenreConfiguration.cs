using AudioDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AudioDelivery.Infrastructure.Data.Configurations;

/// <summary>
/// EF Core configuration for the Genre entity.
///
/// TODO: Complete the configuration by defining:
///   - Table name, primary key
///   - Name property (required, max 100)
///   - Unique index on Name (genre names should not duplicate)
///   - Many-to-many with Artist is configured in ArtistConfiguration
/// </summary>
public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.ToTable("Genres");
        builder.HasKey(g => g.Id);

        // TODO: Configure Name property and unique index
    }
}
