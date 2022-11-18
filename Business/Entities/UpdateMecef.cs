using ItCommerce.DTO.ModelDesign;
using System.Collections.Generic;

namespace ItCommerce.Business.Entities
{
    public class UpdateMecef
    {
        public string key_mecef { get; set; }
        public string key_emecef { get; set; }
        public string key_port { get; set; }
        public string key_g_tax { get; set; }
        public string key_aib { get; set; }

        public static UpdateMecef Create(update_mecef _dbo)
        {
            return new UpdateMecef()
            {
                key_mecef = _dbo.mecef,
                key_emecef = _dbo.emecef,
                key_port = _dbo.port,
                key_g_tax = _dbo.g_tax,
                key_aib = _dbo.aib,
            };
        }

        public update_mecef loadDto()
        {
            update_mecef _dto = new update_mecef()
            {
             mecef = this.key_mecef,
             emecef = this.key_emecef,
             port = this.key_port,
             g_tax = this.key_g_tax,
             aib = this.key_aib,
            };
            return _dto;
        }

        

        public static List<UpdateMecef> CreateFromList(List<update_mecef> _dboList)
        {
            List<UpdateMecef> _list = new List<UpdateMecef>();
            foreach (update_mecef item in _dboList)
            {
                UpdateMecef _businessObject = UpdateMecef.Create(item);
                _list.Add(_businessObject);
            }
            return _list;
        }
    }
}
