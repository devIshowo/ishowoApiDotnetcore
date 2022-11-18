using ItCommerce.Api.Net.Extra;
using ItCommerce.Api.Net.Logger;
using ItCommerce.Api.Net.Models;
using ItCommerce.Business.Entities;
using ItCommerce.Business.Extra;
using ItCommerce.DTO.Factory;
using Microsoft.AspNetCore.Mvc;
using System;
using ItCommerce.DTO.ModelDesign;
using DinkToPdf.Contracts;
using ItCommerce.Reporting.Reports;
using API.Business.Libs;
using System.Management;
using System.Text.RegularExpressions;
using API.Business.Extra;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ItCommerce.Api.Net.Controllers
{

    [CustomExceptionFilter]
    [Route("api/[controller]")]
    public class LicenceController : Controller
    {
        private IItCommerceRepository _itComRepository;
        private ReportObject _reportObject;
        private ResourceObject _resourceObject;
        private IConverter _converter;
        private ITemplateService _templateService;


        public LicenceController(IConverter converter, IItCommerceRepository _repository, ReportObject _repObject, ResourceObject _resObject,
         IT_COMMERCEEntities context, ITemplateService _templtService)
        {
            _itComRepository = _repository;
            _reportObject = _repObject;
            this._resourceObject = _resObject;
            ItCommerceFactory.SetEntity(context);
             _converter = converter;
             _templateService = _templtService;
        }

            /// <summary>
        /// get machine fingerprint
        /// </summary>
        /// <returns></returns>
        [Route("fprint")]
        [HttpGet]
        public  ActionResult<string> FPrint()
        {
            string cpuInfo = string.Empty;
            ManagementClass mc = new ManagementClass("win32_processor");
            ManagementObjectCollection moc = mc.GetInstances();

            foreach (ManagementObject mo in moc)
            {
                if (cpuInfo == "")
                {
                    //Get only the first CPU's ID
                    cpuInfo = mo.Properties["processorID"].Value.ToString();
                    break;
                }
            }
            var format = Regex.Replace(cpuInfo, ".{4}", "$0-");
            return format;
        }

        // post api/Licencenew
        [Route("new")]
        [HttpPost]
        //public Licence LicenceNew(Licence obj)
        //public Licence LicenceNew([FromBody]PrmLicence obj) 
        public ActionResult<Licence> LicenceNew([FromBody] PrmLicence obj)
         //public async Task<IActionResult> LicenceNew([FromBody] PrmLicence obj)
        {
            try
            {
                Licence builtLicence = AppUtils.createLicenceFromQuery(obj);
                Licence result = _itComRepository.SaveLicence(builtLicence);
                if (result.id == 0) return Ok(new { status = "error", message = "Cette demande de licence n'a pas abouti", data = "" });

                return Ok(new { data = result }); 
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                LogLibrary.LogError("LicenceNew", msg);
                return Ok(new { status = "error", message = "Une erreur est survenue lors de la création d'une licence", data = "" });
            }
        }//fin LicenceNew
        // post api/LicenceChange
        [HttpPost]
        public ActionResult<Licence> LicenceChange([FromBody]PrmLicence obj)

        {
            try
            {
                Licence builtLicence = AppUtils.createLicenceFromQuery(obj);

                Licence result = _itComRepository.UpdateLicence(builtLicence);
                if (result.id == 0) return BadRequest(Constant.GenericError);;
                return Ok(result);
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("LicenceChange", msg);
                return BadRequest(Constant.GenericError); 
            }
        }//fin LicenceChange

        // post api/ActiveProfile
        [HttpPost]
        public ActionResult<Profil> ActiveProfile([FromBody]string code)
        {
            try
            {
                Profil result = _itComRepository.GetActiveProfileByCode(code);
                if (result.id == 0) return BadRequest(Constant.GenericError);
                return Ok( result);
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("ActiveProfile", msg);
                return BadRequest(Constant.GenericError);
            }
        }//fin ActiveProfile

        // GET api/currentlic
        [HttpGet]
        public ActionResult<StringObject> CurrentLicCode()
        {
            try
            {
                StringObject result = new StringObject();
                result.name = _itComRepository.GetCurrentLicenceCode();
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("CurrentLicenceCode", msg);
                return BadRequest(Constant.GenericError);
            }
        }//fin CurrentLic

        [HttpGet]
        public StringObject ApiUrl()
        {
            StringObject value = new StringObject();
            value.name = ConfigObject.ApiUrl;
            return value;
        }//fin GetApiUrl



        ///get list modules

        [Route("modules")]
        [HttpGet]
  
        public async Task<IActionResult> GetModules()
       {
                List<ModuleRequest> result = new List<ModuleRequest>(); 
                result = _itComRepository.GetModuleList();
                return Ok(new
                {
                    data = result,
                    message = "",
                    status = "success"
                });
        }//end GetModules

        //get list formules
        [Route("formules")]
        [HttpGet]
       public async Task<IActionResult> GetFormules()
        { 
        
            List<FormuleRequest> result  = new List<FormuleRequest>();
                result = _itComRepository.GetFormuleList();
            return Ok(new
            {
                data = result,
                message= "",
                status= "success"
            });
            
        }//end GetFormules


    }
}
