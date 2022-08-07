using AlexPWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace AlexPWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
    }
}
