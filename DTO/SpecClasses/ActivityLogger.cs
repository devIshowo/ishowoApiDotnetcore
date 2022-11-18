using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;

namespace ItCommerce.DTO.SpecClasses
{
    public class ActivityLogger
    {
        public static void SaveLogger(string msg)
        {
            Log.Information("--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_-- START EXCEPTION -- _--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--");
            Log.Information("The time is {msg}", msg);
            Log.Information("--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_-- END EXCEPTION -- _--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--_--");
        }

    }
}
