using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ItCommerce.DTO.ModelDesign
{
    using System;
    using System.Collections.Generic;
    
    public class categ_produit
    {
        public categ_produit()
        {
            this.produits = new List<produit>();
        }
    
        public int id { get; set; }
        [Required]
        public string nom { get; set; }
        [Required]
        public int id_entreprise { get; set; }
    
        [ForeignKey("id_entreprise")]
        public  virtual entreprise entreprise { get; set; }
        public  virtual ICollection<produit> produits { get; set; }
    }
}
