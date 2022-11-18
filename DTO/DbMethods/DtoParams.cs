using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItCommerce.DTO.ModelDesign;
using ItCommerce.DTO.Factory;
using ItCommerce.DTO.Utils;
using Microsoft.EntityFrameworkCore;
using ItCommerce.DTO.SpecClasses;
using Serilog;

namespace ItCommerce.DTO.DbMethods
{
    public class DtoParams : IDto
    {
        #region  services
        //liste des services
        public static List<service> loadServicesList()
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            List<service> _listDtoList = param.services
                .OrderBy(x => x.nom)
                .ToList<service>();
            return _listDtoList;
        }//fin loadServicesList

        public static List<service> loadService(int id)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            List<service> _listDtoList = param.services
                .Where(x => x.id == id)
                .ToList<service>();
            return _listDtoList;
        }//fin loadService

        public static service createService(service obj)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            param.services.Add(obj);
            param.SaveChanges();
            return obj;
        }//fin createService
        public static service updateService(service obj)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            service searchObject = param.services.Find(obj.id);
            if (searchObject != null)
            {
                searchObject.nom = obj.nom;
                searchObject.description = obj.description;
                searchObject.cout = obj.cout;
                param.Entry(searchObject).State = EntityState.Modified;
                param.SaveChanges();
            }
            return searchObject;
        }//fin updateService

        public static service deleteService(int id)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            service searchObject = param.services.Find(id);
            if (searchObject != null)
            {
                param.services.Remove(searchObject);
                param.SaveChanges();
            }
            return null;
        }//fin deleteService
        //fin zone services

        #endregion  services


        #region  clients
        //liste des clients
        public static List<client> loadClientsList()
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            List<client> _listDtoList = param.clients
                .OrderBy(x => x.nom)
                .ToList<client>();
            return _listDtoList;
        }//fin loadCustomersList
        public static client createCustomer(client obj)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            param.clients.Add(obj);
            param.SaveChanges();
            return obj;
        }//fin createCustomer
        public static client updateCustomer(client obj)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            client searchObject = param.clients.Find(obj.id);
            if (searchObject != null)
            {
                searchObject.nom = obj.nom;
                searchObject.prenom = obj.prenom;
                searchObject.raison_sociale = obj.raison_sociale;
                searchObject.ifu = obj.ifu;
                searchObject.solde = obj.solde;
                searchObject.contact = obj.contact;
                searchObject.whatsapp = obj.whatsapp;
                searchObject.adr_mail = obj.adr_mail;
                searchObject.date_creation = obj.date_creation;
                param.Entry(searchObject).State = EntityState.Modified;
                param.SaveChanges();
            }
            return searchObject;
        }//fin updateCustomer

        public static client deleteCustomer(int id)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            client searchObject = param.clients.Find(id);
            if (searchObject != null)
            {
                param.clients.Remove(searchObject);
                param.SaveChanges();
            }
            return null;
        }//fin deleteCustomer
        //fin zone clients

        #endregion  clients

        #region agences
        /*start agence*/
        //liste des agences
        public static List<agence> loadAgencesList(int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            List<agence> _listDtoList = param.agences.Where(x => x.id_entreprise == id_entreprise).ToList<agence>();
            return _listDtoList;
        }//fin loadAgencesList
        public static agence createAgence(agence obj, int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            obj.id_entreprise = id_entreprise;

            param.agences.Add(obj);
            param.SaveChanges();

            return obj;
        }//fin createAgence
        public static agence updateAgence(agence obj)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            agence searchObject = param.agences.Find(obj.id);
            if (searchObject != null)
            {
                searchObject.nom = obj.nom; searchObject.localisation = obj.localisation;
                param.Entry(searchObject).State = EntityState.Modified;
                param.SaveChanges();
            }
            return searchObject;
        }//fin updateAgence

        public static agence deleteAgence(int id)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            agence searchObject = param.agences.Find(id);
            if (searchObject != null)
            {
                param.agences.Remove(searchObject);
                param.SaveChanges();
            }
            return null;
        }//fin deleteAgence
        /*end agence*/

        #endregion agence

        #region categorie
        /*start categorie*/
        //liste des categories de produit
        public static List<categ_produit> loadCategorieProduitList(int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            List<categ_produit> _listDtoList = param.categ_produit
                .Where(x => x.id_entreprise == id_entreprise)
                .OrderBy(x => x.nom)
                .ToList<categ_produit>();
            return _listDtoList;
        }//fin loadCategorieProduitList

        public static categ_produit createCategory(categ_produit obj, int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            obj.id_entreprise = id_entreprise;
            param.categ_produit.Add(obj);
            param.SaveChanges();
            return obj;
        }//fin createCategory
        public static categ_produit updateCategory(categ_produit obj)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            categ_produit searchObject = param.categ_produit.Find(obj.id);
            if (searchObject != null)
            {
                searchObject.nom = obj.nom;
                param.Entry(searchObject).State = EntityState.Modified;
                param.SaveChanges();
            }
            return searchObject;
        }//fin updateCategory
        public static categ_produit deleteCategory(int id)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            categ_produit searchObject = param.categ_produit.Find(id);
            if (searchObject != null)
            {
                param.categ_produit.Remove(searchObject);
                param.SaveChanges();
            }
            return null;
        }//fin deleteCategory
        /*end category*/

        #endregion category

        #region  types de mesures
        /*types de mesure*/
        //liste des types de mesures
        public static List<type_mesure> loadTypesMesureList(int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            List<type_mesure> _listDtoList = param.type_mesure.Where(x => x.id_entreprise == id_entreprise).ToList<type_mesure>();
            return _listDtoList;
        }//fin loadTypesMesureList

        public static List<produit_type_mesure> loadProduitsTypesMesureList(int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            List<produit_type_mesure> _listDtoList = param.produit_type_mesure.Where(x => x.produit.id_entreprise == id_entreprise).ToList<produit_type_mesure>();
            return _listDtoList;
        }//fin loadProduitsTypesMesureList

        public static type_mesure createMeasureType(type_mesure obj, int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            obj.id_entreprise = id_entreprise;
            param.type_mesure.Add(obj);
            param.SaveChanges();
            return obj;
        }//fin createMeasureType
        public static type_mesure updateMeasureType(type_mesure obj)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            type_mesure searchObject = param.type_mesure.Find(obj.id);
            if (searchObject != null)
            {
                searchObject.nom = obj.nom;
                param.Entry(searchObject).State = EntityState.Modified;
                param.SaveChanges();
            }
            return searchObject;
        }//fin updateMeasureType
        public static type_mesure deleteMeasureType(int id)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            type_mesure searchObject = param.type_mesure.Find(id);
            if (searchObject != null)
            {
                param.type_mesure.Remove(searchObject);
                param.SaveChanges();
            }
            return null;
        }//fin deleteMeasureType
        /*fin types de mesure*/

        #endregion  types de mesure

        #region  fournisseurs
        //liste des fournisseurs
        public static List<fournisseur> loadFournisseursList(int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            List<fournisseur> _listDtoList = param.fournisseurs
                .Where(x => x.id_entreprise == id_entreprise)
                .OrderBy(x => x.nom)
                .ToList<fournisseur>();
            return _listDtoList;
        }//fin loadFournisseursList
        public static fournisseur createSupplier(fournisseur obj, int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            obj.id_entreprise = id_entreprise;

            param.fournisseurs.Add(obj);
            param.SaveChanges();
            return obj;
        }//fin createSupplier
        public static fournisseur updateSupplier(fournisseur obj)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            fournisseur searchObject = param.fournisseurs.Find(obj.id);
            if (searchObject != null)
            {
                searchObject.nom = obj.nom; searchObject.adresse = obj.adresse;
                searchObject.telephone = obj.telephone; searchObject.email = obj.email;
                param.Entry(searchObject).State = EntityState.Modified;
                param.SaveChanges();
            }
            return searchObject;
        }//fin updateSupplier

        public static fournisseur deleteSupplier(int id)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            fournisseur searchObject = param.fournisseurs.Find(id);
            if (searchObject != null)
            {
                param.fournisseurs.Remove(searchObject);
                param.SaveChanges();
            }
            return null;
        }//fin deleteSupplier
         //fin zone fournisseurs

        #endregion  fournisseurs

        #region para-mecef
        public static List<param_mecef> loadParamMecefList()
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            List<param_mecef> _listDtoList = new List<param_mecef>();

            _listDtoList = param.param_mecef.Where(x => x.id != 0).ToList<param_mecef>();
            return _listDtoList;

        }//fin loadParamMecefList

        public static update_mecef updateParamMecef(update_mecef obj)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            param_mecef searchOne = param.param_mecef.Where(x => x.code == "PORT").FirstOrDefault();
            param_mecef searchTwo = param.param_mecef.Where(x => x.code == "MECEF").FirstOrDefault();
            param_mecef searchThree = param.param_mecef.Where(x => x.code == "DEF_TAX").FirstOrDefault();
            param_mecef searchFour = param.param_mecef.Where(x => x.code == "EMECEF").FirstOrDefault();
            param_mecef searchFive = param.param_mecef.Where(x => x.code == "AIB").FirstOrDefault();

            if (searchOne != null)
            {
                searchOne.valeur = obj.port;
                param.SaveChanges();
            }
            if (searchTwo != null)
            {
                searchTwo.valeur = obj.mecef;
                param.SaveChanges();
            }
            if (searchThree != null)
            {
                searchThree.valeur = obj.g_tax;
                param.SaveChanges();
            }
            if (searchFour != null)
            {
                searchFour.valeur = obj.emecef;
                param.SaveChanges();
            }
            if (searchFive != null)
            {
                searchFive.valeur = obj.aib;
                param.SaveChanges();
            }
            return null;
        }//fin updateAgence
        #endregion param-mecef


        #region  rayons
        //liste des rayons
        public static List<rayon> loadRayonsList(int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            profil agent = MainUtil.getProfil(param, idProfil);

            //get profiles roles
            var loggedUserRoles = DtoParams.loadGroupRolesList(agent.groupe.id);

            groupe_roles CheckAllowData = loggedUserRoles.Where(x => x.role.code == "DA").FirstOrDefault();
            List<rayon> _listDtoList = new List<rayon>();
            if (CheckAllowData.statut == true)
            {
                _listDtoList = param.rayons.Where(x => x.agence.id_entreprise == id_entreprise).ToList<rayon>();
            }
            else
            {
                _listDtoList = param.rayons.Where(x => x.id_agence == agent.id_agence).ToList<rayon>();
            }

            return _listDtoList;
        }//fin loadRayonsList
        public static rayon createCompartment(rayon obj, int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            param.rayons.Add(obj);
            param.SaveChanges();
            return obj;
        }//fin createCompartment
        public static rayon updateCompartment(rayon obj)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            rayon searchObject = param.rayons.Find(obj.id);
            if (searchObject != null)
            {
                searchObject.nom = obj.nom; searchObject.id_agence = obj.id_agence;
                param.Entry(searchObject).State = EntityState.Modified;
                param.SaveChanges();
            }
            return searchObject;
        }//fin updateCompartment

        public static rayon deleteCompartment(int id)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            rayon searchObject = param.rayons.Find(id);
            if (searchObject != null)
            {
                param.rayons.Remove(searchObject);
                param.SaveChanges();
            }
            return null;
        }//fin deleteCompartment
        /*end rayon*/


        #endregion  rayons

        #region  produits
        //liste des produits
        public static List<produit> loadProduitsList(int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            List<produit> _listDtoList = param.produits
                .Where(x => x.id_entreprise == id_entreprise)
                .OrderBy(x => x.nom)
                .ToList<produit>();
            return _listDtoList;
        }//fin loadProduitsList

        //liste des produits
        public static List<produit> searchProduitsList(int idProfil, string productName, string productCode)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            List<produit> _listDtoList = param.produits
                .Where(x => x.id_entreprise == id_entreprise
                       && x.code_interne.Trim().ToLower().Contains(productCode.Trim().ToLower())
                       && x.nom.Trim().ToLower().Contains(productName.Trim().ToLower()))
                .OrderBy(x => x.nom)
                .ToList<produit>();
            return _listDtoList;
        }//fin searchProduitsList

        public static List<stock> loadProduitsStockList(int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            List<stock> _listDtoList = param.stocks
                .Where(x => x.produit_type_mesure.produit.id_entreprise == id_entreprise)
                .Distinct()
                .OrderBy(x => x.produit_type_mesure.produit.nom)
                .ToList();
            return _listDtoList;
        }//fin loadProduitsStockList

        /// <summary>
        /// loadProduitMesureItem
        /// </summary>
        /// <returns>The produit mesure item.</returns>
        /// <param name="idProdMesure">Identifier prod mesure.</param>
        public static produit_type_mesure loadProduitMesureItem(int idProdMesure)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            produit_type_mesure dto = param.produit_type_mesure.Find(idProdMesure);
            return dto;
        }//fin loadProduitMesureItem

        public static produit_stock_limit loadProduitStockLimitItem(int idProdMesure)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            produit_stock_limit dto = param.produit_stock_limit.Find(idProdMesure);
            return dto;
        }//fin loadProduitMesureItem

        public static produit createProduct(produit obj, int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);

            //TODO: create a new instance of product


            obj.id_entreprise = id_entreprise;

            //trim the name
            obj.nom = obj.nom.Trim();

            //todo: assurer lunicite du 

            obj.code_fabricant = string.Format("{0}", param.produits.Where(x => x.id_entreprise == id_entreprise).Count() + 1).ToString().PadLeft(10, '0');
            obj.code_interne = obj.code_fabricant;
            param.Entry(obj).State = EntityState.Modified;

            //check product unicity
            List<produit> _listDtoList = new List<produit>();
            if (obj.code_fabricant != null)
            {
                _listDtoList = param.produits.Where(x => x.id_entreprise == id_entreprise &&
                    x.code_fabricant == obj.code_fabricant).ToList<produit>();
                if (_listDtoList.Count != 0) { return null; }
            }
            param.produits.Add(obj);
            param.SaveChanges();

            //create produit_type_mesure && product_stock lines
            if (obj.produit_type_mesure != null)
            {
                foreach (var item in obj.produit_type_mesure)
                {
                    item.id_produit = obj.id;
                    param.produit_type_mesure.Add(item);
                    param.SaveChanges();

                    stock stockElt = new stock();
                    stockElt.qte_reelle = 0;
                    stockElt.qte_vendable = 0;
                    stockElt.date_modif = DateTime.Now;
                    stockElt.p_achat = 1;
                    stockElt.p_vente = 1;
                    stockElt.id_produit_mesure = item.id;
                    stockElt.id_rayon = 1;
                    stockElt.reference = obj.code_fabricant;
                    stockElt.date_exp = null;
                    param.stocks.Add(stockElt);
                    param.SaveChanges();
                }
            }

            if (obj.produit_corresp_mesure != null)
            {
                foreach (var item in obj.produit_corresp_mesure)
                {
                    var newCorrespondance = new produit_corresp_mesure();

                    produit_type_mesure bulkKey = param.produit_type_mesure.Where(x => x.id_produit == obj.id && x.id_type_mesure == item.produit_type_mesure_parent.id_type_mesure).FirstOrDefault();
                    produit_type_mesure retailKey = param.produit_type_mesure.Where(x => x.id_produit == obj.id && x.id_type_mesure == item.produit_type_mesure_enfant.id_type_mesure).FirstOrDefault();

                    newCorrespondance.quantite = item.quantite;
                    newCorrespondance.id_produit_mesure_parent = bulkKey.id;
                    newCorrespondance.id_produit_mesure_enfant = retailKey.id;

                    param.produit_corresp_mesure.Add(newCorrespondance);
                    param.SaveChanges();
                }

            }
            return obj;
        }//fin createProduct

        public static bool createAssociations(List<produit_corresp_mesure> obj)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            param.produit_corresp_mesure.AddRange(obj);
            param.SaveChanges();
            return true;
        }//fin createAssociations


        public static List<produit_type_mesure> updateMeasureTypes(List<produit_type_mesure> objList)
        {
            List<produit_type_mesure> result = new List<produit_type_mesure>();
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            foreach (produit_type_mesure itemMT in objList)
            {
                if (itemMT.id != 0)
                {
                    produit_type_mesure searchMT = param.produit_type_mesure.Find(itemMT.id);
                    searchMT.id_produit = itemMT.id_produit;
                    searchMT.id_type_mesure = itemMT.id_type_mesure;
                    param.Entry(searchMT).State = EntityState.Modified;
                    result.Add(searchMT);
                }
                else
                {
                    param.produit_type_mesure.Add(itemMT);
                    result.Add(itemMT);
                }
            }
            param.SaveChanges();
            return result;
        }//fin updateMeasureTypes


        public static bool updateAssociations(List<produit_corresp_mesure> objList)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            foreach (produit_corresp_mesure itemCM in objList)
            {
                if (itemCM.id != 0)
                {
                    produit_corresp_mesure searchCM = param.produit_corresp_mesure.Find(itemCM.id);
                    searchCM.id_produit_mesure_enfant = itemCM.id_produit_mesure_enfant;
                    searchCM.id_produit_mesure_parent = itemCM.id_produit_mesure_parent;
                    searchCM.quantite = itemCM.quantite;
                    param.Entry(searchCM).State = EntityState.Modified;
                }
                else
                {
                    param.produit_corresp_mesure.Add(itemCM);
                }
            }
            param.SaveChanges();
            return true;
        }//fin updateAssociations


        public static produit updateProduct(produit obj)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            produit searchObject = param.produits.Find(obj.id);
            if (searchObject != null)
            {
                //update product simple object first
                searchObject.nom = obj.nom;
                searchObject.code_fabricant = obj.code_fabricant;
                searchObject.code_interne = obj.code_interne;
                searchObject.id_categ = obj.id_categ;
                param.Entry(searchObject).State = EntityState.Modified;
                param.SaveChanges();
            }
            return searchObject;
        }//fin updateCompartment

        public static produit deleteProduct(int id)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            produit searchObject = param.produits.Find(id);
            if (searchObject != null)
            {
                //delete association
                foreach (produit_type_mesure item in searchObject.produit_type_mesure)
                {
                    List<produit_corresp_mesure> pElements = param.produit_corresp_mesure.Where(x => x.id_produit_mesure_enfant == item.id || x.id_produit_mesure_parent == item.id).ToList();
                    if (pElements != null)
                    {
                        param.produit_corresp_mesure.RemoveRange(pElements);
                    }
                };

                //delete stock
                foreach (produit_type_mesure item in searchObject.produit_type_mesure)
                {
                    stock stock = param.stocks.Where(x => x.id_produit_mesure == item.id).FirstOrDefault();
                    param.stocks.Remove(stock);
                };

                //remove measure type
                param.produit_type_mesure.RemoveRange(searchObject.produit_type_mesure);

                //remove product
                param.produits.Remove(searchObject);
                param.SaveChanges();
            }
            return null;
        }//fin deleteProduct

        public static produit_type_mesure deleteProductMeasureType(produit_type_mesure obj)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            produit_type_mesure searchObject = param.produit_type_mesure.Find(obj.id);
            if (searchObject != null)
            {
                param.produit_type_mesure.Remove(searchObject);
                param.SaveChanges();
            }
            return null;
        }//fin deleteProductMeasureType

        public static produit deleteProductAssociation(produit_corresp_mesure obj)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            produit_corresp_mesure searchObject = param.produit_corresp_mesure.Find(obj.id);
            if (searchObject != null)
            {
                param.produit_corresp_mesure.Remove(searchObject);
                param.SaveChanges();
            }
            return null;
        }//fin deleteProductAssociation


        #endregion  produits

        #region  groupes

        public static groupe createGroup(groupe obj, List<role> roles, int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            int countRoles = roles.Where(x => x.statut == true).Count();
            obj.nb_roles = countRoles;
            param.groupes.Add(obj);
            param.SaveChanges();

            foreach (role itemRole in roles)
            {
                groupe_roles newGroupe_Role = new groupe_roles();
                newGroupe_Role.id_groupe = obj.id;
                newGroupe_Role.id_role = itemRole.id;
                newGroupe_Role.statut = itemRole.statut;
                param.groupe_roles.Add(newGroupe_Role);
                //add historique
                param.SaveChanges();
            }

            return obj;
        }//fin createGroup

        public static groupe updateGroup(groupe obj, List<role> roles, int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            groupe searchObject = param.groupes.Find(obj.id);
            if (searchObject != null)
            {
                searchObject.nom = obj.nom;
                searchObject.code = obj.nom;
                int countRoles = roles.Where(x => x.statut == true).Count();
                searchObject.nb_roles = countRoles;
                param.Entry(searchObject).State = EntityState.Modified;
                param.SaveChanges();

                foreach (role itemRole in roles)
                {
                    groupe_roles searchRole = param.groupe_roles.Select(x => x).Where(x => x.id_role == itemRole.id && x.id_groupe == obj.id).ToList<groupe_roles>()[0];
                    searchRole.statut = itemRole.statut;
                    param.Entry(searchRole).State = EntityState.Modified;
                    param.SaveChanges();
                }
            }
            return searchObject;
        }//fin updateGroup

        //liste des groupes
        public static List<groupe> loadGroupesList()
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            List<groupe> _listDtoList = param.groupes.Select(x => x).ToList<groupe>();
            return _listDtoList;
        }//fin loadGroupesList

        //liste des groupes
        public static groupe loadGroup(int idGroup)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            groupe _listDtoList = param.groupes.Select(x => x).Where(x => x.id == idGroup).ToList<groupe>()[0];
            return _listDtoList;
        }//fin loadGroupesList
        #endregion  groupes


        #region groupe_roles

        public static groupe_roles createGroupeRoles(groupe_roles obj, int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            param.groupe_roles.Add(obj);
            param.SaveChanges();
            return obj;
        }//fin createGroup

        public static groupe_roles updateGroupeRoles(groupe_roles obj)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            groupe_roles searchObject = param.groupe_roles.Find(obj.id);
            if (searchObject != null)
            {
                searchObject.statut = obj.statut;
                param.Entry(searchObject).State = EntityState.Modified;
                param.SaveChanges();
            }
            return searchObject;
        }//fin updateGroup

        //liste des groupes
        public static List<groupe_roles> loadGroupRolesList(int idGroup)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            List<groupe_roles> _listDtoList = param.groupe_roles.Select(x => x).Where(x => x.id_groupe == idGroup).ToList<groupe_roles>();
            return _listDtoList;
        }//fin loadGroupesList
        #endregion  groupe_roles

        #region roles
        //liste des roles
        public static List<role> loadRolesList()
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            List<role> _listDtoList = param.roles.Select(x => x).OrderBy(x => x.id).ToList<role>();
            return _listDtoList;
        }//fin loadRolesList

        //liste des roles by key
        public static role loadRolesListbyId(int idRole)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            role _listDtoList = param.roles.Select(x => x).Where(x => x.id == idRole).OrderBy(x => x.id).ToList<role>()[0];
            return _listDtoList;
        }//fin loadRolesList
        #endregion roles


        #region objectif_vente
        /*start objectif_vente*/

        //liste des objectif_vente
        public static List<objectif_vente> loadObjectifVentesList(int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_agence = MainUtil.getAgenceId(param, idProfil);
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            profil loggedUser = MainUtil.getProfil(param, idProfil);
            if (loggedUser == null) return new List<objectif_vente>();

            //get profiles roles
            var loggedUserRoles = DtoParams.loadGroupRolesList(loggedUser.groupe.id);
            if (loggedUserRoles.Count() == 0)
            {
                throw new InvalidOperationException("Erreur groupe role");
            }
            groupe_roles CheckAllowData = loggedUserRoles.Where(x => x.role.code == "DA").FirstOrDefault();

            IQueryable<objectif_vente> filteredDtoList;
            if (CheckAllowData.statut == true)
            {
                filteredDtoList = param.objectif_vente.Where(x => x.agence.id_entreprise == id_entreprise);
            }
            else
            {
                filteredDtoList = param.objectif_vente.Where(x => x.id_agence == id_agence && x.id_profil == idProfil);
            }
            List<objectif_vente> _listDtoList = filteredDtoList.Select(x => x).OrderByDescending(x => x.id).ToList<objectif_vente>();
            foreach (objectif_vente item in _listDtoList)
            {
                if (item.montant_atteint >= item.montant_objectif) { item.est_atteint = true; } else { item.est_atteint = false; }

            }
            return _listDtoList;
        }//fin loadObjectifVentesList

        public static List<objectif_vente> loadObjectifVentesList(DateTime startDate, DateTime endDate, int idProfil)
        {
            startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, 00, 00, 00);
            endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, 23, 59, 59);

            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_agence = MainUtil.getAgenceId(param, idProfil);
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            profil loggedUser = MainUtil.getProfil(param, idProfil);
            if (loggedUser == null) return new List<objectif_vente>();

            //get profiles roles
            var loggedUserRoles = DtoParams.loadGroupRolesList(loggedUser.groupe.id);
            groupe_roles CheckAllowData = loggedUserRoles.Where(x => x.role.code == "DA").FirstOrDefault();

            //filter by user depending on profile group
            IQueryable<objectif_vente> filteredDtoList;
            if (CheckAllowData.statut == true)
            {
                filteredDtoList = param.objectif_vente.Where(x => x.agence.id_entreprise == id_entreprise);
            }
            else
            {
                filteredDtoList = param.objectif_vente.Where(x => x.id_agence == id_agence);
            }
            List<objectif_vente> _listDtoList = filteredDtoList
                .Where(x => x.date_debut.CompareTo(startDate) > 0 && x.date_fin.CompareTo(endDate) < 0)
                .OrderByDescending(x => x.id).ToList<objectif_vente>();
            foreach (objectif_vente item in _listDtoList)
            {
                if (item.montant_atteint >= item.montant_objectif) { item.est_atteint = true; } else { item.est_atteint = false; }
            }

            return _listDtoList;
        }//fin loadObjectifVentesList


        public static objectif_vente createObjectifVente(objectif_vente obj, int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_agence = MainUtil.getAgenceId(param, idProfil);
            obj.id_agence = id_agence;

            param.objectif_vente.Add(obj);
            param.SaveChanges();
            rapport_utilisateur getStats = DtoStats.GetTotalSalesByProfil(obj.date_debut, obj.date_fin, obj.id_profil)[0];
            obj.montant_atteint = (int)getStats.TotalVentes;
            param.Entry(obj).State = EntityState.Modified;

            //mis à jour du statut de l'objectif
            if (obj.montant_atteint >= obj.montant_objectif)
            {
                obj.est_atteint = true; param.Entry(obj).State = EntityState.Modified;
            }
            else
            {
                obj.est_atteint = false; param.Entry(obj).State = EntityState.Modified;
            }
            param.SaveChanges();
            return obj;
        }//fin createObjectifVente



        public static objectif_vente updateObjectifVente(objectif_vente obj)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            objectif_vente searchObject = param.objectif_vente.Find(obj.id);
            if (searchObject != null)
            {
                searchObject.date_debut = obj.date_debut; searchObject.date_fin = obj.date_fin;
                searchObject.id_profil = obj.id_profil; searchObject.id_profil = obj.id_profil;
                searchObject.montant_objectif = obj.montant_objectif;
                param.Entry(searchObject).State = EntityState.Modified;
                param.SaveChanges();

                rapport_utilisateur getStats = DtoStats.GetTotalSalesByProfil(searchObject.date_debut, searchObject.date_fin, searchObject.id_profil)[0];
                searchObject.montant_atteint = (int)getStats.TotalVentes;
                param.Entry(searchObject).State = EntityState.Modified;

                //mis à jour du statut de l'objectif
                if (searchObject.montant_atteint >= searchObject.montant_objectif)
                {
                    searchObject.est_atteint = true; param.Entry(searchObject).State = EntityState.Modified;
                }
                else
                {
                    searchObject.est_atteint = false; param.Entry(searchObject).State = EntityState.Modified;
                }
                param.SaveChanges();
            }
            return searchObject;
        }//fin updateObjectifVente

        public static objectif_vente deleteObjectifVente(int id)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            objectif_vente searchObject = param.objectif_vente.Find(id);
            if (searchObject != null)
            {
                param.objectif_vente.Remove(searchObject);
                param.SaveChanges();
            }
            return null;
        }//fin deleteObjectifVente

        /*end objectif_vente*/

        #endregion objectif_vente

        #region banque
        //liste des banques
        public static List<banque> loadBanquesList(int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            List<banque> _listDtoList = param.banques.Where(x => x.id_entreprise == id_entreprise).ToList<banque>();
            return _listDtoList;
        }//fin loadBanquesList

        public static banque createBanque(banque obj, int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            obj.id_entreprise = id_entreprise;

            param.banques.Add(obj);
            param.SaveChanges();
            return obj;
        }//fin createBanque

        public static banque updateBanque(banque obj)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            banque searchObject = param.banques.Find(obj.id);
            if (searchObject != null)
            {
                searchObject.nom = obj.nom; searchObject.adresse = obj.adresse; searchObject.contact = obj.contact;
                param.Entry(searchObject).State = EntityState.Modified;
                param.SaveChanges();
            }
            return searchObject;
        }//fin updateBanque

        public static banque deleteBanque(int id)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            banque searchObject = param.banques.Find(id);
            if (searchObject != null)
            {
                param.banques.Remove(searchObject);
                param.SaveChanges();
            }
            return null;
        }//fin deleteBanque


        #endregion banque

        #region types de compte
        //liste des types de comptes
        public static List<type_compte> loadTypeCompteList(int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            List<type_compte> _listDtoList = param.type_compte.ToList<type_compte>();
            return _listDtoList;
        }//fin loadTypeCompteList
        #endregion

        #region les logs
        //liste des logs
        public static List<log> loadLogList(int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            List<log> _listDtoList = param.logs.Where(x => x.id_entreprise == id_entreprise)
                .OrderByDescending(x => x.date_log)
                .ToList<log>();
            return _listDtoList;
        }//fin loadLogList

        //liste des logs
        public static List<log> loadLogList(DateTime startDate, DateTime endDate, int idProfil)
        {
            startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, 00, 00, 00);
            endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, 23, 59, 59);

            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            List<log> _listDtoList = param.logs.Where(x => x.id_entreprise == id_entreprise)
                .Where(x => x.date_log.CompareTo(startDate) > 0 && x.date_log.CompareTo(endDate) < 0)
                .OrderByDescending(x => x.date_log)
                .ToList<log>();
            return _listDtoList;
        }//fin loadLogList

        #endregion

        public static entreprise loadUserCompany(int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            entreprise _company = param.entreprises.Find(id_entreprise);
            return _company;
        }//fin loadUserCompany

    }
}
