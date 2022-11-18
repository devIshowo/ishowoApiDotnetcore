using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ItCommerce.Api.Net.Models;
using ItCommerce.Business.Entities;
using ItCommerce.Api.Net.Logger;
using ItCommerce.Reporting;
using ItCommerce.DTO.SpecClasses;
using ItCommerce.Api.Net.Extra;
using ItCommerce.Business.Actions;
using ItCommerce.Business.Extra;
using System.IO;
using ItCommerce.DTO.ModelDesign;
using ItCommerce.DTO.Factory;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using API.Business.Extra;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ItCommerce.Api.Net.Controllers
{

    [UserLicenseCheckerFilter]
    [CustomExceptionFilter]
    [Route("api/[controller]/[action]")]
    //[Route("[controller]")]
    public class ServicesController : Controller
    {

        private IItCommerceRepository _itComRepository;
        // private IHostingEnvironment _hostingEnvironment;

        private ResourceObject _resourceObject;
        private ILogger _logger;

        public ServicesController(IItCommerceRepository _repository, ResourceObject _resObject, IT_COMMERCEEntities context, ILoggerFactory loggerFactory)
        {
            _itComRepository = _repository;
            _resourceObject = _resObject;
            _logger = loggerFactory.CreateLogger("Params");
            ItCommerceFactory.SetEntity(context);
        }

        #region prestations
        // GET api/prestations
        [HttpGet]
        public ActionResult<List<Prestation>> Prestations()
        {
            try
            {
                List<Prestation> gList = _itComRepository.Prestations();
                return gList;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("Prestations", msg);
                               ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }

        // GET api/prestations/{id}
        [HttpGet("{id}")]
        public ActionResult<List<Prestation>> Prestation(int id)
        {
            try
            {
                List<Prestation> gList = _itComRepository.Prestation(id);
                return gList;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("Prestations", msg);
                               ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }

   
        // post api/prestations
        [HttpPost]
        public ActionResult<Prestation> CreatePrestation([FromBody] Prestation obj)  //
        {
            try
            {
                Prestation result = _itComRepository.CreatePrestation(obj);
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("Prestations", msg);
                               ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }

        // put api/prestations
        [HttpPut]
        public ActionResult<Prestation> UpdatePrestation([FromBody] Prestation obj)
        {
            try
            {
                Prestation result = _itComRepository.UpdatePrestation(obj);
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("Prestations", msg);
                               ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }
        // delete api/prestations
        [HttpDelete("{id}")]
        public ActionResult<Prestation> DeletePrestation(int id)
        {
            try
            {
                Prestation result = _itComRepository.DeletePrestation(id);
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("Prestations", msg);
                               ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }

     

        /*end prestations*/

        #endregion prestations

        [HttpGet]
        public ActionResult<List<string>> ErrorLogs()
        {
            try
            {


                List<string> gList = _itComRepository.ErrorLogs();
                return gList;

            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("Groups", msg);
                               ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }



    }
}
