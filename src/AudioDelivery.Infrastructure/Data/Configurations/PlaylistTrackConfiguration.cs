using AudioDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AudioDelivery.Infrastructure.Data.Configurations;

public class PlaylistTrackConfiguration : IEntityTypeConfiguration<PlaylistTrack>
{
    public void Configure(EntityTypeBuilder<PlaylistTrack> builder)
    {
        builder.ToTable("PlaylistTrack");

        builder.HasKey(x => x.Id);

        builder.HasOne(pt => pt.AddedByUser)
            .WithMany()
            .HasForeignKey(pt => pt.AddedByUserId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(pt => new { pt.PlaylistId, pt.Position });
    }
}
