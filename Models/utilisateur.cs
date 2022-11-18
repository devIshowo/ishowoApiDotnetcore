using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ItCommerce.DTO.ModelDesign
{
    using System;
    using System.Collections.Generic;
    
    public class utilisateur 
    {
        public utilisateur()
        {
            this.profils = new List<profil>();
            //this.vente_produit = new List<vente_produit>(); newnew
            this.vente_service = new List<vente_service>();
        }
    
        public int id { get; set; }
        [Required]
        public string nom { get; set; }
        [Required]
        public string prenoms { get; set; }
        [Required]
        public string telephone { get; set; }
        [Required]
        public string adresse { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string token { get; set; } 
        [Required]
        public int id_entreprise { get; set; }
        [Required]
        public Nullable<int> solde { get; set; }
        public DateTime? date_creation { get; set; }
        public DateTime? date_verification { get; set; }

        [ForeignKey("id_entreprise")]
        public  virtual entreprise entreprise { get; set; }
        public  virtual ICollection<profil> profils { get; set; }
        //public  virtual ICollection<vente_produit> vente_produit { get; set; } newnew
        public  virtual ICollection<vente_service> vente_service { get; set; }
    }
}
