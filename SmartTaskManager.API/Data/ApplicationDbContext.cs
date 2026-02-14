using Microsoft.EntityFrameworkCore;
using SmartTaskManager.API.Models;

namespace SmartTaskManager.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

    }
}
