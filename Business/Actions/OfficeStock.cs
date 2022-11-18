using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItCommerce.Business.Entities;
using ItCommerce.DTO.DbMethods;
using ItCommerce.DTO.ModelDesign;
using ItCommerce.Business.Extra;
using ItCommerce.Reporting;
using DinkToPdf;
using DinkToPdf.Contracts;
using ItCommerce.Reporting.Reports;
using ItCommerce.Reporting.ViewModels;

namespace ItCommerce.Business.Actions
{
    public class OfficeStock : IOffice
    {

        #region orders
        public static Order createOrder(Order item)
        {
            commande result = DtoStock.createOrder(item.loadDto(), item.destination.id, item.agent.id);
            return Order.Create(result);
        }

        public static SavedOrder createSavedOrder(SavedOrder item)
        {
            commande result = DtoStock.createSavedOrder(item.loadDto(), item.agent.id);
            return SavedOrder.Create(result);
        }

        public static Order payOrderBalance(Order item)
        {
            commande result = DtoStock.payOrderBalance(item.loadDto(), item.agent.id);
            return Order.Create(result);
        }

        public static List<Order> getListOrders(int id)
        {
            List<Order> _listBsList = Order.CreateFromList(DtoStock.loadCommandesList(id));
            return _listBsList;
        }//fin getListOrders 

        public static List<SavedOrder> getListSavedOrders(int id)
        {
            List<SavedOrder> _listBsList = SavedOrder.CreateFromList(DtoStock.loadSavedCommandesList(id));
            return _listBsList;
        }//fin getListSavedOrders 

        public static List<Order> getListNotCompletedOrders(int id)
        {
            List<Order> _listBsList = Order.CreateFromList(DtoStock.loadCommandesNonRegleesList(id));
            return _listBsList;
        }//fin getListNotCompletedOrders         

        public static List<Order> getListOrders(PeriodParam obj)
        {
            List<Order> _listBsList = Order.CreateFromList(DtoStock.loadCommandesList(obj.startDate, obj.endDate, obj.agent.id));
            return _listBsList;
        }//fin getListOrders

        //deleteSavedOrder
        public static SavedOrder deleteSavedOrder(int id)
        {
            commande result = DtoStock.deleteSavedOrder(id);
            return new SavedOrder();
        }//fin deleteSavedOrder

        #endregion orders


        #region stock view


        public static List<ProductInStock> printBarcodes(List<ProductInStock> prodList, int idProfil)
        {
            return prodList;
        }//fin getStockView

        public static List<ProductInStock> generateBarcodes(List<ProductInStock> prodList, int idProfil)
        {
            List<stock> _dtoStockList = new List<stock>();
            foreach (ProductInStock item in prodList)
            {
                _dtoStockList.Add(item.loadDto());
            }
            List<ProductInStock> _listBsList = ProductInStock.CreateFromList(DtoStock.generateBarcodes(_dtoStockList, idProfil));
            return _listBsList;
        }//fin generatetBarcodes

        public static List<ProductInStock> getStockView(int id)
        {
            List<ProductInStock> _listBsList = ProductInStock.CreateFromList(DtoStock.loadStockView(id));
            return _listBsList;
        }//fin getStockView

        public static List<ProductInStock> searchStockView(int id, string productName, string productCode)
        {
            List<ProductInStock> _listBsList = ProductInStock.CreateFromList(DtoStock.searchStockView(id, productName, productCode));
            return _listBsList;
        }//fin searchStockView

        public static List<ProductInStock> getStockViewForSale(int id)
        {
            List<ProductInStock> _listBsList = ProductInStock.CreateFromList(DtoStock.loadStockViewForSale(id));
            return _listBsList;
        }//fin getStockViewForSale

        public static List<ProductInStock> getStockViewForSaleLimitByName(int id, string name)
        {
            List<ProductInStock> _listBsList = ProductInStock.CreateFromList(DtoStock.loadStockViewForSaleLimitByName(id, name));
            return _listBsList;
        }//fin getStockViewForSale

        public static List<ProductInStock> getStockViewForSaleLimitByCode(int id, string code)
        {
            List<ProductInStock> _listBsList = ProductInStock.CreateFromList(DtoStock.loadStockViewForSaleLimitByCode(id, code));
            return _listBsList;
        }//fin getStockViewForSale

        public static ProductInStock updateStockView(ProductInStock obj, int id)
        {
            ProductInStock _bs = ProductInStock.Create(DtoStock.updateStockView(obj.loadDto(), id));
            return _bs;
        }//fin updateStockView

        public static ProductInStock expandStockView(ProductInStock obj, int id)
        {
            ProductInStock _bs = ProductInStock.Create(DtoStock.expandStockView(obj.loadDto(), id));
            return _bs;
        }//fin expandStockView

        public static List<ProductInStock> getProductStockView(int idUser, int idProduit)
        {
            List<ProductInStock> _listBsList = ProductInStock.CreateFromList(DtoStock.loadProductStockView(idUser, idProduit));
            return _listBsList;
        }//fin getProductStockView

        //get stock expired
        public static List<ProductInStock> getStockExpired(PeriodParam obj, int idUser)
        {
            List<ProductInStock> _listBsList = ProductInStock.CreateFromList(DtoStock.loadProductStockExpired(idUser, obj.startDate));
            return _listBsList;
        }//fin getStockExpired

        #endregion stock view

        #region sales
        //public static Sale createSale(Sale item)
        //{
        //    vente result = DtoStock.createSale(item.loadDto(), item.agent.id);
        //     return Sale.Create(result);
        //}

