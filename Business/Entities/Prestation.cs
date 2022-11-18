using ItCommerce.DTO.ModelDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItCommerce.Business.Entities
{
    public class Prestation
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int prix { get; set; }
        public int id_entreprise { get; set; }


        public static Prestation Create(service _dbo)
        {
            //create prestation
            return new Prestation()
            {
                id = _dbo.id,
                name = _dbo.nom,
                description = _dbo.description,
                prix = _dbo.cout,
            };
        }


        public service loadDto()
        {
            service _dto = new service()
            {
                id = this.id,
                nom = this.name,
                description = this.description,
                cout = this.prix,
                id_entreprise= this.id_entreprise
            };

            return _dto;
        }

        public static List<Prestation> CreateFromList(List<service> _dboList)
        {
            List<Prestation> _list = new List<Prestation>();
            foreach (service item in _dboList)
            {
                Prestation _businessObject = Prestation.Create(item);
                _list.Add(_businessObject);
            }
            return _list;
        }


    }
}
