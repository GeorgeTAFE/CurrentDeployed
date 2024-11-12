using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamTimeS224.Models
{
    // Add index to table/entity to enforce unique constraint on Name property
    [Index(nameof(Name), IsUnique = true)]
    public class RoomType
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "I require a name!")]
        [StringLength(80, MinimumLength = 2, ErrorMessage = "Must be between 2-80 characters")]
        public string Name { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Must be between 0-100")]
        public int Capacity { get; set; }

        public RoomType()
        {
            Name = "";
        }

        public RoomType(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
        }

        // new RoomType()
        // new RoomType("My Room", 100)


        // Reference (navigation property) back to the list of Rooms
        public ICollection<Room>? Rooms { get; set; } = null;
    }
}
