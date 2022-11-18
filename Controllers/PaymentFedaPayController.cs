using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using API.Models;
using ItCommerce.Api.Net.Logger;
using System;
using ItCommerce.Api.Net.Models;
using Microsoft.Extensions.Configuration;
using API.Business.Extra;
using XAct;
using ItCommerce.DTO.Factory;
using ItCommerce.DTO.ModelDesign;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentFedaPayController : ControllerBase
    {
        private IItCommerceRepository _itComRepository;
        public PaymentFedaPayController(IItCommerceRepository _repository)
        {
            _itComRepository = _repository;


        }

        private string token = "sk_sandbox_SopECree3EUGs0EoMDEWZlwq";
 
        [Route("createPayment")]
        [HttpPost]
        public async Task<IActionResult> createTransaction([FromBody] request obj)
        {
            try
            {
               var result = _itComRepository.SearchUserWithEmail(obj.email);

                var operation = new fedapayrequest();
                operation.description = "Payment du module standart ";
                operation.amount = obj.amount;
                operation.customer = new admin();
                operation.callback_url = "wazindo.com";
                operation.currency = new Currency();
                operation.customer.firstname = result.nom;
                operation.currency.iso = "XOF";
                operation.customer.email = result.email;
                operation.customer.lastname = result.nom;
                operation.customer.phone_number = new PhoneNumber();
                operation.customer.phone_number.number = obj.phone;
                var test = operation;

   
                //requet http sur la route api
                var httprequest = new HttpClient();
                httprequest.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                httprequest.DefaultRequestHeaders.Accept.Add(contentType);

                //serealization des données recu
                var action = JsonConvert.SerializeObject(operation);
                HttpContent content = null;
                try
                {
                    content = new StringContent(action);
                    content = new StringContent(action, System.Text.Encoding.UTF8, "application/json");
                }
                catch (Exception)
                {

                    //throw;
                }
                var response = await httprequest.PostAsync("https://sandbox-api.fedapay.com/v1/transactions", content);


                if (response.IsSuccessStatusCode)
                {

                    var jsonResponse = await response.Content.ReadAsStringAsync();
                   
                    var request = JsonConvert.DeserializeObject<fedapaytransaction>(jsonResponse);


                    var emptyMessage = JsonConvert.SerializeObject("");
                    var empty = new StringContent(emptyMessage, System.Text.Encoding.UTF8, "application/json");

                    var sent = await httprequest.PostAsync($"https://sandbox-api.fedapay.com/v1/transactions/{request.V1Transaction.id}/token", empty);


                    var paymentLink = await sent.Content.ReadAsStringAsync();
                    Dictionary<string, object> transactionLink = JsonConvert.DeserializeObject<Dictionary<string, object>>(paymentLink);


                    return Ok(new
                    {
                        link = transactionLink["url"],
                        token = transactionLink["token"],
                        id = request.V1Transaction.id,
                    }) ;
                }

                return BadRequest(Constant.GenericError);

            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("CreateAgent", msg);
                return BadRequest(Constant.GenericError);
            }
        }

        [Route("reference/{id}/{paymentToken}")]
        [HttpGet]
        public async Task<IActionResult> reference(int id, string paymentToken)
        {
            try
            {
                if (String.IsNullOrEmpty(paymentToken) && id == 0)
                {
                    return BadRequest("Cette transaction n'existe pas.");
                }

                var httprequest = new HttpClient();
                httprequest.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                httprequest.DefaultRequestHeaders.Accept.Add(contentType);

                //var action = JsonConvert.SerializeObject("");
                //var content = new StringContent(action, System.Text.Encoding.UTF8, "application/json");

                var response = await httprequest.GetAsync($"https://sandbox-api.fedapay.com/v1/transactions/{id}");


                var stringDat = await response.Content.ReadAsStringAsync();
                //  Dictionary<string, object> transaction = JsonConvert.DeserializeObject<Dictionary<string, object>>(stringData);
                //Dictionary<string, object> id = JsonConvert.DeserializeObject<Dictionary<string, object>>(transaction["v1/transaction"].ToString());
                var transaction = JsonConvert.DeserializeObject<fedapaytransaction>(stringDat);


                return Ok(new
                {
                    id = transaction.V1Transaction.id,
                    status = transaction.V1Transaction.status,
                    reference = transaction.V1Transaction.reference,
                    commission = transaction.V1Transaction.commission
                });
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);

                LogLibrary.LogError("CreateAgent", msg);
                return BadRequest(Constant.GenericError);
            }
        }


    }
}
