using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    public class Shelf
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Name must be at most 20 characters long.")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Name can only contain letters.")]
        public string Name { get; set; }
        [Required]

        public LookUp Type { get; set; }
        [Required]
        public bool IsActived { get; set; }
        [Required]
        public bool IsDeleted { get; set; } 
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a positive number.")]
        public int BookCount { get; set; }

        
        public List<Book>? Books { get; set; }

    }
}
