using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ItCommerce.DTO.ModelDesign
{
    using System;
    using System.Collections.Generic;
    
    public class vente_produit
    {
        public vente_produit()
        {
            this.vente_details = new List<vente_produit_details>();
        }
    
        public int id { get; set; }
        [Required]
        public System.DateTime date_vente { get; set; }
        [Required]
        public double mt_original { get; set; }
        [Required]
        public double mt_remise { get; set; }
        [Required]
        public double mt_tva { get; set; }
        [Required]
        public double mt_a_payer { get; set; }
        [Required]
        public double mt_recu { get; set; }
        [Required]
        public double reliquat { get; set; }
        [Required]
        public double reste_a_payer { get; set; }
        public double t_aib { get; set; }
        public double mt_aib { get; set; }
        public Nullable<int> id_categ_vente { get; set; }
        [Required]
        public int id_agence { get; set; }
        public Nullable<int> id_client { get; set; }
        [Required]
        public int id_profil { get; set; }
        public Nullable<bool> avec_facture { get; set; }
        [Required]
        public string reference { get; set; }
        public bool is_devis { get; set; }
        public  virtual ICollection<vente_produit_details> vente_details { get; set; }

        [ForeignKey("id_agence")]
        public  virtual agence agence { get; set; }
        
        [ForeignKey("id_categ_vente")]
        public  virtual categ_vente categ_vente { get; set; }
        
        [ForeignKey("id_profil")]
        public virtual profil profil { get; set; }
        
        [ForeignKey("id_client")]
        public  virtual client client { get; set; }
    }
}
