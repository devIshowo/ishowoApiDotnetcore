using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ItCommerce.DTO.ModelDesign
{
    using System;
    using System.Collections.Generic;
    
    public  class transfert_details
    {
        public int id { get; set; }
        [Required]
        public int id_produit_mesure { get; set; }
        [Required]
        public int id_transfert { get; set; }
        [Required]
        public int quantite { get; set; }
        [Required]
        public int p_achat { get; set; }
        [Required]
        public int p_vente { get; set; }
    
        [ForeignKey("id_produit_mesure")]
        public  virtual produit_type_mesure produit_type_mesure { get; set; }
        [ForeignKey("id_transfert")]
        public  virtual transfert transfert { get; set; }
    }
}
