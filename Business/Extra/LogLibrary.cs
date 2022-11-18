using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Sentry;

namespace ItCommerce.Api.Net.Logger
{
    public class LogLibrary
    {
        //private static string _source = "Application";
        //private static string _logEntry = "Application";
        //private static string _logType = "DataBase";

        /// <summary>
        /// log un message
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        /// <param name="entryType"></param>
        private static void LogEvent(string source, string message, string type)
        {
            //EventLogEntryType x =  
            try
            {
                //todo: ecrire dans un fichier texte tout simplement

                string logText = string.Format("Log du {0}: {1}/ {2}", DateTime.Now.ToLongTimeString(),
                                           source, message);

                var logPath = System.IO.Path.GetTempFileName();
                var logFile = System.IO.File.Create(logPath);
                var logWriter = new System.IO.StreamWriter(logFile);
                SentrySdk.CaptureMessage(logText);
                logWriter.WriteLine(logText);
                logWriter.Dispose();
                
            }
            catch (Exception ex)
            {
                SentrySdk.CaptureException(ex);
            }
        }

        /// <summary>
        /// enregistre une erreur
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        public static void LogError(string source, string message)
        {
            LogEvent(source, message, "Error");
        }

        /// <summary>
        /// enregistre un warning
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        public static void LogWarning(string source, string message)
        {
            LogEvent(source, message, "Warning");
        }

        /// <summary>
        /// enregistre une information
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        public static void LogInformation(string source, string message)
        {
            LogEvent(source, message, "Information");
        }
    }
}
