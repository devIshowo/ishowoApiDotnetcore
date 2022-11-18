using ItCommerce.DTO.ModelDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItCommerce.Business.Entities
{
    public class StockTransfer
    {
        public int id { get; set; }
        public Profil agent { get; set; }
        public Compartment source { get; set; }
        public Compartment destination { get; set; }
        
        public List<ProductInStock> product_lines { get; set; }
        public DateTime date { get; set; }


        public static StockTransfer Create(transfert _dbo)
        {
            List<ProductInStock> pSList = new List<ProductInStock>();
            if (_dbo.transfert_details != null)
            {
                ProductInStock pStk;
                foreach (var item in _dbo.transfert_details)
                {
                    pStk = new ProductInStock();
                    pStk.product = (item.produit_type_mesure != null) ? ProdMeasureType.Create(item.produit_type_mesure) : null;
                    pStk.quantity_transfer = item.quantite;
                    pStk.purchase_price = item.p_achat;
                    pStk.selling_price = item.p_vente;
                    pSList.Add(pStk);
                }
            }

            return new StockTransfer()
            {
                id = _dbo.id,
                source = (_dbo.rayon_source != null) ? Compartment.Create(_dbo.rayon_source) : null,
                destination = (_dbo.rayon_destination != null) ? Compartment.Create(_dbo.rayon_destination) : null,
                agent = (_dbo.profil != null) ? Profil.Create(_dbo.profil) : null,
                date = _dbo.date_transfert,
                product_lines = pSList,
            };
        }

        public transfert loadDto()
        {
            //transfert details
            List<transfert_details> detailsLines = new List<transfert_details>();
            foreach (var item in this.product_lines)
            {
                if (item.quantity_transfer <= 0) continue;
                transfert_details detailLine = new transfert_details();
                detailLine.id_produit_mesure = (item.product != null) ? item.product.id : 0;
                detailLine.quantite = item.quantity_transfer;
                detailLine.p_achat = item.purchase_price;
                detailLine.p_vente = item.selling_price;
                detailsLines.Add(detailLine);
            }

            //transfert
            transfert _dto = new transfert()
            {
                id = this.id,
                date_transfert = DateTime.Now,
                id_source = (this.source != null) ? this.source.id : 0,
                id_destination = (this.destination != null) ? this.destination.id : 0,
                id_profil = (this.agent != null) ? this.agent.id : 0,
                transfert_details = detailsLines,
            };
            return _dto;
        }//fin loadDto


        public static List<StockTransfer> CreateFromList(List<transfert> _dboList)
        {
            List<StockTransfer> _list = new List<StockTransfer>();
            foreach (transfert item in _dboList)
            {
                StockTransfer _businessObject = StockTransfer.Create(item);
                _list.Add(_businessObject);
            }
            return _list;
        }


    }
}
