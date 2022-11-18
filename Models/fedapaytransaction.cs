
using Newtonsoft.Json;
using System;

namespace API.Models
{
    public class fedapaytransaction
    {
       
            [JsonProperty("v1/transaction")]

            public V1Transaction V1Transaction { get; set; }



    }

    public class V1Transaction
        {
            public string klass { get; set; }
            public int id { get; set; }
            public string reference { get; set; }
            public int amount { get; set; }
            public string description { get; set; }
            public string callback_url { get; set; }
            public string status { get; set; }
            public int customer_id { get; set; }
            public int currency_id { get; set; }
            public object mode { get; set; }
            public string operation { get; set; }
            public Metadata metadata { get; set; }
            public object commission { get; set; }
            public object fees { get; set; }
            public int fixed_commission { get; set; }
            public object amount_transferred { get; set; }
            public DateTime created_at { get; set; }
            public DateTime updated_at { get; set; }
            public object approved_at { get; set; }
            public object canceled_at { get; set; }
            public object declined_at { get; set; }
            public object refunded_at { get; set; }
            public object transferred_at { get; set; }
            public object deleted_at { get; set; }
            public object last_error_code { get; set; }
            public object custom_metadata { get; set; }
            public object amount_debited { get; set; }
            public object receipt_url { get; set; }
            public object payment_method_id { get; set; }
            public object sub_accounts_commissions { get; set; }
            public object transaction_key { get; set; }
        }
                public class Metadata
                {
                    public PaidCustomer paid_customer { get; set; }
                    public string transfer_schedule_jobid { get; set; }
                }

                public class PaidCustomer
                {
                    public int id { get; set; }
                    public string firstname { get; set; }
                    public string lastname { get; set; }
                    public string email { get; set; }
                    public int account_id { get; set; }
                    public object deleted_at { get; set; }
                    public DateTime created_at { get; set; }
                    public DateTime updated_at { get; set; }
                    public int phone_number_id { get; set; }
                }

}
