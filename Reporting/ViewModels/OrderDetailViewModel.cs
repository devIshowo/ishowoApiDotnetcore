using ItCommerce.Business.Entities;
using ItCommerce.DTO.ModelDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItCommerce.Reporting.ViewModels
{
    public class OrderDetailView
    {
        public Order orderDetail { get; set; }
        public DateTime datePrint { get; set; }
        public string baseUrl { get; set; }

    }
}
