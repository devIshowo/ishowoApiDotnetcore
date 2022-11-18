using ItCommerce.Business.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ItCommerce.Reporting.Reports;
using DinkToPdf.Contracts;
using ItCommerce.Reporting.Factory;
using ItCommerce.Business.Extra;
using ItCommerce.Reporting.ViewModels;
using QRCodeApp;

namespace ItCommerce.Reporting
{
    public class PrinterManager
    {
        /// <summary>
        /// print devis
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static string printDevis(ProductSale _data, Company company)
        {
            try
            {
                //CostEstimateForm formManager = new CostEstimateForm(_data, destinationPath, resourcePath, company);
                //string printedFile = formManager.generatePdf();
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }//end printDevis


        /// <summary>
        /// print order
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static SaleViewModel printSaleList(List<ProductSale> _saleList, PeriodParam param)
        {
            try
            {
                SaleViewModel viewModel = new SaleViewModel();
                viewModel.saleList = _saleList;
                viewModel.param = param;
                return viewModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }//end sale printSaleList

        // /// <summary>
        // /// print orders list
        // /// </summary>
        // /// <param name=""></param>
        // /// <returns></returns>
        // public static string printOrderList(List<Order> _dataList, DateTime startDate, DateTime endDate,
        //     string destinationPath, string resourcePath, Company company)
        // {
        //     try
        //     {
        //         //OrderListForm formManager = new OrderListForm(_dataList, startDate, endDate, destinationPath, resourcePath, company);
        //         //string printedFile = formManager.generatePdf();
        //         return null;
        //     }
        //     catch (Exception ex)
        //     {
        //         throw ex;
        //     }
        // }//end sale printOrderList

        /// <summary>
        /// print order
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static OrderViewModel printOrderList(List<Order> _orderList, PeriodParam param)
        {
            try
            {
                OrderViewModel viewModel = new OrderViewModel();
                viewModel.orderList = _orderList;
                viewModel.param = param;
                return viewModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }//end sale printOrderList


        /// <summary>
        /// print transfert list
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static TransfertViewModel printTransferList(List<StockTransfer> _dataList, PeriodParam param)
        {
            try
            {
                TransfertViewModel viewModel = new TransfertViewModel();
                viewModel.transfertList = _dataList;
                viewModel.param = param;
                return viewModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }//end sale printTransferList

        /// <summary>
        /// print Transfer
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static StockTransfer printTransfertDetail(StockTransfer _data)
        {
            try
            {
                var viewModel = _data;
                return viewModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }//end sale printTransfer

        /// printStockView
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static StockViewModel printStockView(List<ProductInStock> _prodList)
        {
            try
            {
                StockViewModel viewModel = new StockViewModel();
                viewModel.stockList = _prodList;
                return viewModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }//end sale printStockView

        /// <summary>
        /// print sale invoice
        /// </summary>
        /// <param name="_sale"></param>
        /// <returns></returns>
        public static string printTestReceiptPrinterForPOS(ProductSale _sale, string destinationPath, string resourcePath, Company company)
        {
            try
            {
                //SalePOSForm invoice = new SalePOSForm(_sale, destinationPath, resourcePath, company);
                //string printedFile = invoice.generatePdf();
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }//end printTestReceiptPrinterForPOS

        /// <summary>
        /// print test
        /// </summary>
        /// <param name="_sale"></param>
        /// <returns></returns>
        public static async Task<byte[]> printTestFile(ITemplateService _templateService, IConverter _converter,
            ProductSale _sale, string destinationPath, string resourcePath, Company company)
        {
            try
            {
                var viewModel = new ProductSale();
                string reportTemplate = "Model3";
                var printedFile = await ReportManager.GenerateReportFile(_templateService, _converter, reportTemplate, viewModel);
                return printedFile;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }//end sale invoice

        /// <summary>
        /// print order
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static string printOrder(Order _data, Company company)
        {
            try
            {
                //OrderForm formManager = new OrderForm(_data, destinationPath, resourcePath, company);
                //string printedFile = formManager.generatePdf();
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }//end sale printOrder


        /// <summary>
        /// print order
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static Order printOrderDetail(Order _data)
        {
            try
            {
                var viewModel = _data;
                return viewModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }//end sale printOrder

        /// <summary>
        /// print order
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static ProductSale printSaleNew(ProductSale _data)
        {
            try
            {
                var viewModel = _data;
                return viewModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }//end sale printSaleNew

        public static ServiceSale printServiceSaleNew(ServiceSale _data)
        {
            try
            {
                var viewModel = _data;
                return viewModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }//end sale printSaleNew

        /// <summary>
        /// print order
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static ProductSale printSaleDetail(ProductSale _data)
        {
            try
            {
                var viewModel = _data;
                return viewModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }//end sale printSaleDetail

        ///genere uen serie de code barres
        public static List<BarcodeViewModel> printBarCodeList(List<ProductInStock> _dataList)
        {
            try
            {
                List<BarcodeViewModel> bcVMList = new List<BarcodeViewModel>();
                foreach (var item in _dataList)
                {
                    BarcodeViewModel vm = new BarcodeViewModel();
                    // item.barcode = "9782409007613";
                    vm.base64Image = QRCodeTagHelper.GenereteNewCode(item.barcode);
                    if (ProdMeasureType.ToString(item.product).Length > 35)
                    {
                        vm.description = ProdMeasureType.ToString(item.product).Substring(0, 32) + "...";
                    }
                    else
                    {
                        vm.description = ProdMeasureType.ToString(item.product);
                    }
                    bcVMList.Add(vm);
                }
                var viewModel = bcVMList;
                return viewModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }//end sale printBarCodeList

    }
}
