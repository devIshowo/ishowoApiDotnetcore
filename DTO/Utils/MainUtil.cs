using ItCommerce.DTO.ModelDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItCommerce.DTO.Utils
{
    public class MainUtil
    {

        /// <summary>
        /// get entreprise id
        /// </summary>
        /// <param name="param"></param>
        /// <param name="idProfil"></param>
        /// <returns></returns>
        public static int getEntrepriseId(IT_COMMERCEEntities param, int idProfil)
        {
            List<profil> profilList = param.profils.Where(x => x.id == idProfil).ToList<profil>();
            if (profilList.Count == 0) return 0;
            profil currentProfile = profilList.First<profil>();
            int id_entreprise = currentProfile.agence.id_entreprise;
            return id_entreprise;
        }//fin getentreprise

        public static int getTypeCompteCaisseId(IT_COMMERCEEntities param)
        {
            List<type_compte> typeCompteCaisseList = param.type_compte
                                  //.Include(c => c.Emails)
                                  .Where(x => x.nom.ToLower().Equals("caisse"))
                                                          .ToList<type_compte>();
        if (typeCompteCaisseList.Count == 0) return 0;
            type_compte currentTypeCompte = typeCompteCaisseList.First<type_compte>();
            int id_type_compte = currentTypeCompte.id;
            return id_type_compte;
        }//fin getTypeCompteCaisseId

        //recupere le compte caisse principal
        public static compte getCaisseAccount(IT_COMMERCEEntities param, int idProfil)
        {
            //get company 
            List<profil> profilList = param.profils.Where(x => x.id == idProfil).ToList<profil>();
            if (profilList.Count == 0) return null;
            profil currentProfile = profilList.First<profil>();
            int id_entreprise = currentProfile.agence.id_entreprise;

            //id type compte pr caisse
            int idTypeCompteCaisse = getTypeCompteCaisseId(param);

            //get account
            List<compte> _listDtoList = param.comptes.Where(x => x.id_entreprise == id_entreprise &&
                x.id_type_compte == idTypeCompteCaisse).ToList<compte>();
            if (_listDtoList.Count == 0) return null;

            compte compteCaisse = _listDtoList.First();
            return compteCaisse;
        }//fin getentreprise

        /// get agency id
        /// </summary>
        /// <param name="param"></param>
        /// <param name="idProfil"></param>
        /// <returns></returns>
        public static int getAgenceId(IT_COMMERCEEntities param, int idProfil)
        {
            List<profil> profilList = param.profils.Where(x => x.id == idProfil).ToList<profil>();
            if (profilList.Count == 0) return 0;
            profil currentProfile = profilList.First<profil>();
            int id_agence = currentProfile.id_agence;
            return id_agence;
        }//fin get agence

        /// get profil id
        /// </summary>
        /// <param name="param"></param>
        /// <param name="idProfil"></param>
        /// <returns></returns>
        public static profil getProfil(IT_COMMERCEEntities param, int idProfil)
        {
            List<profil> profilList = param.profils.Where(x => x.id == idProfil).ToList<profil>();
            if (profilList.Count == 0) return null;
            profil currentProfile = profilList.First<profil>();
            return currentProfile;
        }//fin get profil

        /// get profil objectif vente
        /// </summary>
        /// <param name="param"></param>
        /// <param name="idProfil"></param>
        /// <returns></returns>
        public static objectif_vente getUserObjectifVente(IT_COMMERCEEntities param, int idProfil)
        {
            List<objectif_vente> objVenteList = param.objectif_vente.Where(x => x.id_profil == idProfil)
                .OrderByDescending(x => x.id).ToList<objectif_vente>();
            if (objVenteList.Count == 0) return null;
            objectif_vente currentObjVente = objVenteList.First<objectif_vente>();
            return currentObjVente;
        }//fin get objectif_vente

        public static int getAdminGroupId(IT_COMMERCEEntities param)
        {
            List<groupe> groupList = param.groupes.Where(x => x.code == "ADMIN").ToList<groupe>();
            if (groupList.Count == 0) return 0;
            groupe currentGroupe = groupList.First<groupe>();
            int id_groupe = currentGroupe.id;
            return id_groupe;
        }//fin getAdminGroupId

        /// <summary>
        /// genere un codebarre
        /// </summary>
        /// <returns></returns>
        public static string generateReference(IT_COMMERCEEntities param, int size)
        {
            string generated = string.Empty;
            startingPoint:
            {
                StringBuilder builder = new StringBuilder();
                Random random = new Random();
                string ch;
                for (int i = 0; i < size; i++)
                {
                    ch = Convert.ToString(random.Next(10));
                    builder.Append(ch);
                }
                generated = builder.ToString();
                //search it
                int foundStock = param.stocks.Count(x => x.reference.ToLower().Equals(generated));
                if (foundStock > 0) { goto startingPoint; }
            }
            return generated.Substring(0, size);
        }//fin generateReference


        public static DateTime getMinTimeValue()
        {
            DateTime minDatetime = new DateTime(1800, 1, 1);
            return minDatetime;
        }//fin getMinTimeValue

    }
}
