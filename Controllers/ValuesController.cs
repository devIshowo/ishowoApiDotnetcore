using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ItCommerce.DTO.ModelDesign;
using System.IO;
using System;
using ItCommerce.Business.Extra;
using System.IO.Ports;
using QRCoder;
using System.Drawing;
using ZXing.QrCode.Internal;
using Sentry;
using ItCommerce.Api.Net.Logger;
using Microsoft.AspNetCore.Authorization;

namespace ASP.NETCoreWithEFCore.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ValuesController : Controller
    {

        private readonly IT_COMMERCEEntities _context;
        public ValuesController(IT_COMMERCEEntities context)
        {
            _context = context;
        }

        // GET: api/students    
        public List<categ_vente> Test()
        {

            LogLibrary.LogError("Groups", "encore un autre test");
            SentrySdk.CaptureMessage("Erreurs");
            //_context.Database.c
            var x = _context.categ_vente.ToList();
            return x;
        }
        [HttpGet]
        public IActionResult Aleatoire()
        {
            var requestUrl = $"{Request.Scheme}://{Request.Host.Value}/Views/Index.cshtml";
            return PartialView("Views/Index.cshtml");
        }

        [HttpGet]
        public string Aleatoired()
        {
            var requestUrl = $"{Request.Scheme}://{Request.Host.Value}/Views/Index.cshtml";
            return requestUrl;
        }

        [HttpGet  , Authorize(Roles = "user")]
        public IActionResult testa()
        {
            return PartialView("Views/Test1.cshtml");
        }

        [HttpGet]
        public IActionResult testb()
        {
            return PartialView("Views/Test2.cshtml");
        }

        [HttpGet]
        public IActionResult testc()
        {
            return PartialView("Views/Test3.cshtml");
        }

        [HttpGet]
        public string testmecef()
        {
            SerialPort ComPort = new SerialPort();
            SPHandler spHandler = new SPHandler();
            ComPort.PortName = "COM4";

            ComPort.BaudRate = 115200;      //convert Text to Integer
            ComPort.Parity = (Parity)Enum.Parse(typeof(Parity), "None"); //convert Text to Parity
            ComPort.DataBits = 8;        //convert Text to Integer
            ComPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), "1");  //convert Text to stop bits

            spHandler.SetPort(ComPort.PortName, ComPort.BaudRate, 1000);
            spHandler.Open();

            //  Normalisation
            try
            {
                int seq = 21;
                seq++;

                // Command C1h
                var cmd_c1 = Trame.Command("", seq++, "c1", "");
                string c1h_response = spHandler.ExecuteCommand(cmd_c1);

                if (c1h_response != "\u0015")
                {
                    //Récuperer les données
                    string val = c1h_response.Split('?')[1];
                    string[] val1 = val.Split('\u0004');
                    string[] val2 = val1[0].Split(',');

                    // Affecter aux variables globales
                    GlobalVars.NIM = val2[0];
                    GlobalVars.IFU = val2[1];
                    GlobalVars.DT = val2[2];
                    GlobalVars.TC = val2[3];
                    GlobalVars.FVC = val2[4];
                    GlobalVars.FRC = val2[5];
                    GlobalVars.TAXA = val2[6];
                    GlobalVars.TAXB = val2[7];
                    GlobalVars.TAXC = val2[8];
                    GlobalVars.TAXD = val2[9];
                }
                return "NIM: " + GlobalVars.NIM + " IFU: " + GlobalVars.IFU;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet("{inputText}")]
        public IActionResult go(string inputText)
        {
            //QRCodeGenerator oQRCodeGenerator = new QRCodeGenerator();
            //QRCodeData oQRCodeData = oQRCodeGenerator.CreateQrCode(inputText, QRCodeGenerator.ECCLevel.Q);
            //QRCode oQRCode = new QRCode(oQRCodeData);
            //Bitmap bitmap = oQRCode.GetGraphic(15);
            //var bitmapBytes = ConvertBitmapToBytes(bitmap);
            //return File(bitmapBytes, "image/jpeg");
            return Ok();
        }

        private byte[] ConvertBitmapToBytes(Bitmap bitmap)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                ImageConverter converter = new ImageConverter();
                return (byte[])converter.ConvertTo(bitmap, typeof(byte[]));
            }
        }

        [HttpGet]
        public string Css()
        {
            string pair = "123456789";
            var sub = pair.Substring(4, 4);
            return sub;
        }

        // GET api/students/1    
        [HttpGet("{id}")]
        public categ_vente Get(int id)
        {
            return _context.categ_vente.FirstOrDefault(x => x.id == id);
        }

        // POST api/students    
        //[HttpPost]    
        //public IActionResult Post([FromBody]User value)    
        //{    
        //    _context.Users.Add(value);    
        //    _context.SaveChanges();    
        //    return StatusCode(201, value);    
        //} 

        // POST api/profils    
        //[HttpPost]    
        //public IActionResult Profil([FromBody]categ_vente value)    
        //{
        //    //_context.Profils.Add(value);    
        //    //_context.SaveChanges();    
        //    //return StatusCode(201, value); 
        //    return BadRequest(Constant.GenericError);;
        //} 
    }
}
