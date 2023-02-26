namespace Backend.Data.Models.Output
{
    public class ReservationOutput
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ParkingSpotId { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
