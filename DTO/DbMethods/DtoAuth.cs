using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItCommerce.DTO.ModelDesign;
using ItCommerce.DTO.Factory;
using ItCommerce.DTO.Utils;
using Microsoft.EntityFrameworkCore;
using Utils.IwajuTech.Business.Factories;
using System.Security.Cryptography;
using ItCommerce.Business.Entities;
using XAct;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ItCommerce.DTO.DbMethods
{
    public class DtoAuth : IDto
    {


        #region  users

        
        //recheche de users avce email
        public static utilisateur searchUserWithEmail(string email)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            List<utilisateur> _getUser = param.users.Where(x => x.email.Trim().Equals(email)).Select(x => x).ToList<utilisateur>();
            if (_getUser.Count() == 0)
            {
                throw new InvalidOperationException("L'utilisateur de cette opération n'existe pas");
            }
            utilisateur info = _getUser.First<utilisateur>();

            return info;
      
        }//fin loadAgentsList

        //liste des users
        public static List<utilisateur> loadAgentsList(int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            List<utilisateur> _listDtoList = param.users.Where(x => x.id_entreprise == id_entreprise).ToList<utilisateur>();
            return _listDtoList;
        }//fin loadAgentsList


        public static utilisateur createAgent(utilisateur obj, int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            obj.id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            obj.date_creation = DateTime.Now;
            param.users.Add(obj);
            param.SaveChanges();
            return obj;
        }//fin createAgent

        public static utilisateur updateAgent(utilisateur obj)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            utilisateur searchObject = param.users.Find(obj.id);
            if (searchObject != null)
            {
                searchObject.nom = obj.nom; searchObject.prenoms = obj.prenoms; searchObject.adresse = obj.adresse;
                searchObject.telephone = obj.telephone; searchObject.email = obj.email;
                param.Entry(searchObject).State = EntityState.Modified;
                param.SaveChanges();
            }
            return searchObject;
        }//fin updateAgent

        public static utilisateur deleteAgent(int id)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            utilisateur searchObject = param.users.Find(id);
            if (searchObject != null)
            {
                param.users.Remove(searchObject);
                param.SaveChanges();
            }
            return null;
        }//fin deleteAgent
         //fin zone users


        //vérifié le compte de l'utilisateur
        public static utilisateur verifiedUser(string key)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            List<utilisateur> searchUserByToken = param.users.Where(x => x.token.Trim().Equals(key)).Select(x => x).ToList<utilisateur>();

            if (searchUserByToken.Count() == 0)
            {
                throw new InvalidOperationException("Le code de validation n'est pas valide");
            }


            utilisateur foundUser = searchUserByToken.First<utilisateur>();

            if (foundUser.date_verification != null)
            {
                return null;
            }

            foundUser.date_verification = DateTime.Now;
            param.SaveChanges();

            return foundUser;
        }//fin vérifié le compte de l'utilisateur

        #endregion  users


        #region  profils

        public static profil signin(profil obj)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();

            List<profil> searchObjectList = param.profils.Where(x =>
            x.login.Trim().Equals(obj.login.Trim()) && x.pwd.Trim().Equals(obj.pwd.Trim())).Select(x => x).ToList<profil>();

            if (searchObjectList.Count == 0)
            {
                throw new InvalidOperationException("Profil non trouvé");
                //return new profil();
            }

            
            profil foundProfil = searchObjectList.First();

            utilisateur checkId = param.users.Where(x => x.id == foundProfil.id_user ).FirstOrDefault();
            if (checkId.date_verification == null )
            {
                throw new InvalidOperationException("Votre compte n'a pas encore été vérifié");
            }

            if (param.users.Count() == 0)
            {
                throw new InvalidOperationException("Aucune Licence trouvée sur cette instance de ISHOWO");
            }


            //get last activity
            if (param.licences.Count() == 0)
            {
                throw new InvalidOperationException("Aucune Licence trouvée sur cette instance de ISHOWO");
            }


            licence lastLicence = param.licences.Where(x => x.id_entreprise == foundProfil.agence.id_entreprise)
                .OrderByDescending(x => x.id)
                .First<licence>();
            if (lastLicence == null)
            {
                //return new profil();
                throw new InvalidOperationException("Licence non trouvée");
            }
            else
            {
                //current date and licence dates
                if (lastLicence.date_activation.CompareTo(DateTime.Now) > 0)
                {
                    //return new profil();
                    throw new InvalidOperationException("Date activation non valide");
                }
                if (lastLicence.date_expiration.CompareTo(DateTime.Now) < 0)
                {
                    //return new profil(); 
                    throw new InvalidOperationException("Date d'expiration non valide");
                }

                //current date and last log
                int countLogs = param.logs.Count(x => x.id_entreprise == foundProfil.agence.id_entreprise);
                if (countLogs == 0)
                {
                    //return new profil();
                    throw new InvalidOperationException("Aucune activité trouvée");
                }
                else
                {
                    log lastLog = param.logs.Where(x => x.id_entreprise == foundProfil.agence.id_entreprise).OrderByDescending(x => x.id).First<log>();

                    if (lastLog.date_log.CompareTo(DateTime.Now) > 0)
                    {
                        //return new profil(); 
                        throw new InvalidOperationException("Date de la dernière activité invalide");
                    }
                }
            }

            //log the connection
            //create log by the way
            log newLog = new log();
            newLog.categorie = "CONNEXION"; newLog.date_log = DateTime.Now;
            newLog.desc = string.Format("Connexion de l'utilisateur {0} ", foundProfil.login);
            if (foundProfil != null)
            {
                newLog.actor = string.Format("{0} {1}", foundProfil.user.nom, foundProfil.user.prenoms);
                newLog.actor = (newLog.actor.Length > 30) ? newLog.actor.Substring(0, 30) : newLog.actor;
            }
            newLog.id_entreprise = foundProfil.agence.id_entreprise;
            param.logs.Add(newLog);

            //db action
            param.SaveChanges();
            foundProfil.pwd = "";

            return foundProfil;
        }//fin signin

        //liste des clients
        public static List<utilisateur> loadCustomersList(int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            List<utilisateur> _listDtoList = param.users.Where(x => x.id_entreprise == id_entreprise).ToList<utilisateur>();
            return _listDtoList;
        }//fin loadCustomersList


        //liste des profils
        public static List<profil> loadProfilsList(int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            int id_agence = MainUtil.getAgenceId(param, idProfil);
            profil agent = MainUtil.getProfil(param, idProfil);

            //get profiles roles
            var loggedUserRoles = DtoParams.loadGroupRolesList(agent.groupe.id);
            groupe_roles CheckAllowData = loggedUserRoles.Where(x => x.role.code == "DA").FirstOrDefault();

            List<profil> _listDtoList = new List<profil>();
            if (CheckAllowData.statut == true)
            {
                _listDtoList = param.profils.Where(x => x.agence.id_entreprise == id_entreprise).ToList<profil>();
            }
            else
            {
                _listDtoList = param.profils.Where(x => x.id_agence == id_agence).ToList<profil>();
            }

            return _listDtoList;
        }//fin loadProfilsList

        public static profil createProfile(profil obj, int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            obj.date_creation = DateTime.Now;
            param.profils.Add(obj);
            param.SaveChanges();
            return obj;
        }//fin createProfile

        public static profil updateProfil(profil obj)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            profil searchObject = param.profils.Find(obj.id);
            if (searchObject != null)
            {
                searchObject.login = obj.login;
                searchObject.id_groupe = obj.id_groupe; searchObject.id_user = obj.id_user;
                searchObject.id_agence = obj.id_agence;
                param.Entry(searchObject).State = EntityState.Modified;
                param.SaveChanges();
            }
            return searchObject;
        }//fin updateProfil


        public static entreprise updateCompanyProfil(entreprise obj)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            entreprise searchObject = param.entreprises.Find(obj.id);
            if (searchObject != null)
            {
                searchObject.raison_sociale = obj.raison_sociale; searchObject.adresse = obj.adresse;
                searchObject.telephone = obj.telephone; searchObject.secteur_activite = obj.secteur_activite;
                param.Entry(searchObject).State = EntityState.Modified;
                param.SaveChanges();
            }
            return searchObject;
        }//fin updateCompanyProfil

        public static profil updateCompanyLogo(string login, string logo)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();

            List<profil> searchObjectList = param.profils.Where(x => x.login.Trim().Equals(login.Trim()))
                .Select(x => x).ToList<profil>();
            if (searchObjectList.Count == 0) { return new profil(); }
            profil foundProfil = searchObjectList.First();

            //update entreprise
            entreprise entrPrise = foundProfil.agence.entreprise;
            entrPrise.logo = logo;
            param.Entry(entrPrise).State = EntityState.Modified;
            param.SaveChanges();

            foundProfil.agence.entreprise = entrPrise;
            return foundProfil;

        }//fin updateCompanyLogo

        public static profil updateUserProfil(profil obj)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            profil searchObject = param.profils.Find(obj.id);
            if (searchObject != null)
            {
                searchObject.pwd = obj.pwd;
                param.Entry(searchObject).State = EntityState.Modified;
                param.SaveChanges();
            }
            return searchObject;
        }//fin updateUserProfil

        public static profil deleteProfil(int id)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            profil searchObject = param.profils.Find(id);
            if (searchObject != null)
            {
                param.profils.Remove(searchObject);
                param.SaveChanges();
            }
            return null;
        }//fin deleteProfil
        /*end profil*/


        #endregion  profils



        #region licence methods

        public static licence loadLastLicence()
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            List<entreprise> listEntreprises = param.entreprises.ToList<entreprise>();
            if (listEntreprises.Count == 0) { return new licence(); }
            entreprise currentEntreprise = listEntreprises.Last();

            licence _lastLicence = new licence();
            List<licence> _listLicence = param.licences.Where(x => x.id_entreprise == currentEntreprise.id).ToList<licence>();
            if (_listLicence.Count == 0) { return new licence(); }
            else
            {
                _lastLicence = _listLicence.Last();
            }

            return _lastLicence;
        }//fin loadProfilsList

        public static licence saveLicence(licence licence, entreprise entrep, utilisateur admin, string adminPwd, string username, string token)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
           
            //entreprise
            List<entreprise> listEntreprises = param.entreprises.ToList<entreprise>();
            if (listEntreprises.Count != 0)
            {
                entrep = listEntreprises.Last();
            }
            else
            {
                if (entrep.localisation == null) entrep.localisation = "";
                if (entrep.logo == null) entrep.logo = "";
                if (entrep.email == null) entrep.email = "";

                param.entreprises.Add(entrep);
                param.SaveChanges();
            }

            //agence
            agence mainAgency = new agence();
            mainAgency.nom = "AGENCE PRINCIPALE";
            mainAgency.id_entreprise = entrep.id;
            mainAgency.localisation = "";
            param.agences.Add(mainAgency);
            param.SaveChanges();

            //rayon
            rayon mainRayon = new rayon();
            mainRayon.id_agence = mainAgency.id;
            mainRayon.is_magasin = false;
            mainRayon.nom = "RAYON PRINCIPAL";
            mainRayon.id_agence = mainAgency.id;
            param.rayons.Add(mainRayon);
            param.SaveChanges();

            //licence
            licence.id_entreprise = entrep.id;
            param.licences.Add(licence);
            param.SaveChanges();

            //admin
            admin.id_entreprise = entrep.id;
            admin.adresse = "";
            admin.email = admin.email;
            admin.token = token;
            admin.date_creation = DateTime.Now;
            param.users.Add(admin);
            param.SaveChanges();

            //profil admin
            profil adminProfil = new profil();
            adminProfil.login = username;
            adminProfil.pwd = adminPwd;
            adminProfil.id_groupe = MainUtil.getAdminGroupId(param);
            adminProfil.id_agence = param.agences.First<agence>().id;
            adminProfil.id_user = admin.id;
            param.profils.Add(adminProfil);
            param.SaveChanges();

            //first log for company
            log firstLog = new log();
            string adminIdentity = "";
            if (adminProfil.user != null)
            {
                adminIdentity = adminProfil.user.nom + " " + adminProfil.user.prenoms;
                if (adminIdentity.Length > 30) { adminIdentity = adminIdentity.Substring(0, 30); }
            }
            else
            {
                adminIdentity = "ADMIN";
            }

            firstLog.actor = adminIdentity;
            firstLog.date_log = DateTime.Now;
            firstLog.categorie = "ACTIVATION";
            firstLog.id_entreprise = entrep.id;
            firstLog.desc = "ACTIVATION DE L'OFFRE DEMO";
            param.logs.Add(firstLog);
            param.SaveChanges();

            return licence;
        }//fin saveLicence

        

        public static licence updateLicence(licence newLicence)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();

            //last licence
            licence lastLicence = loadLastLicence();
            if (lastLicence == null) return null;

            licence entityLicence = param.licences.Find(lastLicence.id);

            //update that licence
            entityLicence.cle = newLicence.cle;
            entityLicence.date_activation = newLicence.date_activation;
            entityLicence.date_expiration = newLicence.date_expiration;
            entityLicence.est_active = newLicence.est_active;
            entityLicence.code = newLicence.code;

            param.Entry(entityLicence).State = EntityState.Modified;
            param.SaveChanges();



            //first log for company
            log firstLog = new log();
            firstLog.actor = "ADMIN";
            firstLog.date_log = DateTime.Now;
            firstLog.categorie = "ACTIVATION";
            firstLog.id_entreprise = entityLicence.id_entreprise;
            firstLog.desc = "MISE A JOUR DE L OFFRE";
            param.SaveChanges();

            return entityLicence;
        }//fin updateLicence

        /// <summary>
        /// get active admin user
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static profil getActiveProfile(string code)
        {

            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            List<licence> licenceList = param.licences.Where(x => x.code == code).ToList();
            if (licenceList.Count == 0) return null;

            entreprise company = licenceList.Last().entreprise;
            if (company == null) return null;

            int idAdminGroup = MainUtil.getAdminGroupId(param);

            List<profil> profileList = param.profils.Where(x => x.id_groupe == idAdminGroup).ToList();
            if (profileList.Count == 0) return null;

            profil activeUser = profileList.First();
            activeUser.pwd = "";

            return activeUser;

        }//fin getActiveProfile


        #endregion

    }
}
