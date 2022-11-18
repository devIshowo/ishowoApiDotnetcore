using ItCommerce.DTO.ModelDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItCommerce.DTO.DbMethods;

namespace ItCommerce.Business.Entities
{
    public class ProdStockLimit
    {
        public int id { get; set; }
        public Product product { get; set; }
        public int id_product { get; set; }
        public ProdMeasureType produit_measure_type { get; set; }
        public int quantity { get; set; }
        public Profil agent { get; set;  }

        public static ProdStockLimit Create(produit_stock_limit _dbo)
        {
            return new ProdStockLimit()
            {
                id = _dbo.id,
                product = (_dbo.produit != null) ? Product.CreateLight(_dbo.produit) : null,
                id_product = _dbo.id_produit,
                produit_measure_type = (_dbo.produit_type_mesure != null) ? ProdMeasureType.Create(_dbo.produit_type_mesure) : null,
                quantity = _dbo.quantite,
            };
        }

        public static string ToString(ProdStockLimit item)
        {
            return item.product.name;
        }
        public static ProdStockLimit CreateDeep(int _dboId)
        {
            produit_stock_limit _dbo = DtoParams.loadProduitStockLimitItem(_dboId);
                
            return new ProdStockLimit()
            {
                id = _dbo.id,
                product = (_dbo.produit != null) ? Product.CreateLight(_dbo.produit) : null,
                id_product = _dbo.id_produit,
                produit_measure_type = (_dbo.produit_type_mesure != null) ? ProdMeasureType.Create(_dbo.produit_type_mesure) : null,
                quantity = _dbo.quantite,
            };
        }

        public produit_stock_limit loadDto()
        {
            produit_stock_limit _dto = new produit_stock_limit()
            {
                id = this.id,
                id_produit = (this.product != null) ? this.product.id : this.id_product,
                id_produit_type_mesure = (this.produit_measure_type != null) ? this.produit_measure_type.measure_type.id : 0,
                quantite = this.quantity,
            };
            return _dto;
        }

        public static List<ProdStockLimit> CreateFromList(List<produit_stock_limit> _dboList)
        {
            List<ProdStockLimit> _list = new List<ProdStockLimit>();
            foreach (produit_stock_limit item in _dboList)
            {
                ProdStockLimit _businessObject = ProdStockLimit.Create(item);
                _list.Add(_businessObject);
            }
            return _list;
        }


    }
}
