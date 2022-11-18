using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ItCommerce.DTO.ModelDesign
{
    using System;
    using System.Collections.Generic;
    
    public  class vente_produit_details
    {
        public int id { get; set; }
        [Required]
        public int quantite { get; set; }
        [Required]
        public int id_vente { get; set; }
        [Required]
        public int p_achat { get; set; }
        [Required]
        public int p_vente { get; set; }
        public double mt_remise { get; set; }
        public double mt_tva { get; set; }
        [Required]
        public string tax { get; set; }
        public string libellets { get; set; }
        public Nullable <int> ts { get; set; }
        [Required]
        public int id_produit_mesure { get; set; }
        [Required]
        public int id_rayon { get; set; }
    
        [ForeignKey("id_produit_mesure")]
        public  virtual produit_type_mesure produit_type_mesure { get; set; }
        [ForeignKey("id_rayon")]
        public  virtual rayon rayon { get; set; }
        [ForeignKey("id_vente")]
        public  virtual vente_produit vente { get; set; }
    }
}
