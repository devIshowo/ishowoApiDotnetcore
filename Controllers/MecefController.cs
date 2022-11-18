using System;
using ItCommerce.Api.Net.Models;
using ItCommerce.Api.Net.Extra;
using ItCommerce.DTO.ModelDesign;
using ItCommerce.DTO.Factory;
using ItCommerce.Business.Entities;
using Microsoft.AspNetCore.Mvc;
using ItCommerce.Api.Net.Logger;
using ItCommerce.DTO.SpecClasses;
using System.Collections.Generic;
using ItCommerce.Reporting.ViewModels;
using Wkhtmltopdf.NetCore;
using Serilog;
using System.Threading.Tasks;
using API.Business.Extra;

namespace ItCommerce.Api.Net.Controllers
{
    [UserLicenseCheckerFilter]
    [CustomExceptionFilter]
    [Route("api/[controller]/[action]")]
    //[Route("[controller]")]
    public class MecefController : Controller
    {

        private IItCommerceRepository _itComRepository;
        private ResourceObject _resourceObject;
        //private IHostingEnvironment _hostingEnvironment;
        private ReportObject _reportObject;
        readonly IGeneratePdf _generatePdf;
        private ILogger _logger;

        public MecefController(IItCommerceRepository _repository, ReportObject _repObject, ResourceObject _resObject, IT_COMMERCEEntities context,IGeneratePdf generatePdf)
        {
            _itComRepository = _repository;
            _reportObject = _repObject;
            this._resourceObject = _resObject;
            ItCommerceFactory.SetEntity(context);
            _generatePdf = generatePdf;
        }
        #region param-mecef
        // GET api/param-mecef
        [HttpGet("{id}")]
        public ActionResult<List<ParamMecef>> ParamsMecef(int id)
        {
            try
            {
                List<ParamMecef> gList = _itComRepository.ParamsMecef(id);
                return gList;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("Params Mecef", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }

        // post api/category
        [HttpPut]
        public ActionResult<UpdateMecef> UpdateParamMecef([FromBody] UpdateMecef obj)
        {
            try
            {
                UpdateMecef result = _itComRepository.UpdateParamMecef(obj);
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("Params Mecef Mis à jour", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }
        #endregion param-mecef

        #region normalisation
        // post api/NormaliseInvoice
        [HttpPost]
        public ActionResult<Invoice> NormaliseInvoice([FromBody] int id)
        {
            try
            {
                Invoice result = _itComRepository.NormaliseInvoice(id);
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }//fin NormaliseInvoice

        #endregion normalisation


        #region facture d'avoir
        // post api/CancelInvoice
        [HttpPost]
        public async Task<IActionResult> CancelInvoice([FromBody] ProductSale obj)
        {
            try
            {
                var models = _itComRepository.CancelInvoice(obj);
                if (models.saleData.customer == null)
                {
                    models.saleData.customer = new Customer();
                }
                AvoirDetailView AvoirDetails = new AvoirDetailView()
                {
                    baseUrl = $"{Request.Scheme}://{Request.Host.Value}",
                    datePrint = DateTime.Now,
                    saleDetail = models.saleData,
                    invoice = models.invoice,
                    Ref_ORIGIN = models.Ref_Origin,
                    Mecef_ORIGIN = models.Mecef_Origin
                };
                return await _generatePdf.GetPdf("Views/AvoirDetail.cshtml", AvoirDetails);
            }
            catch (Exception ex)
            {
                string msg = StringObject.ExtractExceptionData(ex);// string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                LogLibrary.LogError("PrintAvoir", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }//fin CancelInvoice

        #endregion facture d'avoir

    }
}
