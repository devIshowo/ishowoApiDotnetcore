using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewCrypter
{
    public class ConManager
    {
        public static string GetUrl(string code, string dbInstance)
        {
            string codeR = Beautify.decodeVar(code);
            string instanceNameProd = dbInstance; // "IT_COMMERCE";
            string dbNameProd = "IT_COMMERCE";
            string loginProd = "sa";
            string passwordProd = codeR;
            string connStringProd = string.Format(@"metadata=res://*/ModelDesign.ItCommerce.csdl|res://*/ModelDesign.ItCommerce.ssdl|res://*/ModelDesign.ItCommerce.msl;provider=System.Data.SqlClient;provider connection string='data source={0};initial catalog={1};persist security info=True;user id={2};Password={3};MultipleActiveResultSets=True;App=EntityFramework'", instanceNameProd, dbNameProd, loginProd, passwordProd);

            return connStringProd;
        }
    }
}
