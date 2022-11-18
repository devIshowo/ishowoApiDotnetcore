using ItCommerce.DTO.ModelDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItCommerce.Business.Entities
{
    public class MeasureType
    {
        public int id { get; set; }
        public string name { get; set; }
        public Profil agent { get; set; }


        public static MeasureType Create(type_mesure _dbo)
        {
            return new MeasureType()
            {
                id = _dbo.id,
                name = _dbo.nom,
            };
        }

        public type_mesure loadDto()
        {
            type_mesure _dto = new type_mesure()
            {
                id = this.id,
                nom = this.name,
            };
            return _dto;
        }

        public static List<MeasureType> CreateFromList(List<type_mesure> _dboList)
        {
            List<MeasureType> _list = new List<MeasureType>();
            foreach (type_mesure item in _dboList)
            {
                MeasureType _businessObject = MeasureType.Create(item);
                _list.Add(_businessObject);
            }
            return _list;
        }

       
    }
}
