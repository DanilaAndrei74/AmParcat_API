using Backend.Data.Database.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Data.Database.Configuration
{
    internal class BuildingConfig : IEntityTypeConfiguration<Building>
    {
        public void Configure(EntityTypeBuilder<Building> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Address).IsRequired();
            builder.Property(p => p.Deleted).HasDefaultValue(false).IsRequired();

            builder.HasMany(build => build.Floors)
                .WithOne(build => build.Building)
                .HasForeignKey(p => p.BuildingId)
                .IsRequired();
        }
    }
}