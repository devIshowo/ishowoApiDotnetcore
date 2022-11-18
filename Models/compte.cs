using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ItCommerce.DTO.ModelDesign
{
    using System;
    using System.Collections.Generic;
    
    public class compte
    {
        public compte()
        {
            this.mouvement_compte = new List<mouvement_compte>();
        }
    
        public int id { get; set; }
        [Required]
        public string intitule { get; set; }
        [Required]
        public string reference { get; set; }
        [Required]
        public int solde { get; set; }
        [Required]
        public int id_type_compte { get; set; }
        public Nullable<int> id_banque { get; set; }
        public Nullable<int> id_profil { get; set; }
        [Required]
        public int id_entreprise { get; set; }
    
        [ForeignKey("id_banque")]
        public  virtual banque banque { get; set; }
        [ForeignKey("id_entreprise")]
        public  virtual entreprise entreprise { get; set; }
        [ForeignKey("id_profil")]
        public  virtual profil profil { get; set; }
        [ForeignKey("id_type_compte")]
        public  virtual type_compte type_compte { get; set; }
        public  virtual ICollection<mouvement_compte> mouvement_compte { get; set; }
    }
}
