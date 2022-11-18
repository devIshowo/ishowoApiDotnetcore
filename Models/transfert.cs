using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ItCommerce.DTO.ModelDesign
{
    using System;
    using System.Collections.Generic;
    
    public class transfert
    {
         public transfert()
        {
            this.transfert_details = new List<transfert_details>();
        }
    
        public int id { get; set; }
        [Required]
        public System.DateTime date_transfert { get; set; }
        [Required]
        public int id_source { get; set; }
        [Required]
        public int id_destination { get; set; }
        [Required]
        public int id_profil { get; set; }
        [Required]
        public string reference { get; set; }
    
        [ForeignKey("id_profil")]
        public  virtual profil profil { get; set; }
        [ForeignKey("id_source")]
        public  virtual rayon rayon_source { get; set; }
        [ForeignKey("id_destination")]
        public  virtual rayon rayon_destination { get; set; }
        public  virtual ICollection<transfert_details> transfert_details { get; set; }
    }
}
