using Backend.Data.Database.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Data.Database.Configuration
{
    internal class ParkingSpotConfig : IEntityTypeConfiguration<ParkingSpot>
    {
        public void Configure(EntityTypeBuilder<ParkingSpot> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.ZoneId).IsRequired();
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Status).IsRequired().HasDefaultValue(1);
            builder.Property(p => p.Deleted).HasDefaultValue(false).IsRequired();

            builder.HasMany(build => build.Reservations)
                .WithOne(build => build.ParkingSpot)
                .HasForeignKey(p => p.ParkingSpotId)
                .IsRequired();
            builder.HasOne(build => build.Zone)
                .WithMany(build => build.ParkingSpots)
                .HasForeignKey(p => p.ZoneId)
                .IsRequired();
        }
    }
}