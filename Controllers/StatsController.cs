using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItCommerce.Api.Net.Extra;
using ItCommerce.Api.Net.Logger;
using ItCommerce.Api.Net.Models;
using ItCommerce.Business.Entities;
using ItCommerce.Business.Extra;
using Microsoft.AspNetCore.Mvc;
using ItCommerce.DTO.ModelDesign;
using ItCommerce.DTO.Factory;
using API.Business.Extra;

namespace ItCommerce.Api.Net.Controllers
{
    [UserLicenseCheckerFilter]
    [CustomExceptionFilter]
    [Route("api/[controller]/[action]")]
    public class StatsController : ControllerBase
    {
        private IItCommerceRepository _itComRepository;
        private ReportObject _reportObject;

        public StatsController(IItCommerceRepository _repository, ReportObject _repObject, IT_COMMERCEEntities context)
        {
            _itComRepository = _repository;
            _reportObject = _repObject;
            ItCommerceFactory.SetEntity(context); 
        }

        // post api/GetCurrentReport
        [HttpPost]
        public ActionResult<GlobalReport> CurrentReport([FromBody]Profil obj)
        {
            try
            {
                GlobalReport result = _itComRepository.GetCurrentReport(obj);
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                LogLibrary.LogError("GetCurrentReport", msg);
                return BadRequest(Constant.GenericError);;
            }
        }//fin GetCurrentReport

        // post api/PeriodGlobalReport
        [HttpPost]
        public ActionResult<GlobalReport> PeriodGlobalReport([FromBody]PeriodParam obj)
        {
            try
            {
                GlobalReport result = _itComRepository.GetPeriodGlobalReport(obj);
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                LogLibrary.LogError("PeriodGlobalReport", msg);
                return BadRequest(Constant.GenericError);;
            }
        }//fin PeriodGlobalReport

        // post api/PeriodGlobalReport
        [HttpPost]
        public ActionResult<GlobalReport> ProductPeriodGlobalReport([FromBody]PeriodParam obj)
        {
            try
            {
                GlobalReport result = _itComRepository.GetProductPeriodGlobalReport(obj);
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                LogLibrary.LogError("ProductPeriodGlobalReport", msg);
                return BadRequest(Constant.GenericError);;
            }
        }//fin ProductPeriodGlobalReport

        // post api/GetProductMostSoldByQtyReport
        [HttpPost]
        public ActionResult<List<ProductInStock>> MostSoldProdByQty([FromBody]PeriodParam obj)
        {
            try
            {
                List<ProductInStock> result = _itComRepository.GetProductMostSoldByQtyReport(obj);
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                LogLibrary.LogError("MostSoldProdByQty", msg);
                return BadRequest(Constant.GenericError);;
            }
        }//fin MostSoldProdByQty

        // post api/MostSoldProdByCa
        [HttpPost]
        public ActionResult<List<ProductInStock>> MostSoldProdByCa([FromBody]PeriodParam obj)
        {
            try
            {
                List<ProductInStock> result = _itComRepository.GetProductMostSoldByCaReport(obj);
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                LogLibrary.LogError("MostSoldProdByCa", msg);
                return BadRequest(Constant.GenericError);;
            }
        }//fin MostSoldProdByCa

        // post api/MostSoldProdByProfit
        [HttpPost]
        public ActionResult<List<ProductInStock>> MostSoldProdByProfit([FromBody]PeriodParam obj)
        {
            try
            {
                List<ProductInStock> result = _itComRepository.GetProductMostSoldByProfitReport(obj);
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                LogLibrary.LogError("MostSoldProdByProfit", msg);
                return BadRequest(Constant.GenericError);;
            }
        }//fin MostSoldProdByProfit

        // post api/SalesReportBySellers
        [HttpPost]
        public ActionResult<List<UserReport>> SalesReportBySellers([FromBody]PeriodParam obj)
        {
            try
            {
                List<UserReport> result = _itComRepository.GetSalesReportBySellers(obj);
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                LogLibrary.LogError("SalesReportBySellers", msg);
                return BadRequest(Constant.GenericError);;
            }
        }//fin SalesReportBySellers

        // post api/SalesReportByCustomers
        [HttpPost]
        public ActionResult<List<UserReport>> SalesReportByCustomers([FromBody]PeriodParam obj)
        {
            try
            {
                List<UserReport> result = _itComRepository.GetSalesReportByCustomers(obj);
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                LogLibrary.LogError("SalesReportByCustomers", msg);
                return BadRequest(Constant.GenericError);;
            }
        }//fin SalesReportByCustomers

    }
}
