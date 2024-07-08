using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class LookUp
    {
        public int Id { get; set; }
        public int LookUpCategoryId { get; set; }
        public string Name { get; set; } 
        public string Code { get; set; }
        public DateTime CreationDate { get; set; }
 
    }
}
