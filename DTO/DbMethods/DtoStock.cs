using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItCommerce.DTO.ModelDesign;
using ItCommerce.DTO.Factory;
using ItCommerce.DTO.SpecClasses;
using ItCommerce.DTO.Utils;
using Microsoft.EntityFrameworkCore;
using ItCommerce.Reporting.ViewModels;

namespace ItCommerce.DTO.DbMethods
{
    public class DtoStock : IDto
    {

        #region orders
        /// <summary>
        /// creer commande
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static commande createOrder(commande obj, int idRayon, int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            obj.id_entreprise = id_entreprise;

            obj.reference = string.Format("{0}", param.commandes.Where(x => x.id_entreprise == id_entreprise).Count() + 1).ToString().PadLeft(10, '0');
            param.commandes.Add(obj);

            //mise a jr stock
            List<stock> stockList = param.stocks.Where(x => x.id_rayon == idRayon).ToList<stock>();
            foreach (commande_details itemCmdet in obj.commande_details)
            {
                stock stockElt = stockList.Find(x => x.id_produit_mesure == itemCmdet.id_produit_mesure);

                //check prod type mesure first
                List<produit_type_mesure> prodTypemesureList = param.produit_type_mesure.Where(x => x.id == itemCmdet.id_produit_mesure).ToList<produit_type_mesure>();
                produit_type_mesure prodTypemesure = (prodTypemesureList.Count != 0) ? prodTypemesureList.First<produit_type_mesure>() : null;

                if (stockElt != null)
                {
                    stockElt.qte_reelle = stockElt.qte_reelle + itemCmdet.qte_cmde;
                    stockElt.qte_vendable = stockElt.qte_vendable + itemCmdet.qte_cmde;
                    stockElt.date_modif = DateTime.Now;
                    stockElt.p_achat = itemCmdet.p_achat;
                    stockElt.p_vente = (itemCmdet.p_vente != null) ? (int)itemCmdet.p_vente : (int)itemCmdet.p_achat;
                    stockElt.reference = (itemCmdet.reference == null || itemCmdet.reference == "") ? prodTypemesure.produit.code_fabricant : itemCmdet.reference;
                    stockElt.date_exp = itemCmdet.date_exp;
                    param.Entry(stockElt).State = EntityState.Modified;
                }
                else
                {
                    stockElt = new stock();
                    stockElt.qte_reelle = itemCmdet.qte_cmde;
                    stockElt.qte_vendable = itemCmdet.qte_cmde;
                    stockElt.date_modif = DateTime.Now;
                    stockElt.p_achat = itemCmdet.p_achat;
                    stockElt.p_vente = (itemCmdet.p_vente != null) ? (int)itemCmdet.p_vente : (int)itemCmdet.p_achat;
                    stockElt.id_produit_mesure = itemCmdet.id_produit_mesure;
                    stockElt.id_rayon = idRayon;

                    stockElt.reference = (itemCmdet.reference == null || itemCmdet.reference == "") ? prodTypemesure.produit.code_fabricant : itemCmdet.reference; //(prodTypemesure != null) ? prodTypemesure.produit.code_fabricant : "";
                    stockElt.date_exp = itemCmdet.date_exp;
                    param.stocks.Add(stockElt);
                }
            }

            #region avoir ou du fournisseur
            //avoir ou du du fournisseur
            fournisseur searchFournisseur = param.fournisseurs.Find(obj.id_fournisseur);
            if (obj.montant_sorti != obj.montant_cmde)
            {
                if (searchFournisseur != null)
                {
                    if (searchFournisseur.solde == null) searchFournisseur.solde = 0;
                    if (obj.montant_sorti < obj.montant_cmde) //indiquer du au fournisseur
                    {
                        searchFournisseur.solde = searchFournisseur.solde - (obj.montant_cmde - obj.montant_sorti);
                        param.Entry(searchFournisseur).State = EntityState.Modified;
                    }
                    else
                    {
                        if (obj.montant_sorti > obj.montant_cmde) //indiquer avoir chez le fournisseur
                        {
                            searchFournisseur.solde = searchFournisseur.solde + (obj.montant_sorti - obj.montant_cmde);
                            param.Entry(searchFournisseur).State = EntityState.Modified;
                        }
                    }
                }
            }
            #endregion

            #region operation caisse
            //retrait dans la caisse
            compte compteCaisse = MainUtil.getCaisseAccount(param, idProfil);
            if (compteCaisse != null)
            {
                compteCaisse.solde -= obj.montant_sorti;
                param.Entry(compteCaisse).State = EntityState.Modified;
            }
            #endregion

            //db action
            param.SaveChanges();
            return obj;
        }//fin createOrder


        public static commande createSavedOrder(commande obj, int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            obj.id_entreprise = id_entreprise;

            obj.reference = string.Format("{0}", param.commandes.Where(x => x.id_entreprise == id_entreprise).Count() + 1).ToString().PadLeft(10, '0');
            param.commandes.Add(obj);

            param.SaveChanges();
            return obj;
        }//fin createsavedOrder



        /// <summary>
        /// payOrderBalance
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static commande payOrderBalance(commande obj, int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            obj.id_entreprise = id_entreprise;

            int balancedAmount = 0;

            commande searchObject = param.commandes.Find(obj.id);
            fournisseur searchFournisseur = param.fournisseurs.Find(obj.id_fournisseur);

            if (searchObject != null)
            {
                if (searchObject.montant_sorti < searchObject.montant_cmde)
                {
                    balancedAmount = obj.montant_sorti - searchObject.montant_sorti;
                    searchObject.montant_sorti = obj.montant_sorti;
                    param.Entry(searchObject).State = EntityState.Modified;

                    //du au fournisseur
                    if (searchFournisseur != null)
                    {
                        if (searchFournisseur.solde == null) searchFournisseur.solde = 0;
                        searchFournisseur.solde = searchFournisseur.solde + balancedAmount;
                        param.Entry(searchFournisseur).State = EntityState.Modified;
                    }

                }
            }

            //ajout dans la caisse
            compte compteCaisse = MainUtil.getCaisseAccount(param, idProfil);
            if (compteCaisse != null)
            {
                compteCaisse.solde -= balancedAmount;
                param.Entry(compteCaisse).State = EntityState.Modified;
            }

            //db action
            param.SaveChanges();
            return obj;
        }//fin payOrderBalance


