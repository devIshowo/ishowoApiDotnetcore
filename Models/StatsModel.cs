using ItCommerce.DTO.ModelDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItCommerce.DTO.ModelDesign
{
    public class StatsModel
    {
        public int number { get; set; }
        public int sum { get; set; }
        public produit_type_mesure product { get; set; }
    }
}
