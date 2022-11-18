
using Encrypter.Lib;
using ItCommerce.DTO.Factory;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
//using Utils.IwajuTech.Business.Factories;

namespace ItCommerce.Business.Extra
{
    class Secu
    {
        //internal static string RainDalKey
        //{
        //    get
        //    {
        //        return "48kh$h543hcfh353#2fav*0=";
        //    }
        //}

        //internal static string RainDalIv
        //{
        //    get
        //    {
        //        return "2dxw9df3s2sdsdcsd3a2adf6";
        //    }
        //}

        //internal static string AskerParam {
        //    get { return "48kh$h543cfh353#2fav*0=";
        //    }
        //}
        //internal static string AskerSalt
        //{
        //    get
        //    { return "4sd5ds8kh$h54qe54feez5c5ezf3fh353efez45#2faazsv*0="; }
        //}

        //public static string InstallKey
        //{
        //    get
        //    {
        //        return "hSVsOOWt5lF3llF7Rn+OECxcfte+81WTzKua/SlMBBM=";
        //    }
        //}

        //public static string AppConfigPath
        //{
        //    get
        //    {
        //        return @"C:\inetpub\wwwroot\itcommerce\"; 
        //    }
        //}

        //public static string DbInstanceName
        //{
        //    get
        //    {
        //        string instancePart = "LART"; // "LART / IT_COMMERCE";

        //        string computerName = System.Environment.MachineName;
        //        string instanceNameProd = string.Format(@"{0}\{1}", computerName, instancePart); 
        //        return instanceNameProd;
        //    }
        //}

        //public static string DbConnString
        //{
        //    get
        //    {
        //        string instanceNameProd = Secu.DbInstanceName;
        //        string dbNameProd = "IT_COMMERCE";
        //        string loginProd = "sa";
        //        string passwordProd = "{0}";
        //        string connStringProd = string.Format(@"metadata=res://*/ModelDesign.ItCommerce.csdl|res://*/ModelDesign.ItCommerce.ssdl|res://*/ModelDesign.ItCommerce.msl;provider=System.Data.SqlClient;provider connection string='data source={0};initial catalog={1};persist security info=True;user id={2};Password={3};MultipleActiveResultSets=True;App=EntityFramework'", instanceNameProd, dbNameProd, loginProd, passwordProd);

        //        return connStringProd;
        //    }
        //}

        //public static string PrefPath { get {  return @"preferences.prf";   } }
        //public static string PaPath { get { return @"pa.prf"; } }

        //public static string PublickeyPath { get { return @"public.key"; } }
        //public static string PrivateKeyPath { get { return @"private.key"; } }


        /// <summary>
        /// read content
        /// </summary>
        /// <param name="content"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        //public static string ReadContent(string filePath)
        //{
        //    try
        //    {
        //        string resultCrypted = File.ReadAllText(filePath);
        //        //List<byte[]> keys = RainDal.GenerateAlgorithmInputs(Secu.AskerSalt);
        //        string decryptedValue = ProCrypter.DoDecrypt(resultCrypted, Secu.PublickeyPath);            // RainDal.DecryptString(str, keys[0], keys[1]);
        //        return decryptedValue;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}//end ReadContent


        //public static string ReadTextContent(string resultCrypted)
        //{
        //    try
        //    {
        //        //List<byte[]> keys = RainDal.GenerateAlgorithmInputs(Secu.AskerSalt);
        //        string decryptedValue = ProCrypter.DoDecrypt(resultCrypted, Secu.PublickeyPath);  //RainDal.DecryptString(str, keys[0], keys[1]);
        //        return decryptedValue;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}//end ReadTextContent

    }

