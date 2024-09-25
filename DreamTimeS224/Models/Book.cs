﻿using System.ComponentModel.DataAnnotations;

namespace DreamTimeS224.Models
{
    public class Book
    {
        [Key]
        [MaxLength(13)]
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
        public string ImageFilename { get; set; }
        
        [Required]
        [Range(0,9999)]
        public int Copies { get; set; }
        
        [MaxLength(50)]
        public string Publisher { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DatePublished { get; set; }

        [MaxLength(20)]
        public string Edition { get; set; }

        // Links/relationships/associations with other entities
        [Required]
        public Genre Genre { get; set; }
        public int GenreId { get; set; }  // If this is not specified, EF will create a property/field for the foreign key
    }
}
