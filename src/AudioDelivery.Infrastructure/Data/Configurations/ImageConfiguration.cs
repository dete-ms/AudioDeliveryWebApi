using AudioDelivery.Domain.Entities;
using AudioDelivery.Domain.JoinTables;
using AudioDelivery.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AudioDelivery.Infrastructure.Data.Configurations;

/// <summary>
/// Configures the entity mapping for the Image type in the Entity Framework model.
/// Images use many-to-many relationships so they can be shared across multiple entities.
/// </summary>
public class ImageConfiguration : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.ToTable("Images");
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Url)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(i => i.Height)
            .HasDefaultValue(0);
        builder.Property(i => i.Width)
            .HasDefaultValue(0);

        builder.HasMany(i => i.Albums)
            .WithMany(a => a.Images)
            .UsingEntity<AlbumImage>(
                j => j.HasOne(ai => ai.Album)
                    .WithMany()
                    .HasForeignKey(ai => ai.AlbumId)
                    .OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne(ai => ai.Image)
                    .WithMany()
                    .HasForeignKey(ai => ai.ImageId)
                    .OnDelete(DeleteBehavior.Cascade),
                j => j.ToTable(nameof(AlbumImage))
            );

        builder.HasMany(i => i.Artists)
            .WithMany(a => a.Images)
            .UsingEntity<ArtistImage>(
                j => j.HasOne(ai => ai.Artist)
                    .WithMany()
                    .HasForeignKey(ai => ai.ArtistId)
                    .OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne(ai => ai.Image)
                    .WithMany()
                    .HasForeignKey(ai => ai.ImageId)
                    .OnDelete(DeleteBehavior.Cascade),
                j => j.ToTable(nameof(ArtistImage))
            );

        builder.HasMany(i => i.Playlists)
            .WithMany(p => p.Images)
            .UsingEntity<PlaylistImage>(
                j => j.HasOne(pi => pi.Playlist)
                    .WithMany()
                    .HasForeignKey(pi => pi.PlaylistId)
                    .OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne(pi => pi.Image)
                    .WithMany()
                    .HasForeignKey(pi => pi.ImageId)
                    .OnDelete(DeleteBehavior.Cascade),
                j => j.ToTable(nameof(PlaylistImage))
            );

        builder.HasMany(i => i.Users)
            .WithMany(u => u.Images)
            .UsingEntity<UserImage>(
                j => j.HasOne(ui => ui.User)
                    .WithMany()
                    .HasForeignKey(ui => ui.UserId)
                    .OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne(ui => ui.Image)
                    .WithMany()
                    .HasForeignKey(ui => ui.ImageId)
                    .OnDelete(DeleteBehavior.Cascade),
                j => j.ToTable(nameof(UserImage))
            );

        builder.HasMany(i => i.Categories)
            .WithMany(c => c.Images)
            .UsingEntity<CategoryImage>(
                j => j.HasOne(ci => ci.Category)
                    .WithMany()
                    .HasForeignKey(ci => ci.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne(ci => ci.Image)
                    .WithMany()
                    .HasForeignKey(ci => ci.ImageId)
                    .OnDelete(DeleteBehavior.Cascade),
                j => j.ToTable(nameof(CategoryImage))
            );
    }
}
