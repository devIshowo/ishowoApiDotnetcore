using ItCommerce.DTO.ModelDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItCommerce.Business.Entities
{
    public class MeasureAssociation
    {
        public int id { get; set; }
        public ProdMeasureType bulk  { get; set; }
        public int quantity { get; set; }
        public ProdMeasureType retail { get; set; }
 



        public static MeasureAssociation Create(produit_corresp_mesure _dbo)
        {
            return new MeasureAssociation()
            {
                id = _dbo.id,
                bulk = ProdMeasureType.Create(_dbo.produit_type_mesure_parent),
                quantity = _dbo.quantite,
                retail = ProdMeasureType.Create(_dbo.produit_type_mesure_enfant),
            };
        }

        public produit_corresp_mesure loadDto()
        {
            produit_corresp_mesure _dto = new produit_corresp_mesure()
            {
                id = this.id,
                id_produit_mesure_parent = (this.bulk != null) ? this.bulk.id: 0,
                id_produit_mesure_enfant = (this.retail != null) ? this.retail.id : 0,
                quantite = this.quantity,
            };
            return _dto;
        }

        public static List<MeasureAssociation> CreateFromList(List<produit_corresp_mesure> _dboList)
        {
            List<MeasureAssociation> _list = new List<MeasureAssociation>();
            foreach (produit_corresp_mesure item in _dboList)
            {
                MeasureAssociation _businessObject = MeasureAssociation.Create(item);
                _list.Add(_businessObject);
            }
            return _list;
        }

    }
}
