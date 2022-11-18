using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using XAct.Diagnostics.Services.Implementations;

namespace ItCommerce.Business.Extra
{
    public class eMCFServices
    {
        //test
        public static string baseUrl = "https://developper.impots.bj/sygmef-emcf/api/invoice";
        //prod
        //public static string baseUrl = "https://sygmef.impots.bj/emcf/api/invoice";


        public static async Task<StatusResponse> GetStatutAsync(string token)
        {
            var defaultProxy = WebRequest.GetSystemWebProxy();
            defaultProxy.Credentials = CredentialCache.DefaultCredentials;

            HttpClientHandler handler = new HttpClientHandler()
            {
                //Proxy = new WebProxy() { Address = new Uri("http://10.87.6.34:8080") },
                Proxy = defaultProxy,
                UseProxy = true
            };

            HttpClient client = new HttpClient(handler);
            //HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            try
            {
                HttpResponseMessage response = await client.GetAsync(baseUrl);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                StatusResponse statusResponse = JsonConvert.DeserializeObject<StatusResponse>(responseBody);
                return statusResponse;
            }
            catch (Exception ex)
            {
                string x = ex.Message;
                return new StatusResponse();
            }
           
        }

        // Demande de facture
        public static async Task<InvoiceResponseData> PostInvoice(string token, InvoiceRequestData invoiceRequestData)
        {
            var defaultProxy = WebRequest.GetSystemWebProxy();
            defaultProxy.Credentials = CredentialCache.DefaultCredentials;

            HttpClientHandler handler = new HttpClientHandler()
            {
                //Proxy = new WebProxy() { Address = new Uri("http://10.87.6.34:8080") },
                Proxy = defaultProxy,
                UseProxy = true
            };

            HttpClient client = new HttpClient(handler);
            //HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var json = JsonConvert.SerializeObject(invoiceRequestData);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(baseUrl, data);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            InvoiceResponseData invoiceResponseData = JsonConvert.DeserializeObject<InvoiceResponseData>(responseBody);
            return invoiceResponseData;
        }

        // Demande de finalisation
        public static async Task<SecurityElements> ConfirmInvoice(string token, string id, string action)
        {
            var defaultProxy = WebRequest.GetSystemWebProxy();
            defaultProxy.Credentials = CredentialCache.DefaultCredentials;

            HttpClientHandler handler = new HttpClientHandler()
            {
                //Proxy = new WebProxy() { Address = new Uri("http://10.87.6.34:8080") },
                Proxy = defaultProxy,
                UseProxy = true
            };

            HttpClient client = new HttpClient(handler);
            //HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await client.PutAsync($"{baseUrl}/{id}/{action}", null);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            SecurityElements securityElements = JsonConvert.DeserializeObject<SecurityElements>(responseBody);
            return securityElements;
        }

        // Demande de détails
        public static async Task<InvoiceDetails> GetInvoiceAsync(string token, string id)
        {
            var defaultProxy = WebRequest.GetSystemWebProxy();
            defaultProxy.Credentials = CredentialCache.DefaultCredentials;

            HttpClientHandler handler = new HttpClientHandler()
            {
                //Proxy = new WebProxy() { Address = new Uri("http://10.87.6.34:8080") },
                Proxy = defaultProxy,
                UseProxy = true
            };

            HttpClient client = new HttpClient(handler);
            //HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await client.GetAsync($"{baseUrl}/{id}");
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            InvoiceDetails invoiceDetails = JsonConvert.DeserializeObject<InvoiceDetails>(responseBody);
            return invoiceDetails;
        }
    }
}