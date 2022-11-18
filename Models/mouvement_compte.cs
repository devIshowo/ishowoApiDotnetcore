using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ItCommerce.DTO.ModelDesign
{
    using System;
    using System.Collections.Generic;
    
    public  class mouvement_compte
    {
        public int id { get; set; }
        [Required]
        public int id_compte { get; set; }
        [Required]
        public int credit { get; set; }
        [Required]
        public int debit { get; set; }
        [Required]
        public int solde { get; set; }
        [Required]
        public string numero_piece { get; set; }
        [Required]
        public System.DateTime date_mvt { get; set; }
        [Required]
        public string motif { get; set; }
        [Required]
        public int id_profil { get; set; }
        [Required]
        public int id_entreprise { get; set; }


        [ForeignKey("id_compte")]
        public  virtual compte compte { get; set; }
        [ForeignKey("id_entreprise")]
        public  virtual entreprise entreprise { get; set; }
        [ForeignKey("id_profil")]
        public  virtual profil profil { get; set; }
    }
}
