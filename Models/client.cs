using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ItCommerce.DTO.ModelDesign
{
    using System;
    using System.Collections.Generic;

    public partial class client
    {
        public client()
        {
            this.vente_service = new HashSet<vente_service>();
        }

        public int id { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string raison_sociale { get; set; }
        public string ifu { get; set; }
        public int solde { get; set; }
        public string contact { get; set; }
        public string whatsapp { get; set; }
        public string adr_mail { get; set; }
        public DateTime date_creation { get; set; }
        public virtual ICollection<vente_service> vente_service { get; set; }
    }
}
