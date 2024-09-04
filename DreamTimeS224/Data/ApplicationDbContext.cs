using DreamTimeS224.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DreamTimeS224.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        // Create DbSet properties for each entity to track with Entity Framework
        public DbSet<Genre> Genres { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
