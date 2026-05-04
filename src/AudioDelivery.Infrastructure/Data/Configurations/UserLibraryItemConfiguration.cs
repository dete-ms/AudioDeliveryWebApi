using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using AudioDelivery.Domain.JoinTables;

namespace AudioDelivery.Infrastructure.Data.Configurations;

public class UserLibraryItemConfiguration : IEntityTypeConfiguration<UserLibraryItem>
{
    public void Configure(EntityTypeBuilder<UserLibraryItem> builder)
    {
        builder.ToTable("UserLibraryItems");
        builder.HasKey(uli => uli.Id);

        builder.HasOne(uli => uli.User)
                .WithMany(user => user.LibraryItems)
                .HasForeignKey(uli => uli.UserId)
                .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(uli => uli.Track)
               .WithMany()
               .HasForeignKey(uli => uli.TrackId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(uli => uli.Album)
               .WithMany()
               .HasForeignKey(uli => uli.AlbumId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(uli => uli.Artist)
               .WithMany()
               .HasForeignKey(uli => uli.ArtistId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(uli => uli.Playlist)
               .WithMany()
               .HasForeignKey(uli => uli.PlaylistId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasIndex(uli => new { uli.UserId, uli.TrackId })
            .IsUnique();

        builder.HasIndex(uli => new { uli.UserId, uli.AlbumId })
            .IsUnique();

        builder.HasIndex(uli => new { uli.UserId, uli.ArtistId })
            .IsUnique();

        builder.HasIndex(uli => new { uli.UserId, uli.PlaylistId })
            .IsUnique();
    }
}
