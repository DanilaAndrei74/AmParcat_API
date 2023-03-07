using Backend.Data.Database.Entity;

namespace Backend.Data.Models.Output
{
    public class ParkingSpotWithReservationOutput
    {
        public Guid Id { get; set; }
        public Guid ZoneId { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public ReservationAddOn? Reservation { get; set; } 
    }

    public class ReservationAddOn
    {
        public Guid ReservationId { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
    }

    public class ParkingSpotWithReservation
    {
        public ParkingSpot ParkingSpot { get; set; }
        public ReservationAddOn Reservation { get; set; }
    }


}
