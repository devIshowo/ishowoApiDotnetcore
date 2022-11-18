using ItCommerce.DTO.ModelDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItCommerce.Business.Entities
{
    public class SaleTarget
    {
        public int id { get; set; }
        public Profil agent { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public int amount_target { get; set; }
        public int amount_obtained { get; set; }
        public bool is_reached { get; set; }

        public static SaleTarget Create(objectif_vente _dbo)
        {
            return new SaleTarget()
            {
                id = _dbo.id,
                agent = (_dbo.profil != null) ? Profil.Create(_dbo.profil) : null,
                start_date = _dbo.date_debut,
                end_date = _dbo.date_fin,
                amount_target = _dbo.montant_objectif,
                amount_obtained = _dbo.montant_atteint,
                is_reached = _dbo.est_atteint,
            };
        }

        public objectif_vente loadDto()
        {
            //objectif_vente
            objectif_vente _dto = new objectif_vente()
            {
                id = this.id,
                date_debut = this.start_date,
                date_fin = this.end_date,
                id_profil = (this.agent != null) ? this.agent.id : 0,
                montant_objectif = this.amount_target,
                montant_atteint = this.amount_obtained,
                est_atteint = this.is_reached,
            };
            return _dto;
        }


        public static List<SaleTarget> CreateFromList(List<objectif_vente> _dboList)
        {
            List<SaleTarget> _list = new List<SaleTarget>();
            foreach (objectif_vente item in _dboList)
            {
                SaleTarget _businessObject = SaleTarget.Create(item);
                _list.Add(_businessObject);
            }
            return _list;
        }


    }
}
