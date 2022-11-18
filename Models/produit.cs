using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ItCommerce.DTO.ModelDesign
{
    using System;
    using System.Collections.Generic;
    
    public class produit
    {
         public produit()
        {
            this.produit_type_mesure = new List<produit_type_mesure>();
            this.produit_corresp_mesure = new List<produit_corresp_mesure>();
        }
    
        public int id { get; set; }
        [Required]
        public string nom { get; set; }
        [Required]
        public int id_categ { get; set; }
        [Required]
        public int id_entreprise { get; set; }
        //[Required]
        public string description { get; set; }

        public string image { get; set; }
        public string code_interne { get; set; }
        public string code_fabricant { get; set; }
    
        [ForeignKey("id_categ")]
        public  virtual categ_produit categ_produit { get; set; }
        [ForeignKey("id_entreprise")]
        public  virtual entreprise entreprise { get; set; }
        public  virtual ICollection<produit_type_mesure> produit_type_mesure { get; set; }
        public  virtual ICollection<produit_corresp_mesure> produit_corresp_mesure { get; set; }
    }
}
