using System.ComponentModel.DataAnnotations;

namespace DreamTimeS224.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; } = null!;  // Suppress null warnings using the ! (null-forgiving) operator


        // Reference (navigation property) back to the list of Books
        public ICollection<Book>? Books { get; set; } = null;
    }
}