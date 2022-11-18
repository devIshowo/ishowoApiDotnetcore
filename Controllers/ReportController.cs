using ItCommerce.Api.Net.Extra;
using ItCommerce.Api.Net.Logger;
using ItCommerce.Api.Net.Models;
using ItCommerce.Business.Entities;
using ItCommerce.Business.Extra;
using ItCommerce.DTO.SpecClasses;
using ItCommerce.Reporting;
using Microsoft.AspNetCore.Mvc;
using System;
using ItCommerce.DTO.ModelDesign;
using ItCommerce.DTO.Factory;
using ItCommerce.DTO.SpecClasses;
using DinkToPdf;
using DinkToPdf.Contracts;
using System.IO;
using ItCommerce.Reporting.Reports;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ItCommerce.Reporting.ViewModels;
using System.Collections.Generic;
using ItCommerce.Reporting.Factory;
using Wkhtmltopdf.NetCore;
using API.Business.Extra;

namespace ItCommerce.Api.Net.Controllers
{
    [UserLicenseCheckerFilter]
    [CustomExceptionFilter]
    [Route("api/[controller]/[action]")]
    public class ReportController : Controller
    {
        private IItCommerceRepository _itComRepository;
        private ReportObject _reportObject;
        private ResourceObject _resourceObject;
        private IConverter _converter;
        private ITemplateService _templateService;
        readonly IGeneratePdf _generatePdf;
        private ILogger _logger;

        public ReportController(IItCommerceRepository _repository, ReportObject _repObject, ResourceObject _resObject, IT_COMMERCEEntities context,
                                IConverter converter, ITemplateService _templtService, ILoggerFactory loggerFactory, IGeneratePdf generatePdf)
        {
            _itComRepository = _repository;
            //_itComRepository._context = context;
            _reportObject = _repObject;
            this._resourceObject = _resObject;
            ItCommerceFactory.SetEntity(context);
            _converter = converter;
            _templateService = _templtService;
            _logger = loggerFactory.CreateLogger("Reports");
            _generatePdf = generatePdf;
        }

        // post api/PrintSales
        [HttpPost]
        public async Task<IActionResult> PrintSales([FromBody] PeriodParam obj)
        {
            try
            {
                var models = _itComRepository.PrintSaleList(obj);
                models.datePrint = DateTime.Now;
                models.baseUrl = $"{Request.Scheme}://{Request.Host.Value}";
                return await _generatePdf.GetPdf("Views/SaleList.cshtml", models);
            }
            catch (Exception ex)
            {
                string msg = StringObject.ExtractExceptionData(ex); // string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                LogLibrary.LogError("PrintSales", msg);
                ActivityLogger.SaveLogger(msg);
                throw new Exception("Une erreur inconnue est survenue");
                ////
                ///;
            }
        }//fin PrintSales


