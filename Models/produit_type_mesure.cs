using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ItCommerce.DTO.ModelDesign
{
    using System;
    using System.Collections.Generic;
    
    public class produit_type_mesure
    {
         public produit_type_mesure()
        {
            this.commande_details = new List<commande_details>();
            //this.produit_corresp_mesure = new List<produit_corresp_mesure>();
            //this.produit_corresp_mesure1 = new List<produit_corresp_mesure>();
            this.stock_archives = new List<stock_archives>();
            this.stocks = new List<stock>();
            this.transfert_details = new List<transfert_details>();
            this.vente_details = new List<vente_produit_details>(); 
        }
    
        public int id { get; set; }
        [Required]
        public int id_produit { get; set; }
        [Required]
        public int id_type_mesure { get; set; }
        [Required]
        public int quantite { get; set; }
        public string reference { get; set; }
    
        public  virtual ICollection<commande_details> commande_details { get; set; }

        [ForeignKey("id_produit")]
        public  virtual produit produit { get; set; }

        public  virtual ICollection<produit_corresp_mesure> produit_corresp_mesure_parents { get; set; }
        public  virtual ICollection<produit_corresp_mesure> produit_corresp_mesure_enfants { get; set; }

        [ForeignKey("id_type_mesure")]
        public  virtual type_mesure type_mesure { get; set; }
        public  virtual ICollection<stock_archives> stock_archives { get; set; }
        public  virtual ICollection<stock> stocks { get; set; }
        public  virtual ICollection<transfert_details> transfert_details { get; set; }
        public virtual ICollection<vente_produit_details> vente_details { get; set; }
    }
}
