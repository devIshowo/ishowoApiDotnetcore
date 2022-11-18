using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ItCommerce.Business.Extra
{
    public class FileObject
    {
        public string filename { get; set; }
        public bool status { get; set; }

        public FileObject(string _file)
        {
            filename = _file; status = true;
        }

        ///// <summary>
        ///// write file content
        ///// </summary>
        ///// <param name="content"></param>
        ///// <param name="filePath"></param>
        ///// <returns></returns>
        //public static bool WriteContent(string content, string filePath)
        //{
        //    try
        //    {
        //        List<byte[]> keys = RainDal.GenerateAlgorithmInputs(Secu.AskerSalt);
        //        string cryptedValue = RainDal.EncryptString(content, keys[0], keys[1]);

        //        File.WriteAllText(filePath, cryptedValue);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}//end WriteContent

        ///// <summary>
        ///// read content
        ///// </summary>
        ///// <param name="content"></param>
        ///// <param name="filePath"></param>
        ///// <returns></returns>
        //public static string ReadContent(string filePath)
        //{
        //    try
        //    {
        //        string str = File.ReadAllText(filePath);
        //        List<byte[]> keys = RainDal.GenerateAlgorithmInputs(Secu.AskerSalt);
        //        string decryptedValue = RainDal.DecryptString(str, keys[0], keys[1]);
        //        return decryptedValue;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}//end ReadContent

    }
}
