
using System;
using System.Collections.Generic;

using ItCommerce.Business.Entities;
using ItCommerce.Business.Extra;

namespace ItCommerce.Reporting.ViewModels
{
    public class TransfertViewModel {
        public List<StockTransfer> transfertList {get; set;}
        public PeriodParam param { get; set;}
        public DateTime datePrint { get; set;}
        public string baseUrl { get; set; }

    }

}