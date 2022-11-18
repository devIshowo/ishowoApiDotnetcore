using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ItCommerce.Api.Net.Extra
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {

        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            context.Result = new JsonResult(exception.Message);

            //var exception = context.Exception;
            context.Result = new ContentResult
            {
                Content = $"Erreur: {exception.Message}",
                ContentType = "text/plain",
                StatusCode = (int?)HttpStatusCode.BadRequest,
            };
        }

    }
}
