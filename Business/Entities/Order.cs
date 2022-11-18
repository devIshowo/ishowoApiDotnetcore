using ItCommerce.DTO.ModelDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItCommerce.Business.Entities
{
    public class Order
    {
        public int id { get; set; }
        public Profil agent { get; set; }
        public Compartment destination { get; set; }
        public Supplier supplier { get; set; }
        public List<ProductInStock> product_lines { get; set; }
        public DateTime date { get; set; }
        public int amount { get; set; }
        public int amount_paid { get; set; }
        public bool is_balanced { get; set; } //si la commande a ete integralement payee


        public static Order Create(commande _dbo)
        {
            List<ProductInStock> pSList = new List<ProductInStock>();
            if(_dbo.commande_details != null)
            {
                ProductInStock pStk;
                foreach (var item in _dbo.commande_details)
                {
                    pStk = new ProductInStock();
                    pStk.product = (item.produit_type_mesure != null)? ProdMeasureType.Create(item.produit_type_mesure) : null;
                    pStk.quantity = item.qte_cmde;
                    pStk.purchase_price = item.p_achat;
                    pStk.selling_price = (item.p_vente != null) ? (int) item.p_vente : 0;
                    pStk.barcode = item.reference;
                    pStk.expiration_date = item.date_exp;
                    pSList.Add(pStk);
                }
            }

            return new Order()
            {
                id = _dbo.id,
                supplier = (_dbo.fournisseur!=null)? Supplier.Create(_dbo.fournisseur): null,
                date = _dbo.date_cmde,
                amount = _dbo.montant_cmde,
                amount_paid = _dbo.montant_sorti,
                product_lines = pSList,
                destination = (_dbo.rayon != null)? Compartment.Create(_dbo.rayon) : null,
                agent = (_dbo.profil != null) ? Profil.Create(_dbo.profil) : null,
                is_balanced = (_dbo.montant_sorti < _dbo.montant_cmde) ? false : true
            };
        }

        public commande loadDto()
        {
            int montant = 0;

            //commande details
            List<commande_details> detailsCommande = new List<commande_details>();
            foreach (var item in this.product_lines)
            {
                commande_details detailCmde = new commande_details();
                detailCmde.id_produit_mesure = (item.product!=null)? item.product.id : 0;
                detailCmde.qte_cmde = item.quantity;
                detailCmde.p_achat = item.purchase_price;
                detailCmde.p_vente = item.selling_price;
                detailCmde.statut = "EMIS";
                detailCmde.reference = item.barcode;
                detailCmde.date_exp = item.expiration_date;
                detailsCommande.Add(detailCmde);
            }

            //montant
            montant = this.product_lines.Sum(x => x.purchase_price * x.quantity);

            //commande
            commande _dto = new commande()
            {
                id = this.id,
                date_cmde = DateTime.Now,
                id_fournisseur = (this.supplier!=null)? this.supplier.id : 0,
                id_profil = (this.agent != null) ? this.agent.id : 0,
                montant_cmde = montant,
                montant_sorti = this.amount_paid,
                statut = "EMIS",
                id_rayon = (this.destination != null)? this.destination.id : 0,
                commande_details = detailsCommande

            };
            return _dto;
        }


        public static List<Order> CreateFromList(List<commande> _dboList)
        {
            List<Order> _list = new List<Order>();
            foreach (commande item in _dboList)
            {
                Order _businessObject = Order.Create(item);
                _list.Add(_businessObject);
            }
            return _list;
        }

    }
}
