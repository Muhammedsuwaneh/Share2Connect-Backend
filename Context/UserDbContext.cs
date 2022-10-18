using Microsoft.EntityFrameworkCore;
using Share2Connect.Api.Models;

namespace Share2Connect.Api.Context
{
    public partial class UserDbContext : DbContext
    {
        public UserDbContext() { }

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                throw new NotImplementedException();
            }
        }
    }
}
