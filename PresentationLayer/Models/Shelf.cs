﻿using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Shelf
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Name must be at most 20 characters long.")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Name can only contain letters.")]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }

        [Required]


        public bool IsActived { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a positive number.")]
        public int BookCount { get; set; }


        public List<Book>? Books { get; set; }

    }
}
