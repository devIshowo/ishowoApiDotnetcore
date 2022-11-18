using ItCommerce.DTO.ModelDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItCommerce.Business.Entities
{
    public class ServiceSale
    {
        public int id { get; set; }
        public User agent { get; set; }
        public Customer customer { get; set; }
        public List<Services> lines { get; set; }
        public DateTime date { get; set; }
        public int amount_received { get; set; }
        public int amount_real { get; set; }
        public bool is_balanced { get; set; } //si la vente a ete integralement payee

        public static ServiceSale Create(vente_service _dbo)
        {
            List<Services> pSList = new List<Services>();
            if (_dbo.vente_details != null)
            {
                Services pStk;
                foreach (var item in _dbo.vente_details)
                {
                    pStk = Services.CreateFromSaleLine(item);
                    pSList.Add(pStk);
                }
            }

            return new ServiceSale()
            {
                id = _dbo.id,
                date = _dbo.date_vente,
                amount_real = _dbo.montant_vente,
                amount_received = _dbo.montant_recu,
                customer = (_dbo.client != null) ? Customer.Create(_dbo.client) : null,
                agent = (_dbo.utilisateur != null) ? User.Create(_dbo.utilisateur) : null,
                lines = pSList,
                is_balanced = (_dbo.montant_recu < _dbo.montant_vente) ? false : true
            };
        }


        public static ServiceSale CreateDeep(vente_service _dbo)
        {
            List<Services> pSList = new List<Services>();
            if (_dbo.vente_details != null)
            {
                Services pStk;
                foreach (var item in _dbo.vente_details)
                {
                    pStk = Services.CreateFromSaleLineDeep(item);
                    pSList.Add(pStk);
                }
            }

            return new ServiceSale()
            {
                id = _dbo.id,
                date = _dbo.date_vente,
                amount_real = _dbo.montant_vente,
                amount_received = _dbo.montant_recu,
                customer = (_dbo.client != null) ? Customer.Create(_dbo.client) : null,
                agent = (_dbo.utilisateur != null) ? User.Create(_dbo.utilisateur) : null,
                lines = pSList,
                is_balanced = (_dbo.montant_recu < _dbo.montant_vente) ? false : true
            };
        }

        public vente_service loadDto()
        {
            int montant = 0;

            //vente details
            List<vente_service_details> detailsVente = new List<vente_service_details>();
            foreach (var item in this.lines)
            {
                vente_service_details detailVente = new vente_service_details();
                detailVente.quantite = item.quantity;
                detailVente.prix = item.price;
                detailVente.montant = item.montant;
                detailsVente.Add(detailVente);
            }

            //montant
            montant = this.lines.Sum(x => x.price * x.quantity);

            //vente
            vente_service _dto = new vente_service()
            {
                id = this.id,
                date_vente = DateTime.Now,
                id_agent = (this.agent != null) ? this.agent.id : 0,
                id_client = (this.customer != null) ? this.customer.id : 0,
                montant_vente = montant,
                montant_recu = this.amount_received,
                reliquat = this.amount_received - montant,
                vente_details = detailsVente,
                client = (this.customer != null) ? this.customer.loadDto() : null,
                utilisateur = (this.agent != null) ? this.agent.loadDto() : null,
            };
            return _dto;
        }


        public static List<ServiceSale> CreateFromList(List<vente_service> _dboList)
        {
            List<ServiceSale> _list = new List<ServiceSale>();
            foreach (vente_service item in _dboList)
            {
                ServiceSale _businessObject = ServiceSale.Create(item);
                _list.Add(_businessObject);
            }
            return _list;
        }



    }
}
