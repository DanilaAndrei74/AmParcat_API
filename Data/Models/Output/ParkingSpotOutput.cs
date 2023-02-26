namespace Backend.Data.Models.Output
{
    public class ParkingSpotOutput
    {
        public Guid Id { get; set; }
        public Guid ZoneId { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
    }
}
