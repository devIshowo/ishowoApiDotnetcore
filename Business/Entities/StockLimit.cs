using ItCommerce.DTO.ModelDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItCommerce.Business.Entities
{
    public class StockLimit
    {
        public int id { get; set; }
        public Product product { get; set; }
        public MeasureType measure_type { get; set; }
        public int quantity { get; set; }
        public Profil agent { get; set; }



        public static StockLimit Create(produit_type_mesure _dbo)
        {
            return new StockLimit()
            {
                id = _dbo.id,
                product = (_dbo.produit != null) ? Product.Create(_dbo.produit) : new Product(),
                measure_type = (_dbo.type_mesure != null) ? MeasureType.Create(_dbo.type_mesure) : new MeasureType(),
                quantity = _dbo.quantite,
            };
        }

        public produit_type_mesure loadDto()
        {
            produit_type_mesure _dto = new produit_type_mesure()
            {
                id = this.id,
                id_produit = (this.product != null) ? this.product.id : 0,
                id_type_mesure = (this.measure_type != null) ? this.measure_type.id : 0,
                quantite = this.quantity,
            };
            return _dto;
        }

        public static List<StockLimit> CreateFromList(List<produit_type_mesure> _dboList)
        {
            List<StockLimit> _list = new List<StockLimit>();
            foreach (produit_type_mesure item in _dboList)
            {
                StockLimit _businessObject = StockLimit.Create(item);
                _list.Add(_businessObject);
            }
            return _list;
        }
    }
}
