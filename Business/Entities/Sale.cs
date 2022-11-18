using ItCommerce.DTO.ModelDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItCommerce.Business.Entities
{
    public abstract class Sale
    {
        public int id { get; set; }
        public User agent { get; set; }
        public Customer customer { get; set; }
        public DateTime date { get; set; }
        public List<object> lines { get; set; }
        public int amount_received { get; set; }
        public int amount_real { get; set; }
        public bool is_balanced { get; set; } //si la vente a ete integralement payee

        public Sale(int sale_id, User sale_agent, Customer sale_customer, DateTime sale_date, int sale_amount_received, int sale_amount_real, bool sale_is_balanced)
        {
            id = sale_id;
            agent = sale_agent;
            customer = sale_customer;
            date = sale_date;
            amount_received = sale_amount_received;
            amount_real = sale_amount_real;
            is_balanced = sale_is_balanced;
        }

        public abstract Sale Create(object _dbo);


        public abstract Sale CreateDeep(object _dbo);


        public abstract object loadDto();


        public abstract List<object> CreateFromList(List<object> _dboList);



    }
}
