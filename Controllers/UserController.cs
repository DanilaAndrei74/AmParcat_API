using Backend.Data.CommandsNQueries;
using Backend.Data.Database.Context;
using Backend.Data.Database.Entity;
using Backend.Data.Models.Input;
using Backend.Data.Models.Output;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private readonly DataContext _context;
        private readonly PrintOutputService _printOutput;
        private readonly PasswordService _passwordService;
        public UserController(DataContext context, PrintOutputService printOutput, PasswordService passwordService)
        {
            _context = context;
            _printOutput = printOutput;
            _passwordService = passwordService;
        }

        [HttpGet]
        public ActionResult<List<UserOutput>> Get()
        {
            IEnumerable<User> users = _context.User.Where(user => user.Deleted == false);
            return _printOutput.Users(users);
        }

        [HttpPost]
        public ActionResult<UserOutput> Post(UserInput input)
        {
            var check = _context.User.FirstOrDefault(user => user.Deleted == false && user.Email == input.Email);
            if (check != null) return BadRequest();

            var Salt = _passwordService.CreateSalt();
            var Password = _passwordService.HashPassword(input.Password, Salt);
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

            return Ok(_printOutput.User(user));
        }

        [HttpPut]
        public ActionResult<UserOutput> Put(UserPutInput input)
        {
            var user = _context.User.FirstOrDefault(user => user.Id == input.Id);
            if (user == null) return NotFound();

            user.Email = input.Email ?? user.Email;
            user.Password = _passwordService.HashPassword(input.Password, user.Salt) ?? user.Password;
            user.FirstName = input.FirstName ?? user.FirstName;
            user.LastName = input.LastName ?? user.LastName;
            user.PhotoName = input.PhotoName ?? user.PhotoName;
            user.Deleted = input.Deleted ?? false;

            _context.SaveChanges();

            return Ok(_printOutput.User(user));
        }

        [HttpDelete("{userId}")]
        public ActionResult Delete(Guid userId)
        {
            var user = _context.User.FirstOrDefault(user => user.Id == userId);
            if (user == null) return NotFound();

            user.Deleted = true;

            _context.SaveChanges();

            return NoContent();
        }
    }
}