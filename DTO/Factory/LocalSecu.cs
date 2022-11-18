using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItCommerce.DTO.Factory
{
    public class LocalSecu
    {
        internal static string RainDalKey
        {
            get
            {
                return "48kh$h543hcfh353#2fav*0=";
            }
        }

        internal static string RainDalIv
        {
            get
            {
                return "2dxw9df3s2sdsdcsd3a2adf6";
            }
        }

        public static string AskerParam
        {
            get
            {
                return "48kh$h543cfh353#2fav*0=";
            }
        }
        public static string AskerSalt
        {
            get
            {
                return "4sd5ds8kh$h54qe54feez5c5ezf3fh353efez45#2faazsv*0=";
            }
        }

        public static string InstallKey
        {
            get
            {
                return "hSVsOOWt5lF3llF7Rn+OECxcfte+81WTzKua/SlMBBM=";
            }
        }

        public static string AppConfigPath
        {
            get
            {
                return @"";
            }
        }

        public static string DbInstanceName
        {
            get
            {
                string instancePart = "LART"; //IT_COMMERCE

                string computerName = System.Environment.MachineName;
                string instanceNameProd = string.Format(@"{0}\{1}", computerName, instancePart);
                return instanceNameProd;
            }
        }

        public static string DbConnString
        {
            get
            {
                string instanceNameProd = LocalSecu.DbInstanceName;
                string dbNameProd = "IT_COMMERCE";
                string loginProd = "sa";
                string passwordProd = "{0}";
                string connStringProd = string.Format(@"metadata=res://*/ModelDesign.ItCommerce.csdl|res://*/ModelDesign.ItCommerce.ssdl|res://*/ModelDesign.ItCommerce.msl;provider=System.Data.SqlClient;provider connection string='data source={0};initial catalog={1};persist security info=True;user id={2};Password={3};MultipleActiveResultSets=True;App=EntityFramework'", instanceNameProd, dbNameProd, loginProd, passwordProd);

                return connStringProd;
            }
        }

        public static string PrefPath { get { return @"preferences.prf"; } }
        public static string PaPath { get { return @"pa.prf"; } }

        public static string PublicKeyPath { get { return @"public.key"; } }
        public static string PrivateKeyPath { get { return @"private.key"; } }

        public static string SettingsPath { get { return @"appsettings.json"; } }
        public static string RandomFileName { get { return @"bdv5sd8ds77sd8788sdf5sd"; } }

    }

}
