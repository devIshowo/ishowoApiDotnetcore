using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItCommerce.Business.Entities;
using ItCommerce.DTO.DbMethods;
using ItCommerce.DTO.ModelDesign;
//using Utils.IwajuTech.Business.Factories;
using ItCommerce.Business.Extra;
using System.Net.NetworkInformation;
using System.IO;
using System.Data.SqlClient;
using ItCommerce.DTO.Factory;
using Encrypter.Lib;
using NewCrypter;
using Utils.IwajuTech.Business.Factories;
using Castle.Core.Configuration;
using static Org.BouncyCastle.Math.EC.ECCurve;
using System.Security.Cryptography;
using API.Models;
using Microsoft.AspNetCore.Http;
using System.Drawing;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ItCommerce.Business.Actions
{
    public class OfficeAuth : IOffice
    {
        //

        private readonly IResponseCookies _responseCookies;

        public OfficeAuth( IResponseCookies responseCookies)
        {
            _responseCookies = responseCookies;
        }

        #region profil

        /// <summary>
        /// liste profil
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static List<Profil> getListProfils(int id)
        {
            List<Profil> _listBsList = Profil.CreateFromList(DtoAuth.loadProfilsList(id));
            return _listBsList;
        }//fin getListProfils

        public static Profil createProfil(Profil item)
        {
            //crypt password
            string cryptedPwd = SecurityFactory.cryptAnyWord(item.code, LocalSecu.AskerSalt, LocalSecu.AskerParam);
            item.code = cryptedPwd;

            //item.code = 
            profil result = DtoAuth.createProfile(item.loadDto(), item.agent.id);
            return Profil.Create(result);
        }//fin createProfil

        public static Profil updateProfil(Profil item)
        {
            //crypt password
            string cryptedPwd = SecurityFactory.cryptAnyWord(item.code, LocalSecu.AskerSalt, LocalSecu.AskerParam);
            item.code = cryptedPwd;

            profil result = DtoAuth.updateProfil(item.loadDto());
            return Profil.Create(result);
        }//fin updateProfil

        public static Profil updateCompanyLogo(string login, string logoPath)
        {
            profil result = DtoAuth.updateCompanyLogo(login, logoPath);
            return Profil.Create(result);
        }//fin updateCompanyLogo

        public static Profil updateUserProfil(Profil item)
        {
            //crypt password
            string cryptedPwd = SecurityFactory.cryptAnyWord(item.code, LocalSecu.AskerSalt, LocalSecu.AskerParam);
            item.code = cryptedPwd;

            profil result = DtoAuth.updateProfil(item.loadDto());
            return Profil.Create(result);
        }//fin updateUserProfil

        public static Company updateCompanyProfil(Company item)
        {
            entreprise result = DtoAuth.updateCompanyProfil(item.loadDto());
            return Company.Create(result);
        }//fin updateCompanyProfil



        public static Profil deleteProfil(int id)
        {
            profil result = DtoAuth.deleteProfil(id);
            return new Profil();
        }//fin deleteProfil


        #endregion profil


        #region agents
        /// <summary>
        /// liste agents
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static List<User> getListAgents(int idProfil)
        {
            List<User> _listBsList = User.CreateFromList(DtoAuth.loadAgentsList(idProfil));
            return _listBsList;
        }//fin getListAgents

        public static User createAgent(User item)
        {
            utilisateur result = DtoAuth.createAgent(item.loadDto(), item.agent.id);
            return User.Create(result);
        }//fin createAgent

        public static User updateAgent(User item)
        {
            utilisateur result = DtoAuth.updateAgent(item.loadDto());
            return User.Create(result);
        }//fin updateAgent

        public static User deleteAgent(int id)
        {
            utilisateur result = DtoAuth.deleteAgent(id);
            return new User();
        }//fin deleteAgent

        #endregion agents



        #region licence concern

        public static Profil signin(Profil item)
        {
            //crypt password     
            string cryptedPwd = SecurityFactory.cryptAnyWord(item.code, LocalSecu.AskerSalt, LocalSecu.AskerParam);
            item.code = cryptedPwd;

            profil result = DtoAuth.signin(item.loadDto());
            
            SecurityFactory.CreateToken(result.login);
            var refreshToken = SecurityFactory.GenerateRefreshToken();
            //SecurityFactory.SetRefreshToken(refreshToken);

            return Profil.Create(result);
        }//fin signin



        public static utilisateur verifiedat(string key)
        {

            utilisateur result = DtoAuth.verifiedUser(key);
           
            return result;

        }//fin signin


        public static List<User> getListCustomers(int idProfil)
        {
            List<User> _listBsList = User.CreateFromList(DtoAuth.loadCustomersList(idProfil));
            return _listBsList;
        }//fin getListAgents

        public static bool isRightLicenseKey(string keyCode)
        {
            string finalCode = string.Format("{0}{1}{2}",
                  keyCode.Substring(0, (keyCode.Length / 2) - 3),
                  getMacAddress(),
                  keyCode.Substring((keyCode.Length / 2) - 2)
                  );
            string cryptedKey = SecurityFactory.cryptAnyWord(finalCode, LocalSecu.AskerSalt, LocalSecu.AskerParam);
            bool result = cryptedKey.Equals(LocalSecu.InstallKey);
            return result;
        }//fin isRightLicenseKey

        public static utilisateur searchUserWithEmail(string email)
        {
            utilisateur result = DtoAuth.searchUserWithEmail(email);
            return result;
        }

        public static string TestSetInstallKey(string keyCode)
        {
            string finalCode = string.Format("{0}{1}{2}",
                keyCode.Substring(0, (keyCode.Length / 2) - 3),
                getMacAddress(),
                keyCode.Substring((keyCode.Length / 2) - 2)
                );
            string cryptedKey = SecurityFactory.cryptAnyWord(finalCode, LocalSecu.AskerSalt, LocalSecu.AskerParam);
            return cryptedKey;
        }//fin isRightLicenseKey

        /// <summary>
        /// Gets the MAC address of the current PC.
        /// </summary>
        /// <returns></returns>
        public static string getMacAddress()
        {
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                // Only consider Ethernet network interfaces
                if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet) // &&nic.OperationalStatus == OperationalStatus.Up
                {
                    return nic.GetPhysicalAddress().ToString();
                }
            }
            return null;
        }//fin getmacaddress


        /// <summary>
        /// get current licence data for a company
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Licence getLastLicence()
        {
            Licence _companyLicence = Licence.Create(DtoAuth.loadLastLicence());
            
            //string decryptedKey = ProCrypter.DoDecrypt(_companyLicence.key, Path.Combine(LocalSecu.AppConfigPath, LocalSecu.PrivateKeyPath));
          
            //Licence finalLicence = Licence.Extract(decryptedKey);
            //finalLicence.code = _companyLicence.code; finalLicence.admin = _companyLicence.admin;
            //finalLicence.company = _companyLicence.company; 

            return _companyLicence;
        }//fin getLastLicence

        /// <summary>
        /// get the licence for a profile
        /// </summary>
        /// <param name="_licenceItem"></param>
        /// <returns></returns>
        public static Licence getProfileLicence(licence _licenceItem)
        {
            Licence _companyLicence = Licence.Create(_licenceItem);
            
            string decryptedKey = ProCrypter.DoDecrypt(_companyLicence.key, Path.Combine(LocalSecu.AppConfigPath, LocalSecu.PublicKeyPath));

            Licence finalLicence = Licence.Extract(decryptedKey);
            finalLicence.code = _companyLicence.code; finalLicence.admin = _companyLicence.admin;
            finalLicence.company = _companyLicence.company; finalLicence.key = string.Empty;

            return finalLicence;
        }//fin getProfileLicence


        /// <summary>
        /// change password
        /// </summary>
        private static void changePassword(string newPassword)
        {
            //try
            //{
            //    string connString =
            //           string.Format(@"Data Source = {0}; 
            //        Initial Catalog = IT_COMMERCE; UID = sa; PWD = sa", LocalSecu.DbInstanceName);

            //    SqlConnection connect = new SqlConnection(connString);
            //    connect.Open();
            //    string sqlQuery = string.Format("ALTER LOGIN sa WITH PASSWORD ='{0}'", newPassword);
            //    SqlCommand cmd = new SqlCommand(sqlQuery, connect);

            //    cmd.Connection = connect;
            //    cmd.ExecuteNonQuery();
            //    SqlDataReader reader = null;
            //    reader = cmd.ExecuteReader();
            //    while (reader.Read()) { }
            //    connect.Close();
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }//fin changePassword


        /// <summary>
        /// save a licence got from server
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static Licence saveLicence(Licence item)
        {
            try
            {
                //load connstring first before changing it
                ItCommerceFactory.GetInitEntity();

                //crypt parts
                string passWd = SecurityFactory.cryptAnyWord("admin", LocalSecu.AskerSalt, LocalSecu.AskerParam);

                //generate login
                char[] letter = ("_&atyzdhsvisnfzi43567890"+item.admin.firstname).ToArray();
                Random rdn = new Random();
                string random = null;
                 for (int i = 0; i < 10; i++)
                {
                    random += letter[rdn.Next(8)] ;
                }
                string username = string.Join("", random);

                item.isActive = true;

                //make a copy
                Licence copyLicence = item;
                copyLicence.activationDate = item.activationDate; copyLicence.expiryDate = item.expiryDate;

                //stringify our way
                string codeLicence = item.ToString();

                //item.code = codeLicence;
                //encrypt the str

                var path = LocalSecu.AppConfigPath; //if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                string cryptedKey = ""; // ProCrypter.DoEncrypt(codeLicence, Path.Combine(path, LocalSecu.PublicKeyPath)); //RainDal.EncryptString(codeLicence, keys[0], keys[1]);

                item.key = cryptedKey; //item.activationDate = DateTime.Now; item.expiryDate = DateTime.Now; item.isActive = true;

                //save to file
                // bool status = LocalFileObject.WriteContent(codeLicence, Path.Combine(path, LocalSecu.PrefPath));
                //generate token
                var token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));

                //save to db
                licence result = DtoAuth.saveLicence(item.loadDto(), item.company.loadDto(), item.admin.loadDto(), passWd,username,token);
                copyLicence.id = result.id;
                copyLicence.key = "";
                
                //change passwordn
                string dbCode = item.act;
                // changePassword(dbCode);

                //save conn string to pref
                string passwordProd = dbCode;
                string connStringProd = string.Format(LocalSecu.DbConnString, dbCode);
                //bool statusPwd = LocalFileObject.WriteContent(connStringProd, Path.Combine(path, LocalSecu.PaPath));

                //result
                copyLicence.act = "";

                //save settings
                string userName = Manager.encodeVar(Beautify.encodeVar(dbCode));
                // File.WriteAllText(Path.Combine(LocalSecu.AppConfigPath, LocalSecu.RandomFileName), userName);
                
                string email = item.admin.loadDto().email;
                //NetworkFactory.SendEmail(email, "Vérification de votre compte.",message);
                //try {
                //    NetworkFactory.SendRegisterEmail(email, "Vérification de votre compte.", message, username, token);
                //}
                //catch (Exception ex)
                //{
                //}
                
                return copyLicence;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }//fin saveLicence

        /// <summary>
        /// update a licence got from server
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static Licence updateLicence(Licence item)
        {
            try
            {
                //make a copy
                Licence copyLicence = item;
                item.isActive = true;
                copyLicence.activationDate = item.activationDate; copyLicence.expiryDate = item.expiryDate;

                //stringify our way
                string codeLicence = item.ToString();
                                                                                        //List<byte[]> keys = RainDal.GenerateAlgorithmInputs(LocalSecu.AskerSalt);//string cryptedKey = RainDal.EncryptString(codeLicence, keys[0], keys[1]);
                // string cryptedKey = ProCrypter.DoEncrypt(codeLicence, Path.Combine(LocalSecu.AppConfigPath, LocalSecu.PublicKeyPath));
                                                                            
                item.key = ""; // cryptedKey;                                                      //item.activationDate = DateTime.Now; item.expiryDate = DateTime.Now; 

                //save to file
                // var path = LocalSecu.AppConfigPath; if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                // bool status = LocalFileObject.WriteContent(codeLicence, Path.Combine(path, LocalSecu.PrefPath));

                licence result = DtoAuth.updateLicence(item.loadDto());
                copyLicence.id = result.id;
                copyLicence.key = "";

                //result
                return copyLicence;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }//fin updateLicence

        public static string getDefaultConnString()
        {
            string connStringProd = string.Format(LocalSecu.DbConnString, "sa");
            //List<byte[]> keys = RainDal.GenerateAlgorithmInputs(LocalSecu.AskerSalt);
            string cryptedValue = connStringProd; // ProCrypter.DoEncrypt(connStringProd, LocalSecu.PublicKeyPath);
                                        //string cryptedValue = RainDal.EncryptString(connStringProd, keys[0], keys[1]);
            return cryptedValue;
        }//fin getDefaultConnString

        public static string getOrigConnString()
        {
            //return IOffice.ConnString;
            return "test";
        }//fin getDefaultConnString

        /// <summary>
        /// get main user for which this code is ok
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static Profil getActiveProfile(string code)
        {
            profil result = DtoAuth.getActiveProfile(code);
            return Profil.Create(result);
        }


        #endregion

    }
}
