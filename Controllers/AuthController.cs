using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ItCommerce.Api.Net.Models;
using ItCommerce.Business.Entities;
using ItCommerce.Api.Net.Logger;
using ItCommerce.Api.Net.Extra;
using ItCommerce.Business.Extra;
using ItCommerce.DTO.ModelDesign;
using ItCommerce.DTO.Factory;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Sockets;
using System.Net.Http;
using Org.BouncyCastle.Asn1.Ocsp;
using Microsoft.AspNetCore.Http;
using XAct.Messages;
using API.Business.Extra;
using Utils.IwajuTech.Business.Factories;
using ZXing;

namespace ItCommerce.Api.Net.Controllers
{
    [UserLicenseCheckerFilter]
    [CustomExceptionFilter]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        public static Profil user = new Profil();
        private IItCommerceRepository _itComRepository;
        private readonly IConfiguration _configuration;
        private readonly ItCommerceRepository _userService;
        //private ResourceObject _resourceObject;

        public AuthController(IItCommerceRepository _repository, IT_COMMERCEEntities context)
        {
            _itComRepository = _repository;
            //_configuration = configuration;
            //_userService = userService;
            ItCommerceFactory.SetEntity(context);

        }

        [HttpGet, Authorize]
        public ActionResult<string> GetMe()
        {
            var login = _userService.GetMyName();
            return Ok(login);
        }

        #region users
        // GET api/users
        [HttpGet("{id}")]
        public ActionResult<List<User>> Agents(int id)
        {
            try {
                List<User> gList = _itComRepository.Agents(id);
                return gList;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("Agents", msg);
                return BadRequest(Constant.GenericError);
            }
        }

        // GET api/users
        [HttpGet("{id}")]
        public ActionResult<List<User>> Customers(int id)
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
                return BadRequest(Constant.GenericError);
            }
        }

        // post api/users
        [HttpPost]
        public ActionResult<User> CreateAgent([FromBody] User obj)
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
                return BadRequest(Constant.GenericError); 
            }
        }
        // post api/users
        [HttpPut]
        public ActionResult<User> UpdateAgent([FromBody] User obj)
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
                return BadRequest(Constant.GenericError);
            }
        }
        // post api/users
        [HttpDelete("{id}")]
        public ActionResult<User> DeleteAgent(int id)
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
                return BadRequest(Constant.GenericError);
            }
        }
        /*end users*/
        #endregion users


        #region profiles
        // GET api/profiles
        [HttpGet("{id}")]
        public ActionResult<List<Profil>> Profiles(int id)
        {
            try {
                List<Profil> gList = _itComRepository.Profiles(id);
                return gList;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("Profiles", msg);
                return BadRequest(Constant.GenericError); ;
            }
        }
        // post api/profiles
        [HttpPost]
        public ActionResult<Profil> CreateProfile([FromBody] Profil obj)
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
                 return BadRequest(Constant.GenericError); ;
            }
        }
        // put api/profiles
        [HttpPut]
        public ActionResult<Profil> UpdateProfile([FromBody] Profil obj)
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
                return BadRequest(Constant.GenericError);
            }
        }

        // put api/profiles
        [HttpPut]
        public ActionResult<Profil> UpdateUserProfile([FromBody] Profil obj)
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
                return BadRequest(Constant.GenericError);
            }
        }

        // put api/profiles
        [HttpPut]
        public ActionResult<Company> UpdateCompanyProfile([FromBody] Company obj)
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
                return BadRequest(Constant.GenericError);
            }
        }//fin UpdateCompanyProfile

        // delete api/profiles
        [HttpDelete("{id}")]
        public ActionResult<Profil> DeleteProfile(int id)
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
                return BadRequest(Constant.GenericError);
            }
        }
        /*end profiles*/


        // post api/login
        [Route("signin")]
        [HttpPost]
        public IActionResult Signin([FromBody] Profil obj)
        {
            try
            {
                //ConfigObject.CheckLicence();
                //if (ConfigObject.IsLicenseOk == false) return BadRequest(Constant.GenericError);;
                Profil result = _itComRepository.Signin(obj);
                var token = SecurityFactory.CreateToken(result.login);

                if (result.id == 0) return BadRequest(Constant.GenericError); ;
                return Ok(new
                {
                    data = result,
                    token = token
                });
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("Signin", msg);
                throw ex;
                return BadRequest(Constant.GenericError);
            }
        }


        // post api/login
        [Route("verification/{key}")]
        [HttpGet]
        public IActionResult verification(string key)
        {
            try
            {
                var result = _itComRepository.Verified(key);
                if(result == null) return Ok(new{ message = "Votre compte est déja actif "});
                return Ok(new
                {
                    data = result.nom + result.prenoms
                }) ;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                LogLibrary.LogError("CheckLicence", msg);
                 return BadRequest(Constant.GenericError);
            }
        }
    

        // post api/check: used to check licence
        [HttpGet]
        public IActionResult Tic()
        {
            try
            {
                ConfigObject.CheckLicence(); return BadRequest(Constant.GenericError);
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                LogLibrary.LogError("CheckLicence", msg);
                return BadRequest(Constant.GenericError);
            }
        }//fin CheckLicence



        #endregion profiles

        private string CreateToken(Profil profil)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, profil.login)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                    _configuration.GetSection("AppSettings:Token").Value
               ));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(60),
                    signingCredentials: creds
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

  

    }
}
