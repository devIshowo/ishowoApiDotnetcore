using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ItCommerce.Api.Net.Models;
using ItCommerce.Business.Entities;
using ItCommerce.Business.Query;
using ItCommerce.Api.Net.Logger;
using ItCommerce.Business.Extra;
using ItCommerce.Api.Net.Extra;
using ItCommerce.DTO.ModelDesign;
using ItCommerce.DTO.Factory;
using System.Threading.Tasks;
using DinkToPdf;
using DinkToPdf.Contracts;
using System.IO;
using ItCommerce.Reporting.Reports;
using Microsoft.Extensions.Logging;
using Wkhtmltopdf.NetCore;
using ItCommerce.Reporting.ViewModels;
using API.Business.Extra;

namespace ItCommerce.Api.Net.Controllers
{
    [UserLicenseCheckerFilter]
    [CustomExceptionFilter]
    [Route("api/[controller]/[action]")]
    public class StockController : Controller
    {
        private IItCommerceRepository _itComRepository;
        private ResourceObject _resourceObject;
        //private IHostingEnvironment _hostingEnvironment;
        private ReportObject _reportObject;
        private IConverter _converter;
        private ITemplateService _templateService;
        readonly ILogger<StockController> _log;
        readonly IGeneratePdf _generatePdf;

        public StockController(IItCommerceRepository _repository, ReportObject _repObject, ResourceObject _resObject, IT_COMMERCEEntities context,
         IConverter converter, ITemplateService _templtService, ILogger<StockController> log, IGeneratePdf generatePdf)
        {
            _itComRepository = _repository;
            _reportObject = _repObject;
            this._resourceObject = _resObject;
            ItCommerceFactory.SetEntity(context);
            _converter = converter;
            _templateService = _templtService;
            _log = log;
            _generatePdf = generatePdf;
        }

        #region orders
        // post api/order
        [HttpPost]
        public ActionResult<Order> CreateOrder([FromBody] Order obj)
        {
            try
            {
                Order result = _itComRepository.CreateOrder(obj);
                return result;
            }
            catch (Exception ex)
            {
                string msg = StringObject.ExtractExceptionData(ex); //string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                                                                    // LogLibrary.LogError("CreateOrder", msg);
                _log.LogInformation(msg);
                return BadRequest(Constant.GenericError);;
            }
        }//fin CreateOrder

        [HttpPost]
        public ActionResult<SavedOrder> SaveOrder([FromBody] SavedOrder obj)
        {
            try
            {
                SavedOrder result = _itComRepository.CreateSavedOrder(obj);
                return result;
            }
            catch (Exception ex)
            {
                string msg = StringObject.ExtractExceptionData(ex); //string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                                                                    // LogLibrary.LogError("CreateOrder", msg);
                _log.LogInformation(msg);
                return BadRequest(Constant.GenericError);;
            }
        }//fin CreateOrder

        // post api/orderbalance
        [HttpPost]
        public ActionResult<Order> PayOrderBalance([FromBody] Order obj)
        {
            try
            {
                Order result = _itComRepository.PayOrderBalance(obj);
                return result;
            }
            catch (Exception ex)
            {
                string msg = StringObject.ExtractExceptionData(ex); //string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                // LogLibrary.LogError("PayOrderBalance", msg);
                _log.LogInformation(msg);
                return BadRequest(Constant.GenericError);;
            }
        }//fin PayOrderBalance


        // GET api/orders
        [HttpGet("{id}")]
        public ActionResult<List<Order>> Orders(int id)
        {
            try
            {
                List<Order> gList = _itComRepository.Orders(id);
                return gList;
            }
            catch (Exception ex)
            {
                string msg = StringObject.ExtractExceptionData(ex); //string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                // LogLibrary.LogError("Orders", msg);
                _log.LogInformation(msg);
                return BadRequest(Constant.GenericError);;
            }
        }

