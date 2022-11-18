using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ItCommerce.DTO.ModelDesign
{
    using System;
    using System.Collections.Generic;
    
    public class vente_service
    {
        public vente_service()
        {
            this.vente_details = new List<vente_service_details>();
        }

        public int id { get; set; }
        [Required]
        public System.DateTime date_vente { get; set; }
        [Required]
        public int montant_vente { get; set; }
        [Required]
        public int montant_recu { get; set; }
        [Required]
        public int reliquat { get; set; }
        [Required]
        public Nullable<int> id_client { get; set; }
        [Required]
        public int id_agent { get; set; }
        [Required]
        public string reference { get; set; }

        [ForeignKey("id_agence")]
        public virtual agence agence { get; set; }
        [ForeignKey("id_categ_vente")]
        public virtual categ_vente categ_vente { get; set; }
        [ForeignKey("id_agent")]
        public virtual utilisateur utilisateur { get; set; }
        public virtual profil profil { get; set; }
        [ForeignKey("id_client")]
        public virtual client client { get; set; }
        public virtual ICollection<vente_service_details> vente_details { get; set; }
    }
}
