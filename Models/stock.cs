using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ItCommerce.DTO.ModelDesign
{
    using System;
    using System.Collections.Generic;
    
    public  class stock
    {
        public int id { get; set; }
        [Required]
        public int id_produit_mesure { get; set; }
        [Required]
        public int id_rayon { get; set; }
        [Required]
        public int qte_vendable { get; set; }
        [Required]
        public int qte_reelle { get; set; }
        [Required]
        public System.DateTime date_modif { get; set; }
        [Required]
        public int p_vente { get; set; }
        [Required]
        public int p_achat { get; set; }
        public string reference { get; set; }
        public Nullable<System.DateTime> date_exp { get; set; }
    
        [ForeignKey("id_produit_mesure")]
        public  virtual produit_type_mesure produit_type_mesure { get; set; }

        [ForeignKey("id_rayon")]
        public  virtual rayon rayon { get; set; }

    }
}