        // GET api/orders
        [HttpGet("{id}")]
        public ActionResult<List<SavedOrder>> SavedOrders(int id)
        {
            try
            {
                List<SavedOrder> gList = _itComRepository.SavedOrders(id);
                return gList;
            }
            catch (Exception ex)
            {
                string msg = StringObject.ExtractExceptionData(ex); //string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                // LogLibrary.LogError("Orders", msg);
                _log.LogInformation(msg);
                return BadRequest(Constant.GenericError);;
            }
        }


        // GET api/ordersnotcompleted
        [HttpGet("{id}")]
        public ActionResult<List<Order>> OrdersNotCompleted(int id)
        {
            try
            {
                List<Order> gList = _itComRepository.OrdersNotCompleted(id);
                return gList;
            }
            catch (Exception ex)
            {
                string msg = StringObject.ExtractExceptionData(ex); //string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                // LogLibrary.LogError("OrdersNotCompleted", msg);
                _log.LogInformation(msg);
                return BadRequest(Constant.GenericError);;
            }
        }//fin OrdersNotCompleted


        // post api/orders
        [HttpPost]
        public ActionResult<List<Order>> SearchOrders([FromBody] PeriodParam obj)
        {
            try
            {
                obj.startDate = obj.startDate.AddHours(1); obj.endDate = obj.endDate.AddHours(1);//because the sender is using gmt
                List<Order> gList = _itComRepository.SearchOrders(obj);
                return gList;
            }
            catch (Exception ex)
            {
                string msg = StringObject.ExtractExceptionData(ex); //string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                // LogLibrary.LogError("SearchOrders", msg);
                _log.LogInformation(msg);
                return BadRequest(Constant.GenericError);;
            }
        }

        // delete api/savedOrder
        [HttpDelete("{id}")]
        public ActionResult<SavedOrder> DeleteSavedOrder(int id)
        {
            try
            {
                SavedOrder result = _itComRepository.DeleteSavedOrder(id);
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                LogLibrary.LogError("DeleteSavedOrder", msg);
                _log.LogError(msg);
                return BadRequest(Constant.GenericError);;
            }
        }
        /*end delete savedOrder*/
        #endregion orders

        #region stock view

        // GET api/stockview
        [HttpGet("{id}")]
        public ActionResult<List<ProductInStock>> StockView(int id)
        {
            try
            {
                List<ProductInStock> gList = _itComRepository.StockView(id);
                return gList;
            }
            catch (Exception ex)
            {
                string msg = StringObject.ExtractExceptionData(ex); //string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                // LogLibrary.LogError("StockView", msg);
                _log.LogInformation(msg);
                return BadRequest(Constant.GenericError);;
            }
        }

        // post api/searchstockview
        [HttpPost("{id}")]
        public ActionResult<List<ProductInStock>> SearchStock([FromBody] QuerySale obj, int id)
        {
            try
            {
                List<ProductInStock> gList = _itComRepository.SearchStock(id, obj.product_name, obj.product_code);
                return gList;
            }
            catch (Exception ex)
            {
                string msg = StringObject.ExtractExceptionData(ex); //string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                // LogLibrary.LogError("SearchStockView", msg);
                _log.LogInformation(msg);
                return BadRequest(Constant.GenericError);;
            }
        }//ed SearchStock

        // GET api/stockviewforsale
        [HttpGet("{id}")]
        public ActionResult<List<ProductInStock>> StockViewForSale(int id)
        {
            try
            {
                List<ProductInStock> gList = _itComRepository.StockViewForSale(id);
                return gList;
            }
            catch (Exception ex)
            {
                string msg = StringObject.ExtractExceptionData(ex); //string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                // LogLibrary.LogError("StockViewForSale", msg);
                _log.LogInformation(msg);
                return BadRequest(Constant.GenericError);;
            }
        }//fin StockViewForSale

