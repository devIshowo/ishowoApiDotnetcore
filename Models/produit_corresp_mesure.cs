using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ItCommerce.DTO.ModelDesign
{
    using System;
    using System.Collections.Generic;
    
    public class produit_corresp_mesure
    {
        public int id { get; set; }
        [Required]
        public int id_produit_mesure_parent { get; set; }
        [Required]
        public int id_produit_mesure_enfant { get; set; }
        [Required]
        public int quantite { get; set; }

        [ForeignKey("id_produit_mesure_parent")]
        public virtual produit_type_mesure produit_type_mesure_parent { get; set; }
        [ForeignKey("id_produit_mesure_enfant")]
        public virtual produit_type_mesure produit_type_mesure_enfant { get; set; }
    }




}
