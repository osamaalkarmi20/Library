using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.DTO
{
    public class CreationShelf
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Name must be at most 20 characters long.")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Name can only contain letters.")]
        public string Name { get; set; }
        [Required]

        public int Type { get; set; }
        [Required]


        public bool IsActived { get; set; }
        [Required]
        public bool IsDeleted { get; set; } 

        public List<Book>? Books { get; set; }

    }
}
