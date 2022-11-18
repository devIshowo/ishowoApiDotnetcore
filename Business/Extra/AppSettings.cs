
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItCommerce.Business.Extra
{

    public class AppSettings
    {
        public Logging Logging { get; set; }
        public Username UserName { get; set; }
        public Dbinstance DbInstance { get; set; }
        public Filepath FilePath { get; set; }
        public Resourcespath ResourcesPath { get; set; }
        public Microsoftcode MicrosoftCode { get; set; }
        public Printing Printing { get; set; }

        /// <summary>
        /// load class from string
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static AppSettings loadSettingData(string filePath)
        {
            //load file content
            string jsonContent = string.Empty;
            using (StreamReader r = new StreamReader(filePath))
            {
                var json = r.ReadToEnd();
                var jobj = JObject.Parse(json);
                foreach (var item in jobj.Properties())
                {
                    item.Value = item.Value.ToString().Replace("v1", "v2");
                }
                jsonContent = jobj.ToString();
            }

            AppSettings converted = JsonConvert.DeserializeObject<AppSettings>(jsonContent);
            return converted;
        }//fin loadSettingData

        /// <summary>
        /// save setting data
        /// </summary>
        /// <returns></returns>
        public static string saveSettingData(string filePath, AppSettings settings)
        {
            string serialized = JsonConvert.SerializeObject(settings);  
            File.WriteAllText(filePath, serialized);
            return serialized;
        }//fin saveSettingData
    }

    public class Logging
    {
        public bool IncludeScopes { get; set; }
        public Loglevel LogLevel { get; set; }
    }

    public class Loglevel
    {
        public string Default { get; set; }
        public string System { get; set; }
        public string Microsoft { get; set; }
    }

    public class Username
    {
        public string value { get; set; }
    }

    public class Dbinstance
    {
        public string value { get; set; }
    }

    public class Filepath
    {
        public string value { get; set; }
    }

    public class Resourcespath
    {
        public string value { get; set; }
    }

    public class Microsoftcode
    {
        public string entity { get; set; }
    }

    public class Printing
    {
        public string invoice_type { get; set; }
    }


    
}
