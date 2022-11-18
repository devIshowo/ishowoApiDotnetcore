using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItCommerce.DTO.ModelDesign;

namespace ItCommerce.Business.Entities
{
    public class Group
    {
        public int id { get; set; }
        public string name { get; set; }
        public string reference { get; set; }
        public int total_laws { get; set; }

        public groupe loadDto()
        {
            groupe _dto = new groupe()
            {
                id = this.id,
                nom = this.name,
                code = this.reference,
                nb_roles = this.total_laws,
            };
            return _dto;
        }

        public static Group Create(groupe _dbo)
        {
            return new Group()
            {
                id = _dbo.id,
                reference = _dbo.code,
                name = _dbo.nom,
                total_laws = _dbo.nb_roles,
            };
        }

        public static List<Group> CreateFromList(List<groupe> _dboList)
        {
            List<Group> _list = new List<Group>();
            foreach (groupe item in _dboList)
            {
                Group _businessObject = Group.Create(item);
                _list.Add(_businessObject);
            }
            return _list;
        }

    }
}
