using ItCommerce.DTO.ModelDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItCommerce.Business.Entities
{
    public class Compartment
    {
        public int id { get; set; }
        public string name { get; set; }
        public Agency agency { get; set; }
        public Profil agent { get; set; }



        public static Compartment Create(rayon _dbo)
        {
            return new Compartment()
            {
                id = _dbo.id,
                name = _dbo.nom,
                agency = (_dbo.agence != null)? Agency.Create(_dbo.agence) : new Agency()
            };
        }

        public rayon loadDto()
        {
            rayon _dto = new rayon()
            {
                id = this.id,
                nom = this.name,
                id_agence = (this.agency != null) ? this.agency.id : 0,
            };
            return _dto;
        }


        public static List<Compartment> CreateFromList(List<rayon> _dboList)
        {
            List<Compartment> _list = new List<Compartment>();
            foreach (rayon item in _dboList)
            {
                Compartment _businessObject = Compartment.Create(item);
                _list.Add(_businessObject);
            }
            return _list;
        }
    }
}
