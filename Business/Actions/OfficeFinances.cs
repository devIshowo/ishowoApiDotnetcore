using ItCommerce.Business.Entities;
using ItCommerce.Business.Extra;
using ItCommerce.DTO.Actions;
using ItCommerce.DTO.ModelDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItCommerce.Business.Actions
{
    public class OfficeFinances : IOffice
    {


        #region comptes virtuels

        /// <summary>
        /// liste banques
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static List<VirtualAccount> getListVirtualAccounts(int id)
        {
            List<VirtualAccount> _listBsList = VirtualAccount.CreateFromList(DtoFinances.loadComptesList(id));
            return _listBsList;
        }//fin getListVirtualAccounts

        public static VirtualAccount createVirtualAccount(VirtualAccount item)
        {
            compte result = DtoFinances.createCompte(item.loadDto(), item.agent.id);
            return VirtualAccount.Create(result);
        }//fin createVirtualAccount

        public static VirtualAccount updateVirtualAccount(VirtualAccount item)
        {
            compte result = DtoFinances.updateCompte(item.loadDto());
            return VirtualAccount.Create(result);
        }//fin updateVirtualAccount

        public static VirtualAccount deleteVirtualAccount(int id)
        {
            compte result = DtoFinances.deleteCompte(id);
            return new VirtualAccount();
        }//fin deleteVirtualAccount

        #endregion comptes virtuels


        #region operations financieres
        public static FinOperation createFinOperation(FinOperation item)
        {
            mouvement_compte result = DtoFinances.createOperationFinanciere(item.loadDto(), (int)item.operation_type, item.agent.id);
            return FinOperation.Create(result);
        }

        public static List<FinOperation> getListFinOperations(int id)
        {
            List<FinOperation> _listBsList = FinOperation.CreateFromList(DtoFinances.loadOperationsFinancieresList(id));
            return _listBsList;
        }

        public static List<FinOperation> getListFinOperations(PeriodParam obj)
        {
            List<FinOperation> _listBsList = FinOperation.CreateFromList(DtoFinances.loadOperationsFinancieresList(obj.startDate, obj.endDate, obj.agent.id));
            return _listBsList;
        }
        #endregion

    }
}
