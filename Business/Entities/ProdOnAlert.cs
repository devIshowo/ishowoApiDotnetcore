using ItCommerce.DTO.SpecClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItCommerce.Business.Entities
{
    public class ProdOnAlert
    {
        public ProductInStock pStock { get; set; }
        public StockLimit pLimit { get; set; }
        public Profil agent { get; set; }

        public static ProdOnAlert Create(prodhseuil _dbo)
        {
            return new ProdOnAlert()
            {
                pStock = (_dbo.stock_actuel != null) ? ProductInStock.Create(_dbo.stock_actuel) : null,
                pLimit = (_dbo.seuil != null) ? StockLimit.Create(_dbo.seuil) : null,
            };
        }

        public static List<ProdOnAlert> CreateFromList(List<prodhseuil> _dboList)
        {
            List<ProdOnAlert> _list = new List<ProdOnAlert>();
            foreach (prodhseuil item in _dboList)
            {
                ProdOnAlert _businessObject = ProdOnAlert.Create(item);
                _list.Add(_businessObject);
            }
            return _list;
        }

    }
}
