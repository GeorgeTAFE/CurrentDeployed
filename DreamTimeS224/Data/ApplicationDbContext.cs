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

            builder.Entity<SessionType>().HasData(
                new SessionType { Id = 1, Name = "Morning" },
                new SessionType { Id = 2, Name = "Afternoon" },
                new SessionType { Id = 3, Name = "Evening" }
            );


            /*
             * Generate a list of timeslots in 30-minute increments
             */

            // List to hold timeslots
            List<Timeslot> timeslots = new List<Timeslot>();

            // OPTION 1: Use TimeOnly to ignore the irrelevant date as much as possible
            //TimeOnly startTime = new TimeOnly(8, 0, 0);
            //TimeOnly endTime = new TimeOnly(22, 0, 0);
            //for (TimeOnly currentTime = startTime; currentTime <= endTime; currentTime = currentTime.AddMinutes(30))
            //{
            //    timeslots.Add(new Timeslot { Time = new DateTime(new DateOnly(), currentTime) });
            //}

            // OPTION 2: Use DateTime and a simple int in the loop
            DateTime startDateTime = new DateTime(1, 1, 1, 8, 0, 0);  // 8:00AM
            DateTime endDateTime = new DateTime(1, 1, 1, 22, 0, 0);  // 10:00PM
            for (int minutes = 0; startDateTime.AddMinutes(minutes) <= endDateTime; minutes += 30)
            {
                timeslots.Add(new Timeslot { Time = startDateTime.AddMinutes(minutes) });
            }
            
            // Add list of timeslots to EF collection
            builder.Entity<Timeslot>().HasData(timeslots);

            // OPTION 3: Manually add each timeslot to EF collection
            //builder.Entity<Timeslot>().HasData(
            //    new Timeslot { Time = new DateTime(2000, 1, 1, 8, 0, 0) },  // 8:00 AM
            //    new Timeslot { Time = new DateTime(2000, 1, 1, 8, 30, 0) },  // 8:30 AM
            //    new Timeslot { Time = new DateTime(2000, 1, 1, 9, 0, 0) }  // 9:00 AM
            //);


            /*
             * Define foreign key cascade rules
             */

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
