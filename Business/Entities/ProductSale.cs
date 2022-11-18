using ItCommerce.DTO.ModelDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItCommerce.Business.Entities
{
    public class ProductSale
    {
        public int id { get; set; }
        public Profil agent { get; set; }
        public Customer customer { get; set; }
        public List<ProductLine> lines { get; set; }
        public DateTime date { get; set; }
        public double amount_original { get; set; }
        public double amount_remise { get; set; }
        public double amount_tva { get; set; }
        public double amount_to_pay { get; set; }
        public double amount_paid { get; set; }
        public double remainder { get; set; }
        public double taux_aib { get; set; }
        public double amount_aib { get; set; }
        public double rest_to_pay { get; set; }
        public Nullable<bool> with_invoice { get; set; }
        public string reference { get; set; }
        public bool is_balanced { get; set; } //si la vente a ete integralement payee



        public static ProductSale Create(vente_produit _dbo)
        {
            List<ProductLine> pSList = new List<ProductLine>();
            if (_dbo.vente_details != null)
            {
                ProductLine pStk;
                foreach (var item in _dbo.vente_details)
                {
                    pStk = ProductLine.CreateFromSaleLine(item);
                    pSList.Add(pStk);
                }
            }

            return new ProductSale()
            {
                id = _dbo.id,
                date = _dbo.date_vente,
                amount_original = _dbo.mt_original,
                amount_remise = _dbo.mt_remise,
                amount_tva = _dbo.mt_tva,
                amount_to_pay = _dbo.mt_a_payer,
                amount_paid = _dbo.mt_recu,
                amount_aib = _dbo.mt_aib,
                taux_aib = _dbo.t_aib,
                remainder = _dbo.reliquat,
                rest_to_pay = _dbo.reste_a_payer,
                customer = (_dbo.client != null) ? Customer.Create(_dbo.client) : null,
                agent = (_dbo.profil != null) ? Profil.Create(_dbo.profil) : null,
                lines = pSList,
                reference = _dbo.reference,
                with_invoice = _dbo.avec_facture,
                is_balanced = (_dbo.mt_recu < _dbo.mt_a_payer) ? false : true
            };
        }

        public static ProductSale CreateDeep(vente_produit _dbo)
        {
            List<ProductLine> pSList = new List<ProductLine>();
            if (_dbo.vente_details != null)
            {
                ProductLine pStk;
                foreach (var item in _dbo.vente_details)
                {
                    pStk = ProductLine.CreateFromSaleLineDeep(item);
                    pSList.Add(pStk);
                }
            }

            return new ProductSale()
            {
                id = _dbo.id,
                date = _dbo.date_vente,
                amount_original = _dbo.mt_original,
                amount_remise = _dbo.mt_remise,
                amount_tva = _dbo.mt_tva,
                amount_to_pay = _dbo.mt_a_payer,
                amount_paid = _dbo.mt_recu,
                remainder = _dbo.reliquat,
                taux_aib = _dbo.t_aib,
                amount_aib = _dbo.mt_aib,
                rest_to_pay = _dbo.reste_a_payer,
                customer = (_dbo.client != null) ? Customer.Create(_dbo.client) : null,
                agent = (_dbo.profil != null) ? Profil.Create(_dbo.profil) : null,
                lines = pSList,
                reference = _dbo.reference,
                with_invoice = _dbo.avec_facture,
                is_balanced = (_dbo.mt_recu < _dbo.mt_a_payer) ? false : true
            };
        }

        public vente_produit loadDto()
        {
            int montant = 0;

            //vente details
            List<vente_produit_details> detailsVente = new List<vente_produit_details>();
            foreach (var item in this.lines)
            {
                vente_produit_details detailVente = new vente_produit_details();
                detailVente.id_produit_mesure = (item.product != null) ? item.product.id : 0;
                detailVente.quantite = item.quantite;
                detailVente.id_rayon = (item.compartment != null) ? item.compartment.id : 0;
                detailVente.p_vente = item.p_vente;
                detailVente.p_achat = item.p_achat;
                detailVente.mt_remise = item.mt_remise;
                detailVente.mt_tva = item.mt_tva;
                detailVente.tax = item.tax;
                detailVente.libellets = item.libellets;
                detailVente.ts = item.ts;
                detailsVente.Add(detailVente);
            }

            //montant
            montant = this.lines.Sum(x => x.p_vente * x.quantite);

            //vente
            vente_produit _dto = new vente_produit()
            {

                id = this.id,
                date_vente = DateTime.Now,
                id_profil = (this.agent != null) ? this.agent.id : 0,
                id_client = (this.customer != null) ? this.customer.id : 0,
                mt_a_payer = this.amount_to_pay,
                mt_recu = this.amount_paid,
                reste_a_payer = this.rest_to_pay,
                reliquat = this.amount_paid - this.amount_to_pay,
                vente_details = detailsVente,
                mt_remise = this.amount_remise,
                mt_tva = this.amount_tva,
                mt_aib = this.amount_aib,
                t_aib = this.taux_aib,
                mt_original = this.amount_original,
                avec_facture = this.with_invoice,
                reference = (this.reference != null) ? this.reference : "",
                client = (this.customer != null) ? this.customer.loadDto() : null,
            };
            return _dto;
        }


        public static List<ProductSale> CreateFromList(List<vente_produit> _dboList)
        {
            List<ProductSale> _list = new List<ProductSale>();
            foreach (vente_produit item in _dboList)
            {
                ProductSale _businessObject = ProductSale.Create(item);
                _list.Add(_businessObject);
            }
            return _list;
        }



        /*devis area functions*/
        #region devis area
        public vente_produit loadDevisDto()
        {
            int montant = 0;

            //vente details
            List<vente_produit_details> detailsDevis = new List<vente_produit_details>();
            foreach (var item in this.lines)
            {
                vente_produit_details detailDevi = new vente_produit_details();
                detailDevi.id_produit_mesure = (item.product != null) ? item.product.id : 0;
                detailDevi.quantite = item.quantite;
                detailDevi.id_rayon = (item.compartment != null) ? item.compartment.id : 0;
                detailDevi.p_vente = item.p_vente;
                detailDevi.p_achat = item.p_achat;
                detailsDevis.Add(detailDevi);
            }

            //montant
            montant = this.lines.Sum(x => x.p_vente * x.quantite);

            //vente
            vente_produit _dto = new vente_produit()
            {
                id = this.id,
                date_vente = DateTime.Now,
                id_profil = (this.agent != null) ? this.agent.id : 0,
                id_client = (this.customer != null) ? this.customer.id : 0,
                mt_a_payer = montant,
                vente_details = detailsDevis,
                client = (this.customer != null) ? this.customer.loadDto() : null,
            };
            return _dto;
        }//fin loadDevisDto


        public static ProductSale CreateFromDevis(vente_produit _dbo)
        {
            List<ProductLine> pSList = new List<ProductLine>();
            if (_dbo.vente_details != null)
            {
                ProductLine pStk;
                foreach (var item in _dbo.vente_details)
                {
                    pStk = ProductLine.CreateFromDevisLine(item);
                    pSList.Add(pStk);
                }
            }

            return new ProductSale()
            {
                id = _dbo.id,
                date = _dbo.date_vente,
                amount_to_pay = _dbo.mt_a_payer,
                customer = (_dbo.client != null) ? Customer.Create(_dbo.client) : null,
                agent = (_dbo.profil != null) ? Profil.Create(_dbo.profil) : null,
                lines = pSList
            };
        }//fin CreateFromDevis


        public static List<ProductSale> CreateFromDevisList(List<vente_produit> _dboList)
        {
            List<ProductSale> _list = new List<ProductSale>();
            foreach (vente_produit item in _dboList)
            {
                ProductSale _businessObject = ProductSale.CreateFromDevis(item);
                _list.Add(_businessObject);
            }
            return _list;
        }
        #endregion devise area


    }
}
