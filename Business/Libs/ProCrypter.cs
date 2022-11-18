using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Security.Cryptography;
using System.IO;

namespace Encrypter.Lib
{
    public class ProCrypter
    {
        private static int currentBitStrength = 2048;

        /// <summary>
        /// enrypt with text and key
        /// </summary>
        /// <param name="inputText"></param>
        /// <param name="_publicKeyFile"></param>
        /// <returns></returns>
        public static string DoEncrypt(string inputText, string _publicKeyFile)
        {
            string filePath = _publicKeyFile; string fileString = null;
            if (File.Exists(filePath))
            {
                StreamReader streamReader = new StreamReader(filePath, true);
                fileString = streamReader.ReadToEnd();
                streamReader.Close();
            }

            if (fileString != null)
            {
                string bitStrengthString = fileString.Substring(0, fileString.IndexOf("</BitStrength>") + 14);
                fileString = fileString.Replace(bitStrengthString, "");
                int bitStrength = Convert.ToInt32(bitStrengthString.Replace("<BitStrength>", "").Replace("</BitStrength>", ""));

                if (fileString != null)
                {
                    try
                    {
                        string encrypted = encryptString(inputText, bitStrength, fileString);

                        return encrypted;
                    }
                    catch (CryptographicException CEx)
                    { throw new Exception("ERROR: \nOne of the following has occured.\nThe cryptographic service provider cannot be acquired.\nThe length of the text being encrypted is greater than the maximum allowed length.\nThe OAEP padding is not supported on this computer.\n" + "Exact error: " + CEx.Message); }
                    catch (Exception Ex)
                    { throw new Exception("ERROR: \n" + Ex.Message); }
                }
            }
            else { throw new Exception("ERROR: You Can Not Encrypt A NULL Value!!!"); }
            return "";
        }//end mainecrypt

        /// <summary>
        /// decrypt with text and key file
        /// </summary>
        /// <param name="inputText"></param>
        /// <param name="_privateKeyFile"></param>
        /// <returns></returns>
        public static string DoDecrypt(string inputText, string _privateKeyFile)
        {
            string filePath = _privateKeyFile; string fileString = null;
            if (File.Exists(filePath))
            {
                StreamReader streamReader = new StreamReader(filePath, true);
                fileString = streamReader.ReadToEnd();
                streamReader.Close();
            }

            if (fileString != null)
            {
                string bitStrengthString = fileString.Substring(0, fileString.IndexOf("</BitStrength>") + 14);
                fileString = fileString.Replace(bitStrengthString, "");
                int bitStrength = Convert.ToInt32(bitStrengthString.Replace("<BitStrength>", "").Replace("</BitStrength>", ""));

                if (fileString != null)
                {
                    try
                    {
                        string encrypted = decryptString(inputText, bitStrength, fileString);
                        return encrypted;
                    }
                    catch (CryptographicException CEx)
                    { throw new Exception("ERROR: \nOne of the following has occured.\nThe cryptographic service provider cannot be acquired.\nThe length of the text being encrypted is greater than the maximum allowed length.\nThe OAEP padding is not supported on this computer.\n" + "Exact error: " + CEx.Message); }
                    catch (Exception Ex)
                    { throw new Exception("ERROR: \n" + Ex.Message); }
                }
            }
            else { throw new Exception("ERROR: You Can Not Encrypt A NULL Value!!!"); }
            return "";
        }

        /// <summary>
        /// generate keys
        /// </summary>
        public static void GenerateKeyPair(string publicKey, string privateKey)
        {
            RSACryptoServiceProvider RSAProvider = new RSACryptoServiceProvider(currentBitStrength);
            string publicAndPrivateKeys = "<BitStrength>" + currentBitStrength.ToString() + "</BitStrength>" + RSAProvider.ToXmlString(true);
            string justPublicKey = "<BitStrength>" + currentBitStrength.ToString() + "</BitStrength>" + RSAProvider.ToXmlString(false);
            if (saveFile(privateKey, publicAndPrivateKeys))
            { while (!saveFile(publicKey, justPublicKey)) {; } }
        }

        /// <summary>
        /// encrypt string
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="dwKeySize"></param>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        private static string encryptString(string inputString, int dwKeySize, string xmlString)
        {
            // TODO: Add Proper Exception Handlers
            RSACryptoServiceProvider rsaCryptoServiceProvider = new RSACryptoServiceProvider(dwKeySize);
            rsaCryptoServiceProvider.FromXmlString(xmlString);
            int keySize = dwKeySize / 8;
            byte[] bytes = Encoding.UTF32.GetBytes(inputString);
            // The hash function in use by the .NET RSACryptoServiceProvider here is SHA1
            // int maxLength = ( keySize ) - 2 - ( 2 * SHA1.Create().ComputeHash( rawBytes ).Length );
            int maxLength = keySize - 42;
            int dataLength = bytes.Length;
            int iterations = dataLength / maxLength;
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i <= iterations; i++)
            {
                byte[] tempBytes = new byte[(dataLength - maxLength * i > maxLength) ? maxLength : dataLength - maxLength * i];
                Buffer.BlockCopy(bytes, maxLength * i, tempBytes, 0, tempBytes.Length);
                byte[] encryptedBytes = rsaCryptoServiceProvider.Encrypt(tempBytes, true);
                // Be aware the RSACryptoServiceProvider reverses the order of encrypted bytes after encryption and before decryption.
                // If you do not require compatibility with Microsoft Cryptographic API (CAPI) and/or other vendors.
                // Comment out the next line and the corresponding one in the DecryptString function.
                Array.Reverse(encryptedBytes);
                // Why convert to base 64?
                // Because it is the largest power-of-two base printable using only ASCII characters
                stringBuilder.Append(Convert.ToBase64String(encryptedBytes));
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// decrypt string
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="dwKeySize"></param>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        private static string decryptString(string inputString, int dwKeySize, string xmlString)
        {
            // TODO: Add Proper Exception Handlers
            RSACryptoServiceProvider rsaCryptoServiceProvider = new RSACryptoServiceProvider(dwKeySize);
            rsaCryptoServiceProvider.FromXmlString(xmlString);
            int base64BlockSize = ((dwKeySize / 8) % 3 != 0) ? (((dwKeySize / 8) / 3) * 4) + 4 : ((dwKeySize / 8) / 3) * 4;
            int iterations = inputString.Length / base64BlockSize;
            ArrayList arrayList = new ArrayList();
            for (int i = 0; i < iterations; i++)
            {
                byte[] encryptedBytes = Convert.FromBase64String(inputString.Substring(base64BlockSize * i, base64BlockSize));
                // Be aware the RSACryptoServiceProvider reverses the order of encrypted bytes after encryption and before decryption.
                // If you do not require compatibility with Microsoft Cryptographic API (CAPI) and/or other vendors.
                // Comment out the next line and the corresponding one in the EncryptString function.
                Array.Reverse(encryptedBytes);
                arrayList.AddRange(rsaCryptoServiceProvider.Decrypt(encryptedBytes, true));
            }
            return Encoding.UTF32.GetString(arrayList.ToArray(Type.GetType("System.Byte")) as byte[]);
        }

        /// <summary>
        /// save key files
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="outputString"></param>
        /// <returns></returns>
        private static bool saveFile(string filePath, string outputString)
        {
            try
            {
                StreamWriter streamWriter = new StreamWriter(filePath, false);
                if (outputString != null)
                { streamWriter.Write(outputString); }
                streamWriter.Close();
                return true;
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                return false;
            }
        }//end saveFile
    }


}
