using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Share2Connect.Api.Context;
using Share2Connect.Api.Models;
using System.Security.Claims;
using XAct;
using XAct.Users;

namespace Share2Connect.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementsController : ControllerBase
    {
        private ApplicationDbContext _context;
        public AnnouncementsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // task 
        // 1. New announcement = POST - https://localhost:7195/api/announcements/new
        [HttpPost("new/")]
        public IActionResult New([FromBody] Announcement announcement)
        {
            if (announcement == null) 
                return BadRequest(new { status = 400, message = "request rejected, null announcement" });

            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;

                var userId = (userClaims.FirstOrDefault(o => o.Type == ClaimTypes.PrimarySid)?.Value).ToInt32();

                var newAnnouncement = new Announcement
                {
                    Category = announcement.Category,
                    User_id = userId,
                    Data = announcement.Data
                };

                _context.Announcements.Add(newAnnouncement);
                _context.SaveChanges();

                return Ok(new {
                   status = 200,
                   message = "Announcement saved",
                   data = newAnnouncement
                });
            }

            return NotFound(new { status = 404, message = "invalid request, no user info attached to post" });
        }

        // 2. Get announcements = GET - https://localhost:7195/api/announcements/all
        [HttpGet("all/")]
        public IActionResult Get()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var announcements = _context.Announcements.Include(a => a.Data)
                    .Include(a => a.Data.Participants).ToList();

                string _message = "announcements retrieved";

                if (announcements.Count == 0)
                    _message = "no announcements not found";

                return Ok(new
                {
                    status = 200,
                    message = _message,
                    data = announcements
                });
            }

            return NotFound(new { status = 404, message = "no valid user found" });
        }

        // 3. Get announcement by category - GET - https://localhost:7195/api/announcements/category/category
        [HttpGet("category/{category}")]
        public IActionResult GetAnnouncementsByCategory(string category)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var announcements = _context.Announcements.Include(a => a.Data).Include(a => a.Data.Participants)
                    .Where(a => a.Category == category)
                    .ToList();

                string _message = $"announcements of the category {category} retrieved";

                if (announcements.Count == 0)
                    _message = $"no announcements of the category {category} found";
                    

                return Ok(new
                {
                    status = 200,
                    message = _message,
                    data = announcements
                });
            }

            return NotFound(new { status = 404, message = "no valid user found" });
        }

        // 4. Get announcement by id - GET - https://localhost:7195/api/announcements/id
        [HttpGet("{id}")]
        public IActionResult GetAnnouncementById(int id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var announcement = _context.Announcements
                    .Include(a => a.Data)
                    .Include(a => a.Data.Participants)
                    .FirstOrDefault(a => a.Post_id == id);

                if(announcement == null) return NotFound(new { status = 404, messsage = "announcement not found" });

                return Ok(new
                {
                    status = 200,
                    message = "announcement retrieved",
                    data = announcement
                });
            }

            return NotFound(new { status = 404, message = "no valid user found" });
        }

        // 5. Get announcements by user - GET - https://localhost:7195/api/announcements/user/user_id,
        [HttpGet("user/{user_id}")]
        public IActionResult GetAnnouncementsByUserId(int user_id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var announcements = _context.Announcements
                    .Include(a => a.Data)
                    .Include(a => a.Data.Participants)
                    .Where(a => a.User_id == user_id).ToList();

                string _message = "announcement retrieved";

                if (announcements.Count == 0) 
                    _message = "announcements not found";

                return Ok(new
                {
                    status = 200,
                    message = _message,
                    data = announcements
                });
            }

            return NotFound(new { status = 404, message = "no valid user found" });
        }

        // 6. Delete announcement - DELETE - https://localhost:7195/api/announcements/delete/id
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;

                // for security reasons, there is a need to verify a logged in user's identity before performing any database action
                var userId = (userClaims.FirstOrDefault(o => o.Type == ClaimTypes.PrimarySid)?.Value).ToInt32();

                // get announcement to be updated from db - to avoid deleting another user's record we search for an announcement with 
                // the id of the logged in user
                var announcement = _context.Announcements
                    .Include(a => a.Data)
                    .Include(a => a.Data.Participants)
                    .FirstOrDefault(a => a.Post_id == id && a.User_id == userId);

                if (announcement == null) return NotFound(new { status = 404, message = "no announcement found" });

                _context.Announcements.Remove(announcement);

                _context.SaveChanges();

                return Ok(new { status = 200, message = "announcement deleted successfully"});
            }

            return BadRequest(new { status = 403, message = "unauthorized request" });
        }

        // 7. Update announcement - PUT - https://localhost:7195/api/announcements/update/
        [HttpPut("update/")]
        public IActionResult Update([FromBody] Announcement _announcement)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;

                // get user id 
                var userId = (userClaims.FirstOrDefault(o => o.Type == ClaimTypes.PrimarySid)?.Value).ToInt32();

                // get announcement to be updated from db - to avoid updating another user's record we search for an announcement with 
                // the id of the logged in user
                var announcement = _context.Announcements
                    .Include(a => a.Data)
                    .Include(a => a.Data.Participants)
                    .FirstOrDefault(a => a.Post_id == _announcement.Post_id && a.User_id == userId);

                if (announcement == null) return NotFound(new { status = 404, message = "no announcement found" });

                // update changes
                announcement.Category = _announcement.Category;
                announcement.Data = _announcement.Data;

                // save updated changes
                _context.SaveChanges();

                return Ok(new { status = 200, message = "announcement updated successfully" });
            }

            return BadRequest(new { status = 403, message = "unauthorized request" });
        }
    }
}
