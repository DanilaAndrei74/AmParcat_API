using Backend.Data.Database.Entity;
using Backend.Data.Models.Output;

namespace Backend.Services
{
    public class PrintOutputService
    {
        public List<UserOutput> Users(IEnumerable<User> users)
        {
            var output = new List<UserOutput>();
            foreach (var user in users)
            {
                output.Add(new UserOutput
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    CreatedAt = user.CreatedAt,
                    PhotoName = user.PhotoName,
                });
            }
            return output;
        }

        public UserOutput User(User user)
        {
            return new UserOutput()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CreatedAt = user.CreatedAt,
                PhotoName = user.PhotoName,
            };
        }
    }
}
