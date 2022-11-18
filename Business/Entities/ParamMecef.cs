using ItCommerce.DTO.ModelDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItCommerce.Business.Entities
{
    public class ParamMecef
    {
        public int id { get; set; }
        public string key { get; set; }
        public string value { get; set; }

      
        public static ParamMecef Create(param_mecef _dbo)
        {
            return new ParamMecef()
            {
                id = _dbo.id,
                key = _dbo.code,
                value = _dbo.valeur,
            };
        }

        public param_mecef loadDto()
        {
            param_mecef _dto = new param_mecef()
            {
                id = this.id,
                code = this.key,
                valeur = this.value,
            };
            return _dto;
        }

        

        public static List<ParamMecef> CreateFromList(List<param_mecef> _dboList)
        {
            List<ParamMecef> _list = new List<ParamMecef>();
            foreach (param_mecef item in _dboList)
            {
                ParamMecef _businessObject = ParamMecef.Create(item);
                _list.Add(_businessObject);
            }
            return _list;
        }
    }
}
