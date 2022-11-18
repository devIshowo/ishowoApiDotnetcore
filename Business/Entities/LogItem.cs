using ItCommerce.DTO.ModelDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItCommerce.Business.Entities
{
    public class LogItem
    {
        public int id { get; set; }
        public string description { get; set; }
        public string category { get; set; }
        public string author { get; set; }
        public DateTime actionDate { get; set; }

        public log loadDto()
        {
            log _dto = new log()
            {
                id = this.id,
                actor = this.author,
                desc = this.description,
                categorie = this.category,
                date_log = this.actionDate,
            };
            return _dto;
        }

        public static LogItem Create(log _dbo)
        {
            return new LogItem()
            {
                id = _dbo.id,
                author = _dbo.actor,
                description = _dbo.desc,
                category = _dbo.categorie,
                actionDate = (DateTime) _dbo.date_log,
            };
        }

        public static List<LogItem> CreateFromList(List<log> _dboList)
        {
            List<LogItem> _list = new List<LogItem>();
            foreach (log item in _dboList)
            {
                LogItem _businessObject = LogItem.Create(item);
                _list.Add(_businessObject);
            }
            return _list;
        }
    }
}
