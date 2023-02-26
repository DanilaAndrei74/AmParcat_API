namespace Backend.Data.Models.Input
{
    public class ReservationInput
    {
        public Guid UserId { get; set; }
        public Guid ParkingSpotId { get; set; }
        public DateTime Date { get; set; }
    }
}
