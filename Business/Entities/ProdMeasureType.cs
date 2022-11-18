using ItCommerce.DTO.ModelDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItCommerce.DTO.DbMethods;

namespace ItCommerce.Business.Entities
{
    public class ProdMeasureType
    {
        public int id { get; set; }
        public Product product { get; set; }
        public int id_product { get; set; }
        public MeasureType measure_type { get; set; }
        public int quantity { get; set; }
        public Profil agent { get; set;  }

        public static ProdMeasureType Create(produit_type_mesure _dbo)
        {
            return new ProdMeasureType()
            {
                id = _dbo.id,
                product = (_dbo.produit != null) ? Product.CreateLight(_dbo.produit) : null,
                id_product = _dbo.id_produit,
                measure_type = (_dbo.type_mesure != null) ? MeasureType.Create(_dbo.type_mesure) : null,
                quantity = _dbo.quantite,
            };
        }

        public static string ToString(ProdMeasureType item)
        {
            return item.product.name;
        }
        public static ProdMeasureType CreateDeep(int _dboId)
        {
            produit_type_mesure _dbo = DtoParams.loadProduitMesureItem(_dboId);
                
            return new ProdMeasureType()
            {
                id = _dbo.id,
                product = (_dbo.produit != null) ? Product.CreateLight(_dbo.produit) : null,
                id_product = _dbo.id_produit,
                measure_type = (_dbo.type_mesure != null) ? MeasureType.Create(_dbo.type_mesure) : null,
                quantity = _dbo.quantite,
            };
        }

        public produit_type_mesure loadDto()
        {
            produit_type_mesure _dto = new produit_type_mesure()
            {
                id = this.id,
                id_produit = (this.product != null) ? this.product.id : this.id_product,
                id_type_mesure = (this.measure_type != null) ? this.measure_type.id : 0,
                quantite = this.quantity,
            };
            return _dto;
        }

        public static List<ProdMeasureType> CreateFromList(List<produit_type_mesure> _dboList)
        {
            List<ProdMeasureType> _list = new List<ProdMeasureType>();
            foreach (produit_type_mesure item in _dboList)
            {
                ProdMeasureType _businessObject = ProdMeasureType.Create(item);
                _list.Add(_businessObject);
            }
            return _list;
        }


    }
}
