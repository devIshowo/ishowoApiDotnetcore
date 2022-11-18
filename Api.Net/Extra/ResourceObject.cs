using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItCommerce.Api.Net.Extra
{
    public class ResourceObject
    {
        public string destinationPath { get; set; }
        public string fileName { get; set; }
        public string fileCompletePath { get; set; }

        public ResourceObject(string _destPath)
        {
            destinationPath = _destPath;
        }
    }

    public class QuerySale
    {
        public int id_profile { get; set; }
        public string product_name { get; set; }
        public string product_code { get; set; }

        public QuerySale()
        {
        }
    }
}
