using AudioDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AudioDelivery.Infrastructure.Data.Configurations;

/// <summary>
/// EF Core configuration for the Album entity.
/// </summary>
public class AlbumConfiguration : IEntityTypeConfiguration<Album>
{
    public void Configure(EntityTypeBuilder<Album> builder)
    {
        builder.ToTable("Albums");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(a => a.AlbumType)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(a => a.ReleaseDate)
            .HasMaxLength(15);

        builder.Property(a => a.ReleaseDatePrecision)
            .HasConversion<string>();

        builder.Property(a => a.Popularity);

        builder.Property(a => a.Label)
            .HasMaxLength(100);

        builder.Property(a => a.Uri)
            .HasMaxLength(200);

        builder.Property(a => a.ExternalUrl)
            .HasMaxLength(500);

        builder.HasMany(a => a.Tracks)
            .WithOne(t => t.Album)
            .HasForeignKey(t => t.AlbumId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(a => a.Images)
            .WithOne(i => i.Album)
            .HasForeignKey(i => i.AlbumId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(a => a.Name);
        builder.HasIndex(a => a.AlbumType);
    }
}
