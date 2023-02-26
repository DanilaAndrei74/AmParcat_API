namespace Backend.Data.Models.Input
{
    public class ReservationPutInput
    {
        public Guid? UserId { get; set; }
        public Guid? ParkingSpotId { get; set; }
        public DateTime? Date { get; set; }
    }
}
