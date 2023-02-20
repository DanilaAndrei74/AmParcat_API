namespace Backend.Data.Database.Entity
{
    public class Floor
    {
        public Guid Id { get; set; }
        public Guid BuildingId { get; set; }
        public Building Building { get; set; }
        public string Name { get; set; }
        public bool Deleted { get; set; }
        public IEnumerable<Zone> Zones { get; set; }

    }
}
