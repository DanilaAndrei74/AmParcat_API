namespace Backend.Data.Database.Entity
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Salt { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string PhotoName { get; set; }
        public string Deleted { get; set; }
        public IEnumerable<Reservation> Reservations { get; set; }
    }
}