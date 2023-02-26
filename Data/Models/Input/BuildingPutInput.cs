namespace Backend.Data.Models.Input
{
    public class BuildingPutInput
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public bool? Deleted { get; set; } = false;
    }
}
