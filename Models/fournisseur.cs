using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ItCommerce.DTO.ModelDesign
{
    using System;
    using System.Collections.Generic;
    
    public class fournisseur
    {
        public fournisseur()
        {
            this.commandes = new List<commande>();
        }
    
        public int id { get; set; }
        [Required]
        public string nom { get; set; }
        [Required]
        public string telephone { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string adresse { get; set; }
        [Required]
        public int id_entreprise { get; set; }
        public Nullable<int> solde { get; set; }
    
        public  virtual ICollection<commande> commandes { get; set; }
        [ForeignKey("id_entreprise")]
        public  virtual entreprise entreprise { get; set; }
    }
}
