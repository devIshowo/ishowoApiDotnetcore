using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ItCommerce.DTO.ModelDesign
{
    using System;
    using System.Collections.Generic;
    
    public  class type_compte
    {
         public type_compte()
        {
            this.comptes = new List<compte>();
        }
    
        public int id { get; set; }
        [Required]
        public string nom { get; set; }
    
        public virtual List<compte> comptes { get; set; }
    }
}
