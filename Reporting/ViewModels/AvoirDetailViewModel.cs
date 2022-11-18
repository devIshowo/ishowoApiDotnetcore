using ItCommerce.Business.Entities;
using ItCommerce.DTO.ModelDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItCommerce.Reporting.ViewModels
{
    public class AvoirDetailView
    {
        public ProductSale saleDetail { get; set; }
        public DateTime datePrint { get; set; }
        public string baseUrl { get; set; }
        public string Ref_ORIGIN { get; set; }
        public string Mecef_ORIGIN { get; set; }
        public facture_avoir invoice { get; set; }


    }
}