    /// <summary>
    /// Generates a 16 byte Unique Identification code of a computer
    /// Example: 4876-8DB5-EE85-69D3-FE52-8CF7-395D-2EA9
    /// </summary>
    public class FingerPrint
    {
        private static string fingerPrint = string.Empty;
        public static string Value()
        {
            if (string.IsNullOrEmpty(fingerPrint))
            {
                fingerPrint = GetHash("CPU >> " + cpuId() + "\nBIOS >> " + biosId() + "\nBASE >> " + baseId()
                                     //+"\nDISK >> "+ diskId() + "\nVIDEO >> " + videoId() +"\nMAC >> "+ macId()
                                     );
            }
            return fingerPrint;
        }
        private static string GetHash(string s)
        {
            MD5 sec = new MD5CryptoServiceProvider();
            ASCIIEncoding enc = new ASCIIEncoding();
            byte[] bt = enc.GetBytes(s);
            return GetHexString(sec.ComputeHash(bt));
        }
        private static string GetHexString(byte[] bt)
        {
            string s = string.Empty;
            for (int i = 0; i < bt.Length; i++)
            {
                byte b = bt[i];
                int n, n1, n2;
                n = (int)b;
                n1 = n & 15;
                n2 = (n >> 4) & 15;
                if (n2 > 9)
                    s += ((char)(n2 - 10 + (int)'A')).ToString();
                else
                    s += n2.ToString();
                if (n1 > 9)
                    s += ((char)(n1 - 10 + (int)'A')).ToString();
                else
                    s += n1.ToString();
                if ((i + 1) != bt.Length && (i + 1) % 2 == 0) s += "-";
            }
            return s;
        }
        #region Original Device ID Getting Code
        //Return a hardware identifier
        private static string identifier(string wmiClass, string wmiProperty, string wmiMustBeTrue)
        {
            string result = "";
            try
            {
                //System.Management.ManagementClass mc = new System.Management.ManagementClass(wmiClass);
                //System.Management.ManagementObjectCollection moc = mc.GetInstances();
                //foreach (System.Management.ManagementObject mo in moc)
                //{
                //    if (mo[wmiMustBeTrue].ToString() == "True")
                //    {
                //        //Only get the first one
                //        if (result == "")
                //        {
                //            try
                //            {
                //                result = mo[wmiProperty].ToString();
                //                break;
                //            }
                //            catch
                //            {
                //            }
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }//fin identifier

        //Return a hardware identifier
        private static string identifier(string wmiClass, string wmiProperty)
        {
            try
            {
                string result = "";
                //System.Management.ManagementClass mc = new System.Management.ManagementClass(wmiClass);
                //System.Management.ManagementObjectCollection moc = mc.GetInstances();
                //foreach (System.Management.ManagementObject mo in moc)
                //{
                //    //Only get the first one
                //    if (result == "")
                //    {
                //        try
                //        {
                //            result = mo[wmiProperty].ToString();
                //            break;
                //        }
                //        catch
                //        {
                //        }
                //    }
                //}
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //return result;
        }

        private static string cpuId()
        {
            //Uses first CPU identifier available in order of preference
            //Don't get all identifiers, as very time consuming
            string retVal = identifier("Win32_Processor", "UniqueId");
            if (retVal == "") //If no UniqueID, use ProcessorID
            {
                retVal = identifier("Win32_Processor", "ProcessorId");
                if (retVal == "") //If no ProcessorId, use Name
                {
                    retVal = identifier("Win32_Processor", "Name");
                    if (retVal == "") //If no Name, use Manufacturer
                    {
                        retVal = identifier("Win32_Processor", "Manufacturer");
                    }
                    //Add clock speed for extra security
                    retVal += identifier("Win32_Processor", "MaxClockSpeed");
                }
            }
            return retVal;
        }
        //BIOS Identifier
        private static string biosId()
        {
            return identifier("Win32_BIOS", "Manufacturer")
            + identifier("Win32_BIOS", "SMBIOSBIOSVersion")
            + identifier("Win32_BIOS", "IdentificationCode")
            + identifier("Win32_BIOS", "SerialNumber")
            + identifier("Win32_BIOS", "ReleaseDate")
            + identifier("Win32_BIOS", "Version");
        }
        //Main physical hard drive ID
        private static string diskId()
        {
            return identifier("Win32_DiskDrive", "Model")
            + identifier("Win32_DiskDrive", "Manufacturer")
            + identifier("Win32_DiskDrive", "Signature")
            + identifier("Win32_DiskDrive", "TotalHeads");
        }
        //Motherboard ID
        private static string baseId()
        {
            return identifier("Win32_BaseBoard", "Model")
            + identifier("Win32_BaseBoard", "Manufacturer")
            + identifier("Win32_BaseBoard", "Name")
            + identifier("Win32_BaseBoard", "SerialNumber");
        }
        //Primary video controller ID
        private static string videoId()
        {
            return identifier("Win32_VideoController", "DriverVersion")
            + identifier("Win32_VideoController", "Name");
        }
        //First enabled network card ID
        private static string macId()
        {
            return identifier("Win32_NetworkAdapterConfiguration", "MACAddress", "IPEnabled");
        }
        #endregion
    }


    //registry manager
    public class RegistryBoss
    {
        //create key
        public static void CreateSubKey(string value)
        {
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Accenture"))
            {
                //List<byte[]> keys = RainDal.GenerateAlgorithmInputs(LocalSecu.AskerSalt);
                string cryptedValue = ProCrypter.DoEncrypt(value, LocalSecu.PublicKeyPath); //RainDal.EncryptString(value, keys[0], keys[1]);
                key.SetValue("DefaultColorPalette", cryptedValue);
                key.Close();
            }
        }//fin CreateSubKey

        //read key
        public static string ReadSubKeyValue()
        {
            try
            {
                string subKey = @"SOFTWARE\Accenture";
                string key = "DefaultColorPalette";
                string str = string.Empty; string decryptedValue = string.Empty;
                using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(subKey))
                {
                    if (registryKey != null)
                    {
                        str = registryKey.GetValue(key).ToString();
                        //List<byte[]> keys = RainDal.GenerateAlgorithmInputs(LocalSecu.AskerSalt);
                        decryptedValue = ProCrypter.DoDecrypt(str, LocalSecu.PublicKeyPath); //RainDal.EncryptString(value, keys[0], keys[1]);

                        //decryptedValue = RainDal.DecryptString(str, keys[0], keys[1]);
                        registryKey.Close();
                    }
                }
                return decryptedValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }//fin ReadSubKeyValue

    }


    public class RainDal
    {
        //public static List<byte[]> GenerateAlgorithmInputs(string password)
        //{

        //    byte[] key;
        //    byte[] iv;

        //    List<byte[]> result = new List<byte[]>();

        //    Rfc2898DeriveBytes rfcDb = new Rfc2898DeriveBytes(password, System.Text.Encoding.UTF8.GetBytes(password));

        //    key = rfcDb.GetBytes(16);
        //    iv = rfcDb.GetBytes(16);

        //    result.Add(key);
        //    result.Add(iv);

        //    return result;

        //}

        ///// 
        ///// Chiffre une chaîne de caractère
        ///// 
        ///// Texte clair à chiffrer
        ///// Clé de chiffrement
        ///// Vecteur d'initialisation
        ///// Retourne le texte chiffré
        //public static string EncryptString(string clearText, byte[] strKey, byte[] strIv)
        //{

        //    // Place le texte à chiffrer dans un tableau d'octets
        //    byte[] plainText = Encoding.UTF8.GetBytes(clearText);

        //    // Place la clé de chiffrement dans un tableau d'octets
        //    byte[] key = strKey; // Encoding.UTF8.GetBytes(strKey);

        //    // Place le vecteur d'initialisation dans un tableau d'octets
        //    byte[] iv = strIv; // Encoding.UTF8.GetBytes(strIv);


        //    RijndaelManaged rijndael = new RijndaelManaged();

        //    // Définit le mode utilisé
        //    rijndael.Mode = CipherMode.CBC;

        //    // Crée le chiffreur AES - Rijndael
        //    ICryptoTransform aesEncryptor = rijndael.CreateEncryptor(key, iv);

        //    MemoryStream ms = new MemoryStream();

        //    // Ecris les données chiffrées dans le MemoryStream
        //    CryptoStream cs = new CryptoStream(ms, aesEncryptor, CryptoStreamMode.Write);
        //    cs.Write(plainText, 0, plainText.Length);
        //    cs.FlushFinalBlock();


        //    // Place les données chiffrées dans un tableau d'octet
        //    byte[] CipherBytes = ms.ToArray();


        //    ms.Close();
        //    cs.Close();

        //    // Place les données chiffrées dans une chaine encodée en Base64
        //    return Convert.ToBase64String(CipherBytes);


        //}

        ///// <summary>
        ///// Déchiffre une chaîne de caractère
        ///// </summary>
        ///// <param name="cipherText">Texte chiffré</param>
        ///// <param name="strKey">Clé de déchiffrement</param>
        ///// <param name="strIv">Vecteur d'initialisation</param>
        ///// <returns></returns>
        //public static string DecryptString(string cipherText, byte[] strKey, byte[] strIv)
        //{
        //    try
        //    {

        //        // Place le texte à déchiffrer dans un tableau d'octets
        //        byte[] cipheredData = Convert.FromBase64String(cipherText);

        //        // Place la clé de déchiffrement dans un tableau d'octets
        //        byte[] key = strKey; // Encoding.UTF8.GetBytes(strKey);

        //        // Place le vecteur d'initialisation dans un tableau d'octets
        //        byte[] iv = strIv; // Encoding.UTF8.GetBytes(strIv);

        //        RijndaelManaged rijndael = new RijndaelManaged();
        //        rijndael.Mode = CipherMode.CBC;


        //        // Ecris les données déchiffrées dans le MemoryStream
        //        ICryptoTransform decryptor = rijndael.CreateDecryptor(key, iv);
        //        MemoryStream ms = new MemoryStream(cipheredData);
        //        CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);

        //        // Place les données déchiffrées dans un tableau d'octet
        //        byte[] plainTextData = new byte[cipheredData.Length];

        //        int decryptedByteCount = cs.Read(plainTextData, 0, plainTextData.Length);

        //        ms.Close();
        //        cs.Close();

        //        return Encoding.UTF8.GetString(plainTextData, 0, decryptedByteCount);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}
    }
}
