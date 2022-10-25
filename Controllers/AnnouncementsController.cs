using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Share2Connect.Api.Context;

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
        // 1. Save announcement = POST

        // 2. Get announcements = GET

        // 3. Get announcement by Id = GET/id

        // 4. Delete announcement = DELETE/id 

        // 5. update announcement = PUT
    }
}
