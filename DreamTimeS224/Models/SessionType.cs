﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DreamTimeS224.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class SessionType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Must be 2-20 characters")]
        public string Name { get; set; } = null!;
    }
}
