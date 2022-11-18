
using System;
using ItCommerce.Business.Entities;
using ItCommerce.DTO.ModelDesign;

namespace ItCommerce.Reporting.ViewModels
{
    public class SaleDataView
    {
        public vente_produit sale { get; set; }
        public ProductSale saleData { get; set; }
        public facture invoice { get; set; }
    }

}