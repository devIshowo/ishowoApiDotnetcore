using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItCommerce.DTO.ModelDesign;
using ItCommerce.DTO.Factory;
using ItCommerce.DTO.Utils;
using ItCommerce.DTO.DbMethods;
using Microsoft.EntityFrameworkCore;

namespace ItCommerce.DTO.Actions
{
    public class DtoFinances : IDto
    {


        #region compte
        //liste des comptes
        public static List<compte> loadComptesList(int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            List<compte> _listDtoList = param.comptes.Where(x => x.id_entreprise == id_entreprise).ToList<compte>();
            return _listDtoList;
        }//fin loadComptesList

        public static compte createCompte(compte obj, int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            obj.id_entreprise = id_entreprise;
            //obj.id_profil = idProfil;

            param.comptes.Add(obj);
            param.SaveChanges();
            return obj;
        }//fin createCompte

        public static compte updateCompte(compte obj)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            compte searchObject = param.comptes.Find(obj.id);
            if (searchObject != null)
            {
                searchObject.intitule = obj.intitule; searchObject.id_type_compte = obj.id_type_compte; searchObject.reference = obj.reference;
                searchObject.id_banque = obj.id_banque;
                param.Entry(searchObject).State = EntityState.Modified;
                param.SaveChanges();
            }
            return searchObject;
        }//fin updateCompte

 
        public static compte deleteCompte(int id)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            compte searchObject = param.comptes.Find(id);
            if (searchObject != null)
            {
                param.comptes.Remove(searchObject);
                param.SaveChanges();
            }
            return null;
        }//fin deleteCompte


        #endregion compte


        #region Operation
        //liste des comptes
        public static List<mouvement_compte> loadOperationsFinancieresList(int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            List<mouvement_compte> _listDtoList = param.mouvement_compte
                .Where(x => x.id_entreprise == id_entreprise)
                 .OrderByDescending(x => x.date_mvt)
                .ToList<mouvement_compte>();
            return _listDtoList;
        }//fin loadOperationsList

        public static List<mouvement_compte> loadOperationsFinancieresList(DateTime startDate, DateTime endDate, int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            List<mouvement_compte> _listDtoList = param.mouvement_compte
                .Where(x => x.id_entreprise == id_entreprise)
                .Where(x => x.date_mvt.CompareTo(startDate) > 0 && x.date_mvt.CompareTo(endDate) < 0)
                .OrderByDescending(x => x.date_mvt)
                .ToList<mouvement_compte>();
            return _listDtoList;
        }//fin loadOperationsList

        public static mouvement_compte createOperationFinanciere(mouvement_compte obj, int type_operation, int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            obj.id_entreprise = id_entreprise;

            //Solde du compte
            compte theAccount = param.comptes.Find(obj.id_compte);
            if (theAccount == null) return null;


            switch (type_operation) {
                case 1://depot
                    obj.debit = 0;
                    theAccount.solde = theAccount.solde + obj.credit;
                    break;
                case 2://retrait
                    obj.credit = 0;
                    if ((theAccount.solde - obj.debit) < 0) return null;
                    theAccount.solde = theAccount.solde - obj.debit;
                    break;
                //case 3: obj.debit = 0; break;
                default: obj.debit = 0; obj.credit = 0; break;
            }

            //
            obj.date_mvt = DateTime.Now;
            obj.solde = theAccount.solde;

            //enregistrement
            param.mouvement_compte.Add(obj);
            //mise a jour
            param.Entry(theAccount).State = EntityState.Modified;

            param.SaveChanges();
            return obj;
        }//fin createOperation


        #endregion operation


    }
}
