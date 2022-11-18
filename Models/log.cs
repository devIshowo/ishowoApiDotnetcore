using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ItCommerce.DTO.ModelDesign
{
    using System;
    using System.Collections.Generic;
    
    public class log
    {
        public int id { get; set; }
        [Required]
        public System.DateTime date_log { get; set; }
        [Required]
        public string categorie { get; set; }
        [Required]
        public int id_entreprise { get; set; }
        [Required]
        public string actor { get; set; }
        [Required]
        public string desc { get; set; }
    
        [ForeignKey("id_entreprise")]
        public virtual entreprise entreprise { get; set; }
    }
}
