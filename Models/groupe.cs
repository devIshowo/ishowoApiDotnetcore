using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ItCommerce.DTO.ModelDesign
{
    using System;
    using System.Collections.Generic;
    public class groupe
    {
        public groupe()
        {
            this.profils = new List<profil>();
        }

        public int id { get; set; }
        [Required]
        public string code { get; set; }
        public string nom { get; set; }
        [Required]
        public int nb_roles { get; set; }

        public virtual ICollection<profil> profils { get; set; }
    }
}
