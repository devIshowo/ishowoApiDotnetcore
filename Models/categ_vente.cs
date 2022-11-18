using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ItCommerce.DTO.ModelDesign
{
    using System;
    using System.Collections.Generic;
    
    public class categ_vente
    {
        public categ_vente()
        {
            this.ventes = new List<vente_produit>();
        }
    
        public int id { get; set; }
        [Required]
        public string libelle { get; set; }
    
        public virtual ICollection<vente_produit> ventes { get; set; }
    }
}
