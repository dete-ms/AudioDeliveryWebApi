using AudioDelivery.Domain.Entities;
using AudioDelivery.Infrastructure.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Xml.Linq;

namespace AudioDelivery.Infrastructure.Data.Configurations;

/// <summary>
/// EF Core configuration for the Genre entity.
/// </summary>
public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.ToTable("Genres");
        builder.HasKey(g => g.Id);

        builder.Property(g => g.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(g => g.Name)
            .IsUnique();

        builder.HasData(LoadData());
    }

    private static IEnumerable<Genre> LoadData()
    {
        var names = DataSeeder.LoadNamesFromXml("genres.xml", nameof(Genre));

        var index = 0;
        foreach (var name in names)
        {
            yield return new Genre
            {
                Id = Guid.Parse($"b1000000-0000-0000-0000-{index++ + 1:000000000000}"),
                Name = name
            };
        }
    }
}
