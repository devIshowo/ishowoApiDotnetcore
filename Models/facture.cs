using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ItCommerce.DTO.ModelDesign
{
    public class facture
    {
        public int id { get; set; }
        public string QRCode_Mecef { get; set; }
        public string NIM_Mecef { get; set; }
        [Required]
        public string Code_Mecef { get; set; }
        [Required]
        public string Compteur_Total_Mecef { get; set; }
        [Required]
        public string Compteur_Type_Facture_Mecef { get; set; }
        [Required]
        public string Date_Mecef { get; set; }
        [Required]
        public bool API { get; set; }
        public string IFU { get; set; }
        public int id_vente { get; set; }

        [ForeignKey("id_vente")]
        public virtual vente_produit vente_produit { get; set; }
    }
}
