using ItCommerce.DTO.ModelDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItCommerce.Business.Entities
{
    public class AccountType
    {
        public int id { get; set; }
        public string name { get; set; }
        public Profil agent { get; set; }


        public static AccountType Create(type_compte _dbo)
        {
            return new AccountType()
            {
                id = _dbo.id,
                name = _dbo.nom,
            };
        }

        public type_compte loadDto()
        {
            type_compte _dto = new type_compte()
            {
                id = this.id,
                nom = this.name,
            };
            return _dto;
        }

        public static List<AccountType> CreateFromList(List<type_compte> _dboList)
        {
            List<AccountType> _list = new List<AccountType>();
            foreach (type_compte item in _dboList)
            {
                AccountType _businessObject = AccountType.Create(item);
                _list.Add(_businessObject);
            }
            return _list;
        }


    }
}
