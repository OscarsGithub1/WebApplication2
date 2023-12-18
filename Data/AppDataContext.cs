using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class AppDataContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
        {
        }
    }
}
