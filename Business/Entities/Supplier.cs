using ItCommerce.DTO.ModelDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItCommerce.Business.Entities
{
    public class Supplier
    {
        public int id { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public int balance { get; set; }
        public Profil agent { get; set; }
        



        public static Supplier Create(fournisseur _dbo)
        {
            return new Supplier()
            {
                id = _dbo.id,
                name = _dbo.nom,
                phone = _dbo.telephone,
                email = _dbo.email,
                address = _dbo.adresse,
                balance = (_dbo.solde!= null)? (int)_dbo.solde : 0
            };
        }

        public fournisseur loadDto()
        {
            fournisseur _dto = new fournisseur()
            {
                id = this.id,
                nom = this.name,
                telephone = this.phone,
                email = this.email,
                adresse = this.address,
                solde = this.balance
            };
            return _dto;
        }

        public static List<Supplier> CreateFromList(List<fournisseur> _dboList)
        {
            List<Supplier> _list = new List<Supplier>();
            foreach (fournisseur item in _dboList)
            {
                Supplier _businessObject = Supplier.Create(item);
                _list.Add(_businessObject);
            }
            return _list;
        }
    }
}
