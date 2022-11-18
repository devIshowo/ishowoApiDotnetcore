using ItCommerce.DTO.ModelDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItCommerce.Business.Entities
{
    public class Agency
    {
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public int code_entreprise { get; set; }
        public Profil agent { get; set; }
        public Company company { get; set; }

        public Agency(string _name, string _address, int _code)
        {
            this.name = _name; this.address = _address; this.code_entreprise = _code;
        }

        public Agency() { }



        public static Agency Create(agence _dbo)
        {
            return new Agency()
            {
                id = _dbo.id,
                name = _dbo.nom,
                address = _dbo.localisation,
                company = (_dbo.entreprise != null) ? Company.Create(_dbo.entreprise) : null,
            };
        }

        public agence loadDto()
        {
            agence _dto = new agence()
            {
                id = this.id,
                nom = this.name,
                localisation = this.address,
                id_entreprise = this.code_entreprise
            };
            return _dto;
        }

        

        public static List<Agency> CreateFromList(List<agence> _dboList)
        {
            List<Agency> _list = new List<Agency>();
            foreach (agence item in _dboList)
            {
                Agency _businessObject = Agency.Create(item);
                _list.Add(_businessObject);
            }
            return _list;
        }
    }
}
