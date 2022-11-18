using ItCommerce.Business.Entities;
using ItCommerce.Business.Extra;
using ItCommerce.DTO.DbMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItCommerce.Business.Actions
{
    public class OfficeStats : IOffice
    {
        public static GlobalReport GetCurrentReport(Profil obj)
        {
            DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            DateTime endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);

            GlobalReport result = GlobalReport.Create(DtoStats.GetReport(startDate, endDate, obj.id));
            return result;
        }

        public static GlobalReport GetPeriodGlobalReport(PeriodParam obj)
        {
            obj.startDate = obj.startDate.AddHours(1); obj.endDate = obj.endDate.AddHours(1);
            GlobalReport result = GlobalReport.Create(DtoStats.GetReport(obj.startDate, obj.endDate, obj.agent.id));
            return result;
        }//fin GetPeriodGlobalReport

        public static GlobalReport GetProductPeriodGlobalReport(PeriodParam obj)
        {
            obj.startDate = obj.startDate.AddHours(1); obj.endDate = obj.endDate.AddHours(1);
            obj.startDate = new DateTime(obj.startDate.Year, obj.startDate.Month, obj.startDate.Day, 0, 0, 0);
            GlobalReport result = GlobalReport.Create(DtoStats.GetReportByProduct(obj.startDate, obj.endDate, obj.agent.id, obj.product.id));
            return result;
        } //fin GetProductPeriodGlobalReport

        public static List<ProductInStock> GetMostSoldProdByQuantityReport(PeriodParam obj)
        {
            obj.startDate = obj.startDate.AddHours(1); obj.endDate = obj.endDate.AddHours(1);
            obj.startDate = new DateTime(obj.startDate.Year, obj.startDate.Month, obj.startDate.Day, 0, 0, 0);
            List<ProductInStock> result = ProductInStock.CreateFromList(DtoStats.GetMostSoldProdByQuantityReport(obj.startDate, obj.endDate, obj.agent.id));
            return result;
        }//fin GetMostSoldProdByQuantityReport

        public static List<ProductInStock> GetMostSoldProdByCaReport(PeriodParam obj)
        {
            obj.startDate = obj.startDate.AddHours(1); obj.endDate = obj.endDate.AddHours(1);
            obj.startDate = new DateTime(obj.startDate.Year, obj.startDate.Month, obj.startDate.Day, 0, 0, 0);
            List<ProductInStock> result = ProductInStock.CreateFromList(DtoStats.GetMostSoldProdByCaReport(obj.startDate, obj.endDate, obj.agent.id));
            return result;
        }//fin GetMostSoldProdByCaReport

        public static List<ProductInStock> GetMostSoldProdByProfitReport(PeriodParam obj)
        {
            obj.startDate = obj.startDate.AddHours(1); obj.endDate = obj.endDate.AddHours(1);
            obj.startDate = new DateTime(obj.startDate.Year, obj.startDate.Month, obj.startDate.Day, 0, 0, 0);
            List<ProductInStock> result = ProductInStock.CreateFromList(DtoStats.GetMostSoldProdByProfitReport(obj.startDate, obj.endDate, obj.agent.id));
            return result;
        }//fin GetMostSoldProdByProfitReport


        public static List<UserReport> GetSalesReportBySellers(PeriodParam obj)
        {
            obj.startDate = obj.startDate.AddHours(1); obj.endDate = obj.endDate.AddHours(1);
            obj.startDate = new DateTime(obj.startDate.Year, obj.startDate.Month, obj.startDate.Day, 0, 0, 0);
            List<UserReport> result = UserReport.CreateFromList(DtoStats.GetTotalSalesByProfil(obj.startDate, obj.endDate, obj.agent.id));
            return result;
        }//fin GetSalesReportBySeller

        public static List<UserReport> GetSalesReportByCustomers(PeriodParam obj)
        {
            obj.startDate = obj.startDate.AddHours(1); obj.endDate = obj.endDate.AddHours(1);
            obj.startDate = new DateTime(obj.startDate.Year, obj.startDate.Month, obj.startDate.Day, 0, 0, 0);
            List<UserReport> result = UserReport.CreateFromList(DtoStats.GetTotalSalesByCustomer(obj.startDate, obj.endDate, obj.agent.id));
            return result;
        }//fin GetSalesReportByCustomer
    }
}
