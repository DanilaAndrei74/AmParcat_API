namespace Backend.Data.Database.Entity
{
    public class ParkingSpot
    {
        public Guid Id { get; set; }
        public Guid ZoneId { get; set; }
        public Zone Zone { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public bool Deleted { get; set; }
        public IEnumerable<Reservation> Reservations { get; set; }
    }
}