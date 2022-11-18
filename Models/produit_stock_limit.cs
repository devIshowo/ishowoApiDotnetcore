using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ItCommerce.DTO.ModelDesign
{
    public class produit_stock_limit
    {

        public int id { get; set; }
        [Required]
        public int id_produit { get; set; }
        [Required]
        public int id_produit_type_mesure { get; set; }
        public int quantite { get; set; }
        public string reference { get; set; }

        [ForeignKey("id_produit")]
        public virtual produit produit { get; set; }

        [ForeignKey("id_produit_type_mesure")]
        public virtual produit_type_mesure produit_type_mesure { get; set; }
    }
}


