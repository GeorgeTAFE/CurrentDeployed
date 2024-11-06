using DreamTimeS224.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DreamTimeS224.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        // Create DbSet properties for each entity to track with Entity Framework
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Room> Rooms { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // In here, we can add custom configuration for our entities



            // Define foreign key cascade rules
            builder.Entity<Room>()
                .HasOne(e => e.RoomType)
                .WithMany(rt => rt.Rooms)
                .HasForeignKey(e => e.RoomTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Add customisation for our models/entities
            base.OnModelCreating(builder);
        }
    }
}
