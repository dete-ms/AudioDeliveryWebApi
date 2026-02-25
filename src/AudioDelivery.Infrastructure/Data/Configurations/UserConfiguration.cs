using AudioDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AudioDelivery.Infrastructure.Data.Configurations;

/// <summary>
/// EF Core configuration for the User entity.
/// TODO: Check if artists needs to inherit from User or if we can keep them separate. 
/// The reason for this concern are the followers.
/// TODO: Authentication fields will be added later when we integrate Identity.
/// </summary>
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(u => u.Id);

        builder.Property(u => u.DisplayName)
            .HasMaxLength(200);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(u => u.Country)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(u => u.Uri)
            .HasMaxLength(200);

        builder.Property(u => u.ExternalUrl)
            .HasMaxLength(200);

        builder.HasMany(u => u.Playlists)
            .WithOne(p => p.Owner)
            .HasForeignKey(p => p.OwnerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.Images)
            .WithOne(i => i.User)
            .HasForeignKey(i => i.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.SavedAlbums)
            .WithMany(a => a.SavedByUsers)
            .UsingEntity(j => j.ToTable("UserSavedAlbum"));

        builder.HasMany(u => u.SavedTracks)
            .WithMany(t => t.SavedByUsers)
            .UsingEntity(j => j.ToTable("UserSavedTrack"));

        builder.HasMany(u => u.Followers)
            .WithMany(u => u.FollowedUsers)
            .UsingEntity(j => j.ToTable("UserFollowedUser"));

        builder.HasIndex(u => u.Email)
            .IsUnique()
            .HasFilter("[Email] IS NOT NULL");

        builder.HasIndex(u => u.DisplayName);
    }
}
