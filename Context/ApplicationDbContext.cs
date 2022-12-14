using Microsoft.EntityFrameworkCore;
using Share2Connect.Api.Models;

namespace Share2Connect.Api.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() {}
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        public virtual DbSet<Announcement> Announcements { get; set; }
        public virtual DbSet<User> Users { get; set; }

    }
}
