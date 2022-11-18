using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItCommerce.Api.Net.Extra
{
    public class StringObject
    {
        public string name { get; set; }


        /// <summary>
        /// convert exeption to exception messages
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static string ExtractExceptionData(Exception ex)
        {
            if (ex == null) return string.Empty;
            string exceptionMessage = ex.Message;
            string source = ex.Source;
            string targetSite = (ex.TargetSite != null) ? ex.TargetSite.Name : string.Empty;
            string innerExceptionMessage = (ex.InnerException != null) ? ex.InnerException.Message : string.Empty;
            string baseExceptionMessage = (ex.GetBaseException() != null) ? ex.GetBaseException().Message : string.Empty;
            string stackTrace = ex.StackTrace;
            string msg = string.Format("Exception: {0} ... Source: {1}.... TargetSite: {2}.... BaseException: {3}.... InnerException: {4}.... StackTrace: {5}"
                , exceptionMessage, source, targetSite, baseExceptionMessage, innerExceptionMessage, stackTrace);
            return msg;
        }//fin extractExceptionData

    }
}