        public static List<commande> loadCommandesList(int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            profil loggedUser = MainUtil.getProfil(param, idProfil);

            //get profiles roles
            var loggedUserRoles = DtoParams.loadGroupRolesList(loggedUser.groupe.id);
            groupe_roles CheckAllowData = loggedUserRoles.Where(x => x.role.code == "DA").FirstOrDefault();

            //filter by user depending on profile group
            IQueryable<commande> filteredDtoList;
            if (CheckAllowData.statut == true)
            {
                filteredDtoList = param.commandes.Where(x => x.entreprise.id == id_entreprise);
            }
            else
            {
                filteredDtoList = param.commandes.Where(x => x.id_profil == idProfil);
            }

            List<commande> _listDtoList = filteredDtoList
                .Select(x => x)
                .Where(x => x.statut == "EMIS")
                .OrderByDescending(x => x.date_cmde)
                .ToList<commande>();
            return _listDtoList;
        }//fin loadCommandesList

        public static List<commande> loadSavedCommandesList(int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            profil loggedUser = MainUtil.getProfil(param, idProfil);

            //get profiles roles
            var loggedUserRoles = DtoParams.loadGroupRolesList(loggedUser.groupe.id);
            groupe_roles CheckAllowData = loggedUserRoles.Where(x => x.role.code == "DA").FirstOrDefault();

            //filter by user depending on profile group
            IQueryable<commande> filteredDtoList;
            if (CheckAllowData.statut == true)
            {
                filteredDtoList = param.commandes.Where(x => x.entreprise.id == id_entreprise);
            }
            else
            {
                filteredDtoList = param.commandes.Where(x => x.id_profil == idProfil);
            }

            List<commande> _listDtoList = filteredDtoList
                .Select(x => x)
                .Where(x => x.statut != "EMIS")
                .OrderByDescending(x => x.date_cmde)
                .ToList<commande>();
            return _listDtoList;
        }//fin loadCommandesList

        public static List<commande> loadCommandesNonRegleesList(int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            profil loggedUser = MainUtil.getProfil(param, idProfil);

            //get profiles roles
            var loggedUserRoles = DtoParams.loadGroupRolesList(loggedUser.groupe.id);
            groupe_roles CheckAllowData = loggedUserRoles.Where(x => x.role.code == "DA").FirstOrDefault();

            //filter by user depending on profile group
            IQueryable<commande> filteredDtoList;
            if (CheckAllowData.statut == true)
            {
                filteredDtoList = param.commandes.Where(x => x.entreprise.id == id_entreprise);
            }
            else
            {
                filteredDtoList = param.commandes.Where(x => x.id_profil == idProfil);
            }

            List<commande> _listDtoList = filteredDtoList
                .Where(x => x.montant_sorti < x.montant_cmde)
                .OrderByDescending(x => x.date_cmde)
                .ToList<commande>();
            return _listDtoList;
        }//fin loadCommandesNonRegleesList

        public static List<commande> loadCommandesList(DateTime startDate, DateTime endDate, int idProfil)
        {
            startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, 00, 00, 00);
            endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, 23, 59, 59);
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            profil loggedUser = MainUtil.getProfil(param, idProfil);
            //get profile
            if (loggedUser == null) return new List<commande>();

            //get profiles roles
            var loggedUserRoles = DtoParams.loadGroupRolesList(loggedUser.groupe.id);
            groupe_roles CheckAllowData = loggedUserRoles.Where(x => x.role.code == "DA").FirstOrDefault();

            //filter by user depending on profile group
            IQueryable<commande> filteredDtoList;
            if (CheckAllowData.statut == true)
            {
                filteredDtoList = param.commandes.Where(x => x.entreprise.id == id_entreprise);
            }
            else
            {
                filteredDtoList = param.commandes.Where(x => x.id_profil == idProfil);
            }

