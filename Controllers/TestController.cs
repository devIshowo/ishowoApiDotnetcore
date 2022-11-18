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
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using SixLabors.ImageSharp;
using Microsoft.Extensions.Configuration;
using System.Net.Sockets;
using API.Models;

namespace ItCommerce.Api.Net.Controllers
{

    [CustomExceptionFilter]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        public static Profil user = new Profil();
        private IItCommerceRepository _itComRepository;

        //private ResourceObject _resourceObject;

        public TestController(IItCommerceRepository _repository, IT_COMMERCEEntities context)
        {
            _itComRepository = _repository;
            ItCommerceFactory.SetEntity(context);

        }

        #region users
        // GET api/users
        [HttpGet("{id}")]
        public List<User> Agents(int id)
        {
            try
            {
                List<User> gList = _itComRepository.Agents(id);
                return gList;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("Agents", msg);
                return null;
            }
        }

        // GET api/users
        [HttpGet("{id}")]
        public List<User> Customers(int id)
        {
            try
            {
                List<User> gList = _itComRepository.Customers(id);
                return gList;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("Customers", msg);
                return null;
            }
        }

        // post api/users
        [HttpPost]
        public User CreateAgent([FromBody] User obj)
        {
            try
            {
                User result = _itComRepository.CreateAgent(obj);
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("CreateAgent", msg);
                return null;
            }
        }
        // post api/users
        [HttpPut]
        public User UpdateAgent([FromBody] User obj)
        {
            try
            {
                User result = _itComRepository.UpdateAgent(obj);
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("UpdateAgent", msg);
                return null;
            }
        }
        // post api/users
        [HttpDelete("{id}")]
        public User DeleteAgent(int id)
        {
            try
            {
                User result = _itComRepository.DeleteAgent(id);
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("DeleteAgent", msg);
                return null;
            }
        }
        /*end users*/
        #endregion users


        #region profiles
        // GET api/profiles
        [HttpGet("{id}")]
        public List<Profil> Profiles(int id)
        {
            try
            {
                List<Profil> gList = _itComRepository.Profiles(id);
                return gList;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("Profiles", msg);
                return null;
            }
        }
        // post api/profiles
        [HttpPost]
        public Profil CreateProfile([FromBody] Profil obj)
        {
            try
            {
                Profil result = _itComRepository.CreateProfile(obj);
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("CreateProfile", msg);
                return null;
            }
        }
        // put api/profiles
        [HttpPut]
        public Profil UpdateProfile([FromBody] Profil obj)
        {
            try
            {
                Profil result = _itComRepository.UpdateProfile(obj);
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("UpdateProfile", msg);
                return null;
            }
        }

        // put api/profiles
        [HttpPut]
        public Profil UpdateUserProfile([FromBody] Profil obj)
        {
            try
            {
                Profil result = _itComRepository.UpdateUserProfile(obj);
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("UpdateUserProfile", msg);
                return null;
            }
        }

        // put api/profiles
        [HttpPut]
        public Company UpdateCompanyProfile([FromBody] Company obj)
        {
            try
            {
                Company result = _itComRepository.UpdateCompanyProfile(obj);
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("UpdateCompanyProfile", msg);
                return null;
            }
        }//fin UpdateCompanyProfile

        // delete api/profiles
        [HttpDelete("{id}")]
        public Profil DeleteProfile(int id)
        {
            try
            {
                Profil result = _itComRepository.DeleteProfile(id);
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("DeleteProfile", msg);
                return null;
            }
        }
        /*end profiles*/


        // post api/login
        [Route("signin")]
        [HttpPost]
        public Profil Signin([FromBody] Profil obj)
        {
            try
            {
                //ConfigObject.CheckLicence();
                //if (ConfigObject.IsLicenseOk == false) return null;
                Profil result = _itComRepository.Signin(obj);
                if (result.id == 0) return null;
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("Signin", msg);
                throw ex;
                //return ex.Message;
            }
        }


        // post api/check: used to check licence
        [HttpGet]
        public string Tic()
        {
            try
            {
                ConfigObject.CheckLicence(); return "";
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                LogLibrary.LogError("CheckLicence", msg);
                return null;
            }
        }//fin CheckLicence



        #endregion profiles


        [Route("test")]
        [HttpPost]
        //public async Task<ActionResult<Class>> CreateLicence([FromBody] Class obj)
        public  IActionResult CreateLicence([FromBody] Class obj)
        {
            try
            {
                Class customer = new Class();
                customer.company = obj.company;


                return Ok();
            }
            catch (Exception)
            {
                return Ok("erreur");
            }
            return Ok("");
            //string str = "eded";
            //return Ok(str);

        }


    }

}
