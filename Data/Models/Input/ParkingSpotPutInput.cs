namespace Backend.Data.Models.Input
{
    public class ParkingSpotPutInput
    {
        public Guid? ZoneId { get; set; }
        public string? Name { get; set; }
        public int? Status { get; set; }
    }
}
