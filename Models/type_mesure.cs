using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ItCommerce.DTO.ModelDesign
{
    using System;
    using System.Collections.Generic;
    
    public class type_mesure
    {
        public type_mesure()
        {
            this.produit_type_mesure = new List<produit_type_mesure>();
        }
    
        public int id { get; set; }
        [Required]
        public string nom { get; set; }
        [Required]
        public int id_entreprise { get; set; }
    
        [ForeignKey("id_entreprise")]
        public  virtual entreprise entreprise { get; set; }
        public  virtual ICollection<produit_type_mesure> produit_type_mesure { get; set; }
    }
}
