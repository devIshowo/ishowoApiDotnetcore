
using System;
using ItCommerce.Business.Entities;
using ItCommerce.DTO.ModelDesign;

namespace ItCommerce.Reporting.ViewModels
{
    public class AvoirDataView
    {
        public vente_produit sale { get; set; }
        public ProductSale saleData { get; set; }
        public facture invoice_original { get; set; }
        public facture_avoir invoice { get; set; }
        public string Ref_Origin { get; set; }
        public string Mecef_Origin { get; set; }
    }

}