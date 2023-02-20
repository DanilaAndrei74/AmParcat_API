using Backend.Data.Database.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Data.Database.Configuration
{
    internal class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.FirstName).IsRequired();
            builder.Property(p => p.LastName).IsRequired();
            builder.Property(p => p.Salt).IsRequired();
            builder.Property(p => p.Password).IsRequired();
            builder.Property(p => p.Email).IsRequired();
            builder.Property(p => p.CreatedAt).IsRequired().HasDefaultValueSql("getdate()");
            builder.Property(p => p.PhotoName).IsRequired().HasDefaultValue("");
            builder.Property(p => p.Deleted).HasDefaultValue(false).IsRequired();

            builder.HasMany(build => build.Reservations)
                .WithOne(build => build.User)
                .HasForeignKey(p => p.UserId)
                .IsRequired();
        }
    }
}