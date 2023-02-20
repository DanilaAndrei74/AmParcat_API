namespace Backend.Data.Database.Entity
{
    public class Building
    {
        public Guid Id { get; set; }
        public string Name { get; set; } 
        public string Address { get; set; }
        public bool Deleted { get; set; }
        public IEnumerable<Floor> Floors { get; set; }

    }
}
