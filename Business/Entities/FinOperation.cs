using ItCommerce.DTO.ModelDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItCommerce.Business.Entities
{
    public class FinOperation
    {
        public int id { get; set; }
        public VirtualAccount account { get; set; }
        public int amount { get; set; }
        public int remaining { get; set; }
        public string proof_number { get; set; }
        public string description { get; set; }
        public DateTime operation_date { get; set; }
        public TypeFinOperation operation_type { get; set; }

        public Profil agent { get; set; }


        public static FinOperation Create(mouvement_compte _dbo)
        {
            return new FinOperation()
            {
                id = _dbo.id,
                account = (_dbo.compte != null) ? VirtualAccount.Create(_dbo.compte) : null,
                amount = (_dbo.credit == 0)? _dbo.debit: _dbo.credit,
                operation_type = (_dbo.credit == 0) ? TypeFinOperation.RETRAIT : TypeFinOperation.DEPOT,
                remaining = _dbo.solde,
                proof_number = _dbo.numero_piece,
                description = _dbo.motif,
                operation_date = _dbo.date_mvt,
                agent = (_dbo.profil != null) ? Profil.Create(_dbo.profil) : null,
            };
        }

        public mouvement_compte loadDto()
        {
            mouvement_compte _dto = new mouvement_compte()
            {
                id = this.id,
                id_compte = (this.account != null) ? this.account.id : 0,
                credit = this.amount,
                debit = this.amount,
                solde = this.remaining,
                numero_piece = this.proof_number,
                motif = this.description,
                date_mvt = this.operation_date,
                id_profil = (this.agent != null) ? this.agent.id : 0,
            };
            return _dto;
        }

        public static List<FinOperation> CreateFromList(List<mouvement_compte> _dboList)
        {
            List<FinOperation> _list = new List<FinOperation>();
            foreach (mouvement_compte item in _dboList)
            {
                FinOperation _businessObject = FinOperation.Create(item);
                _list.Add(_businessObject);
            }
            return _list;
        }


    }
}
