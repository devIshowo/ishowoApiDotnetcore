using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ItCommerce.DTO.ModelDesign
{
    using System;
    using System.Collections.Generic;
    
    public class banque
    {
        public banque()
        {
            this.comptes = new List<compte>();
        }
    
        public int id { get; set; }
        [Required]
        public string nom { get; set; }
        public string adresse { get; set; }
        public string contact { get; set; }
        [Required]
        public int id_entreprise { get; set; }
    
        [ForeignKey("id_entreprise")]
        public  virtual entreprise entreprise { get; set; }
        public  virtual ICollection<compte> comptes { get; set; }
    }
}
