using AudioDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AudioDelivery.Infrastructure.Data.Configurations;

/// <summary>
/// EF Core configuration for the Category entity.
///
/// TODO: Complete the configuration by defining:
///   - Table name, primary key
///   - Name (required, max 256)
///   - Slug (required, max 100, unique index)
///   - One-to-many with Image (filtered to CategoryId)
///   - Many-to-many with Playlist (join table "CategoryPlaylist")
/// </summary>
public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");
        builder.HasKey(c => c.Id);

        // TODO: Configure properties and relationships
    }
}
