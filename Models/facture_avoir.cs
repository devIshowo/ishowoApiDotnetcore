using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ItCommerce.DTO.ModelDesign
{
    public class facture_avoir
    {
        public int id { get; set; }
        public string QRCode_Mecef_Avoir { get; set; }
        public string Code_Mecef_Avoir { get; set; }
        public string Compteur_Total_Mecef_Avoir { get; set; }
        public string Compteur_Type_Facture_Mecef_Avoir { get; set; }
        public string Date_Mecef_Avoir { get; set; }
        public int id_vente { get; set; }
        public int id_facture { get; set; }
        public string IFU { get; set; }
        public string NIM_Avoir { get; set; }

        [ForeignKey("id_vente")]
        public virtual vente_produit vente_produit { get; set; }
        [ForeignKey("id_facture")]
        public virtual facture facture { get; set; }
    }
}
