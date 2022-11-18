using ItCommerce.DTO.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItCommerce.DTO.ModelDesign;

namespace ItCommerce.Business.Actions
{
    public class IOffice
    {
        //connection string
        private static string _connString = "";
        public static string ConnString {
            get {
                return _connString;
            }

            set {
                _connString = value;
                ItCommerceFactory.ConnString = value;
            }
        }

        //connection string
        private static IT_COMMERCEEntities _context = null;
        public static IT_COMMERCEEntities Context
        {
            get
            {
                return _context;
            }

            set
            {
                _context = value;
            }
        }

        private static string _dbInstance = "";
        public static string DbInstance
        {
            get
            {
                return _dbInstance;
            }

            set
            {
                _dbInstance = value;
                ItCommerceFactory.DbInstance = value;
            }
        }


       

    }
}
