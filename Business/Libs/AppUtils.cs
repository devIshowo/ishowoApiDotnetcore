using ItCommerce.Business.Entities;
using ItCommerce.Business.Extra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Business.Libs
{
    public class AppUtils
    {

        /// <summary>
        /// create licence object from query
        /// </summary>
        /// <param name="licenceQuery"></param>
        public static Licence createLicenceFromQuery(PrmLicence licenceQuery)
        {

            //try
            //{
                Licence builtLicence = new Licence();

                //assign
                builtLicence.id = licenceQuery.id;
                builtLicence.company = licenceQuery.company;
                builtLicence.admin = licenceQuery.admin;
                builtLicence.code = licenceQuery.code;
                builtLicence.key = licenceQuery.key;
                builtLicence.isActive = licenceQuery.isActive;

                builtLicence.activationDate = DateTime.Now;

                builtLicence.expiryDate = DateTime.Now.AddDays(14);

                builtLicence.currentDate = DateTime.Now;



                builtLicence.activationCost = 0;
                builtLicence.module = "DEMO";
                builtLicence.act = "";

                return builtLicence;
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            
        }//end createLicenceFromQuery
    }
}
