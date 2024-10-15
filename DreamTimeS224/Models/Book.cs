using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DreamTimeS224.Models
{
    public class Book
    {
        [Key]
        [MaxLength(13)]
        [RegularExpression(@"^(\d{10}|\d{13})$", ErrorMessage = "Must be a 10- or 13-digit ISBN")]
        public string ISBN { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Author { get; set; }
        
        [Required]
        [Range(0, 9999)]
        public int Pages { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        [Required]
        [DataType(DataType.ImageUrl)]
        [DisplayName("Image")]
        public string ImageFilename { get; set; }
        
        [Required]
        [Range(0,9999)]
        public int Copies { get; set; }
        
        [MaxLength(50)]
        public string Publisher { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d MMM yyyy}")]
        [DisplayName("Publish Date")]
        public DateTime DatePublished { get; set; }

        [MaxLength(20)]
        public string Edition { get; set; }

        // Links/relationships/associations with other entities
        [Required]
        public Genre Genre { get; set; }
        
        [DisplayName("Genre")]
        public int GenreId { get; set; }  // If this is not specified, EF will create a property/field for the foreign key
    }
}
