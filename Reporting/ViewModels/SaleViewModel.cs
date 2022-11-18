
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItCommerce.Business.Extra;
using ItCommerce.Business.Entities;

namespace ItCommerce.Reporting.ViewModels
{
    public class SaleViewModel {
        public List<ProductSale> saleList {get; set;}
        public PeriodParam param {get; set;}
        public DateTime datePrint {get; set; }
        public string baseUrl {get; set; }
    }

}