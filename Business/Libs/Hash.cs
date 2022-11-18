using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Utils.IwajuTech.Business.Factories
{
    public class Hash
    {

        internal static byte[] HashPasswordWithSalt(byte[] toBeHashed, byte[] salt) 
        { 
            using (var sha256 = SHA256.Create()) { 
                return sha256.ComputeHash(Combine(toBeHashed, salt));
            } 
        } 
        
        private static byte[] Combine(byte[] first, byte[] second) { 
            var ret = new byte[first.Length + second.Length]; 
            Buffer.BlockCopy(first, 0, ret, 0, first.Length); 
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length); 
            return ret; 
        }

        internal static byte[] GenerateSalt()
        { 
            const int SALT_LENGTH = 32; 
            using (var randomNumberGenerator = new RNGCryptoServiceProvider()) {
                var randomNumber = new byte[SALT_LENGTH];
                randomNumberGenerator.GetBytes(randomNumber); 
                return randomNumber; 
            }
        }

        //salt used for passwords
        private static string privateSalt = "9V+YQiQiwab1/DC/evNoEfQ5h8qnZY7omF/8yJ0ASDM=";
        /// <summary>
        /// get the private salt
        /// </summary>
        /// <returns></returns>
        internal static string getPSalt(string askerCode)
        {
            if (askerCode.Equals("d5$g3çf3#2fàv*0=")) { return privateSalt; }
            else
            {
                return "";
            }
        }//

        internal static void setPMVolatileSalt(string askerSalt)
        {
            privateSalt = askerSalt;
        }//

        internal static string getPMVolatileSalt(string askerParam)
        {
            if (askerParam.Equals("48kh$h543çfh353#2fàv*0=")) { return privateSalt; }
            else
            {
                return "";
            }
        }//

    }
}
