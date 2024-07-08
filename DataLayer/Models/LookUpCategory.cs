using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class LookUpCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }// fantasy / history ...
        public string Code { get; set; }
       
        public DateTime CreationDate { get; set; }
        public List<LookUp>? lookUps { get; set; }
    }
}
