using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ItCommerce.Business.Extra
{
    public class InvoiceRequestData
    {
        public string ifu { get; set; }
        public string aib { get; set; }
        public string type { get; set; }
        public Item[] items { get; set; }
        public Client client { get; set; }
        public Operator @operator { get; set; }
        public Payment[] payment { get; set; }
        public string reference { get; set; }
    }

    public class Client
    {
        public string contact { get; set; }
        public string ifu { get; set; }
        public string name { get; set; }
        public string address { get; set; }
    }

    public class Operator
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Item
    {
        public string code { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public float quantity { get; set; }
        public string taxGroup { get; set; }
        public int? taxSpecific { get; set; }
        public int? originalPrice { get; set; }
        public string priceModification { get; set; }
    }

    public class Payment
    {
        public string name { get; set; }
        public int amount { get; set; }
    }

    public class InvoiceResponseData
    {
        public string uid { get; set; }
        public int ta { get; set; }
        public int tb { get; set; }
        public int tc { get; set; }
        public int td { get; set; }
        public int taa { get; set; }
        public int tab { get; set; }
        public int tac { get; set; }
        public int tad { get; set; }
        public int tae { get; set; }
        public int taf { get; set; }
        public int hab { get; set; }
        public int had { get; set; }
        public int vab { get; set; }
        public int vad { get; set; }
        public int total { get; set; }
        public int aib { get; set; }
        public int ts { get; set; }
        public string errorCode { get; set; }
        public string errorDesc { get; set; }
    }

}