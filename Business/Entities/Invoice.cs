using ItCommerce.DTO.ModelDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItCommerce.Business.Entities
{
    public class Invoice
    {
        public int id { get; set; }
        public string QRCODE { get; set; }
        public string NIM { get; set; }
        public string CODE_MECEF { get; set; }
        public string CT_MECEF { get; set; }
        public string CTF_MECEF { get; set; } 
        public string DATE_MECEF { get; set; }
        public bool API_MECEF { get; set; }

        public facture loadDto()
        {
            facture _dto = new facture()
            {
                id = this.id,
                QRCode_Mecef = this.QRCODE,
                NIM_Mecef = this.NIM,
                Code_Mecef = this.CODE_MECEF,
                Compteur_Total_Mecef = this.CT_MECEF,
                Compteur_Type_Facture_Mecef = CTF_MECEF,
                Date_Mecef = this.DATE_MECEF,
                API = this.API_MECEF
            };
            return _dto;
        }

        public static Invoice Create(facture _dbo)
        {
            return new Invoice()
            {
                id = _dbo.id,
                QRCODE = _dbo.QRCode_Mecef,
                NIM = _dbo.NIM_Mecef,
                CODE_MECEF = _dbo.Code_Mecef,
                CT_MECEF = _dbo.Compteur_Total_Mecef,
                CTF_MECEF = _dbo.Compteur_Type_Facture_Mecef,
                DATE_MECEF = _dbo.Date_Mecef,
                API_MECEF = _dbo.API
            };
        }

        public static List<Invoice> CreateFromList(List<facture> _dboList)
        {
            List<Invoice> _list = new List<Invoice>();
            foreach (facture item in _dboList)
            {
                Invoice _businessObject = Invoice.Create(item);
                _list.Add(_businessObject);
            }
            return _list;
        }
    }


}