        // post api/stockviewforsalebyame
        [HttpPost("{id}")]
        public ActionResult<List<ProductInStock>> StockViewForSaleByName([FromBody] QuerySale obj, int id)
        {
            try
            {
                List<ProductInStock> gList = _itComRepository.StockViewForSaleLimitByName(id, obj.product_name);
                return gList;
            }
            catch (Exception ex)
            {
                string msg = StringObject.ExtractExceptionData(ex); //string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                // LogLibrary.LogError("StockViewForSaleByName", msg);
                _log.LogInformation(msg);
                return BadRequest(Constant.GenericError);;
            }
        }//fin StockViewForSaleByName


        // post api/stockviewforsalebycode
        [HttpPost("{id}")]
        public ActionResult<List<ProductInStock>> StockViewForSaleByCode([FromBody] QuerySale obj, int id)
        {
            try
            {
                List<ProductInStock> gList = _itComRepository.StockViewForSaleLimitByCode(id, obj.product_code);
                return gList;
            }
            catch (Exception ex)
            {
                string msg = StringObject.ExtractExceptionData(ex); //string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                // LogLibrary.LogError("StockViewForSaleByCode", msg);
                _log.LogInformation(msg);
                return BadRequest(Constant.GenericError);;
            }
        }//fin StockViewForSaleByCode

        // put api/stockview
        [HttpPut("{id}")]
        public ActionResult<ProductInStock> UpdateStockView([FromBody] ProductInStock obj, int id)
        {
            try
            {
                ProductInStock g = _itComRepository.UpdateStockView(obj, id);
                return g;
            }
            catch (Exception ex)
            {
                string msg = StringObject.ExtractExceptionData(ex); //string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                                                                    // LogLibrary.LogError("UpdateStockView", msg);
                _log.LogInformation(msg);
                return BadRequest(Constant.GenericError);;
            }
        }

        // put api/expandstockview
        [HttpPut("{id}")]
        public ActionResult<ProductInStock> ExpandStockView([FromBody] ProductInStock obj, int id)
        {
            try
            {
                ProductInStock g = _itComRepository.ExpandStockView(obj, id);
                return g;
            }
            catch (Exception ex)
            {
                string msg = StringObject.ExtractExceptionData(ex); //string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                                                                    // LogLibrary.LogError("ExpandStockView", msg);
                _log.LogInformation(msg);
                return BadRequest(Constant.GenericError);;
            }
        }

        // post api/product/stockview
        [HttpPost]
        public ActionResult<List<ProductInStock>> ProductStockView([FromBody] PrmProdStkView ppsV)
        {
            try
            {
                List<ProductInStock> gList = _itComRepository.ProductStockView(ppsV);
                return gList;
            }
            catch (Exception ex)
            {
                string msg = StringObject.ExtractExceptionData(ex); //string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                // LogLibrary.LogError("ProductStockView", msg);
                _log.LogInformation(msg);
                return BadRequest(Constant.GenericError);;
            }
        }


        [HttpPost]
        public ActionResult<List<ProductInStock>> GenerateBarcodes([FromBody] List<ProductInStock> list, Profil utilisateur)
        {
            try
            {
                List<ProductInStock> gList = _itComRepository.generateBarcodes(list, utilisateur.id);
                return gList;
            }
            catch (Exception ex)
            {
                string msg = StringObject.ExtractExceptionData(ex); //string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                // LogLibrary.LogError("GenerateBarcodes", msg);
                _log.LogInformation(msg);
                return BadRequest(Constant.GenericError);;
            }
        }//fin GenerateBarcodes
        #endregion stock view

        #region sale

        // // post api/sale
        // [HttpPost]
        // public FileObject CreateSale([FromBody]Sale obj)
        // {
        //     try
        //     {
        //         string result = _itComRepository.CreateSale(obj, _reportObject.destinationPath, _resourceObject.destinationPath);
        //         FileObject fileObj = new FileObject(result);
        //         return fileObj;
        //     }
        //     catch (InvalidOperationException ex)
        //     {
        //         string msg = StringObject.ExtractExceptionData(ex); //string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
        //         LogLibrary.LogError("CreateSale", msg);
        //         throw ex;
        //     }
        //     catch (Exception ex)
        //     {
        //         string msg = StringObject.ExtractExceptionData(ex); //string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
        //         LogLibrary.LogError("CreateSale", msg);
        //         return BadRequest(Constant.GenericError);;
        //     }
        // }//fin CreateSale

