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
        private IConfiguration _config;

        public RegisterController(UserDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
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

                var newUser = new User
                {
                    FullName = user.FullName,
                    Email = user.Email,
                    Password = Encrypt.GenerateMD5HashedPassword(user.Password),
                    Gender = user.Gender
                };

                // create user 
                _context.Users.Add(newUser);

                // save user
                _context.SaveChanges();

                var _token = Encrypt.GenerateSessionToken(newUser, _config);

                var response = new
                {
                      status = 200,
                      token = _token,
                      message = "user successfully registered",
                      user = newUser
                };

                return Ok(response);
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
               FirstOrDefault(o => o.Email.ToLower() == user.Email.ToLower());

            if (currentUser != null) return true;

            return false;
        }
    }
}
