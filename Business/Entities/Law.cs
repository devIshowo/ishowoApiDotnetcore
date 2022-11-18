using ItCommerce.DTO.ModelDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItCommerce.Business.Entities
{
    public class Law
    {
        public int id { get; set; }
        public string description { get; set; }
        public string reference { get; set; }
        public  bool ischecked { get; set; }


        public role loadDto()
        {
            role _dto = new role()
            {
                id = this.id,
                description = this.description,
                code = this.reference,
                statut = this.ischecked,
            };
            return _dto;
        }

        public static Law Create(role _dbo)
        {
            return new Law()
            {
                id = _dbo.id,
                description = _dbo.description,
                reference = _dbo.code,
                ischecked = _dbo.statut,
            };
        }

        public static List<Law> CreateFromList(List<role> _dboList)
        {
            List<Law> _list = new List<Law>();
            foreach (role item in _dboList)
            {
                Law _businessObject = Law.Create(item);
                _list.Add(_businessObject);
            }
            return _list;
        }
    }
}
