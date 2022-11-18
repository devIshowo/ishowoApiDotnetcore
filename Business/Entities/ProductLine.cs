using ItCommerce.DTO.DbMethods;
using ItCommerce.DTO.ModelDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItCommerce.Business.Entities
{
    public class ProductLine
    {
        public int id { get; set; }
        public ProdMeasureType product { get; set; }
        public Compartment compartment { get; set; }
        public int p_achat { get; set; }
        public int p_vente { get; set; }
        public int quantite { get; set; }
        public double mt_remise { get; set; }
        public double mt_tva { get; set; }
        public string tax { get; set; }
        public string libellets { get; set; }
        public Nullable<int> ts { get; set; }
        public ProductInStock prod { get; set; }

        public static ProductLine Create(stock _dbo)
        {
            return new ProductLine()
            {
                id = _dbo.id,
                product = (_dbo.produit_type_mesure != null) ? ProdMeasureType.Create(_dbo.produit_type_mesure) : null,
                compartment = (_dbo.rayon != null) ? Compartment.Create(_dbo.rayon) : null,
                quantite = _dbo.qte_reelle,
                p_achat = _dbo.p_achat,
                p_vente = _dbo.p_vente,
            };
        }

        public static ProductLine CreateFromSaleLine(vente_produit_details _dbo)
        {
            return new ProductLine()
            {
                id = _dbo.id,
                product = (_dbo.produit_type_mesure != null) ? ProdMeasureType.Create(_dbo.produit_type_mesure) : null,
                compartment = (_dbo.rayon != null) ? Compartment.Create(_dbo.rayon) : null,
                quantite = _dbo.quantite,
                p_achat = _dbo.p_achat,
                p_vente = _dbo.p_vente,
                mt_remise = _dbo.mt_remise,
                mt_tva = _dbo.mt_tva,
                tax = _dbo.tax,
                libellets = _dbo.libellets,
                ts = _dbo.ts,
            };
        }

        public static ProductLine CreateFromSaleLineDeep(vente_produit_details _dbo)
        {
            produit_type_mesure prodMesure = (_dbo.produit_type_mesure == null) ? DtoParams.loadProduitMesureItem(_dbo.id_produit_mesure) : _dbo.produit_type_mesure;

            return new ProductLine()
            {
                id = _dbo.id,
                product = (prodMesure != null) ? ProdMeasureType.Create(prodMesure) : null, ///(_dbo.produit_type_mesure != null) ? ProdMeasureType.Create(prodMesure) : null,
                compartment = (_dbo.rayon != null) ? Compartment.Create(_dbo.rayon) : null,
                quantite = _dbo.quantite,
                p_achat = _dbo.p_achat,
                p_vente = _dbo.p_vente,
                mt_remise = _dbo.mt_remise,
                mt_tva = _dbo.mt_tva,
                tax = _dbo.tax,
                libellets = _dbo.libellets,
                ts = _dbo.ts,
            };
        }

        public static ProductLine CreateFromDevisLine(vente_produit_details _dbo)
        {
            return new ProductLine()
            {
                id = _dbo.id,
                product = (_dbo.produit_type_mesure != null) ? ProdMeasureType.Create(_dbo.produit_type_mesure) : null,
                compartment = (_dbo.rayon != null) ? Compartment.Create(_dbo.rayon) : null,
                quantite = _dbo.quantite,
                p_achat = _dbo.p_achat,
                p_vente = _dbo.p_vente,
                mt_remise = _dbo.mt_remise,
                mt_tva = _dbo.mt_tva,
                tax = _dbo.tax,
                libellets = _dbo.libellets,
                ts = _dbo.ts,
            };
        }

        public stock loadDto()
        {
            stock _dto = new stock()
            {
                id = this.id,
                id_produit_mesure = (this.product != null) ? this.product.id : 0,
                id_rayon = (this.compartment != null) ? this.compartment.id : 0,
                qte_reelle = this.quantite,
                qte_vendable = this.quantite,
                p_achat = this.p_achat,
                p_vente = this.p_vente,
            };
            return _dto;
        }

        public static List<ProductLine> CreateFromList(List<stock> _dboList)
        {
            List<ProductLine> _list = new List<ProductLine>();
            foreach (stock item in _dboList)
            {
                ProductLine _businessObject = ProductLine.Create(item);
                _list.Add(_businessObject);
            }
            return _list;
        }
    }


}
