using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ItCommerce.DTO.ModelDesign
{
    using System;
    using System.Collections.Generic;
    
    public class licence
    {
        public int id { get; set; }
        [Required]
        public int id_entreprise { get; set; }
        [Required]
        public string code { get; set; }
        [Required]
        public string cle { get; set; }
        [Required]
        public bool est_active { get; set; }
        [Required]
        public System.DateTime date_activation { get; set; }
        [Required]
        public System.DateTime date_expiration { get; set; }
        [Required]
        public int montant_paye { get; set; }
    
        [ForeignKey("id_entreprise")]
        public virtual  entreprise entreprise { get; set; }
    }
}
