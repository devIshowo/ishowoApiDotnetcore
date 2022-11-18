// using ItCommerce.DTO.ModelDesign;
using ItCommerce.DTO.ModelDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItCommerce.Business.Entities
{
    public class Customer
    {
        public int id { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public int solde { get; set; }
        public string telephone { get; set; }
        public string whatsapp { get; set; }
        public string email { get; set; }
        public DateTime date_creation { get; set; }
        public string social_reason { get; set; }
        public string num_ifu { get; set; }

        public static Customer Create(client _dbo)
        {
            return new Customer()
            {
                id = _dbo.id,
                nom = _dbo.nom,
                prenom = _dbo.prenom,
                solde = _dbo.solde,
                telephone = _dbo.contact,
                whatsapp = _dbo.whatsapp,
                email = _dbo.adr_mail,
                social_reason = _dbo.raison_sociale,
                num_ifu = _dbo.ifu,
            };
        }

        public client loadDto()
        {
            client _dto = new client()
            {
                id = this.id,
                nom = this.nom,
                prenom = this.prenom,
                solde = this.solde,
                contact = this.telephone,
                whatsapp = this.whatsapp,
                adr_mail= this.email,
                date_creation = this.date_creation,
                raison_sociale = this.social_reason,
                ifu = this.num_ifu,
            };
            return _dto;
        }

        public static List<Customer> CreateFromList(List<client> _dboList)
        {
            List<Customer> _list = new List<Customer>();
            foreach (client item in _dboList)
            {
                Customer _businessObject = Customer.Create(item);
                _list.Add(_businessObject);
            }
            return _list;
        }
    }
}
