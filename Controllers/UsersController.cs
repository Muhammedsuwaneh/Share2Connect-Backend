using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Share2Connect.Api.Models;
using XAct;
using Share2Connect.Api.Context;
using XAct.Users;

namespace Share2Connect.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController] 
    public class UsersController : ControllerBase
    {
        private ApplicationDbContext _context; 

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("get-user-detail")]
        public IActionResult GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if(identity != null)
            {
                var userClaims = identity.Claims;

                var userId = (userClaims.FirstOrDefault(o => o.Type == ClaimTypes.PrimarySid)?.Value).ToInt32();

                var user = _context.Users.FirstOrDefault(o => o.userId == userId);

                var response = new
                {   
                    status = 200,
                    message = "success",
                    User = user,
                };

                return Ok(response);
            }

            return NotFound(new
            {
                status = 404,
                message = "User not found. Please verify entered credentials",
            });
        }

        [HttpGet("get-user/{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _context.Users.FirstOrDefault(o => o.userId == id);

            var response = new
            {
                status = 200,
                message = "success",
                user = user
            };

            if (user != null) return Ok(response);

            return NotFound(new
            {
                status = 404,
                message = "User not found. Please verify entered credentials",
            });
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _context.Users.ToList();

            var response = new {
                status = 200,
                message = "success",
                Users = users
            };

            return Ok(response);
        }
    }
}
