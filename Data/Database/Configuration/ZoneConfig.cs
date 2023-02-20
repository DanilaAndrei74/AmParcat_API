using Backend.Data.Database.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Data.Database.Configuration
{
    internal class ZoneConfig : IEntityTypeConfiguration<Zone>
    {
        public void Configure(EntityTypeBuilder<Zone> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.FloorId).IsRequired();
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Deleted).HasDefaultValue(false).IsRequired();

            builder.HasMany(build => build.ParkingSpots)
                .WithOne(build => build.Zone)
                .HasForeignKey(p => p.ZoneId)
                .IsRequired();
            builder.HasOne(build => build.Floor)
                .WithMany(build => build.Zones)
                .HasForeignKey(p => p.FloorId)
                .IsRequired();
        }
    }
}