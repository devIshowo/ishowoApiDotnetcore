using ItCommerce.Business.Entities;
using ItCommerce.DTO.SpecClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItCommerce.Business.Extra
{
    public class GlobalReport
    {
        public double? totalSell { get; set; }
        public double? totalProfit { get; set; }
        public double? totalOrder { get; set; }
        public double? qtySold { get; set; }
        public double? qtyOrder { get; set; }
        public ProductInStock mostSoldProd { get; set; }
        public ProductInStock mostIncomeProd { get; set; }
        public ProductInStock mostProfitableProd { get; set; }

        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }

        public static GlobalReport Create(rapport_global _dbo)
        {
            return new GlobalReport()
            {
                totalSell = (_dbo.TotalVentes != null)? _dbo.TotalVentes : 0,
                totalProfit = (_dbo.TotalBenefices != null)? _dbo.TotalBenefices : 0,
                totalOrder = (_dbo.TotalCommandes != null)? _dbo.TotalCommandes : 0,
                qtySold = (_dbo.QteVendue != null)? _dbo.QteVendue : 0,
                qtyOrder = (_dbo.QteCommandee != null) ? _dbo.QteCommandee : 0,
                mostSoldProd = (_dbo.ProdPlusVendu != null) ? ProductInStock.Create(_dbo.ProdPlusVendu) : null,
                mostIncomeProd = (_dbo.ProdPlusCA != null) ? ProductInStock.Create(_dbo.ProdPlusCA) : null,
                mostProfitableProd = (_dbo.ProdPlusRentable != null) ? ProductInStock.Create(_dbo.ProdPlusRentable) : null,
                startDate = _dbo.DateDebut,
                endDate = _dbo.DateFin,
            };
        }

        public static List<GlobalReport> CreateFromList(List<rapport_global> _dboList)
        {
            List<GlobalReport> _list = new List<GlobalReport>();
            foreach (rapport_global item in _dboList)
            {
                GlobalReport _businessObject = GlobalReport.Create(item);
                _list.Add(_businessObject);
            }
            return _list;
        }

    }
}
