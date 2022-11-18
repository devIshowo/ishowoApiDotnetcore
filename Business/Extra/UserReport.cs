using ItCommerce.DTO.SpecClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItCommerce.Business.Extra
{
    public class UserReport
    {
        public double? totalSell { get; set; }
        public double? totalProfit { get; set; }     
        public double? qtySales { get; set; }


        public string userLogin { get; set; }
        public double? pourcentage { get; set; }
        public string userName { get; set; }
        public string agency { get; set; }

        public static UserReport Create(rapport_utilisateur _dbo)
        {
            return new UserReport()
            {
                totalSell = (_dbo.TotalVentes != null) ? _dbo.TotalVentes : 0,
                totalProfit = (_dbo.TotalBenefices != null) ? _dbo.TotalBenefices : 0,
                qtySales = (_dbo.NombreVentes != null) ? _dbo.NombreVentes : 0,
                userLogin = _dbo.login,
                pourcentage = _dbo.pourcentage,
                userName = _dbo.identite,
                agency = _dbo.agence,
            };
        }

        public static List<UserReport> CreateFromList(List<rapport_utilisateur> _dboList)
        {
            List<UserReport> _list = new List<UserReport>();
            foreach (rapport_utilisateur item in _dboList)
            {
                UserReport _businessObject = UserReport.Create(item);
                _list.Add(_businessObject);
            }
            return _list;
        }

    }
}