        [HttpPost]
        public async Task<IActionResult> CreateSale([FromBody] ProductSale obj)
        {
            try
            {
                var models = _itComRepository.CreateSale(obj);
                SaleDetailView SaleDetails = new SaleDetailView()
                {
                    baseUrl = $"{Request.Scheme}://{Request.Host.Value}",
                    datePrint = DateTime.Now,
                    saleDetail = models.saleData,
                    invoice = models.invoice
                };
                return await _generatePdf.GetPdf("Views/SaleDetail.cshtml", SaleDetails);
            }
            catch (Exception ex)
            {
                string msg = StringObject.ExtractExceptionData(ex); //string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                // LogLibrary.LogError("CreateSale", msg);
                _log.LogInformation(msg);
                throw new InvalidOperationException("Une erreur est survenue au cours de la normalisation !");
                // return Task;
            }
        }//fin PrintOrder

        // post api/salebalance
        [HttpPost]
        public ActionResult<ProductSale> PaySaleBalance([FromBody] ProductSale obj)
        {
            try
            {
                ProductSale result = _itComRepository.PaySaleBalance(obj);
                return result;
            }
            catch (Exception ex)
            {
                string msg = StringObject.ExtractExceptionData(ex); //string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                                                                    // LogLibrary.LogError("CreateSale", msg);
                _log.LogInformation(msg);
                return BadRequest(Constant.GenericError);;
            }
        }//fin PaySaleBalance


        // GET api/sales
        [HttpGet("{id}")]
        public ActionResult<List<ProductSale>> Sales(int id)
        {
            try
            {
                List<ProductSale> gList = _itComRepository.Sales(id);
                return gList;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                // LogLibrary.LogError("Sales", msg);
                _log.LogInformation(msg);
                return BadRequest(Constant.GenericError);;
            }
        }

        // post api/sales
        [HttpPost]
        public ActionResult<List<ProductSale>> SearchSales([FromBody] PeriodParam obj)
        {
            try
            {
                obj.startDate = obj.startDate.AddHours(1); obj.endDate = obj.endDate.AddHours(1);//because the sender is using gmt
                List<ProductSale> gList = _itComRepository.SearchSales(obj);
                return gList;
            }
            catch (Exception ex)
            {
                string msg = StringObject.ExtractExceptionData(ex); //string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                // LogLibrary.LogError("SearchSales", msg);
                _log.LogInformation(msg);
                return BadRequest(Constant.GenericError);;
            }
        }//fin SearchSales



        [HttpPost]
        public async Task<IActionResult> CreateDevis([FromBody] ProductSale obj)
        {
            try
            {
                var devis = _itComRepository.CreateDevis(obj);
                SaleDetailView SaleDetails = new SaleDetailView()
                {
                    baseUrl = $"{Request.Scheme}://{Request.Host.Value}",
                    datePrint = DateTime.Now,
                    saleDetail = devis,
                    invoice = null
                };
                return await _generatePdf.GetPdf("Views/DevisDetail.cshtml", SaleDetails);
            }
            catch (InvalidOperationException ex)
            {
                string msg = StringObject.ExtractExceptionData(ex); //string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                                                                    // LogLibrary.LogError("CreateDevis", msg);
                _log.LogInformation(msg);
                throw ex;
            }
            catch (Exception ex)
            {
                string msg = StringObject.ExtractExceptionData(ex); //string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                                                                    // LogLibrary.LogError("CreateDevis", msg);
                _log.LogInformation(msg);
                return BadRequest(Constant.GenericError);;
            }
        }//fin CreateDevis

