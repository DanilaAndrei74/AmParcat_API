using Backend.Data.Database.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Data.Database.Configuration
{
    internal class FloorConfig : IEntityTypeConfiguration<Floor>
    {
        public void Configure(EntityTypeBuilder<Floor> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.BuildingId).IsRequired();
            builder.Property(p => p.Name).IsRequired(); 
            builder.Property(p => p.Deleted).HasDefaultValue(false).IsRequired();

            builder.HasMany(build => build.Zones)
                .WithOne(build => build.Floor)
                .HasForeignKey(p => p.FloorId)
                .IsRequired();
            builder.HasOne(build => build.Building)
                .WithMany(build => build.Floors)
                .HasForeignKey(p => p.BuildingId)
                .IsRequired();
        }
    }
}