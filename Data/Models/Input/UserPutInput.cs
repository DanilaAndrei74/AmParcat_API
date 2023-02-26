namespace Backend.Data.Models.Input
{
    public class UserPutInput
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhotoName { get; set; }
        public bool? Deleted { get; set; } = false;
    }
}
