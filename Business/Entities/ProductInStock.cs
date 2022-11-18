using ItCommerce.DTO.ModelDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItCommerce.DTO.DbMethods;

namespace ItCommerce.Business.Entities
{
    public class ProductInStock
    {
        public int id { get; set; }
        public ProdMeasureType product { get; set; }
        public Compartment compartment { get; set; }
        public int quantity { get; set; }
        public int quantity_transfer { get; set; }
        public int quantity_sell { get; set; }
        public int purchase_price { get; set; }
        public int selling_price { get; set; }
        public string barcode { get; set; }
        public Profil agent { get; set; }
        public DateTime? expiration_date { get; set; }


        public static ProductInStock Create(stock _dbo)
        {
            return new ProductInStock()
            {
                id = _dbo.id,
                product = (_dbo.produit_type_mesure != null) ? ProdMeasureType.Create(_dbo.produit_type_mesure) : null,
                compartment = (_dbo.rayon != null) ? Compartment.Create(_dbo.rayon) : null,
                quantity = _dbo.qte_reelle,
                purchase_price = _dbo.p_achat,
                selling_price = _dbo.p_vente,
                barcode = (_dbo.reference != null) ? _dbo.reference : "",
                expiration_date = _dbo.date_exp,
            };
        }

        public static ProductInStock CreateFromSaleLine(vente_produit_details _dbo)
        {
            return new ProductInStock()
            {
                id = _dbo.id,
                product = (_dbo.produit_type_mesure != null) ? ProdMeasureType.Create(_dbo.produit_type_mesure) : null,
                compartment = (_dbo.rayon != null) ? Compartment.Create(_dbo.rayon) : null,
                quantity = _dbo.quantite,
                purchase_price = _dbo.p_achat,
                selling_price = _dbo.p_vente,

            };
        }

        public static ProductInStock CreateFromSaleLineDeep(vente_produit_details _dbo)
        {
            produit_type_mesure prodMesure = (_dbo.produit_type_mesure == null) ? DtoParams.loadProduitMesureItem(_dbo.id_produit_mesure) : _dbo.produit_type_mesure;

            return new ProductInStock()
            {
                id = _dbo.id,
                product = (prodMesure != null) ? ProdMeasureType.Create(prodMesure) : null, ///(_dbo.produit_type_mesure != null) ? ProdMeasureType.Create(prodMesure) : null,
                compartment = (_dbo.rayon != null) ? Compartment.Create(_dbo.rayon) : null,
                quantity = _dbo.quantite,
                purchase_price = _dbo.p_achat,
                selling_price = _dbo.p_vente,
            };
        }

        public static ProductInStock CreateFromDevisLine(vente_produit_details _dbo)
        {
            return new ProductInStock()
            {
                id = _dbo.id,
                product = (_dbo.produit_type_mesure != null) ? ProdMeasureType.Create(_dbo.produit_type_mesure) : null,
                compartment = (_dbo.rayon != null) ? Compartment.Create(_dbo.rayon) : null,
                quantity = _dbo.quantite,
                purchase_price = _dbo.p_achat,
                selling_price = _dbo.p_vente,
            };
        }

        public stock loadDto()
        {
            stock _dto = new stock()
            {
                id = this.id,
                id_produit_mesure = (this.product != null) ? this.product.id : 0,
                id_rayon = (this.compartment != null) ? this.compartment.id : 0,
                qte_reelle = this.quantity,
                qte_vendable = this.quantity,
                p_achat = this.purchase_price,
                p_vente = this.selling_price,
                reference = (this.barcode != null) ? this.barcode : "",
                date_exp = (DateTime?)this.expiration_date,
            };
            return _dto;
        }

        public static List<ProductInStock> CreateFromList(List<stock> _dboList)
        {
            List<ProductInStock> _list = new List<ProductInStock>();
            foreach (stock item in _dboList)
            {
                ProductInStock _businessObject = ProductInStock.Create(item);
                _list.Add(_businessObject);
            }
            return _list;
        }

    }
}
