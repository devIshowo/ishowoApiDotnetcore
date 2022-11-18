using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ItCommerce.DTO.ModelDesign
{
    using System;
    using System.Collections.Generic;
    
    public class objectif_vente
    {
        public int id { get; set; }
        [Required]
        public int id_profil { get; set; }
        [Required]
        public System.DateTime date_debut { get; set; }
        [Required]
        public System.DateTime date_fin { get; set; }
        [Required]
        public int montant_objectif { get; set; }
        [Required]
        public int montant_atteint { get; set; }
        [Required]
        public bool est_atteint { get; set; }
        [Required]
        public int id_agence { get; set; }
    
        [ForeignKey("id_agence")]
        public  virtual agence agence { get; set; }
        [ForeignKey("id_profil")]
        public  virtual profil profil { get; set; }
    }
}
