using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ItCommerce.DTO.ModelDesign
{
    using System;
    using System.Collections.Generic;

    public class profil
    {
        public profil()
        {
            this.commandes = new List<commande>();
            this.comptes = new List<compte>();
            this.mouvement_compte = new List<mouvement_compte>();
            this.objectif_vente = new List<objectif_vente>();
            this.transferts = new List<transfert>();
            //this.vente_produit = new List<vente_produit>(); newnew
            this.vente_service = new List<vente_service>();
        }

        public int id { get; set; }
        [Required]
        public string login { get; set; } = string.Empty;
        [Required]
        public string pwd { get; set; }
        [Required]
        public int id_user { get; set; }
        [Required]
        public int id_groupe { get; set; }
        [Required]
        public int id_agence { get; set; }
        public string refreshtoken { get; set; } = string.Empty;
        public DateTime? date_creation { get; set; }
        public DateTime? tokencreated{ get; set; }
        public DateTime? tokenexpires { get; set; }
       

        [ForeignKey("id_agence")]
        public virtual agence agence { get; set; }

        public virtual ICollection<commande> commandes { get; set; }
        public virtual ICollection<compte> comptes { get; set; }

        [ForeignKey("id_groupe")]
        public virtual groupe groupe { get; set; }

        public virtual ICollection<mouvement_compte> mouvement_compte { get; set; }
        public virtual ICollection<objectif_vente> objectif_vente { get; set; }

        [ForeignKey("id_user")]
        public virtual utilisateur user { get; set; }

        public virtual ICollection<transfert> transferts { get; set; }
        //public virtual ICollection<vente_produit> vente_produit { get; set; } newnew
        public virtual ICollection<vente_service> vente_service { get; set; }
    }
}
