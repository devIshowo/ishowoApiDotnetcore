using ItCommerce.DTO.ModelDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItCommerce.DTO.DbMethods;

namespace ItCommerce.Business.Entities
{
    public class Services
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public Prestation service { get; set; }
        public int quantity { get; set; }
        public int price { get; set; }
        public int montant { get; set; }
        public Profil agent { get; set; }


        public static Services CreateFromSaleLine(vente_service_details _dbo)
        {
            return new Services()
            {
                id = _dbo.id,
                quantity = _dbo.quantite,
                price = _dbo.prix,
                montant= _dbo.montant,

            };
        }

        public service loadDto()
        {
            service _dto = new service()
            {
                id = this.id,
                nom = this.name,
                description = this.description,
                cout = this.price
            };
            return _dto;
        }

        public static Services CreateFromSaleLineDeep(vente_service_details _dbo)
        {

            return new Services()
            {
                id = _dbo.id,
                quantity = _dbo.quantite,
                price = _dbo.prix,
                montant = _dbo.montant,
            };
        }

        public static List<Services> CreateFromList(List<vente_service_details> _dboList)
        {
            List<Services> _list = new List<Services>();
            foreach (vente_service_details item in _dboList)
            {
                Services _businessObject = Services.CreateFromSaleLine(item);
                _list.Add(_businessObject);
            }
            return _list;
        }

    }
}
