using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ItCommerce.Api.Net.Models;
using ItCommerce.Business.Entities;
using ItCommerce.Api.Net.Logger;
using ItCommerce.Reporting;
using ItCommerce.Api.Net.Extra;
using ItCommerce.Business.Actions;
using ItCommerce.Business.Extra;
using System.IO;
using ItCommerce.DTO.ModelDesign;
using ItCommerce.DTO.Factory;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ItCommerce.DTO.SpecClasses;
using WebApplication5;
using Serilog;
using API.Business.Extra;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ItCommerce.Api.Net.Controllers
{

    [UserLicenseCheckerFilter]
    [CustomExceptionFilter]
    [Route("api/[controller]/[action]")]
    //[Route("[controller]")]
    public class ParamsController : Controller
    {

        private IItCommerceRepository _itComRepository;
        // private IHostingEnvironment _hostingEnvironment;

        private ResourceObject _resourceObject;
        //private ILogger _logger;

        public ParamsController(IItCommerceRepository _repository, ResourceObject _resObject, IT_COMMERCEEntities context, ILoggerFactory loggerFactory)
        {
            _itComRepository = _repository;
            _resourceObject = _resObject;
            //_logger = loggerFactory.CreateLogger("Params");
            ItCommerceFactory.SetEntity(context);
        }

        #region customers
        // GET api/customers
        [HttpGet("{id}")]
        public ActionResult<List<Customer>> Customers(int id)
        {
            try
            {
                List<Customer> gList = _itComRepository.Customers();
                return gList;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("Customers", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }
        // post api/customers
        [HttpPost]
        public ActionResult<Customer> CreateCustomer([FromBody] Customer obj)
        {
            try
            {
                Customer result = _itComRepository.CreateCustomer(obj);
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("Customers", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }
        // update api/customers
        [HttpPut]
        public ActionResult<Customer> UpdateCustomer([FromBody] Customer obj)
        {
            try
            {
                Customer result = _itComRepository.UpdateCustomer(obj);
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("Customers", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }

        // delete api/customers
        [HttpDelete("{id}")]
        public ActionResult<Customer> DeleteCustomer(int id)
        {
            try
            {
                Customer result = _itComRepository.DeleteCustomer(id);
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("Customers", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }
        #endregion customers

        #region roles
        // GET api/roles
        [HttpGet]
        public ActionResult<List<Law>> Roles()
        {
            try
            {
                List<Law> gList = _itComRepository.Roles();
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
        #endregion roles

        #region groupe_roles
        // GET api/groupe_roles
        [HttpGet]
        public ActionResult<List<Group_Laws>> GroupRoles(int idGroup)
        {
            try
            {
                List<Group_Laws> gList = _itComRepository.GroupRoles(idGroup);
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
        #endregion groupe_roles

        #region groups

        // GET api/groups
        [HttpPost("{idProfil}")]
        public ActionResult<Group> CreateGroup(int idProfil, [FromBody] GroupHandler obj)
        {
            try
            {
                Group result = _itComRepository.CreateGroup(obj.group, obj.laws, idProfil);
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("Groups", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }
        // put api/groups
        [HttpPut("{idProfil}")]
        public ActionResult<Group> UpdateGroup(int idProfil, [FromBody] GroupHandler obj)
        {
            try
            {
                Group result = _itComRepository.UpdateGroup(obj.group, obj.laws, idProfil);
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("Groups", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }

        // GET api/groups
        [HttpGet]
        public ActionResult<List<GroupHandler>> Groups()
        {
            try
            {
                List<GroupHandler> gList = _itComRepository.Groups();
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

        // GET api/groups
        [HttpGet("{idGroup}")]
        public ActionResult<GroupHandler> GroupWithRoles(int idGroup)
        {
            try
            {
                GroupHandler _group = _itComRepository.GroupLaws(idGroup);
                return _group;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                LogLibrary.LogError("Groups", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }

        // GET api/groups
        [HttpGet]
        public ActionResult<List<Group>> GroupsList()
        {
            try
            {
                List<Group> gList = _itComRepository.GroupsList();
                return gList;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("Groups", msg);
                ActivityLogger.SaveLogger(msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }


        #endregion groups

        #region crypt PassWord

        // GET api/groups
        [HttpGet("{word}")]
        public ActionResult<string> CryptUserPass(string word)
        {
            try
            {
                string cryptedPass = _itComRepository.CryptWord(word);
                return cryptedPass;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("Cryptage de Mot de Passe", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }
        #endregion crypt Password
        #region agency
        /*start agency zone*/
        // GET api/agencies
        [HttpGet("{id}")]
        public ActionResult<List<Agency>> Agencies(int id)
        {
            try
            {
                List<Agency> gList = _itComRepository.Agencies(id);
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

        // post api/agency
        [HttpPost]
        public ActionResult<Agency> CreateAgency([FromBody] Agency obj)
        {
            try
            {
                Agency result = _itComRepository.CreateAgency(obj);
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("Groups", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }
        // post api/agency
        [HttpPut]
        public ActionResult<Agency> UpdateAgency([FromBody] Agency obj)
        {
            try
            {
                Agency result = _itComRepository.UpdateAgency(obj);
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("Groups", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }
        // post api/agency
        [HttpDelete("{id}")]
        public ActionResult<Agency> DeleteAgency(int id)
        {
            try
            {
                Agency result = _itComRepository.DeleteAgency(id);
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("Groups", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }
        /*end agency zone*/
        #endregion agency

        #region Categories
        /*start category zone*/
        // GET api/categories
        [HttpGet("{id}")]
        public ActionResult<List<Category>> Categories(int id)
        {
            try
            {
                List<Category> gList = _itComRepository.Categories(id);
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
        // post api/category
        [HttpPost]
        public ActionResult<Category> CreateCategory([FromBody] Category obj)
        {
            try
            {
                Category result = _itComRepository.CreateCategory(obj);
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("Groups", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }
        // post api/category
        [HttpPut]
        public ActionResult<Category> UpdateCategory([FromBody] Category obj)
        {
            try
            {
                Category result = _itComRepository.UpdateCategory(obj);
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("Groups", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }
        // post api/category
        [HttpDelete("{id}")]
        public ActionResult<Category> DeleteCategory(int id)
        {
            try
            {
                Category result = _itComRepository.DeleteCategory(id);
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("Groups", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }
        /*end category zone*/
        #endregion category

        #region MeasureType
        /*start category zone*/
        // GET api/MeasureType
        [HttpGet("{id}")]
        public ActionResult<List<MeasureType>> MeasureTypes(int id)
        {
            try
            {
                List<MeasureType> gList = _itComRepository.MeasureTypes(id);
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

        // post api/category
        [HttpPost]
        public ActionResult<MeasureType> CreateMeasureType([FromBody] MeasureType obj)
        {
            try
            {
                MeasureType result = _itComRepository.CreateMeasureType(obj);
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                LogLibrary.LogError("Groups", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }

        // post api/category
        [HttpPut]
        public ActionResult<MeasureType> UpdateMeasureType([FromBody] MeasureType obj)
        {
            try
            {
                MeasureType result = _itComRepository.UpdateMeasureType(obj);
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("Groups", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }
        // post api/category
        [HttpDelete("{id}")]
        public ActionResult<MeasureType> DeleteMeasureType(int id)
        {
            try
            {
                MeasureType result = _itComRepository.DeleteMeasureType(id);
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("Groups", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }
        /*end category zone*/
        #endregion MeasureType


        #region suppliers
        /*start suppliers zone*/
        // GET api/suppliers
        [HttpGet("{id}")]
        public ActionResult<List<Supplier>> Suppliers(int id)
        {
            try
            {
                List<Supplier> gList = _itComRepository.Suppliers(id);
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
        // post api/suppliers
        [HttpPost]
        public ActionResult<Supplier> CreateSupplier([FromBody] Supplier obj)
        {
            try
            {
                Supplier result = _itComRepository.CreateSupplier(obj);
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("Groups", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }
        // post api/suppliers
        [HttpPut]
        public ActionResult<Supplier> UpdateSupplier([FromBody] Supplier obj)
        {
            try
            {
                Supplier result = _itComRepository.UpdateSupplier(obj);
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("Groups", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }

        // post api/suppliers
        [HttpDelete("{id}")]
        public ActionResult<Supplier> DeleteSupplier(int id)
        {
            try
            {
                Supplier result = _itComRepository.DeleteSupplier(id);
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("Groups", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }
        /*end agency suppliers*/
        #endregion suppliers


        #region compartments
        // GET api/compartments
        [HttpGet("{id}")]
        public ActionResult<List<Compartment>> Compartments(int id)
        {
            try
            {
                List<Compartment> gList = _itComRepository.Compartments(id);
                return gList;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                LogLibrary.LogError("Compartments", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }
        // post api/compartments
        [HttpPost]
        public ActionResult<Compartment> CreateCompartment([FromBody] Compartment obj)
        {
            try
            {
                Compartment result = _itComRepository.CreateCompartment(obj);
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                LogLibrary.LogError("CreateCompartment", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }
        // put api/compartments
        [HttpPut]
        public ActionResult<Compartment> UpdateCompartment([FromBody] Compartment obj)
        {
            try
            {
                Compartment result = _itComRepository.UpdateCompartment(obj);
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                LogLibrary.LogError("UpdateCompartment", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }
        // delete api/compartments
        [HttpDelete("{id}")]
        public ActionResult<Compartment> DeleteCompartment(int id)
        {
            try
            {
                Compartment result = _itComRepository.DeleteCompartment(id);
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                LogLibrary.LogError("DeleteCompartment", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }
        /*end compartments*/


        #endregion compartments


        #region products
        // GET api/product
        [HttpGet("{id}")]
        public ActionResult<List<Product>> Products(int id)
        {
            try
            {
                List<Product> gList = _itComRepository.Products(id);
                return gList;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("Products", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }

        // post api/product
        [HttpPost("{id}")]
        public ActionResult<List<Product>> SearchProducts([FromBody] QuerySale obj, int id)
        {
            try
            {
                List<Product> gList = _itComRepository.SearchProducts(id, obj.product_name, obj.product_code);
                return gList;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("SearchProducts", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }//SearchProducts

        [HttpGet("{id}")]
        public ActionResult<List<ProductInStock>> ProductsFromStock(int id)
        {
            try
            {
                List<ProductInStock> gList = _itComRepository.ProductsFromStock(id);
                return gList;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("ProductsFromStock", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }

        // GET api/producttypes
        [HttpGet("{id}")]
        public ActionResult<List<ProdMeasureType>> ProductTypes(int id)
        {
            try
            {
                List<ProdMeasureType> gList = _itComRepository.ProductTypes(id);
                return gList;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("ProductTypes", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }

        // post api/products
        [HttpPost]
        public ActionResult<Product> CreateProduct([FromBody] Product obj)  //
        {
            try
            {
                Product result = _itComRepository.CreateProduct(obj);
                return result;
            }
            catch (Exception ex)
            {
                
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("CreateProduct", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }

        // post api/products
        [HttpPost]
        public ActionResult<ProductTest> CreateProductTest([FromBody] ProductTest obj)  //
        {
            try
            {

                return obj;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("CreateProduct", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }

        // put api/products
        [HttpPut]
        public ActionResult<Product> UpdateProduct([FromBody] Product obj)
        {
            try
            {
                Product result = _itComRepository.UpdateProduct(obj);
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("UpdateProduct", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }
        // delete api/products
        [HttpDelete("{id}")]
        public ActionResult<Product> DeleteProduct(int id)
        {
            try
            {
                Product result = _itComRepository.DeleteProduct(id);
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("DeleteProduct", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }

        // put api/products
        [HttpPut]
        public ActionResult<Product> DeleteProdMeasureType([FromBody] ProdMeasureType obj)
        {
            try
            {
                Product result = _itComRepository.DeleteProdMeasureType(obj);
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("DeleteProdMeasureType", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }

        // put api/products
        [HttpPut]
        public ActionResult<Product> DeleteProdAssociation([FromBody] MeasureAssociation obj)
        {
            try
            {
                Product result = _itComRepository.DeleteProdAssociation(obj);
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("DeleteProdAssociation", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }


        /*end products*/

        #endregion products


        #region saletarget

        // GET api/saletarget
        [HttpGet("{id}")]
        public ActionResult<List<SaleTarget>> SaleTargetList(int id)
        {
            try
            {
                List<SaleTarget> gList = _itComRepository.SaleTargetList(id);
                return gList;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("SaleTargetList", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }

        // post api/saletarget
        [HttpPost]
        public ActionResult<List<SaleTarget>> SearchSaleTargetList([FromBody] PeriodParam obj)
        {
            try
            {
                List<SaleTarget> gList = _itComRepository.SaleTargetList(obj);
                return gList;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("SearchSaleTargetList", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }

        // post api/saletarget
        [HttpPost]
        public ActionResult<SaleTarget> CreateSaleTarget([FromBody] SaleTarget obj)
        {
            try
            {
                SaleTarget result = _itComRepository.CreateSaleTarget(obj);
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                LogLibrary.LogError("CreateSaleTarget", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }
        // put api/saletarget
        [HttpPut]
        public ActionResult<SaleTarget> UpdateSaleTarget([FromBody] SaleTarget obj)
        {
            try
            {
                SaleTarget result = _itComRepository.UpdateSaleTarget(obj);
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                LogLibrary.LogError("UpdateSaleTarget", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }

        // delete api/saletarget
        [HttpDelete("{id}")]
        public ActionResult<SaleTarget> DeleteSaleTarget(int id)
        {
            try
            {
                SaleTarget result = _itComRepository.DeleteSaleTarget(id);
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                LogLibrary.LogError("DeleteSaleTarget", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }

        #endregion saletarget



        #region banks
        // GET api/banks
        //[Route("good/tac")]
        [HttpGet("{id}")]
        public ActionResult<List<Bank>> Banks(int id)
        {
            try
            {
                List<Bank> gList = _itComRepository.Banks(id);
                return gList;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                LogLibrary.LogError("Banks", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }

        // post api/banks
        [HttpPost]
        public ActionResult<Bank> CreateBank([FromBody] Bank obj)
        {
            try
            {
                Bank result = _itComRepository.CreateBank(obj);
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                LogLibrary.LogError("CreateBank", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }

        // put api/banks
        [HttpPut]
        public ActionResult<Bank> UpdateBank([FromBody] Bank obj)
        {
            try
            {
                Bank result = _itComRepository.UpdateBank(obj);
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                LogLibrary.LogError("UpdateBank", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }

        // delete api/bank
        [HttpDelete("{id}")]
        public ActionResult<Bank> DeleteBank(int id)
        {
            try
            {
                Bank result = _itComRepository.DeleteBank(id);
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                LogLibrary.LogError("DeleteBank", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }
        /*end banks*/

        #endregion banks


        #region account_types
        // GET api/accounttypes        
        [HttpGet("{id}")]
        public ActionResult<List<AccountType>> AccountTypes(int id)
        {
            try
            {
                List<AccountType> gList = _itComRepository.AccountTypes(id);
                return gList;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                LogLibrary.LogError("AccountTypes", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }//fin AccountTypes

        #endregion


        // api/Uploadlogo
        [HttpPost]
        #region uploadlogo
        public IActionResult UploadLogo()
        {
            try
            {
                if (!Request.HasFormContentType)
                {
                    return BadRequest();
                }
                // Retrieve data from Form
                var form = Request.Form;
                // Retrieve login
                string userLogin = form["user"].First();

                string logoFinalPath = "";

                //update company detail   
                logoFinalPath = string.Format("{0}.jpg", OfficeParams.getUniqueKey());

                // save the logo
                foreach (var file in form.Files)
                {
                    // Process file
                    using (var readStream = file.OpenReadStream())
                    {
                        //logoFinalPath = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                        string logoPath = System.IO.Path.Combine(_resourceObject.destinationPath, logoFinalPath); //System.IO.Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot") + $@"\{logoFinalPath}");

                        //Save file to harddrive
                        using (FileStream fs = System.IO.File.Create(logoPath))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }
                    }
                }

                Profil userProfile = _itComRepository.UpdateCompanyLogo(userLogin, logoFinalPath);
                //if (userProfile != null)
                //{
                //    userProfile.agency.company.logo = System.IO.Path.Combine(_resourceObject.destinationPath, userProfile.agency.company.logo);
                //}

                return Ok(userProfile);

            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                LogLibrary.LogError("AccountTypes", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;

            }
        }
        //end save logo
        #endregion



        [HttpGet]
        public ActionResult<Licence> CurrentLicence()
        {
            try
            {
                Licence result = new Licence();
                result = _itComRepository.GetCurrentLicence();
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("CurrentLicence", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }//fin CurrentLicence

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

                LogLibrary.LogError("CurrentLicCode", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }//fin CurrentLic


        // post api/changelicence
        [HttpPost]
        public ActionResult<Licence> LicenceChange([FromBody] Licence obj)
        {
            try
            {
                Licence result = _itComRepository.SaveLicence(obj);
                if (result.id == 0) return BadRequest(Constant.GenericError);;
                return result;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("LicenceChange", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }//fin LicenceChange


        /**/
        // GET api/logs
        [HttpGet("{id}")]
        public ActionResult<List<LogItem>> Logs(int id)
        {
            try
            {
                List<LogItem> gList = _itComRepository.Logs(id);
                return gList;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                LogLibrary.LogError("Logs", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }

        // GET api/logs
        [HttpPost]
        public ActionResult<List<LogItem>> SearchLogs([FromBody] PeriodParam obj)
        {
            try
            {
                obj.startDate = obj.startDate.AddHours(1); obj.endDate = obj.endDate.AddHours(1);//because the sender is using gmt
                List<LogItem> gList = _itComRepository.Logs(obj);
                return gList;
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                LogLibrary.LogError("Logs", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }//fin Logs


        [HttpGet]
        public async Task<IActionResult> Resources(string image)
        {
            try
            {
                if (image == null) { image = ""; }
                string imagePath = System.IO.Path.Combine(_resourceObject.destinationPath, image);
                var imageObj = System.IO.File.OpenRead(imagePath);
                return await Task.Run(() => File(imageObj, "image/jpeg"));
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                LogLibrary.LogError("Logs", msg);
                ActivityLogger.SaveLogger(msg);
                return BadRequest(Constant.GenericError);;
            }
        }

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
