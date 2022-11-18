//using Encrypter.Lib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItCommerce.DTO.Factory
{
    public class LocalFileObject
    {
        public string filename { get; set; }
        public bool status { get; set; }

        public LocalFileObject(string _file)
        {
            filename = _file; status = true;
        }

        /// <summary>
        /// write file content
        /// </summary>
        /// <param name="content"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool WriteContent(string content, string filePath)
        {
            try
            {
                //List<byte[]> keys = LocalRainDal.GenerateAlgorithmInputs(LocalSecu.AskerSalt);
                //string cryptedValue = ProCrypter.DoEncrypt(content, Path.Combine(LocalSecu.AppConfigPath, LocalSecu.PublicKeyPath)); //LocalRainDal.EncryptString(content, keys[0], keys[1]);

                File.WriteAllText(filePath, null);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }//end WriteContent



        /// <summary>
        /// read content
        /// </summary>
        /// <param name="content"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string ReadContent(string filePath)
        {
            try
            {
                //string content = File.ReadAllText(filePath);
                ////List<byte[]> keys = LocalRainDal.GenerateAlgorithmInputs(LocalSecu.AskerSalt);
                //string decryptedValue = ProCrypter.DoDecrypt(content, Path.Combine(LocalSecu.AppConfigPath, LocalSecu.PrivateKeyPath));  //LocalRainDal.DecryptString(str, keys[0], keys[1]);
                //return decryptedValue;
                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }//end ReadContent

    }

}
