using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ItCommerce.DTO.ModelDesign
{
    using System;
    using System.Collections.Generic;
    
    public class commande
    {
        public commande()
        {
            this.commande_details = new List<commande_details>();
        }
    
        public int id { get; set; }
        [Required]
        public System.DateTime date_cmde { get; set; }
        public Nullable <int> id_fournisseur { get; set; }
        public string statut { get; set; }
        [Required]
        public int id_entreprise { get; set; }
        [Required]
        public int id_profil { get; set; }
        public Nullable<int> id_rayon { get; set; }
        [Required]
        public string reference { get; set; }
        [Required]
        public int montant_cmde { get; set; }
        [Required]
        public int montant_sorti { get; set; }
    
        public  virtual  List<commande_details> commande_details { get; set; }
        [ForeignKey("id_entreprise")]
        public  virtual entreprise entreprise { get; set; }
        [ForeignKey("id_fournisseur")]
        public  virtual fournisseur fournisseur { get; set; }
        [ForeignKey("id_profil")]
        public  virtual profil profil { get; set; }
        [ForeignKey("id_rayon")]
        public  virtual rayon rayon { get; set; }
    }
}
