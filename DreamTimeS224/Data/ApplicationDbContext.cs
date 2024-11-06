using DreamTimeS224.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DreamTimeS224.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        // Create DbSet properties for each entity to track with Entity Framework
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Timeslot> Timeslots { get; set; }
        public DbSet<SessionType> SessionTypes { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // In here, we can add custom configuration for our entities

            // Auto-increment
            builder.Entity<RoomType>(e => e.Property(e => e.Id).ValueGeneratedOnAdd());

            // Add some seed data
            builder.Entity<RoomType>().HasData(
                new RoomType { Id = 1, Name = "Arcadia", Capacity = 130 },
                new RoomType { Id = 2, Name = "Monstrocity", Capacity = 3 },
                new RoomType { Id = 3, Name = "Book Worms", Capacity = 30}
            );

            builder.Entity<Room>().HasData(
                new Room { Code = "A01", RoomTypeId = 1 },
                new Room { Code = "A02", RoomTypeId = 1 },
                new Room { Code = "A03", RoomTypeId = 1 },
                new Room { Code = "M01", RoomTypeId = 2 },
                new Room { Code = "M02", RoomTypeId = 2 },
                new Room { Code = "M03", RoomTypeId = 2 },
                new Room { Code = "M04", RoomTypeId = 2 }
            );

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
