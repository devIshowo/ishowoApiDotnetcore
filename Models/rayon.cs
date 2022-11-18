using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ItCommerce.DTO.ModelDesign
{
    using System;
    using System.Collections.Generic;

    public class rayon
    {
        public rayon()
        {
            this.commandes = new List<commande>();
            this.stock_archives = new List<stock_archives>();
            this.stocks = new List<stock>();
            //this.transferts = new List<transfert>();
            //this.transferts1 = new List<transfert>();
            this.vente_details = new List<vente_produit_details>();
        }

        public int id { get; set; }
        [Required]
        public string nom { get; set; }
        public Nullable<bool> is_magasin { get; set; }
        [Required]
        public int id_agence { get; set; }

        [ForeignKey("id_agence")]
        public virtual agence agence { get; set; }
        public virtual ICollection<commande> commandes { get; set; }
        public virtual ICollection<stock_archives> stock_archives { get; set; }
        public virtual ICollection<stock> stocks { get; set; }
        //public  List<transfert> transferts { get; set; }
        //public  List<transfert> transferts1 { get; set; }
        public virtual ICollection<vente_produit_details> vente_details { get; set; }
    }
}
