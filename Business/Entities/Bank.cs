using ItCommerce.DTO.ModelDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItCommerce.Business.Entities
{
    public class Bank
    {
        public int id { get; set; }
        public Profil agent { get; set; }
        public string name { get; set; }
        public string contact { get; set; }
        public string address { get; set; }

        public banque loadDto()
        {
            banque _dto = new banque()
            {
                id = this.id,
                nom = this.name,
                adresse = this.address,
                contact = this.contact,
            };
            return _dto;
        }

        public static Bank Create(banque _dbo)
        {
            return new Bank()
            {
                id = _dbo.id,
                name = _dbo.nom,
                address = _dbo.adresse,
                contact = _dbo.contact,
            };
        }

        public static List<Bank> CreateFromList(List<banque> _dboList)
        {
            List<Bank> _list = new List<Bank>();
            foreach (banque item in _dboList)
            {
                Bank _businessObject = Bank.Create(item);
                _list.Add(_businessObject);
            }
            return _list;
        }


    }
}
