using AudioDelivery.Domain.Entities;
using AudioDelivery.Domain.JoinTables;
using AudioDelivery.Infrastructure.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AudioDelivery.Infrastructure.Data.Configurations;

/// <summary>
/// EF Core configuration for the Category entity.
/// </summary>
public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.HasMany(c => c.Images)
            .WithOne(i => i.Category)
            .HasForeignKey(i => i.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.Playlists)
            .WithMany()
            .UsingEntity<CategoryPlaylist>(
                j => j.HasOne(cp => cp.Playlist)
                    .WithMany()
                    .HasForeignKey(cp => cp.PlaylistId)
                    .OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne(cp => cp.Category)
                    .WithMany()
                    .HasForeignKey(cp => cp.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade),
                j => j.ToTable(nameof(CategoryPlaylist))
            );

        builder.HasIndex(c => c.Name);

        builder.HasData(LoadData());
    }

    private static IEnumerable<Category> LoadData()
    {
        var names = DataSeeder.LoadNamesFromXml("categories.xml", nameof(Category));

        var index = 0;
        foreach (var name in names)
        {
            yield return new Category
            {
                Id = Guid.Parse($"b1000000-0000-0000-0000-{index++ + 1:000000000000}"),
                Name = name
            };
        }
    }
}
