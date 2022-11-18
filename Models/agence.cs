using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ItCommerce.DTO.ModelDesign
{
    using System;
    using System.Collections.Generic;
    
    public class agence
    {
       public agence()
        {
            this.objectif_vente = new List<objectif_vente>();
            this.profils = new List<profil>();
            this.rayons = new List<rayon>();
            this.ventes = new List<vente_produit>();
        }
    
        public int id { get; set; }
        [Required]
        public string nom { get; set; }
        [Required]
        public string localisation { get; set; }
        [Required]
        public int id_entreprise { get; set; }
    
        [ForeignKey("id_entreprise")]
        public virtual entreprise entreprise { get; set; }
        public  virtual ICollection<objectif_vente> objectif_vente { get; set; }
        public  virtual ICollection<profil> profils { get; set; }
        public  virtual ICollection<rayon> rayons { get; set; }
        public  virtual ICollection<vente_produit> ventes { get; set; }
    }
}