            List<commande> _listDtoList = filteredDtoList
                .Where(x => x.date_cmde.CompareTo(startDate) > 0 && x.date_cmde.CompareTo(endDate) < 0)
                .Select(x => x)
                .Where(x => x.statut == "EMIS")
                .OrderByDescending(x => x.date_cmde)
                .ToList<commande>();
            return _listDtoList;
        }//fin loadCommandesList


        //delete saved order
        public static commande deleteSavedOrder(int id)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            commande searchObject = param.commandes.Find(id);
            List<commande_details> searchDetails = param.commande_details.Where(x => x.id_cmde == id).ToList();
            if (searchObject != null)
            {
                foreach (var item in searchDetails)
                {
                    param.commande_details.Remove(item);
                }


                param.commandes.Remove(searchObject);
                param.SaveChanges();
            }
            return null;
        }//fin deleteStockLimit

        #endregion orders

        #region stock view
        public static List<stock> loadStockView(int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            int id_agence = MainUtil.getAgenceId(param, idProfil);

            //get profile
            profil loggedUser = MainUtil.getProfil(param, idProfil);
            if (loggedUser == null) return new List<stock>();

            //get profiles roles
            var loggedUserRoles = DtoParams.loadGroupRolesList(loggedUser.groupe.id);
            groupe_roles CheckAllowData = loggedUserRoles.Where(x => x.role.code == "DA").FirstOrDefault();

            //filter by user depending on profile group
            IQueryable<stock> filteredDtoList;
            if (CheckAllowData.statut == true)
            {
                filteredDtoList = param.stocks.Where(x => x.rayon.agence.id_entreprise == id_entreprise);
            }
            else
            {
                filteredDtoList = param.stocks.Where(x => x.rayon.id_agence == id_agence);
            }

            filteredDtoList = filteredDtoList
                .OrderBy(x => x.rayon.agence.nom)
                .ThenBy(x => x.rayon.nom)
                .ThenBy(x => x.produit_type_mesure.produit.categ_produit.nom)
                .ThenBy(x => x.produit_type_mesure.produit.nom);

            return filteredDtoList.ToList<stock>();
        }//fin loadStockView


        public static List<stock> searchStockView(int idProfil, string productName, string productCode)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            int id_agence = MainUtil.getAgenceId(param, idProfil);

            //get profile
            profil loggedUser = MainUtil.getProfil(param, idProfil);
            if (loggedUser == null) return new List<stock>();

            //get profiles roles
            var loggedUserRoles = DtoParams.loadGroupRolesList(loggedUser.groupe.id);
            groupe_roles CheckAllowData = loggedUserRoles.Where(x => x.role.code == "DA").FirstOrDefault();

            //filter by user depending on profile group
            IQueryable<stock> filteredDtoList;
            if (CheckAllowData.statut == true)
            {
                filteredDtoList = param.stocks.Where(x => x.rayon.agence.id_entreprise == id_entreprise);
            }
            else
            {
                filteredDtoList = param.stocks.Where(x => x.rayon.id_agence == id_agence);
            }

            filteredDtoList = filteredDtoList
                .Where(x => x.produit_type_mesure.produit.nom.Trim().ToLower().Contains(productName.Trim().ToLower())
                       && x.reference.Trim().ToLower().Contains(productCode.Trim().ToLower()))
                .OrderBy(x => x.produit_type_mesure.produit.nom)
                .ThenBy(x => x.produit_type_mesure.produit.categ_produit.nom)
                .ThenBy(x => x.rayon.nom);

            return filteredDtoList.ToList<stock>();
        }//fin searchStockView


        //show product expired after date specified
        public static List<stock> loadProductStockExpired(int idProfil, DateTime expDate)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            int id_agence = MainUtil.getAgenceId(param, idProfil);

            //get profile
            profil loggedUser = MainUtil.getProfil(param, idProfil);
            if (loggedUser == null) return new List<stock>();

            //get profiles roles
            var loggedUserRoles = DtoParams.loadGroupRolesList(loggedUser.groupe.id);
            groupe_roles CheckAllowData = loggedUserRoles.Where(x => x.role.code == "DA").FirstOrDefault();

            //filter by user depending on profile group
            IQueryable<stock> filteredDtoList;
            if (CheckAllowData.statut == true)
            {
                filteredDtoList = param.stocks.Where(x =>
                x.rayon.agence.id_entreprise == id_entreprise && x.date_exp < expDate);
            }
            else
            {
                filteredDtoList = param.stocks.Where(x => x.rayon.id_agence == id_agence && x.date_exp < expDate);
            }

            filteredDtoList = filteredDtoList
                .OrderBy(x => x.date_exp)
                .ThenBy(x => x.rayon.agence.nom)
                .ThenBy(x => x.rayon.nom)
                .ThenBy(x => x.produit_type_mesure.produit.categ_produit.nom)
                .ThenBy(x => x.produit_type_mesure.produit.nom);

            return filteredDtoList.ToList<stock>();
        }

        public static List<stock> loadStockViewForSale(int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            int id_agence = MainUtil.getAgenceId(param, idProfil);

            //get profile
            profil loggedUser = MainUtil.getProfil(param, idProfil);
            if (loggedUser == null) return new List<stock>();

            //filter by user depending on profile group
            IQueryable<stock> filteredDtoList = filteredDtoList = param.stocks.Where(x => x.rayon.id_agence == id_agence
            && x.qte_reelle != 0);

            filteredDtoList = filteredDtoList
                .OrderBy(x => x.rayon.agence.nom)
                .ThenBy(x => x.rayon.nom)
                .ThenBy(x => x.produit_type_mesure.produit.categ_produit.nom)
                .ThenBy(x => x.produit_type_mesure.produit.nom);

            return filteredDtoList.ToList<stock>();
        }//fin loadStockViewForSale

        /// <summary>
        /// recup max 30 elemets coteat ce om
        /// </summary>
        /// <returns>The stock view for sale limit by name.</returns>
        /// <param name="idProfil">Identifier profil.</param>
        /// <param name="productName">Product name.</param>
        public static List<stock> loadStockViewForSaleLimitByName(int idProfil, string productName)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            int id_agence = MainUtil.getAgenceId(param, idProfil);

            //get profile
            profil loggedUser = MainUtil.getProfil(param, idProfil);
            if (loggedUser == null) return new List<stock>();

            //filter by user depending on profile group
            if (productName == null) productName = "";
            IQueryable<stock> filteredDtoList = filteredDtoList = param.stocks.
             Where(x => x.rayon.id_agence == id_agence
             && x.produit_type_mesure.produit.nom.Trim().ToLower().Contains(productName.Trim().ToLower())
                  );
            //&& x.qte_reelle != 0);

            filteredDtoList = filteredDtoList
                .OrderBy(x => x.produit_type_mesure.produit.nom)
                .ThenBy(x => x.produit_type_mesure.produit.categ_produit.nom)
                .ThenBy(x => x.rayon.nom);
            //.OrderBy(x => x.rayon.agence.nom)

            return filteredDtoList.ToList<stock>();
        }//fin loadStockViewForSaleLimitByName

        /// <summary>
        /// recup max 30 elemets de code barre coteat ce qui est lu sur le lecteur
        /// </summary>
        /// <returns>The stock view for sale limit by code.</returns>
        /// <param name="idProfil">Identifier profil.</param>
        /// <param name="productCode">Product code.</param>
        public static List<stock> loadStockViewForSaleLimitByCode(int idProfil, string productCode)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            int id_agence = MainUtil.getAgenceId(param, idProfil);

            //get profile
            profil loggedUser = MainUtil.getProfil(param, idProfil);
            if (loggedUser == null) return new List<stock>();

            //filter by user depending on profile group
            if (productCode == null) productCode = "";
            IQueryable<stock> filteredDtoList = filteredDtoList = param.stocks.
             Where(x => x.rayon.id_agence == id_agence
                   && x.reference.Trim().ToLower().Contains(productCode.Trim().ToLower())
                  );
            //&& x.qte_reelle != 0);

            filteredDtoList = filteredDtoList
                .OrderBy(x => x.produit_type_mesure.produit.nom)
                .ThenBy(x => x.produit_type_mesure.produit.categ_produit.nom)
                .ThenBy(x => x.rayon.nom);
            //.OrderBy(x => x.rayon.agence.nom)

            return filteredDtoList.ToList<stock>();
        }//fin loadStockViewForSaleLimitByCode

        public static stock updateStockView(stock obj, int id)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            //old stock
            stock searchObject = param.stocks.Find(obj.id);

            int id_entreprise = MainUtil.getEntrepriseId(param, id);

            //get current user            
            profil loggedUser = MainUtil.getProfil(param, id);

            //archive first
            stock_archives stkArchive = new stock_archives();
            stkArchive.date_modif = DateTime.Now;
            stkArchive.id_produit_mesure = searchObject.id_produit_mesure;
            stkArchive.id_rayon = searchObject.id_rayon; stkArchive.quantite = searchObject.qte_reelle;
            stkArchive.p_achat = searchObject.p_achat; stkArchive.p_vente = searchObject.p_vente;
            param.stock_archives.Add(stkArchive);

            //update then
            if (searchObject != null)
            {
                //create log by the way
                log newLog = new ModelDesign.log();
                newLog.categorie = "STOCK"; newLog.date_log = DateTime.Now;
                newLog.desc = string.Format("Modification de stock: Produit {0} De Qté {1} à {2}, De PAchat {3} à {4}, De PVente {5} à {6}",
                 searchObject.produit_type_mesure.produit.nom,
                 searchObject.qte_reelle, obj.qte_reelle, searchObject.p_achat, obj.p_achat, searchObject.p_vente, obj.p_vente
                );
                newLog.id_entreprise = id_entreprise;
                if (loggedUser != null)
                {
                    newLog.actor = string.Format("{0} {1}", loggedUser.user.nom, loggedUser.user.prenoms);
                }
                param.logs.Add(newLog);

                //save object
                searchObject.p_achat = obj.p_achat; searchObject.p_vente = obj.p_vente; searchObject.qte_reelle = obj.qte_reelle;
                searchObject.date_modif = DateTime.Now; searchObject.qte_vendable = obj.qte_vendable;
                searchObject.reference = obj.reference;
                param.Entry(searchObject).State = EntityState.Modified;

            }

            //db action
            param.SaveChanges();
            return searchObject;
        }//fin updateStockView

        public static stock expandStockView(stock obj, int id)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            //old stock
            stock searchObject = param.stocks.Find(obj.id);

            int id_entreprise = MainUtil.getEntrepriseId(param, id);

            //get current user
            profil loggedUser = MainUtil.getProfil(param, id);
            //user searchUser = param.users.Find(id);

            //archive first
            #region MyRegion
            //stock_archives stkArchive = new stock_archives();
            //stkArchive.date_modif = DateTime.Now;
            //stkArchive.id_produit = searchObject.id_produit; stkArchive.id_type_mesure = searchObject.id_type_mesure;
            //stkArchive.id_rayon = searchObject.id_rayon; stkArchive.qte = searchObject.qte_reelle;
            //stkArchive.p_achat = searchObject.p_achat; stkArchive.p_vente = searchObject.p_vente;
            //param.stock_archives.Add(stkArchive); 
            #endregion

            //reduce the actual (parent mesure type) from the  qty: source
            if ((searchObject.qte_reelle - obj.qte_reelle) < 0) { throw new Exception("La quantité mettre en détail n'est pas valide!"); }
            searchObject.qte_reelle = searchObject.qte_reelle - obj.qte_reelle;
            searchObject.qte_vendable = searchObject.qte_vendable - obj.qte_reelle;

            //get the corresponding association : parent => child
            var correspMeasures = param.produit_corresp_mesure.Where(x => x.id_produit_mesure_parent == obj.id_produit_mesure);
            //searchObject.produit_type_mesure.produit_corresp_mesure.Where(x => x.id_produit_mesure_parent == obj.id_produit_mesure);
            if (correspMeasures.Count<produit_corresp_mesure>() == 0) { throw new Exception("Les types de mesure associés sont introuvables!"); }

            produit_corresp_mesure rightAssociation = correspMeasures.First<produit_corresp_mesure>();

            //get the child stock
            var childStockList = param.stocks.Where(x => x.id_produit_mesure == rightAssociation.id_produit_mesure_enfant
            && x.id_rayon == obj.id_rayon);
            stock childStock = new stock();
            if (childStockList.Count<stock>() == 0)
            {
                childStock = new stock();
                childStock.id_produit_mesure = rightAssociation.id_produit_mesure_enfant;
                childStock.id_rayon = obj.id_rayon;
                childStock.qte_reelle = 0; childStock.qte_vendable = 0;
            }
            else
            {
                childStock = childStockList.First<stock>();
            }

            //update the child stock
            int qtyToAddToDetail = rightAssociation.quantite * obj.qte_reelle;
            childStock.qte_reelle = childStock.qte_reelle + qtyToAddToDetail;
            childStock.qte_vendable = childStock.qte_vendable + qtyToAddToDetail;
            childStock.date_modif = DateTime.Now;

            //updates
            param.Entry(searchObject).State = EntityState.Modified;
            if (childStock.id == 0) { param.stocks.Add(childStock); }
            else { param.Entry(childStock).State = EntityState.Modified; }

            //create log by the way
            log newLog = new ModelDesign.log();
            newLog.categorie = "VRAC"; newLog.date_log = DateTime.Now;
            newLog.desc = string.Format("Mise en vrac du produit {0} avec la quantité {1}", searchObject.produit_type_mesure.produit.nom, obj.qte_reelle);
            if (loggedUser != null)
            {
                newLog.actor = string.Format("{0} {1}", loggedUser.user.nom, loggedUser.user.prenoms);
            }
            newLog.id_entreprise = id_entreprise;
            param.logs.Add(newLog);

            //db action
            param.SaveChanges();

            return searchObject;
        }//fin expandStockView


        public static List<stock> loadProductStockView(int idProfil, int idProduit)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_agence = MainUtil.getAgenceId(param, idProfil);

            //stock agence pr le produit
            List<stock> stockList = param.stocks.Where(x => x.id_produit_mesure == idProduit &&
            x.rayon.id_agence == id_agence && x.rayon.is_magasin != true
            ).ToList<stock>();

            return stockList;
        }//fin loadProductStockView

        public static List<stock> generateBarcodes(List<stock> stockList, int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_agence = MainUtil.getAgenceId(param, idProfil);
            if (stockList == null) return new List<stock>();

            //stock 
            List<stock> prodSaved = new List<stock>();
            foreach (stock itemStock in stockList)
            {
                stock searchStock = param.stocks.Find(itemStock.id);
                searchStock.reference = MainUtil.generateReference(param, 13);
                if (searchStock.date_exp == null || searchStock.date_exp.Value.Year == 1) { searchStock.date_exp = MainUtil.getMinTimeValue(); }
                if (searchStock.date_modif == null || searchStock.date_modif.Year == 1) { searchStock.date_modif = MainUtil.getMinTimeValue(); }

                param.Entry(searchStock).State = EntityState.Modified;
                prodSaved.Add(searchStock);
            }

            param.SaveChanges();

            return prodSaved;
        }//fin generateBarcodes

        public static List<stock> printBarcodes(List<stock> stockList, int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_agence = MainUtil.getAgenceId(param, idProfil);
            if (stockList == null) return new List<stock>();

            param.SaveChanges();
            return stockList;

        }//fin printBarcodes

        #endregion stock view

        #region sales
        /// <summary>
        /// creer vente
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static SaleDataView createSale(vente_produit obj, int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_agence = MainUtil.getAgenceId(param, idProfil);
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            obj.id_agence = id_agence;

            //get first category vente 
            categ_vente catVente = param.categ_vente.First<categ_vente>();
            obj.id_categ_vente = catVente.id;  //todo: get the provided by the user


            obj.reference = string.Format("{0}", param.vente_produit.Where(x => x.id_agence == id_agence).Count() + 1).ToString().PadLeft(10, '0');
            param.Entry(obj).State = EntityState.Modified;

            // customer
            if (obj.client.id == 0)
            {
                obj.id_client = null;
            }

            //ventes
            param.vente_produit.Add(obj);

            //rayon de l'agence
            List<rayon> rayonsList = param.rayons.Where(x => x.id_agence == obj.id_agence).ToList<rayon>();


            //mise a jr stock
            //List<stock> stockList = param.stocks.Where(x => rayonsList.Contains(x.rayon)).ToList<stock>();

            #region stock update region
            List<stock> stockList = param.stocks.Where(x => x.rayon != null &&
               x.rayon.id_agence == obj.id_agence //&& x.rayon.is_magasin != true
               ).ToList<stock>();
            foreach (vente_produit_details itemCmdet in obj.vente_details)
            {
                stock stockElt = stockList.Find(x =>
                x.id_produit_mesure == itemCmdet.id_produit_mesure && x.id_rayon == itemCmdet.id_rayon
                );
                if (stockElt != null)
                {
                    if ((stockElt.qte_reelle - itemCmdet.quantite) < 0)
                    {
                        string errorText = string.Format("Stock insuffisant pour le produit {0} / {1} du rayon {2}", stockElt.produit_type_mesure.produit.nom, stockElt.produit_type_mesure.type_mesure.nom, stockElt.rayon.nom);
                        throw new InvalidOperationException(errorText);
                    }

                    stockElt.qte_reelle = stockElt.qte_reelle - itemCmdet.quantite;
                    stockElt.qte_vendable = stockElt.qte_vendable - itemCmdet.quantite;
                    stockElt.date_modif = DateTime.Now;
                    param.vente_produit_details.Add(itemCmdet);
                }
                //add historique
            }
            #endregion

            #region operation caisse
            //ajout dans la caisse
            compte compteCaisse = MainUtil.getCaisseAccount(param, idProfil);
            if (compteCaisse != null)
            {
                compteCaisse.solde += (int)obj.mt_a_payer;
                param.Entry(compteCaisse).State = EntityState.Modified;
            }
            #endregion

            //mise a jour de l'objectif du vendeur
            objectif_vente profilObjVente = MainUtil.getUserObjectifVente(param, idProfil);
            if (profilObjVente != null)
            {
                if (profilObjVente.date_fin.AddDays(1).CompareTo(DateTime.Now) > 0)
                {
                    profilObjVente.montant_atteint += (int)obj.mt_a_payer; param.Entry(profilObjVente).State = EntityState.Modified;
                }
                //mis à jour du statut de l'objectif
                if (profilObjVente.montant_atteint >= profilObjVente.montant_objectif)
                {
                    profilObjVente.est_atteint = true; param.Entry(profilObjVente).State = EntityState.Modified;
                }
                else
                {
                    profilObjVente.est_atteint = false; param.Entry(profilObjVente).State = EntityState.Modified;
                }
            }

            //db action
            param.SaveChanges();
            SaleDataView saveSale = new SaleDataView();
            facture normalisation = null;
            saveSale.invoice = normalisation;

            //load sale completely for print next
            if (obj != null)
            {
                var loadedSale = param.vente_produit.Find(obj.id);
                saveSale.sale = loadedSale;
                if (obj.mt_recu < obj.mt_a_payer)
                {
                    saveSale.invoice = null;
                    return saveSale;
                }

                param_mecef searchParam = param.param_mecef.Where(x => x.code == "EMECEF").FirstOrDefault();
                param_mecef searchParamTwo = param.param_mecef.Where(x => x.code == "MECEF").FirstOrDefault();
                if (searchParam.valeur == "true")
                {
                    //load normalisation
                    normalisation = DtoMecef.normalizeSaleOnline(obj.id).Result;
                    saveSale.invoice = normalisation;
                    obj.avec_facture = true;
                    param.SaveChanges();
                }
                else if (searchParamTwo.valeur == "true")
                {
                    //load normalisation
                    normalisation = DtoMecef.normalizeSale(obj.id);
                    saveSale.invoice = normalisation;
                    obj.avec_facture = true;
                    param.SaveChanges();
                }
            }

            return saveSale;
        }//fin createSale

        /// <summary>
        /// payOrderBalance
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static vente_produit paySaleBalance(vente_produit obj, int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_agence = MainUtil.getAgenceId(param, idProfil);
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            obj.id_agence = id_agence;

            int balancedAmount = 0;

            vente_produit searchObject = param.vente_produit.Find(obj.id);
            utilisateur searchClient = param.users.Find(obj.id_client);

            if (searchObject != null)
            {
                if (searchObject.mt_recu < searchObject.mt_a_payer)
                {
                    balancedAmount = (int)obj.mt_recu - (int)searchObject.mt_recu;
                    searchObject.mt_recu = obj.mt_recu;
                    searchObject.reliquat = obj.reliquat;
                    searchObject.reste_a_payer = obj.reste_a_payer;
                    param.Entry(searchObject).State = EntityState.Modified;

                    //du par le client
                    if (searchClient != null)
                    {
                        if (searchClient.solde == null) searchClient.solde = 0;
                        searchClient.solde = searchClient.solde + balancedAmount;
                        param.Entry(searchClient).State = EntityState.Modified;
                    }

                }
            }

            //depot dans la caisse
            compte compteCaisse = MainUtil.getCaisseAccount(param, idProfil);
            if (compteCaisse != null)
            {
                compteCaisse.solde += balancedAmount;
                param.Entry(compteCaisse).State = EntityState.Modified;
            }

            //db action
            param.SaveChanges();
            return obj;
        }//fin paySaleBalance


        //printSaleNormalised
        public static facture printSaleNormalised(int idVente)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            facture search = param.factures.Where(x => x.id_vente == idVente).FirstOrDefault();
            if (search != null)
            {
                return search;
            }
            else
            {
                return null;
            }
        }
        //fin printSaleNormalised

        //printSaleCanceled
        public static facture_avoir printSaleCanceled(int idVente)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            facture_avoir search = param.factures_avoir.Where(x => x.id_vente == idVente).FirstOrDefault();
            if (search != null)
            {
                return search;
            }
            else
            {
                return null;
            }
        }
        //fin printSaleCanceled

        //searchSale
        public static vente_produit searchSale(int idSale)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            vente_produit search = param.vente_produit.Where(x => x.id == idSale).FirstOrDefault();
            if (search != null)
            {
                return search;
            }
            else
            {
                return null;
            }
        }
        //fin searchSale

        public static List<vente_produit> loadVentesList(int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            //get profile
            profil loggedUser = MainUtil.getProfil(param, idProfil);
            if (loggedUser == null) return new List<vente_produit>();
            //get profiles roles
            var loggedUserRoles = DtoParams.loadGroupRolesList(loggedUser.groupe.id);
            groupe_roles CheckAllowData = loggedUserRoles.Where(x => x.role.code == "DA").FirstOrDefault();

            //filter by user depending on profile group
            IQueryable<vente_produit> filteredDtoList;
            if (CheckAllowData.statut == true)
            {
                filteredDtoList = param.vente_produit.Where(x => x.agence.id_entreprise == id_entreprise && x.is_devis == false);
            }
            else
            {
                filteredDtoList = param.vente_produit.Where(x => x.id_profil == idProfil && x.is_devis == false);
            }

            List<vente_produit> _listDtoList = filteredDtoList.Select(x => x)
                .OrderByDescending(x => x.date_vente)
                .ToList<vente_produit>();
            return _listDtoList;
        }//fin loadVentesList

        public static List<vente_produit> loadVentesList(DateTime startDate, DateTime endDate, int idProfil)
        {
            startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, 00, 00, 00);
            endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, 23, 59, 59);

            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            //get profile
            profil loggedUser = MainUtil.getProfil(param, idProfil);
            if (loggedUser == null) return new List<vente_produit>();
            //get profiles roles
            var loggedUserRoles = DtoParams.loadGroupRolesList(loggedUser.groupe.id);
            groupe_roles CheckAllowData = loggedUserRoles.Where(x => x.role.code == "DA").FirstOrDefault();

            //filter by user depending on profile group
            IQueryable<vente_produit> filteredDtoList;
            if (CheckAllowData.statut == true)
            {
                filteredDtoList = param.vente_produit.Where(x => x.agence.id_entreprise == id_entreprise && x.is_devis == false);
            }
            else
            {
                filteredDtoList = param.vente_produit.Where(x => x.id_profil == idProfil && x.is_devis == false);
            }

            List<vente_produit> _listDtoList = filteredDtoList
                .Where(x => x.date_vente.CompareTo(startDate) > 0 && x.date_vente.CompareTo(endDate) < 0)
                .OrderByDescending(x => x.date_vente)
                .ToList<vente_produit>();
            return _listDtoList;
        }//fin loadVentesList

        #endregion


        #region devis
        /// <summary>
        /// creer devis
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static vente_produit createDevis(vente_produit obj, int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_agence = MainUtil.getAgenceId(param, idProfil);
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            obj.id_agence = id_agence;

            //get first category vente 
            categ_vente catVente = param.categ_vente.First<categ_vente>();
            obj.id_categ_vente = catVente.id;  //todo: get the provided by the user
            obj.is_devis = true;

            obj.reference = string.Format("{0}", param.vente_produit.Where(x => x.id_agence == id_agence).Count() + 1).ToString().PadLeft(10, '0');
            param.Entry(obj).State = EntityState.Modified;

            // customer
            if (obj.client.id == 0)
            {
                obj.id_client = null;
            }

            //ventes
            param.vente_produit.Add(obj);

            //rayon de l'agence
            List<rayon> rayonsList = param.rayons.Where(x => x.id_agence == obj.id_agence).ToList<rayon>();

            #region stock update region
            List<stock> stockList = param.stocks.Where(x => x.rayon != null &&
               x.rayon.id_agence == obj.id_agence //&& x.rayon.is_magasin != true
               ).ToList<stock>();
            foreach (vente_produit_details itemCmdet in obj.vente_details)
            {
                stock stockElt = stockList.Find(x =>
                x.id_produit_mesure == itemCmdet.id_produit_mesure && x.id_rayon == itemCmdet.id_rayon
                );
                if (stockElt != null)
                {
                    param.vente_produit_details.Add(itemCmdet);
                }
                //add historique
            }
            #endregion

            //db action
            param.SaveChanges();
            return obj;

        }//fin createDevis


        public static List<vente_produit> loadDevisList(int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            //get profile
            profil loggedUser = MainUtil.getProfil(param, idProfil);
            if (loggedUser == null) return new List<vente_produit>();
            //get profiles roles
            var loggedUserRoles = DtoParams.loadGroupRolesList(loggedUser.groupe.id);
            groupe_roles CheckAllowData = loggedUserRoles.Where(x => x.role.code == "DA").FirstOrDefault();

            //filter by user depending on profile group
            IQueryable<vente_produit> filteredDtoList;
            if (CheckAllowData.statut == true)
            {
                filteredDtoList = param.vente_produit.Where(x => x.agence.id_entreprise == id_entreprise && x.is_devis == true);
            }
            else
            {
                filteredDtoList = param.vente_produit.Where(x => x.id_profil == idProfil && x.is_devis == true);
            }

            List<vente_produit> _listDtoList = filteredDtoList.Select(x => x)
                .OrderByDescending(x => x.date_vente)
                .ToList<vente_produit>();
            return _listDtoList;
        }//fin loadDevisList

        public static List<vente_produit> loadDevisList(DateTime startDate, DateTime endDate, int idProfil)
        {
            startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, 00, 00, 00);
            endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, 23, 59, 59);

            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            //get profile
            profil loggedUser = MainUtil.getProfil(param, idProfil);
            if (loggedUser == null) return new List<vente_produit>();
            //get profiles roles
            var loggedUserRoles = DtoParams.loadGroupRolesList(loggedUser.groupe.id);
            groupe_roles CheckAllowData = loggedUserRoles.Where(x => x.role.code == "DA").FirstOrDefault();

            //filter by user depending on profile group
            IQueryable<vente_produit> filteredDtoList;
            if (CheckAllowData.statut == true)
            {
                filteredDtoList = param.vente_produit.Where(x => x.agence.id_entreprise == id_entreprise);
            }
            else
            {
                filteredDtoList = param.vente_produit.Where(x => x.id_profil == idProfil);
            }

            List<vente_produit> _listDtoList = filteredDtoList
                .Where(x => x.date_vente.CompareTo(startDate) > 0 && x.date_vente.CompareTo(endDate) < 0)
                .OrderByDescending(x => x.date_vente)
                .ToList<vente_produit>();
            return _listDtoList;
        }//fin loadDevisList

        #endregion


        #region services sales
        /// <summary>
        /// creer vente
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static vente_service createServiceSale(vente_service obj, int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_agence = MainUtil.getAgenceId(param, idProfil);
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);

            //ventes
            param.vente_service.Add(obj);


            return obj;
        }//fin createSale


        public static List<vente_service> loadVentesServiceList(int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            //get profile
            profil loggedUser = MainUtil.getProfil(param, idProfil);
            if (loggedUser == null) return new List<vente_service>();
            //get profiles roles
            var loggedUserRoles = DtoParams.loadGroupRolesList(loggedUser.groupe.id);
            groupe_roles CheckAllowData = loggedUserRoles.Where(x => x.role.code == "DA").FirstOrDefault();

            //filter by user depending on profile group
            IQueryable<vente_service> filteredDtoList;
            if (CheckAllowData.statut == true)
            {
                filteredDtoList = param.vente_service.Where(x => x.agence.id_entreprise == id_entreprise);
            }
            else
            {
                filteredDtoList = param.vente_service.Where(x => x.id_agent == idProfil);
            }

            List<vente_service> _listDtoList = filteredDtoList.Select(x => x)
                .OrderByDescending(x => x.date_vente)
                .ToList<vente_service>();
            return _listDtoList;
        }//fin loadVentesList

        public static List<vente_service> loadVentessServiceList(DateTime startDate, DateTime endDate, int idProfil)
        {
            startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, 00, 00, 00);
            endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, 23, 59, 59);

            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            //get profile
            profil loggedUser = MainUtil.getProfil(param, idProfil);
            if (loggedUser == null) return new List<vente_service>();
            //get profiles roles
            var loggedUserRoles = DtoParams.loadGroupRolesList(loggedUser.groupe.id);
            groupe_roles CheckAllowData = loggedUserRoles.Where(x => x.role.code == "DA").FirstOrDefault();

            //filter by user depending on profile group
            IQueryable<vente_service> filteredDtoList;
            if (CheckAllowData.statut == true)
            {
                filteredDtoList = param.vente_service.Where(x => x.agence.id_entreprise == id_entreprise);
            }
            else
            {
                filteredDtoList = param.vente_service.Where(x => x.id_agent == idProfil);
            }

            List<vente_service> _listDtoList = filteredDtoList
                .Where(x => x.date_vente.CompareTo(startDate) > 0 && x.date_vente.CompareTo(endDate) < 0)
                .OrderByDescending(x => x.date_vente)
                .ToList<vente_service>();
            return _listDtoList;
        }//fin loadVentesList


        #endregion services sales


        #region  stock limit
        //liste des limites de stock
        public static List<produit_stock_limit> loadStockLimit(int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);

            //filter by user depending on profile group
            IQueryable<produit_stock_limit> filteredDtoList;
            filteredDtoList = param.produit_stock_limit.Where(x => x.produit.id_entreprise == id_entreprise);

            return filteredDtoList.ToList<produit_stock_limit>(); ;
        }//fin loadStockLimit

        //liste des produits en dessous du seuil de stock
        public static List<stock> loadProduitsHorsSeuilStock(int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            int id_agence = MainUtil.getAgenceId(param, idProfil);

            //get profile
            profil loggedUser = MainUtil.getProfil(param, idProfil);
            if (loggedUser == null) return new List<stock>();
            //get profiles roles
            var loggedUserRoles = DtoParams.loadGroupRolesList(loggedUser.groupe.id);
            if (loggedUserRoles.Count() == 0)
            {
                throw new InvalidOperationException("Erreur groupe role");
            }
            groupe_roles CheckAllowData = loggedUserRoles.Where(x => x.role.code == "DA").FirstOrDefault();

            //filter by user depending on profile group
            IQueryable<stock> filteredDtoList;
            if (CheckAllowData.statut == true)
            {
                filteredDtoList = param.stocks.Where(x => x.rayon.agence.id_entreprise == id_entreprise);
            }
            else
            {
                filteredDtoList = param.stocks.Where(x => x.rayon.id_agence == id_agence);
            }

            #region top
            ////seuil de stock
            List<produit_stock_limit> _listSeuil = param.produit_stock_limit.ToList<produit_stock_limit>();

            ////check pr chaque produit
            List<stock> produitsHorsSeuil = new List<stock>();
            foreach (var itemStock in filteredDtoList)
            {

                //search in seuil if exists
                List<produit_stock_limit> searchedSeuil = _listSeuil.Where(x => x.id_produit_type_mesure == itemStock.id_produit_mesure).ToList<produit_stock_limit>();
                if (searchedSeuil.Count != 0)
                {
                    produit_stock_limit relatedSeuil = searchedSeuil.First<produit_stock_limit>();

                    //check if under seuil
                    stock prodHorsSeuil = new stock();
                    if (itemStock.qte_reelle <= relatedSeuil.quantite)
                    {
                        produitsHorsSeuil.Add(itemStock);
                    }


                }
                else if (itemStock.qte_reelle < 1)
                {
                    produitsHorsSeuil.Add(itemStock);
                }

            }
            #endregion

            return produitsHorsSeuil.ToList<stock>();
        }//fin loadProduitsHorsSeuilStock

        public static produit_stock_limit createStockLimit(produit_stock_limit obj, int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);

            //search id_produit_type_mesure

            produit_type_mesure searchProdObject = param.produit_type_mesure.Where(x => x.id_produit == obj.id_produit && x.id_type_mesure == obj.id_produit_type_mesure)
                .ToList<produit_type_mesure>()[0];

            obj.id_produit_type_mesure = searchProdObject.id;

            //search if exist
            List<produit_stock_limit> searchListObject = param.produit_stock_limit.Where(x => x.id_produit == obj.id_produit &&
          x.id_produit_type_mesure == obj.id_produit_type_mesure).ToList<produit_stock_limit>();
            produit_stock_limit searchObject = null;
            if (searchListObject.Count != 0)
            {
                searchObject = searchListObject.First<produit_stock_limit>();
                if ((searchObject.quantite + obj.quantite) > 0)
                {
                    searchObject.quantite += obj.quantite;
                    param.Entry(searchObject).State = EntityState.Modified;
                    param.SaveChanges();
                }

            }
            else
            {
                //add if not exists
                if (obj.quantite > 0)
                {
                    param.produit_stock_limit.Add(obj);
                    param.SaveChanges();
                }
            }

            return obj;
        }//fin createStockLimit

        public static produit_stock_limit updateStockLimit(produit_stock_limit obj)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            produit_stock_limit searchObject = param.produit_stock_limit.Find(obj.id);
            if (searchObject != null)
            {
                searchObject.quantite = obj.quantite;
                param.Entry(searchObject).State = EntityState.Modified;
                param.SaveChanges();
            }
            return searchObject;
        }//fin updateStockLimit

        public static produit_stock_limit deleteStockLimit(int id)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            produit_stock_limit searchObject = param.produit_stock_limit.Find(id);
            if (searchObject != null)
            {
                param.produit_stock_limit.Remove(searchObject);
                param.SaveChanges();
            }
            return null;
        }//fin deleteStockLimit

        #endregion  stock limit

        #region transfer

        /// <summary>
        /// creer transfer
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static transfert createTransfer(transfert obj, int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_agence = MainUtil.getAgenceId(param, idProfil);
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            //get profile
            profil agent = MainUtil.getProfil(param, idProfil);

            //transfert
            obj.reference = string.Format("{0}", param.transferts.Where(x => x.profil.id_agence == id_agence).Count() + 1).ToString().PadLeft(10, '0');
            param.transferts.Add(obj);

            //get profiles roles
            var loggedUserRoles = DtoParams.loadGroupRolesList(agent.groupe.id);
            groupe_roles CheckAllowData = loggedUserRoles.Where(x => x.role.code == "DA").FirstOrDefault();

            //mise a jour du stock du rayon source
            //stock rayon source
            List<stock> stockSourceList = new List<stock>();
            if (CheckAllowData.statut == true)
            {
                stockSourceList = param.stocks.Where(x => x.rayon != null && x.id_rayon == obj.id_source &&
                x.rayon.agence.id_entreprise == id_entreprise).ToList<stock>();
            }
            else
            {
                stockSourceList = param.stocks.Where(x => x.rayon != null && x.id_rayon == obj.id_source &&
                    x.rayon.id_agence == id_agence).ToList<stock>();
            }


            //stock rayon destination
            List<stock> stockDestinationList = new List<stock>();
            if (CheckAllowData.statut == true)
            {
                stockDestinationList = param.stocks.Where(x => x.rayon != null && x.id_rayon == obj.id_destination &&
                x.rayon.agence.id_entreprise == id_entreprise).ToList<stock>();
            }
            else
            {
                stockDestinationList = param.stocks.Where(x => x.rayon != null && x.id_rayon == obj.id_destination &&
                x.rayon.id_agence == id_agence).ToList<stock>();
            }

            param.SaveChanges();

            //diminution du stock source
            foreach (transfert_details itemLine in obj.transfert_details)
            {
                if (itemLine.quantite != 0)
                {
                    stock stockEltSource = stockSourceList.Find(x =>
                            x.id_produit_mesure == itemLine.id_produit_mesure);
                    if (stockEltSource != null)
                    {
                        if ((stockEltSource.qte_reelle - itemLine.quantite) >= 0)
                        {
                            stockEltSource.qte_reelle = stockEltSource.qte_reelle - itemLine.quantite;
                            stockEltSource.qte_vendable = stockEltSource.qte_vendable - itemLine.quantite;
                            stockEltSource.date_modif = DateTime.Now;
                            param.Entry(stockEltSource).State = EntityState.Modified;
                        }
                        else { continue; }
                    }
                    else { continue; }
                    //fin mise a jour du stock source

                    //diminution du stock destination
                    //mise a jour du stock du rayon destination
                    stock stockEltDestination = stockDestinationList.Find(x =>
                                x.id_produit_mesure == itemLine.id_produit_mesure
                    );
                    if (stockEltDestination != null)
                    {
                        stockEltDestination.qte_reelle = stockEltDestination.qte_reelle + itemLine.quantite;
                        stockEltDestination.qte_vendable = stockEltDestination.qte_vendable + itemLine.quantite;
                        stockEltDestination.date_modif = DateTime.Now;
                        param.Entry(stockEltDestination).State = EntityState.Modified;
                    }
                    else
                    {
                        stockEltDestination = new stock();
                        stockEltDestination.reference = string.Format("{0}", param.commandes.Where(x => x.id_entreprise == id_entreprise).Count() + 1).ToString().PadLeft(10, '0');
                        stockEltDestination.id_produit_mesure = itemLine.id_produit_mesure;
                        stockEltDestination.id_rayon = obj.id_destination;
                        stockEltDestination.qte_reelle = itemLine.quantite;
                        stockEltDestination.qte_vendable = itemLine.quantite;
                        stockEltDestination.date_modif = DateTime.Now;
                        stockEltDestination.p_achat = itemLine.p_achat;
                        stockEltDestination.p_vente = itemLine.p_vente;
                        param.stocks.Add(stockEltDestination);

                    }
                }
            }
            //fin mise a jour du stock destination

            //db action
            param.SaveChanges();
            return obj;

            //todo:add historique
        }//fin createTransfer

        public static List<transfert> loadTransfertsList(int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            int id_agence = MainUtil.getAgenceId(param, idProfil);

            //get profile
            profil loggedUser = MainUtil.getProfil(param, idProfil);
            if (loggedUser == null) return new List<transfert>();

            //get profiles roles
            var loggedUserRoles = DtoParams.loadGroupRolesList(loggedUser.groupe.id);
            groupe_roles CheckAllowData = loggedUserRoles.Where(x => x.role.code == "DA").FirstOrDefault();

            IQueryable<transfert> filteredDtoList;
            if (CheckAllowData.statut == true)
            {
                filteredDtoList = param.transferts.Where(x => x.rayon_source.agence.id_entreprise == id_entreprise
                                                         && x.rayon_destination.agence.id_entreprise == id_entreprise);
            }
            else
            {
                filteredDtoList = param.transferts.Where(x => x.rayon_source.id_agence == id_agence
                                                         && x.rayon_destination.id_agence == id_agence && x.id_profil == idProfil);
            }

            List<transfert> _listDtoList = filteredDtoList.Select(x => x)
                .OrderByDescending(x => x.date_transfert)
                .ToList<transfert>();

            return _listDtoList;
        }//fin loadTransfertsList

        public static List<transfert> loadTransfertsList(DateTime startDate, DateTime endDate, int idProfil)
        {
            startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, 00, 00, 00);
            endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, 23, 59, 59);

            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            int id_agence = MainUtil.getAgenceId(param, idProfil);

            //get profile
            profil loggedUser = MainUtil.getProfil(param, idProfil);
            if (loggedUser == null) return new List<transfert>();

            //get profiles roles
            var loggedUserRoles = DtoParams.loadGroupRolesList(loggedUser.groupe.id);
            groupe_roles CheckAllowData = loggedUserRoles.Where(x => x.role.code == "DA").FirstOrDefault();

            //filter by user depending on profile group  // stock
            IQueryable<transfert> filteredDtoList;
            if (CheckAllowData.statut == true)
            {
                filteredDtoList = param.transferts.Where(x => x.rayon_source.agence.id_entreprise == id_entreprise
                                                         && x.rayon_destination.agence.id_entreprise == id_entreprise);
            }
            else
            {
                filteredDtoList = param.transferts.Where(x => x.rayon_source.id_agence == id_agence
                   && x.rayon_destination.id_agence == id_agence && x.id_profil == idProfil);
            }

            List<transfert> _listDtoList = filteredDtoList
                .Where(x => x.date_transfert.CompareTo(startDate) > 0 && x.date_transfert.CompareTo(endDate) < 0)
                .Select(x => x)
                .OrderByDescending(x => x.date_transfert)
                .ToList<transfert>();

            return _listDtoList;
        }//fin loadTransfertsList



        #endregion

    }
}
