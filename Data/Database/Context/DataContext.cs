using Backend.Data.Database.Configuration;
using Backend.Data.Database.Entity;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data.Database.Context
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options ) : base(options)
        {

        }
        
        public DbSet<Building> Building { get; set; }
        public DbSet<Floor> Floor { get; set; }
        public DbSet<Zone> Zone { get; set; }
        public DbSet<ParkingSpot> ParkingSpot{ get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating( ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BuildingConfig());
            modelBuilder.ApplyConfiguration(new FloorConfig());
            modelBuilder.ApplyConfiguration(new ZoneConfig());
            modelBuilder.ApplyConfiguration(new ParkingSpotConfig());
            modelBuilder.ApplyConfiguration(new ReservationConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());
        }
    }
}
