using ItCommerce.DTO.ModelDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItCommerce.Business.Entities
{
    public class ProductTest
    {
        public int id { get; set; }
        public string name { get; set; }
        public string reference { get; set; }

        public Category category { get; set; }
        public List<ProdMeasureType> measure_types { get; set; }
        public List<MeasureAssociation> measure_associations { get; set; }
        public Profil agent { get; set; }





    }
}
