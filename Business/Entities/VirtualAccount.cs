using ItCommerce.DTO.ModelDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItCommerce.Business.Entities
{
    public class VirtualAccount
    {
        public int id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public int amount { get; set; }
        public AccountType account_type { get; set; }
        public Profil beneficiaire { get; set; }
        public Bank bank { get; set; }

        public Profil agent { get; set; }


        public static VirtualAccount Create(compte _dbo)
        {
            return new VirtualAccount()
            {
                id = _dbo.id,
                name = _dbo.intitule,
                code = _dbo.reference,
                amount = _dbo.solde,
                account_type = (_dbo.type_compte!=null)? AccountType.Create(_dbo.type_compte): null,
                bank = (_dbo.banque != null)? Bank.Create(_dbo.banque) : null,
                beneficiaire = (_dbo.profil != null) ? Profil.Create(_dbo.profil) : null,
            };
        }

        public compte loadDto()
        {
            compte _dto = new compte()
            {
                id = this.id,
                intitule = this.name,
                reference = this.code,
                solde = this.amount,
                id_type_compte = (this.account_type != null)? this.account_type.id : 0,
                id_banque = (this.bank != null)? this.bank.id : (int?)null,
                id_profil = (this.beneficiaire != null) ? this.beneficiaire.id : (int?)null,
            };
            return _dto;
        }

        public static List<VirtualAccount> CreateFromList(List<compte> _dboList)
        {
            List<VirtualAccount> _list = new List<VirtualAccount>();
            foreach (compte item in _dboList)
            {
                VirtualAccount _businessObject = VirtualAccount.Create(item);
                _list.Add(_businessObject);
            }
            return _list;
        }

    }
}
