using ItCommerce.Business.Entities;
using ItCommerce.Business.Extra;
using ItCommerce.DTO.Actions;
using ItCommerce.DTO.ModelDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItCommerce.Reporting;
using DinkToPdf;
using DinkToPdf.Contracts;
using ItCommerce.Reporting.Reports;
using Microsoft.AspNetCore.Mvc;
using ItCommerce.Reporting.ViewModels;
using ItCommerce.DTO.DbMethods;

namespace ItCommerce.Business.Actions
{
    public class OfficeReporting : IOffice
    {

        #region print

        //print order detail
        public static Order printOrderDetail(Order _order)
        {
            var result = PrinterManager.printOrderDetail(_order);
            return result;
        } //PrintOrderDetail

        //print sale detail
        public static SaleDataView printSaleDetail(ProductSale _sale)
        {
            SaleDataView dtoVente = new SaleDataView();
            ProductSale saleDetail = PrinterManager.printSaleDetail(_sale);
            facture normalisation = DtoStock.printSaleNormalised(saleDetail.id);
            if (normalisation != null)
            {
                dtoVente.invoice = normalisation;
            }
            dtoVente.saleData = saleDetail;
            return dtoVente;
        } //PrintSaleDetail

        public static AvoirDataView printAvoirDetail(ProductSale _sale)
        {
            AvoirDataView dtoVente = new AvoirDataView();
            ProductSale saleDetail = PrinterManager.printSaleDetail(_sale);
            facture searchInvoice = DtoStock.printSaleNormalised(_sale.id);
            facture_avoir normalisation = DtoStock.printSaleCanceled(saleDetail.id);
            if (normalisation != null)
            {
                dtoVente.invoice = normalisation;
            }
            dtoVente.saleData = saleDetail;
            dtoVente.Ref_Origin = searchInvoice.Compteur_Total_Mecef;
            dtoVente.Mecef_Origin = searchInvoice.Code_Mecef;
            return dtoVente;
        } //PrintSaleDetail

        //print sale list
        public static SaleViewModel printSaleList(List<ProductSale> _saleList, PeriodParam param)
        {
            var result = PrinterManager.printSaleList(_saleList, param);
            return result;
        } //printSaleList

        //print order list
        public static OrderViewModel printOrderList(List<Order> _orderList, PeriodParam param)
        {
            var result = PrinterManager.printOrderList(_orderList, param);
            return result;
        } //printOrderList

        //print stock view
        public static StockViewModel printStockView(List<ProductInStock> _prodList)
        {
            var result = PrinterManager.printStockView(_prodList);
            return result;
        } //printStockView


        //print transferts view
        public static TransfertViewModel printTransfertList(List<StockTransfer> _transfertList, PeriodParam obj)
        {
            var result = PrinterManager.printTransferList(_transfertList, obj);
            return result;
        } //printTransfertsView

        public static StockTransfer printTransfertDetail(StockTransfer _transfert)
        {
            var result = PrinterManager.printTransfertDetail(_transfert);
            return result;
        } //PrintSaleDetail

        //print barcodes
        public static List<BarcodeViewModel> printBarCodeList(List<ProductInStock> _prodList)
        {
            var result = PrinterManager.printBarCodeList(_prodList);
            return result;
        } //printBarCodeList




        #endregion

    }
}
