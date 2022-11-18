using ItCommerce.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItCommerce.Business.Extra
{
    public class PrmLicence
    {

        public int id { get; set; }
        public Company company { get; set; }
        public User admin { get; set; }
        public string code { get; set; }
        public string key { get; set; }
        public bool isActive { get; set; }
        public string activationDate { get; set; }
        public string expiryDate { get; set; }

        private string _now;
        public string currentDate { get { return _now; } set { _now = value; } }
        public int activationCost { get; set; }
        public string module { get; set; }
        public string act { get; set; }


    }
}