        public static SaleDataView createSale(ProductSale _sale)
        {
            SaleDataView dtoVente = DtoStock.createSale(_sale.loadDto(), _sale.agent.id);
            ProductSale savedSale = ProductSale.CreateDeep(dtoVente.sale);
            var result = PrinterManager.printSaleNew(savedSale);
            dtoVente.saleData = result;
            return dtoVente;
        } //PrintOrderDetail


        public static ProductSale paySaleBalance(ProductSale item)
        {
            vente_produit result = DtoStock.paySaleBalance(item.loadDto(), item.agent.id);
            return ProductSale.Create(result);
        }

        public static Invoice normalizeSale(int id)
        {
            facture result = DtoMecef.normalizeSale(id);
            return Invoice.Create(result);
        }

        public static AvoirDataView cancelSale(ProductSale obj)
        {
            AvoirDataView dtoVente = new AvoirDataView();
            facture searchInvoice = DtoStock.printSaleNormalised(obj.id);
            facture_avoir processCancel = DtoMecef.choiceCancelMethod(obj.id);
            dtoVente.Ref_Origin = searchInvoice.Compteur_Total_Mecef;
            dtoVente.Mecef_Origin = searchInvoice.Code_Mecef;
            dtoVente.saleData = obj;
            dtoVente.invoice = processCancel;
            return dtoVente;
        }

        public static ProductSale createDevis(ProductSale item)
        {
            vente_produit dtoVente = DtoStock.createDevis(item.loadDto(), item.agent.id);
            ProductSale savedSale = ProductSale.CreateDeep(dtoVente);
            var result = PrinterManager.printSaleNew(savedSale);
            return result;
        }

        public static List<ProductSale> getListSales(int id)
        {
            List<ProductSale> _listBsList = ProductSale.CreateFromList(DtoStock.loadVentesList(id));
            return _listBsList;
        }
        public static List<ProductSale> getListSales(PeriodParam obj)
        {
            List<ProductSale> _listBsList = ProductSale.CreateFromList(DtoStock.loadVentesList(obj.startDate, obj.endDate, obj.agent.id));
            return _listBsList;
        }//fin getListSales

        public static List<ProductSale> getListDevis(int id)
        {
            List<ProductSale> _listBsList = ProductSale.CreateFromDevisList(DtoStock.loadDevisList(id));
            return _listBsList;
        }
        public static List<ProductSale> getListDevis(PeriodParam obj)
        {
            List<ProductSale> _listBsList = ProductSale.CreateFromDevisList(DtoStock.loadDevisList(obj.startDate, obj.endDate, obj.agent.id));
            return _listBsList;
        }//fin getListDevis

        #endregion sales

        #region services sales

        public static async Task<byte[]> createServiceSale(ServiceSale _sale)
        {
            vente_service dtoVente = DtoStock.createServiceSale(_sale.loadDto(), _sale.agent.id);
            ServiceSale savedSale = ServiceSale.CreateDeep(dtoVente);
            var result = PrinterManager.printServiceSaleNew(savedSale);
            return null;
        } //PrintOrderDetail


        public static List<ServiceSale> getListServiceSales(int id)
        {
            List<ServiceSale> _listBsList = ServiceSale.CreateFromList(DtoStock.loadVentesServiceList(id));
            return _listBsList;
        }
        public static List<ServiceSale> getListServiceSales(PeriodParam obj)
        {
            List<ServiceSale> _listBsList = ServiceSale.CreateFromList(DtoStock.loadVentessServiceList(obj.startDate, obj.endDate, obj.agent.id));
            return _listBsList;
        }//fin getListSales



        #endregion services sales

        #region stock limit

        /// <summary>
        /// liste stock limit
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static List<ProdStockLimit> getStockLimit(int id)
        {
            List<ProdStockLimit> _listBsList = ProdStockLimit.CreateFromList(DtoStock.loadStockLimit(id));
            return _listBsList;
        }//fin getStockLimit

        /// liste produit en dessous du stock
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static List<ProductInStock> getListProdUnderStock(int id)
        {
            List<ProductInStock> _listBsList = ProductInStock.CreateFromList(DtoStock.loadProduitsHorsSeuilStock(id));
            return _listBsList;
        }//fin getListProdUnderStock

        public static ProdStockLimit createStockLimit(ProdStockLimit item)
        {
            produit_stock_limit result = DtoStock.createStockLimit(item.loadDto(), item.agent.id);
            return ProdStockLimit.Create(result);
        }//fin createStockLimit
        public static ProdStockLimit updateStockLimit(ProdStockLimit item)
        {
            produit_stock_limit result = DtoStock.updateStockLimit(item.loadDto());
            return ProdStockLimit.Create(result);
        }//fin updateStockLimit

        public static ProdStockLimit deleteStockLimit(int id)
        {
            produit_stock_limit result = DtoStock.deleteStockLimit(id);
            return new ProdStockLimit();
        }//fin deleteStockLimit


        #endregion stock limit



        #region transfer

        public static StockTransfer createTransfer(StockTransfer item)
        {
            transfert result = DtoStock.createTransfer(item.loadDto(), item.agent.id);
            return StockTransfer.Create(result);
        }

        public static List<StockTransfer> getListStockTransfers(int id)
        {
            List<StockTransfer> _listBsList = StockTransfer.CreateFromList(DtoStock.loadTransfertsList(id));
            return _listBsList;
        }//fin getListStockTransfers

        public static List<StockTransfer> getListStockTransfers(PeriodParam obj)
        {
            List<StockTransfer> _listBsList = StockTransfer.CreateFromList(DtoStock.loadTransfertsList(obj.startDate, obj.endDate, obj.agent.id));
            return _listBsList;
        }//fin getListStockTransfers



        #endregion transfer

    }
}
