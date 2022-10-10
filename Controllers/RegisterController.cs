using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Share2Connect.Backend.Context;
using Share2Connect.Backend.Encryption;
using Share2Connect.Backend.Models;

namespace Share2Connect.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private UserDbContext _context;

        public RegisterController(UserDbContext context)
        {
            _context = context;
        }
        
        /// <summary>
        /// Register new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IActionResult Register([FromBody] User user)
        {
            // validate if user exist
            var doesUserExist = ValidateUser(user);

            if(!doesUserExist)
            {
                // create user 
                _context.Users.Add(new User { FullName = user.FullName, Email = user.Email, 
                    Password = Encrypt.GenerateMD5HashedPassword(user.Password), Gender = user.Gender } );

                // save user
                _context.SaveChanges();

                return Ok("user created");
            }

            return BadRequest("Something went wrong. User seems to exist");
        }

        /// <summary>
        /// Validate user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private bool ValidateUser(User user)
        {
            // check if user exist
            var currentUser = _context.Users.
               FirstOrDefault(o => o.Email.ToLower() == user.Email.ToLower()
               && o.Password == Encrypt.GenerateMD5HashedPassword(user.Password));

            if (currentUser != null) return true;

            return false;
        }
    }
}
