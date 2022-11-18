using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ItCommerce.DTO.ModelDesign
{
    public class role
    {
        public int id { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        public string code { get; set; }
        public  bool statut { get; set; }
    }
}
