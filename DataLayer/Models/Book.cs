﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public int ShelfId { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Name must be at most 20 characters long.")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Name can only contain letters.")]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Aurther must be at most 20 characters long.")]
        public string Aurther { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a positive number.")]
        public int Quantity { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
        public byte[]? PDF { get; set; }
        [NotMapped]
        
        public IFormFile? PDFFile { get; set; }
        public Shelf? Shelf { get; set; }



    }

}
