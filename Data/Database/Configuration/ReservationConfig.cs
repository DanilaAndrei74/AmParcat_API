using Backend.Data.Database.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Data.Database.Configuration
{
    internal class ReservationConfig : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {

            builder.HasKey(x => x.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.UserId).IsRequired();
            builder.Property(p => p.ParkingSpotId).IsRequired();
            builder.Property(p => p.Date).IsRequired();
            builder.Property(p => p.CreatedAt).IsRequired().HasDefaultValueSql("getdate()");
            builder.Property(p => p.Deleted).HasDefaultValue(false).IsRequired();

            builder.HasOne(build => build.User)
                .WithMany(build => build.Reservations)
                .HasForeignKey(p => p.UserId)
                .IsRequired();
            builder.HasOne(build => build.ParkingSpot)
                .WithMany(build => build.Reservations)
                .HasForeignKey(p => p.ParkingSpotId)
                .IsRequired();
        }
    }
}