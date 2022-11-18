

using ItCommerce.DTO.Factory;
using NewCrypter;
using System;
using System.IO;

namespace ItCommerce.Business.Extra
{
    public class ConfigObject
    {
        public static string UserLicense { get; set; }
        public static int RemainingDays { get; set; }
        public static bool IsLicenseOk { get; set; }
        public static string InvoiceType { get; set; }
        public static string ConnString { get; set; }
        public static string DbInstance { get; set; }
        public static string ApiUrl { get; set; }

        public static string GetUrl(string inFileCode, string dbInstance)
        {
            string realConn = Manager.decodeVar(inFileCode);
            string url = ConManager.GetUrl(realConn, dbInstance);
            return url;
        }

        public static string GetUserName()
        {
            string content = File.ReadAllText(Path.Combine(LocalSecu.AppConfigPath, LocalSecu.RandomFileName) );
            return content;
        }


        public static void CreateUserName()
        {
            string userName = Manager.encodeVar(Beautify.encodeVar("sa"));
            File.WriteAllText(Path.Combine(LocalSecu.AppConfigPath, LocalSecu.RandomFileName), userName);
        }

        public static string GetCode(string code)
        {
            string beauty = Beautify.encodeVar(code);
            string realConn = Manager.encodeVar(beauty);
            //string url = ConManager.GetUrl(realConn, dbInstance);
            return realConn;
        }


        public static void CheckLicence()
        {
            try
            {
                if (ConfigObject.IsLicenseOk == true)
                {
                    //checks

                    //last licence
                    //Licence lastLicence = OfficeAuth.getLastLicence();

                    ////if
                    //if (lastLicence != null)
                    //{
                    //    //if activated
                    //    if (lastLicence.isActive == false)
                    //    {
                    //        ConfigObject.IsLicenseOk = false; return;
                    //    }
                    //    else
                    //    {
                    //        //get backuped licence
                    //        var path = LocalSecu.AppConfigPath;
                    //        string backupedLicenceStr = LocalFileObject.ReadContent(Path.Combine(path, LocalSecu.PrefPath));
                    //        Licence backupedLicence = Licence.Extract(backupedLicenceStr);

                    //        //compare licence key
                    //        if (backupedLicence.key != lastLicence.key)
                    //        {
                    //            ConfigObject.IsLicenseOk = false; return;
                    //        }

                    //        //compare activation date with db activation date
                    //        if (backupedLicence.activationDate.Day != lastLicence.activationDate.Day || backupedLicence.activationDate.Month != lastLicence.activationDate.Month
                    //        || backupedLicence.activationDate.Year != lastLicence.activationDate.Year)
                    //        {
                    //            ConfigObject.IsLicenseOk = false; return;
                    //        }

                    //        //compare expiry date with db expiry date
                    //        if (backupedLicence.expiryDate.Day != lastLicence.expiryDate.Day || backupedLicence.expiryDate.Month != lastLicence.expiryDate.Month
                    //        || backupedLicence.expiryDate.Year != lastLicence.expiryDate.Year)
                    //        {
                    //            ConfigObject.IsLicenseOk = false; return;
                    //        }

                    //        //expiry date coherence
                    //        if (backupedLicence.currentDate.CompareTo(backupedLicence.expiryDate) > 0)
                    //        {
                    //            ConfigObject.IsLicenseOk = false; return;
                    //        }

                    //        //expiry date coherence
                    //        if (DateTime.Now.CompareTo(backupedLicence.activationDate) < 0)
                    //        {
                    //            ConfigObject.IsLicenseOk = false; return;
                    //        }

                    //        //compare current date with system current date
                    //        if (backupedLicence.currentDate.Day != DateTime.Now.Day || backupedLicence.currentDate.Month != DateTime.Now.Month
                    //            || backupedLicence.currentDate.Year != DateTime.Now.Year)
                    //        {
                    //            //check sur la date de derniere execution
                    //            if (backupedLicence.currentDate.CompareTo(DateTime.Now) > 0)
                    //            {
                    //                ConfigObject.IsLicenseOk = false; return;
                    //            }
                    //            else
                    //            {
                    //                //update current date
                    //                backupedLicence.currentDate = DateTime.Now;
                    //                //save then
                    //                string toBeSavedContent = backupedLicence.ToString();
                    //                bool status = LocalFileObject.WriteContent(toBeSavedContent, Path.Combine(LocalSecu.AppConfigPath, LocalSecu.PrefPath));
                    //            }
                    //        }

                    //        //licence encore active
                    //        ConfigObject.RemainingDays = (backupedLicence.expiryDate - backupedLicence.currentDate).Days;
                    //        ConfigObject.IsLicenseOk = true;

                    //    }
                    //}
                    //else
                    //{

                    //    ConfigObject.IsLicenseOk = false; return;
                    //}
                }
            }
            catch (Exception)
            {
                ConfigObject.IsLicenseOk = false; return;
            }
        }
    }
}
