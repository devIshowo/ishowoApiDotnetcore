using ItCommerce.DTO.ModelDesign;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItCommerce.Business.Extra;
using Microsoft.EntityFrameworkCore; 
using  ItCommerce.Business.Actions;

namespace ItCommerce.DTO.Factory
{
    public class ItCommerceFactory 
    {
        public static string ConnString = null;
        public static string DbInstance = null;
        private static IT_COMMERCEEntities _context = null;


        /* public IT_COMMERCEEntities Create()
        {
            return _context;
        }*/

        public static void SetEntity(IT_COMMERCEEntities _context)
        {
            ItCommerceFactory._context = _context;
        }


        public static IT_COMMERCEEntities Create()
        {
            string connectionString = IOffice.ConnString;
            var optionsBuilder = new DbContextOptionsBuilder<IT_COMMERCEEntities>();
            optionsBuilder.UseNpgsql(connectionString);
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.EnableSensitiveDataLogging();

            // Ensure that the SQLite database and sechema is created!
            var context = new IT_COMMERCEEntities(optionsBuilder.Options);
            context.Database.EnsureCreated();

            return context;
        }

        public static IT_COMMERCEEntities GetEntity()
        {
            /*try
            {
                 var connectionString = ConfigObject.ConnString; 
                //ItCommerceFactory.ConnString = (ItCommerceFactory.ConnString == null) ? LocalFileObject.ReadContent(Path.Combine(LocalSecu.AppConfigPath, LocalSecu.PaPath)) : ItCommerceFactory.ConnString;
                return new IT_COMMERCEEntities(connectionString);
            }
            catch (Exception ex)
            {
                 var connectionString = ConfigObject.ConnString; 
                return null; // new IT_COMMERCEEntities(connectionString);
            }*/


            //IT_COMMERCEEntities myContext = null; // new IT_COMMERCEEntities();
            //myContext = ItCommerceFactory._context;
            return ItCommerceFactory.Create(); // myContext;
        }

        public static IT_COMMERCEEntities GetInitEntity()
        {
            return ItCommerceFactory.Create(); 
        }
    }
}
