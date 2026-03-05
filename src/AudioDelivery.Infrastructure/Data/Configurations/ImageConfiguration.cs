using AudioDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AudioDelivery.Infrastructure.Data.Configurations;

/// <summary>
/// Configures the entity mapping for the Image type in the Entity Framework model.
/// </summary>
/// <remarks>The DeleteBehavior is set to NoAction by design. 
/// Deleting images should be performed manually because cascade delete may cause cycles or multiple cascade paths.
/// Remember to .Include() and/or .ThenInclude() the Images when deleting.</remarks>
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
