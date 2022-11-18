using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NewCrypter
{
    public class Beautify
    {
        public Beautify()
        {
        }

        private static string GetRandomString(int maxLen)
        {
            string path = Path.GetRandomFileName();
            path = path.Replace(".", ""); // Remove period.
            return path.Substring(0, maxLen);
        }

        public static string encodeVar(string code) {
            Random random = new Random();
            int randomNumber1 = random.Next(1, 10);
            int randomNumber2 = random.Next(1, 10);
            int randomNumber3 = random.Next(1, 10);

            string beauty = string.Format("{0}{1}{2}{3}{4}{5}{6}", GetRandomString(randomNumber1), ":", GetRandomString(randomNumber2), ":", code, ":", GetRandomString(randomNumber3));
            return beauty;
        }

        public static string decodeVar(string code) {
            try
            {
                string partLeft = code.Split(':')[2];
                return partLeft;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    
    }
}
