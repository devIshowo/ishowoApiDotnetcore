using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ItCommerce.DTO.ModelDesign
{
    using System;
    using System.Collections.Generic;

    public class service
    {
        public int id { get; set; }
        [Required]
        public string nom { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        public int cout { get; set; }
        public int id_entreprise { get; set; }

        [ForeignKey("id_entreprise")]
        public virtual entreprise entreprise { get; set; }

    }
}
