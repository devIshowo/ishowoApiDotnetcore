using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using ItCommerce.Business.Actions;
using Microsoft.AspNetCore.Mvc;
using ItCommerce.Api.Net.Logger;
using ItCommerce.Business.Entities;
using ItCommerce.Business.Extra;
using System.IO;
//using Microsoft.AspNetCore.Hosting.Internal;
using ItCommerce.DTO.Factory;

namespace ItCommerce.Api.Net.Extra
{
    public class UserLicenseCheckerFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            try
            {

                //check licence
                if (ConfigObject.IsLicenseOk == false)
                {

                }
            }
            catch (Exception ex)
            {
                string message = "Vous n'avez pas la licence requise pour utiliser cette application. Veuillez contacter IWAJU TECH si ce probleme persiste."; LogLibrary.LogError("Unauthorized Access", message); actionContext.Result = new ContentResult { Content = $"Erreur: {message}", ContentType = "text/plain", StatusCode = (int?)HttpStatusCode.Unauthorized, };
                LogLibrary.LogError("UserLicenceChecker", message);

                string msg = string.Format("{0}{1}", ex.GetBaseException().Message, ex.StackTrace);
                LogLibrary.LogError("UserLicenceChecker", msg);
                return;
            }

        }
    }
}
