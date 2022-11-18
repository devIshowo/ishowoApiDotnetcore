using Newtonsoft.Json;
using System;

namespace API.Models
{
    public class fedapayrequest
    {

            public string description { get; set; }
            public int amount { get; set; }
            public Currency currency { get; set; }
            public string callback_url { get; set; }
            public admin customer { get; set; }

     }
            public class Currency
            {
        public string iso { get; set; } = "XOF";
            }


            public class admin
            {
                public string firstname { get; set; }
                public string lastname { get; set; }
                public string email { get; set; }
                public PhoneNumber phone_number { get; set; }

            }
            public class PhoneNumber
            {
                public string number { get; set; }
        public string country { get; set; } = "bj";

            }

    public class request
    {
        public string description { get; set; }
        public int amount { get; set; }
        public int duration { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
    }



}
