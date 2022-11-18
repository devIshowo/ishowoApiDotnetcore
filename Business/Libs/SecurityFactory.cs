using API.Models;
using Castle.Core.Configuration;
using ItCommerce.Business.Entities;
using ItCommerce.DTO.ModelDesign;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Utils.IwajuTech.Business.Factories
{
    public class SecurityFactory 
    {
        private readonly IConfiguration _configuration;
        public readonly static  IResponseCookies _responseCookies;

        public SecurityFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }
            
        /// <summary>
        /// genere un mot de passe de longueur specifiee
        /// </summary>
        /// <returns></returns>
        public static string getNewPassword(int mdpLength)
        {
            string choixPossibles = "0&12;34_56ç7}89aze/rt(yuio@pqsdfg)hùjk{lmwx!cvbn#AZ-ERT]YUI?OPàQS=DFGH[JKL$MW:XCVèBN";

            string choixLettre = "";

            //mdpLength caracteres
            string motPasse = "";
            for (int i = 0; i < mdpLength; i++)
            {
                Random r = new Random();
                int x = mdpLength * r.Next(138);
                Thread.Sleep(20 * r.Next(108));

                int startIndex = new Random(x).Next(choixPossibles.Length - 1);
                choixLettre = choixPossibles.Substring(startIndex, 1);
                motPasse += string.Format("{0}", choixLettre);
            }
            return motPasse;
        }//

        /// <summary>
        /// genere un mot de passe de longueur specifiee
        /// </summary>
        /// <returns></returns>
        public static string getNewLogin(string surName, string lastName)
        {
            //suffixe
            int randomParam = 15;
            Random r = new Random();
            int randomResult = randomParam * r.Next(138);

            //login
            string result = string.Format("{0}{1}{2}", surName.Substring(0, 1), lastName, randomResult);

            return result;
        }//

        /// <summary>
        /// crypte un mot de passe ou toute chaine de caracteres a securiser
        /// </summary>
        /// <returns></returns>
        public static string cryptAnyWord(string password)
        {
            string tS = Hash.getPSalt("d5$g3çf3#2fàv*0=");
            byte[] tSInString = Convert.FromBase64String(tS);
            string hashedPassword = Convert.ToBase64String(Hash.HashPasswordWithSalt(Encoding.UTF8.GetBytes(password), 
                tSInString));
            return hashedPassword;
        }//

        /// <summary>
        /// crypte un mot de passe ou toute chaine de caracteres a securiser
        /// </summary>
        /// <returns></returns>
        public static string cryptAnyWord(string password, string mySalt, string myAskerParam)
        {
            Hash.setPMVolatileSalt(mySalt);
            string tS = Hash.getPMVolatileSalt(myAskerParam);
            byte[] tSInString = Convert.FromBase64String(tS);
            string hashedPassword = Convert.ToBase64String(Hash.HashPasswordWithSalt(Encoding.UTF8.GetBytes(password),
                tSInString));
            return hashedPassword;
        }//

        /// <summary>
        /// crypte un mot de passe ou toute chaine de caracteres a securiser
        /// </summary>
        /// <returns></returns>
        public static string decryptAnyWord(string password, string mySalt, string myAskerParam)
        {
            Hash.setPMVolatileSalt(mySalt);
            string tS = Hash.getPMVolatileSalt(myAskerParam);
            byte[] tSInString = Convert.FromBase64String(tS);
            string hashedPassword = Convert.ToBase64String(Hash.HashPasswordWithSalt(Encoding.UTF8.GetBytes(password),
                tSInString));
            return hashedPassword;
        }//

        public static string CreateToken(string obj)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, obj)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("Le test de l'usage ddu token"));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                        claims: claims,
                        expires: DateTime.Now.AddHours(1),
                        signingCredentials: cred);
                
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public static RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddHours(1),
                Created = DateTime.Now
            };

            return refreshToken;
        }

        public static void SetRefreshToken(RefreshToken newRefreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires,
            };
            _responseCookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);
                
        }
    }
}

    