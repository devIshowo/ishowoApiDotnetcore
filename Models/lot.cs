using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ItCommerce.DTO.ModelDesign
{
    using System;
    using System.Collections.Generic;
    
    public class lot
    {
        public int id { get; set; }
        [Required]
        public string reference { get; set; }
        [Required]
        public System.DateTime date_lot { get; set; }
        public Nullable<System.DateTime> date_creation { get; set; }
        public Nullable<System.DateTime> date_expiration { get; set; }
    }
}