        // GET api/devis
        [HttpGet("{id}")]
        public ActionResult<List<ProductSale>> Devis(int id)
        {
            try
            {
                List<ProductSale> gList = _itComRepository.Devis(id);
                return gList;
            }
            catch (Exception ex)
            {
                string msg = StringObject.ExtractExceptionData(ex); //string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                // LogLibrary.LogError("Devis", msg);
                _log.LogInformation(msg);
                return BadRequest(Constant.GenericError);;
            }
        }//fin devis

        // post api/devis
        [HttpPost]
        public ActionResult<List<ProductSale>> SearchDevis([FromBody] PeriodParam obj)
        {
            try
            {
                obj.startDate = obj.startDate.AddHours(1); obj.endDate = obj.endDate.AddHours(1);//because the sender is using gmt
                List<ProductSale> gList = _itComRepository.SearchDevis(obj);
                return gList;
            }
            catch (Exception ex)
            {
                string msg = StringObject.ExtractExceptionData(ex); //string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                                                                    // LogLibrary.LogError("SearchDevis", msg);
                _log.LogInformation(msg);
                return BadRequest(Constant.GenericError);;
            }
        }//fin SearchDevis


        #endregion sale

        #region services sales
        [HttpPost]
        public async Task<IActionResult> CreateServiceSale([FromBody] ServiceSale obj)
        {
            try
            {
                byte[] pdf = await _itComRepository.CreateServiceSale(obj, _templateService, _converter);
                return new FileContentResult(pdf, "application/pdf");
            }
            catch (Exception ex)
            {
                string msg = StringObject.ExtractExceptionData(ex); //string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                // LogLibrary.LogError("CreateSale", msg);
                _log.LogInformation(msg);
                throw ex;
                // return Task;
            }
        }//fin PrintOrder


        // GET api/servicesales
        [HttpGet("{id}")]
        public ActionResult<List<ServiceSale>> ServiceSale(int id)
        {
            try
            {
                List<ServiceSale> gList = _itComRepository.ServiceSales(id);
                return gList;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                // LogLibrary.LogError("Sales", msg);
                _log.LogInformation(msg);
                return BadRequest(Constant.GenericError);;
            }
        }

        // post api/servicesales
        [HttpPost]
        public ActionResult<List<ServiceSale>> SearchServiceSale([FromBody] PeriodParam obj)
        {
            try
            {
                obj.startDate = obj.startDate.AddHours(1); obj.endDate = obj.endDate.AddHours(1);//because the sender is using gmt
                List<ServiceSale> gList = _itComRepository.SearchServiceSales(obj);
                return gList;
            }
            catch (Exception ex)
            {
                string msg = StringObject.ExtractExceptionData(ex); //string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                // LogLibrary.LogError("SearchSales", msg);
                _log.LogInformation(msg);
                return BadRequest(Constant.GenericError);;
            }
        }//fin SearchSales

        #endregion services sales

        #region stock limit

        // GET api/stocklimit
        [HttpGet("{id}")]
        public ActionResult<List<ProdStockLimit>> StockLimit(int id)
        {
            try
            {
                List<ProdStockLimit> gList = _itComRepository.StockLimit(id);
                return gList;
            }
            catch (Exception ex)
            {
                string msg = StringObject.ExtractExceptionData(ex); //string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                                                                    // LogLibrary.LogError("StockLimit", msg);
                _log.LogInformation(msg);
                return BadRequest(Constant.GenericError);;
            }
        }

        // GET api/understock
        [HttpGet("{id}")]
        public ActionResult<List<ProductInStock>> UnderStockProducts(int id)
        {
            try
            {
                List<ProductInStock> gList = _itComRepository.GetProdOnAlertList(id);
                return gList;
            }
            catch (Exception ex)
            {
                string msg = StringObject.ExtractExceptionData(ex); //string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                                                                    // LogLibrary.LogError("UnderStockProducts", msg);
                _log.LogInformation(msg);
                return BadRequest(Constant.GenericError);;
            }
        }//fin UnderStockProducts


