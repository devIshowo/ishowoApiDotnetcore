using ItCommerce.DTO.ModelDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItCommerce.Business.Entities
{
    public class Group_Laws
    {
        public int id { get; set; }
        public int id_group { get; set; }
        public int id_law { get; set; }
        public bool status { get; set; }


        public groupe_roles loadDto()
        {
            groupe_roles _dto = new groupe_roles()
            {
                id = this.id,
                id_groupe = this.id_group,
                id_role = this.id_law,
                statut = this.status,
            };
            return _dto;
        }

        public static Group_Laws Create(groupe_roles _dbo)
        {
            return new Group_Laws()
            {
                id = _dbo.id,
                id_group = _dbo.id_groupe,
                id_law = _dbo.id_role,
                status = _dbo.statut,
            };
        }

        public static List<Group_Laws> CreateFromList(List<groupe_roles> _dboList)
        {
            List<Group_Laws> _list = new List<Group_Laws>();
            foreach (groupe_roles item in _dboList)
            {
                Group_Laws _businessObject = Group_Laws.Create(item);
                _list.Add(_businessObject);
            }
            return _list;
        }


    }
}
