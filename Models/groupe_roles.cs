using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ItCommerce.DTO.ModelDesign
{
    public class groupe_roles
    {
        public int id { get; set; }
        [Required]
        public int id_groupe { get; set; }
        [Required]
        public int id_role { get; set; }
        [Required]
        public bool statut { get; set; }

        [ForeignKey("id_groupe")]
        public virtual groupe groupe { get; set; }
        [ForeignKey("id_role")]
        public virtual role role { get; set; }
    }
}
