using ItCommerce.DTO.ModelDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItCommerce.DTO.SpecClasses
{
    public class rapport_global
    {
        public double? TotalVentes { get; set; }
        public double? TotalBenefices { get; set; }
        public double? TotalCommandes { get; set; }
        public double? QteVendue { get; set; }
        public double? QteCommandee { get; set; }
        public stock ProdPlusVendu { get; set; }
        public stock ProdPlusCA { get; set; } //qui appporte plus d'entree: donc qui contribue plus au chiffre d'affaires de la periode
        public stock ProdPlusRentable { get; set; }

        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
    }

    public class rapport_utilisateur
    {
        public double? TotalVentes { get; set; }
        public double? TotalBenefices { get; set; }
        public double? NombreVentes { get; set; }//nombre de ventes # du nombre de produits vendus

        public string login { get; set; }
        public double? pourcentage { get; set; }
        public Nullable<int> reference { get; set; }
    
        public string identite { get; set; }
        public string agence { get; set; }
        
    }
}