        // post api/stocklimit
        [HttpPost]
        public ActionResult<ProdStockLimit> CreateStockLimit([FromBody] ProdStockLimit obj)
        {
            try
            {
                ProdStockLimit result = _itComRepository.CreateStockLimit(obj);
                return result;
            }
            catch (Exception ex)
            {
                string msg = StringObject.ExtractExceptionData(ex); //string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                // LogLibrary.LogError("CreateStockLimit", msg);
                _log.LogInformation(msg);
                return BadRequest(Constant.GenericError);;
            }
        }
        // put api/stocklimit
        [HttpPut]
        public ActionResult<ProdStockLimit> UpdateStockLimit([FromBody] ProdStockLimit obj)
        {
            try
            {
                ProdStockLimit result = _itComRepository.UpdateStockLimit(obj);
                return result;
            }
            catch (Exception ex)
            {
                string msg = StringObject.ExtractExceptionData(ex); //string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                // LogLibrary.LogError("UpdateStockLimit", msg);
                _log.LogInformation(msg);
                return BadRequest(Constant.GenericError);;
            }
        }
        // delete api/stocklimit
        [HttpDelete("{id}")]
        public ActionResult<ProdStockLimit> DeleteStockLimit(int id)
        {
            try
            {
                ProdStockLimit result = _itComRepository.DeleteStockLimit(id);
                return result;
            }
            catch (Exception ex)
            {
                string msg = StringObject.ExtractExceptionData(ex); //string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                // LogLibrary.LogError("DeleteStockLimit", msg);
                _log.LogInformation(msg);
                return BadRequest(Constant.GenericError);;
            }
        }
        /*end DeleteStockLimit*/

        // post api/stockview
        [HttpPost("{id}")]
        public ActionResult<List<ProductInStock>> StockExpired([FromBody] PeriodParam obj, int id)
        {
            try
            {
                List<ProductInStock> g = _itComRepository.StockExpired(obj, id);
                return g;
            }
            catch (Exception ex)
            {
                string msg = StringObject.ExtractExceptionData(ex); //string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                                                                    // LogLibrary.LogError("StockExpired", msg);
                _log.LogInformation(msg);
                return BadRequest(Constant.GenericError);;
            }
        }


        #endregion stock limit

        #region transfer

        [HttpPost]
        public ActionResult<StockTransfer> CreateTransfer([FromBody] StockTransfer obj)
        {
            try
            {
                StockTransfer result = _itComRepository.CreateTransfer(obj);
                return result;
            }
            catch (Exception ex)
            {
                string msg = StringObject.ExtractExceptionData(ex); //string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                // LogLibrary.LogError("CreateTransfer", msg);
                _log.LogInformation(msg);
                return BadRequest(Constant.GenericError);;
            }
        }//fin CreateTransfer

        // GET api/StockTransfers
        [HttpGet("{id}")]
        public ActionResult<List<StockTransfer>> StockTransfers(int id)
        {
            try
            {
                List<StockTransfer> gList = _itComRepository.StockTransfers(id);
                return gList;
            }
            catch (Exception ex)
            {
                string msg = StringObject.ExtractExceptionData(ex); //string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                // LogLibrary.LogError("StockTransfers", msg);
                _log.LogInformation(msg);
                return BadRequest(Constant.GenericError);;
            }
        }

        // post api/StockTransfers
        [HttpPost]
        public ActionResult<List<StockTransfer>> SearchStockTransfers([FromBody] PeriodParam obj)
        {
            try
            {
                obj.startDate = obj.startDate.AddHours(1); obj.endDate = obj.endDate.AddHours(1);//because the sender is using gmt
                List<StockTransfer> gList = _itComRepository.SearchStockTransfers(obj);
                return gList;
            }
            catch (Exception ex)
            {
                string msg = StringObject.ExtractExceptionData(ex); //string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                // LogLibrary.LogError("SearchStockTransfers", msg);
                _log.LogInformation(msg);
                return BadRequest(Constant.GenericError);;
            }
        }//fin SearchStockTransfers


        #endregion

    }
}
