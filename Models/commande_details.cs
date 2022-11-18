using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ItCommerce.DTO.ModelDesign
{
    using System;
    using System.Collections.Generic;
    
    public class commande_details
    {
        public int id { get; set; }
        [Required]
        public int qte_cmde { get; set; }
        public Nullable<int> qte_recue { get; set; }
        public string statut { get; set; }
        [Required]
        public int id_cmde { get; set; }
        [Required]
        public int p_achat { get; set; }
        [Required]
        public int id_produit_mesure { get; set; }
        [Required]
        public Nullable<int> p_vente { get; set; }
        public string reference { get; set; }
        public Nullable<System.DateTime> date_exp { get; set; }
    
        [ForeignKey("id_cmde")]
        public virtual commande commande { get; set; }
        [ForeignKey("id_produit_mesure")]
        public  virtual produit_type_mesure produit_type_mesure { get; set; }
    }
}
