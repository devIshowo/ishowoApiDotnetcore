using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ItCommerce.Business.Entities;
using ItCommerce.Api.Net.Logger;
using ItCommerce.Api.Net.Extra;
using ItCommerce.Api.Net.Models;
using ItCommerce.Business.Extra;
using ItCommerce.DTO.ModelDesign;
using ItCommerce.DTO.Factory;
using API.Business.Extra;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ItCommerce.Api.Net.Controllers
{
    [UserLicenseCheckerFilter]
    [CustomExceptionFilter]
    [Route("api/[controller]/[action]")]
    public class FinancesController : Controller
    {

        private IItCommerceRepository _itComRepository;

        public FinancesController(IItCommerceRepository _repository, IT_COMMERCEEntities context)
        {
            _itComRepository = _repository;
            ItCommerceFactory.SetEntity(context); 
        }

        #region virtualaccounts
        // GET api/virtualaccounts
        [HttpGet("{id}")]
        public ActionResult<List<VirtualAccount>> VAccounts(int id)
        {
            try
            {
                List<VirtualAccount> gList = _itComRepository.VirtualAccounts(id);
                return gList;
            }
            catch (Exception ex)
            {
                string msg = StringObject.ExtractExceptionData(ex);
                LogLibrary.LogError("VAccounts", msg);
                return BadRequest(Constant.GenericError);;
            }
        }

        // post api/virtualaccount
        [HttpPost]
        public ActionResult<VirtualAccount> CreateVAccount([FromBody]VirtualAccount obj)
        {
            try
            {
                VirtualAccount result = _itComRepository.CreateVirtualAccount(obj);
                return result;
            }
            catch (Exception ex)
            {
                string msg = StringObject.ExtractExceptionData(ex);
                LogLibrary.LogError("CreateVAccount", msg);
                return BadRequest(Constant.GenericError);;
            }
        }

        // put api/virtualaccount
        [HttpPut]
        public ActionResult<VirtualAccount> UpdateVAccount([FromBody]VirtualAccount obj)
        {
            try
            {
                VirtualAccount result = _itComRepository.UpdateVirtualAccount(obj);
                return result;
            }
            catch (Exception ex)
            {
                string msg = StringObject.ExtractExceptionData(ex);
                LogLibrary.LogError("UpdateVAccount", msg);
                return BadRequest(Constant.GenericError);;
            }
        }

        // delete api/virtualaccount
        [HttpDelete("{id}")]
        public ActionResult<VirtualAccount> DeleteVAccount(int id)
        {
            try
            {
                VirtualAccount result = _itComRepository.DeleteVirtualAccount(id);
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                LogLibrary.LogError("DeleteVAccount", msg);
                return BadRequest(Constant.GenericError);;
            }
        }

        #endregion virtual accounts


        // GET api/virtualaccounts
        [HttpGet("{id}")]
        public ActionResult<List<FinOperation>> FinOperations(int id)
        {
            try
            {
                List<FinOperation> gList = _itComRepository.FinOperations(id);
                return gList;
            }
            catch (Exception ex)
            {
                string msg = StringObject.ExtractExceptionData(ex); 
                LogLibrary.LogError("FinOperations", msg);
                return BadRequest(Constant.GenericError);;
            }
        }//fin FinOperations

        [HttpPost]
        public ActionResult<List<FinOperation>> SearchFinOperations([FromBody]PeriodParam obj)
        {
            try
            {
                obj.startDate = obj.startDate.AddHours(1); obj.endDate = obj.endDate.AddHours(1);//because the sender is using gmt
                List<FinOperation> gList = _itComRepository.SearchFinOperations(obj);
                return gList;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("SearchFinOperations", msg);
                return BadRequest(Constant.GenericError);;
            }
        }//fin SearchFinOperations

        // post api/finoperations
        [HttpPost]
        public ActionResult<FinOperation> CreateFinOperation([FromBody]FinOperation obj)
        {
            try
            {
                FinOperation result = _itComRepository.CreateFinOperation(obj);
                return result;
            }
            catch (Exception ex)
            {
                string msg = StringObject.ExtractExceptionData(ex);
                LogLibrary.LogError("CreateFinOperation", msg);
                return BadRequest(Constant.GenericError);;
            }
        }//fin CreateFinOperation


    }
}
