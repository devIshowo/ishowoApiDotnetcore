using ItCommerce.DTO.ModelDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItCommerce.Business.Entities
{
    public class Invoice_Cancel { 
        public int id { get; set; }
        public string QRCODE_AVOIR { get; set; }
        public string CODE_MECEF_AVOIR { get; set; }
        public string CT_MECEF_AVOIR { get; set; }
        public string CTF_MECEF_AVOIR { get; set; }
        public string DATE_MECEF_AVOIR { get; set; }
        public string NIM { get; set; }

        public facture_avoir loadDto()
        {
            facture_avoir _dto = new facture_avoir()
            {
                id = this.id,
                QRCode_Mecef_Avoir = this.QRCODE_AVOIR,
                Code_Mecef_Avoir = this.CODE_MECEF_AVOIR,
                Compteur_Total_Mecef_Avoir = this.CT_MECEF_AVOIR,
                Compteur_Type_Facture_Mecef_Avoir = CTF_MECEF_AVOIR,
                Date_Mecef_Avoir = this.DATE_MECEF_AVOIR,
                NIM_Avoir = this.NIM,
            };
            return _dto;
        }

        public static Invoice_Cancel Create(facture_avoir _dbo)
        {
            return new Invoice_Cancel()
            {
                id = _dbo.id,
                QRCODE_AVOIR = _dbo.QRCode_Mecef_Avoir,
                CODE_MECEF_AVOIR = _dbo.Code_Mecef_Avoir,
                CT_MECEF_AVOIR = _dbo.Compteur_Total_Mecef_Avoir,
                CTF_MECEF_AVOIR = _dbo.Compteur_Type_Facture_Mecef_Avoir,
                DATE_MECEF_AVOIR = _dbo.Date_Mecef_Avoir,
                NIM = _dbo.NIM_Avoir
            };
        }

        public static List<Invoice_Cancel> CreateFromList(List<facture_avoir> _dboList)
        {
            List<Invoice_Cancel> _list = new List<Invoice_Cancel>();
            foreach (facture_avoir item in _dboList)
            {
                Invoice_Cancel _businessObject = Invoice_Cancel.Create(item);
                _list.Add(_businessObject);
            }
            return _list;
        }
    }


}
