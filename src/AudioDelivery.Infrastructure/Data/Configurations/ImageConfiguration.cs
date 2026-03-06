using AudioDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AudioDelivery.Infrastructure.Data.Configurations;

/// <summary>
/// Configures the entity mapping for the Image type in the Entity Framework model.
/// </summary>
/// <remarks>
/// The DeleteBehavior is set to NoAction by design to avoid cascade delete cycles or multiple cascade paths.
/// When deleting a related principal (e.g., Album, Artist, Playlist, User, or Category), any associated Images
/// must be deleted explicitly (for example, by removing them via the DbContext before deleting the principal),
/// or the relationship must be configured to cascade deletes in a controlled way. Simply eager-loading Images
/// with .Include() / .ThenInclude() does not delete them automatically.
/// </remarks>
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

        builder.HasOne(i => i.Album)
            .WithMany(a => a.Images)
            .HasForeignKey(i => i.AlbumId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(i => i.Artist)
            .WithMany(a => a.Images)
            .HasForeignKey(i => i.ArtistId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(i => i.Playlist)
            .WithMany(p => p.Images)
            .HasForeignKey(i => i.PlaylistId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(i => i.User)
            .WithMany(u => u.Images)
            .HasForeignKey(i => i.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(i => i.Category)
            .WithMany(c => c.Images)
            .HasForeignKey(i => i.CategoryId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
