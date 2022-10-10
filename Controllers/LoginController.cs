﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.IdentityModel.Tokens;
using System.CodeDom.Compiler;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Share2Connect.Backend.Encryption;
using Share2Connect.Backend.Context;
using Share2Connect.Backend.Models;

namespace Share2Connect.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private UserDbContext _context;

        // inject config
        public LoginController(IConfiguration config, UserDbContext context)
        {
            _config = config;
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost]
        [Produces("application/json")]
        public IActionResult Login([FromBody] UserAuthentication userLogin)
        {
            var user = Authenticate(userLogin);

            if (user != null)
            {
                // get session token 
                var _token = Encrypt.GenerateSessionToken(user, _config);

                // create a response 
                var response = new
                {
                    token = _token,
                    message = "authenticated successfully",
                    status = "200"
                };

                return Ok(response);
            }

            return NotFound("User not found. Please check entered credentials");
        }

        /// <summary>
        /// Authenticates user
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        private User Authenticate(UserAuthentication userLogin)
        {
            // check if user exist
            var currentUser = _context.Users.
           FirstOrDefault(o => o.Email.ToLower() == userLogin.Email.ToLower()
           && o.Password == Encrypt.GenerateMD5HashedPassword(userLogin.Password));

            if (currentUser != null) return currentUser;

            return null;
        }
    }
}
