namespace Backend.Data.Database.Entity
{
    public class Zone
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid FloorId { get; set; }
        public Floor Floor { get; set; }
        public bool Deleted { get; set; }
        public IEnumerable<ParkingSpot> ParkingSpots { get; set; }
    }
}
