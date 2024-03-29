﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Share2Connect.Api.Context;
using Share2Connect.Api.Encryption;
using Share2Connect.Api.Models;

namespace Share2Connect.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private ApplicationDbContext _context;
        private IConfiguration _config;

        public RegisterController(ApplicationDbContext context, IConfiguration config)
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
                    userNameText = user.userNameText,
                    userMail = user.userMail,
                    userPassword = Encrypt.GenerateMD5HashedPassword(user.userPassword),
                    userGender = user.userGender,
                    userBio = user.userBio,
                    userPhoneNumber = user.userPhoneNumber,
                    userImage = user.userImage,
                    userDepartment = user.userDepartment
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

            return BadRequest(new {
                status = 400,
                message = "Request rejected. User already exist",
            });
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
               FirstOrDefault(o => o.userMail.ToLower() == user.userMail.ToLower() ||
               o.userNameText.ToLower() == user.userNameText.ToLower());

            if (currentUser != null) return true;

            return false;
        }
    }
}
