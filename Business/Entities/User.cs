using ItCommerce.DTO.ModelDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItCommerce.Business.Entities
{
    public class User
    {

        public int id { get; set; }
        public string lastname { get; set; }
        public string firstname { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public int balance { get; set; }
        public Profil agent { get; set; }
        public DateTime? date_of_creation { get; set; }

        public static User Create(utilisateur _dbo)
        {
            return new User()
            {
                id = _dbo.id,
                lastname = _dbo.nom,
                firstname = _dbo.prenoms,
                phone = _dbo.telephone,
                email = _dbo.email,
                address = _dbo.adresse,
                balance = (_dbo.solde != null) ? (int)_dbo.solde : 0,
                date_of_creation = _dbo.date_creation
            };
        }

        public utilisateur loadDto()
        {
            utilisateur _dto = new utilisateur()
            {
                id = this.id,
                nom = this.lastname,
                prenoms = this.firstname,
                telephone = this.phone,
                email = this.email,
                adresse = this.address,
                solde = this.balance,
                date_creation = this.date_of_creation
            };
            return _dto;
        }

        public static List<User> CreateFromList(List<utilisateur> _dboList)
        {
            List<User> _list = new List<User>();
            foreach (utilisateur item in _dboList)
            {
                User _businessObject = User.Create(item);
                _list.Add(_businessObject);
            }
            return _list;
        }

    }
}
