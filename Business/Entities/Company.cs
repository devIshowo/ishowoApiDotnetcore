using ItCommerce.DTO.ModelDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItCommerce.Business.Entities
{
    public class Company
    {
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string description { get; set; }
        public string logo { get; set; }
        public Profil agent { get; set; }
        public Licence currentLicence { get; set; }

        public Company(string _name, string _address, string _phone, string _email, string _logo)
        {
            this.name = _name; this.address = _address; this.phone = _phone; this.email = _email; this.logo = _logo;
        }

        public Company() { }

        //get logo
        public string getLogo() {
            return (this.logo != null) ? this.logo : "logo.jpg";
        }



        public static Company Create(entreprise _dbo)
        {
            return new Company()
            {
                id = _dbo.id,
                name = _dbo.raison_sociale,
                address = _dbo.adresse,
                phone = _dbo.telephone,
                email = _dbo.email,
                logo = (_dbo.logo != null)? _dbo.logo :  "logo.jpg",
                description = _dbo.secteur_activite
            };
        }

        public entreprise loadDto()
        {
            entreprise _dto = new entreprise()
            {
                id = this.id,
                raison_sociale = this.name,
                adresse = this.address,
                telephone = this.phone,
                email = this.email,
                logo = this.logo,
                secteur_activite = this.description
            };
            return _dto;
        }



        public static List<Company> CreateFromList(List<entreprise> _dboList)
        {
            List<Company> _list = new List<Company>();
            foreach (entreprise item in _dboList)
            {
                Company _businessObject = Company.Create(item);
                _list.Add(_businessObject);
            }
            return _list;
        }

    }
}
