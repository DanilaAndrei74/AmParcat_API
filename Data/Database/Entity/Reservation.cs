namespace Backend.Data.Database.Entity
{
    public class Reservation
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid ParkingSpotId { get; set; }
        public ParkingSpot ParkingSpot { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Deleted { get; set; }
    }
}