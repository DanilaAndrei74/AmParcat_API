using Backend.Data.Database.Context;
using Backend.Data.Database.Entity;
using Backend.Data.Models.Input;
using Backend.Data.Models.Output;
using Backend.Services;

namespace Backend.Data.CommandsNQueries
{
    public class UserCnQ
    {
        private readonly DataContext _context;
        private readonly PasswordService _pwdService;
        private readonly PrintOutputService _printOutput;
        public UserCnQ(DataContext context, PasswordService pwdService, PrintOutputService printOutput)
        {
            _context = context;
            _pwdService = pwdService;
            _printOutput = printOutput;
        }

        public List<UserOutput> Get()
        {
            IEnumerable<User> users = _context.User.Where(user => user.Deleted == false);
            return _printOutput.Users(users);
        }

        public List<UserOutput> Get(Guid id)
        {
            IEnumerable<User> user = (IEnumerable<User>)_context.User.FirstOrDefault(user => user.Id == id && user.Deleted == false);
            return _printOutput.Users(user);
        }

        public UserOutput Post(UserInput input)
        {
            var Salt = _pwdService.CreateSalt();
            var Password = _pwdService.HashPassword(input.Password, Salt);
            var user = new User()
            {
                Email = input.Email,
                Password = Password,
                Salt = Salt,
                FirstName = input.FirstName,
                LastName = input.LastName,
                PhotoName = input.PhotoName
            };

            _context.Add(user);
            _context.SaveChanges();

            return _printOutput.User(user);
        }

        protected UserOutput Put(UserPutInput input)
        {
            var user = _context.User.FirstOrDefault(user => user.Id == input.Id);
            if (user == null) return null;
          
            user.Email = input.Email ?? user.Email;
            user.Password = _pwdService.HashPassword(input.Password, user.Salt) ?? user.Password;
            user.FirstName = input.FirstName ?? user.FirstName;
            user.LastName = input.LastName ?? user.LastName;
            user.PhotoName = input.PhotoName ?? user.PhotoName;

            _context.SaveChanges();

            return _printOutput.User(user);
        }
    }
}
