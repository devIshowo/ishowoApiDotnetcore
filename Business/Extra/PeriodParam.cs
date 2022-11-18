using ItCommerce.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItCommerce.Business.Extra
{
    public class PeriodParam
    {
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public Profil agent { get; set; }
        public ProdMeasureType product { get; set; }
        public List<ProductInStock> products { get; set; }
    }
}
