using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ItCommerce.Business.Extra
{
    public class InvoiceDetails
    {
        public string ifu { get; set; }
        public string aib { get; set; }
        public string type { get; set; }
        public Item[] items { get; set; }
        public Client client { get; set; }
        public Operator _operator { get; set; }
        public Payment[] payment { get; set; }
        public string reference { get; set; }
        public string errorCode { get; set; }
        public string errorDesc { get; set; }
    }
}