using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using ItCommerce.DTO.ModelDesign;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ItCommerce.Api.Net.Extra;
using Microsoft.AspNetCore;
using Serilog;
using System.IO;
using Sentry;

namespace WebApplication5
{
    public class Program
    {
        public static void Main(string[] args)
        {

           
            try
            {
                ConfigureLogger(); 

                var configuration = new ConfigurationBuilder()
               .AddCommandLine(args)
               .Build();

                var host = CreateWebHostBuilder(args, configuration) //original
                                                                     // .UseKestrel(options => {
                                                                     //    options.ThreadCount = 4,
                                                                     //    options.UseUrls("http://localhost:6060")
                                                                     //    options.UseConnectionLogging();
                                                                     //})
                .Build();

                AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
               
                host.Run();

            }
            catch (Exception ex)
            {
                SentrySdk.CaptureException(ex);
            }

           

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
             Host.CreateDefaultBuilder(args)
              .ConfigureWebHostDefaults(webBuilder =>
             {
                 webBuilder.UseStartup<Startup>().UseSerilog();
          

             });
        public static void ConfigureLogger()
        {

            Log.Logger = new LoggerConfiguration()
             
            .WriteTo.File(path: "Logs/ISHOWO" + DateTime.Now.ToString("_dd-MM-yyyy_") + ".txt", outputTemplate: "{NewLine} {Timestamp: dd-MM-yyyy HH-mm-ss} {MachineName} {ThreadId} {Message} {Exception:1} {NewLine}")
            .Enrich.WithThreadId()
            .Enrich.WithMachineName()
            .CreateLogger();

        }
      public static  Serilog.ILogger getLogInstance()
        {
            return Log.Logger;
        }


        public static IWebHostBuilder CreateWebHostBuilder(string[] args, IConfigurationRoot config) =>

            // use this to allow command line parameters in the config

            WebHost.CreateDefaultBuilder(args)
                
              
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseSentry()    
                .UseStartup<Startup>()
                .UseConfiguration(config);
    }


}
