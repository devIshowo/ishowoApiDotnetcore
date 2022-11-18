using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItCommerce.Api.Net.Extra
{
    public class ReportObject
    {
        public string destinationPath { get; set; }
        public string fileName { get; set; }
        public string fileCompletePath { get; set; }

        public ReportObject(string _destPath)
        {
            destinationPath = _destPath;
        }
    }
}