        // post api/PrintSale
        [HttpPost]
        public async Task<IActionResult> PrintSale([FromBody] ProductSale obj)
        {
            try
            {
                if (obj.with_invoice == false)
                {
                    var models = _itComRepository.PrintAvoirDetail(obj);

                    if (models.saleDetail.customer == null)
                    {
                        models.saleDetail.customer = new Customer();
                    }
                    AvoirDetailView SaleDetails = new AvoirDetailView()
                    {
                        baseUrl = $"{Request.Scheme}://{Request.Host.Value}",
                        datePrint = DateTime.Now,
                        saleDetail = models.saleDetail,
                        invoice = models.invoice,
                        Ref_ORIGIN = models.Ref_ORIGIN,
                        Mecef_ORIGIN = models.Mecef_ORIGIN
                    };
                    return await _generatePdf.GetPdf("Views/AvoirDetail.cshtml", SaleDetails);
                }
                else
                {
                    var models = _itComRepository.PrintSaleDetail(obj);

                    if (models.saleDetail.customer == null)
                    {
                        models.saleDetail.customer = new Customer();
                    }
                    SaleDetailView SaleDetails = new SaleDetailView()
                    {
                        baseUrl = $"{Request.Scheme}://{Request.Host.Value}",
                        datePrint = DateTime.Now,
                        saleDetail = models.saleDetail,
                        invoice = models.invoice
                    };
                    return await _generatePdf.GetPdf("Views/SaleDetail.cshtml", SaleDetails);
                }
            }
            catch (Exception ex)
            {
                string msg = StringObject.ExtractExceptionData(ex);// string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                LogLibrary.LogError("PrintSale", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }//fin PrintSale

        // post api/PrintOrders
        [HttpPost]
        public async Task<IActionResult> PrintOrders([FromBody] PeriodParam obj)
        {
            try
            {
                var models = _itComRepository.PrintOrderList(obj);
                models.datePrint = DateTime.Now;
                models.baseUrl = $"{Request.Scheme}://{Request.Host.Value}";
                return await _generatePdf.GetPdf("Views/OrderList.cshtml", models);
            }
            catch (Exception ex)
            {
                string msg = StringObject.ExtractExceptionData(ex); // string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                LogLibrary.LogError("PrintOrders", msg);
                ActivityLogger.SaveLogger(msg);
                throw ex;
            }
        }//fin PrintSales

        // post api/PrintOrder
        [HttpPost]
        public async Task<IActionResult> PrintOrder([FromBody] Order obj)
        {
            try
            {

                var models = _itComRepository.PrintOrderDetail(obj);
                OrderDetailView OrderDetails = new OrderDetailView()
                {
                    baseUrl = $"{Request.Scheme}://{Request.Host.Value}",
                    datePrint = DateTime.Now,
                    orderDetail = models
                };
                return await _generatePdf.GetPdf("Views/OrderDetail.cshtml", OrderDetails);
            }
            catch (Exception ex)
            {
                string msg = StringObject.ExtractExceptionData(ex);// string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                LogLibrary.LogError("PrintOrder", msg);
                ActivityLogger.SaveLogger(msg);
                throw ex;
            }
        }//fin PrintOrder


        // post api/PrintTransfers
        [HttpPost]
        public async Task<IActionResult> PrintTransfers([FromBody] PeriodParam obj)
        {
            try
            {
                var models = _itComRepository.PrintTransfers(obj);
                models.datePrint = DateTime.Now;
                models.baseUrl = $"{Request.Scheme}://{Request.Host.Value}";
                return await _generatePdf.GetPdf("Views/TransfertList.cshtml", models);
            }
            catch (Exception ex)
            {
                string msg = StringObject.ExtractExceptionData(ex);// string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                LogLibrary.LogError("PrintTransfers", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }//fin PrintTransfers

        // post api/PrintTransfer
        [HttpPost]
        public async Task<IActionResult> PrintTransfer([FromBody] StockTransfer obj)
        {
            try
            {
                var models = _itComRepository.PrintTransfer(obj);
                TransfertDetailView TransfertDetails = new TransfertDetailView()
                {
                    baseUrl = $"{Request.Scheme}://{Request.Host.Value}",
                    datePrint = DateTime.Now,
                    transfertDetail = models
                };
                return await _generatePdf.GetPdf("Views/TransfertDetail.cshtml", TransfertDetails);
            }
            catch (Exception ex)
            {
                string msg = StringObject.ExtractExceptionData(ex);// string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                LogLibrary.LogError("PrintTransfer", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }//fin PrintTransfer


        // post api/PrintOrder
        [HttpPost]
        public async Task<IActionResult> PrintStockView([FromBody] Profil obj)
        {
            try
            {
                var models = _itComRepository.PrintStockView(obj);
                models.datePrint = DateTime.Now;
                models.baseUrl = $"{Request.Scheme}://{Request.Host.Value}";
                models.company = obj.agency.company;
                return await _generatePdf.GetPdf("Views/StockList.cshtml", models);
            }
            catch (Exception ex)
            {
                string msg = StringObject.ExtractExceptionData(ex);// string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                LogLibrary.LogError("PrintStockView", msg);
                ActivityLogger.SaveLogger(msg);
                throw ex;
            }
        }//fin PrintStockView


        // post api/PrintDevis
        [HttpPost]
        public async Task<IActionResult> PrintDevis([FromBody] ProductSale obj)
        {
            try
            {
                var models = _itComRepository.PrintDevis(obj);
                return await _generatePdf.GetPdf("Views/SaleDetail.cshtml", models);
            }
            catch (Exception ex)
            {
                string msg = StringObject.ExtractExceptionData(ex);// string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                LogLibrary.LogError("PrintDevis", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }//fin PrintDevis

        // post api/PrintBarcodes
        //[HttpPost]
        //public FileObject PrintBarcodes([FromBody] PeriodParam param)
        //{
        //    try
        //    {
        //        FileObject fileObj = _itComRepository.PrintBarcodes(param, _reportObject.destinationPath, _resourceObject.destinationPath);
        //        return fileObj;
        //    }
        //    catch (Exception ex)
        //    {
        //        string msg = StringObject.ExtractExceptionData(ex);// string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
        //        LogLibrary.LogError("PrintBarcodes", msg);
        //        return BadRequest(Constant.GenericError);;
        //    }
        //}//fin PrintBarcodes

        // post api/PrintOrder
        [HttpPost]
        public async Task<IActionResult> PrintBarcodes([FromBody] PeriodParam obj)
        {
            try
            {
                var models = _itComRepository.PrintBarcodes(obj);
                BarcodeListModel listing = new BarcodeListModel()
                {
                    barcodes = models,
                    baseUrl = $"{Request.Scheme}://{Request.Host.Value}",
                    datePrint = DateTime.Now
                };
                return await _generatePdf.GetPdf("Views/BarcodeList.cshtml", listing);
            }
            catch (Exception ex)
            {
                string msg = StringObject.ExtractExceptionData(ex);
                // string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                LogLibrary.LogError("PrintBarcodes", msg);
                ActivityLogger.SaveLogger(msg);
                throw ex;
                //throw new Exception("Une erreur est survenue");
            }
        }//fin PrintBarcodes


    }
}
