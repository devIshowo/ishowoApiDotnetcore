using System;
using System.Globalization;
using System.Linq;

namespace ItCommerce.Business.Extra
{
    public class Trame
    {
        public static string Command(string dataHex, int seq, string cmd, string data)
        {
            string SOH = "01";
            string LEN = "";
            string SEQ = seq.ToString();
            string CMD = cmd;
            string DATA = data;
            string AMB = "05";
            string BCC = "";
            string ETX = "03";

            // Convertir deciLen en Hex
            int deciLen = string.IsNullOrEmpty(dataHex) ? 4 : dataHex.Split('-').Count() + 4;

            // Calcul de la LEN
            int bi2 = int.Parse("20", NumberStyles.HexNumber);
            int sum = deciLen + bi2;
            LEN = sum.ToString("x");

            int deciBcc = 0;

            // Somme des valeurs des octets de LEN à AMB
            if (!string.IsNullOrEmpty(dataHex))
            {
                for (int i = 0; i < dataHex.Split('-').Count(); i++)
                {
                    var x = dataHex.Split('-')[i];
                    deciBcc += HexToInt(x);
                }
            }

            deciBcc += HexToInt(LEN) + HexToInt(SEQ) + HexToInt(CMD) + HexToInt(AMB);

            var hexBcc = deciBcc.ToString("x").PadLeft(4, '0');

            // Calcul de la BCC
            foreach (char item in hexBcc)
            {
                BCC += "3" + item;
            }

            string result = SOH + LEN + SEQ + CMD + DATA + AMB + BCC + ETX;

            return result;
        }

        //public static string DecimalToHexadecimal(int dec)
        //{
        //    if (dec < 1) return "0";

        //    int hex = dec;
        //    string hexStr = string.Empty;

        //    while (dec > 0)
        //    {
        //        hex = dec % 16;

        //        if (hex < 10)
        //            hexStr = hexStr.Insert(0, Convert.ToChar(hex + 48).ToString());
        //        else
        //            hexStr = hexStr.Insert(0, Convert.ToChar(hex + 55).ToString());

        //        dec /= 16;
        //    }

        //    return hexStr;
        //}


        public static int HexToInt(string hexValue)
        {
            int number = int.Parse(hexValue, NumberStyles.HexNumber);

            return number;
        }

        public static string HexToBinary(string hexValue)
        {
            int number = HexToInt(hexValue);

            byte[] bytes = BitConverter.GetBytes(number);

            string binaryString = string.Empty;
            foreach (byte singleByte in bytes)
            {
                binaryString += Convert.ToString(singleByte, 2);
            }

            return binaryString;
        }
    }
}
