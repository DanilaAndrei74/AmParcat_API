namespace Backend.Data.Models.Output
{
    public class UserOutput
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string PhotoName { get; set; }
    }
}
