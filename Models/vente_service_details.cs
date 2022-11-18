using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ItCommerce.DTO.ModelDesign
{
    using System;
    using System.Collections.Generic;
    
    public  class vente_service_details
    {
        public int id { get; set; }
        [Required]
        public int quantite { get; set; }
        [Required]
        public int prix { get; set; }
        [Required]
        public int montant { get; set; }
        [Required]
        public int id_vente { get; set; }
        [Required]
        public int id_service { get; set; }
    
        [ForeignKey("id_service")]
        public  virtual service service { get; set; }

        [ForeignKey("id_vente")]
        public  virtual vente_service vente { get; set; }
    }
}
