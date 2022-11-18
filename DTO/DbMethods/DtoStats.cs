using ItCommerce.DTO.Factory;
using ItCommerce.DTO.ModelDesign;
using ItCommerce.DTO.SpecClasses;
using ItCommerce.DTO.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ItCommerce.DTO.DbMethods
{
    public class DtoStats : IDto
    {
        /// <summary>
        /// Point pour periode de facon globale
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="idProfil"></param>
        /// <returns></returns>
        public static rapport_global GetReport(DateTime startDate, DateTime endDate, int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            int id_agence = MainUtil.getAgenceId(param, idProfil);
            profil loggedUser = MainUtil.getProfil(param, idProfil);

            //get profiles roles
            var loggedUserRoles = DtoParams.loadGroupRolesList(loggedUser.groupe.id);
            if(loggedUserRoles.Count() == 0)
            {
                throw new InvalidOperationException("Erreur groupe role");
            }
            try
            {
                groupe_roles CheckAllowData = loggedUserRoles.Where(x => x.role.code == "DA").FirstOrDefault();
                //filter list ventes details by user depending on profile group
                IQueryable<vente_produit_details> filteredSoldDetailsDtoList;
                if (CheckAllowData.statut == true)
                {
                    filteredSoldDetailsDtoList = param.vente_produit_details.Where(x => x.vente.agence.id_entreprise == id_entreprise && x.vente.date_vente > startDate && x.vente.date_vente < endDate);
                }
                else
                {
                    filteredSoldDetailsDtoList = param.vente_produit_details.Where(x => x.vente.id_agence == id_agence && x.vente.date_vente > startDate && x.vente.date_vente < endDate);
                }

                //filter list commandes by user depending on profile group
                IQueryable<commande> filteredOrderDtoList;
                if (CheckAllowData.statut == true)
                {
                    filteredOrderDtoList = param.commandes.Where(x => x.rayon.agence.id_entreprise == id_entreprise && x.date_cmde > startDate && x.date_cmde < endDate);
                }
                else
                {
                    filteredOrderDtoList = param.commandes.Where(x => x.rayon.id_agence == id_agence && x.date_cmde > startDate && x.date_cmde < endDate);
                }


                //result
                rapport_global pointPeriode = new rapport_global();
                pointPeriode.DateDebut = startDate; pointPeriode.DateFin = endDate;

                try
                {
                    pointPeriode.QteVendue = filteredSoldDetailsDtoList.Sum(x => (int?)x.quantite);
                    pointPeriode.TotalVentes = filteredSoldDetailsDtoList.Sum(x => (int?)(x.quantite * (x.p_vente)));
                    pointPeriode.TotalBenefices = filteredSoldDetailsDtoList.Sum(x => (int?)(x.quantite * (x.p_vente - x.p_achat)));
                    pointPeriode.TotalCommandes = filteredOrderDtoList.Sum(x => (int?)x.montant_sorti);
                    //zone produit perf

                    //produit plus vendu en terme de quantite vendue
                    var produitPlusVendu = filteredSoldDetailsDtoList.ToList()
                        .GroupBy(x => x.produit_type_mesure.id)
                        .Select(grp => new { number = grp.Key, sum = grp.Sum(x => x.quantite), product = grp.FirstOrDefault<vente_produit_details>().produit_type_mesure })
                        .OrderByDescending(x => x.sum).ToList();
                    stock produitPlusVenduAsStk = new stock();

                    if (produitPlusVendu.Count != 0)
                    {
                        produitPlusVenduAsStk.produit_type_mesure = produitPlusVendu.First().product;
                        produitPlusVenduAsStk.qte_reelle = produitPlusVendu.First().sum;
                    }
                    pointPeriode.ProdPlusVendu = produitPlusVenduAsStk;

                    //produit plus CA: plus vendu en terme de chiffre daffaire
                    var produitPlusCA = filteredSoldDetailsDtoList.ToList()
                        .GroupBy(x => x.produit_type_mesure.id)
                        .Select(grp => new { number = grp.Key, sum = grp.Sum(x => x.quantite * x.p_vente), product = grp.FirstOrDefault<vente_produit_details>().produit_type_mesure })
                        .OrderByDescending(x => x.sum).ToList();
                    stock produitPlusCAAsStk = new stock();
                    if (produitPlusCA.Count != 0)
                    {
                        produitPlusCAAsStk.produit_type_mesure = produitPlusVendu.First().product;
                        produitPlusCAAsStk.qte_reelle = produitPlusCA.First().sum;
                    }
                    pointPeriode.ProdPlusCA = produitPlusCAAsStk;

                    //produit plus rentable: en terme de benefice
                    var produitPlusRentable = filteredSoldDetailsDtoList.ToList()
                        .GroupBy(x => x.produit_type_mesure.id)
                        .Select(grp => new { number = grp.Key, sum = grp.Sum(x => x.quantite * (x.p_vente - x.p_achat)), product = grp.FirstOrDefault<vente_produit_details>().produit_type_mesure })
                        .OrderByDescending(x => x.sum).ToList();
                    stock produitPlusRentableAsStk = new stock();
                    if (produitPlusRentable.Count != 0)
                    {
                        produitPlusRentableAsStk.produit_type_mesure = produitPlusRentable.First().product;
                        produitPlusRentableAsStk.qte_reelle = produitPlusRentable.First().sum;
                    }
                    pointPeriode.ProdPlusRentable = produitPlusRentableAsStk;
                }
                catch (Exception)
                {
                    pointPeriode = new rapport_global();
                    //throw;
                }


                return pointPeriode;
            }catch(Exception ex)
            {
                throw new InvalidOperationException("Une erreur est survenue");
            }
            

        }//fin GetReport

        /// <summary>
        /// Point pour periode de facon globale
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="idProfil"></param>
        /// <returns></returns>
        public static rapport_global GetReportByProduct(DateTime startDate, DateTime endDate, int idProfil, int idProductMeasure)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            int id_agence = MainUtil.getAgenceId(param, idProfil);
            profil loggedUser = MainUtil.getProfil(param, idProfil);

            //get profiles roles
            var loggedUserRoles = DtoParams.loadGroupRolesList(loggedUser.groupe.id);
            groupe_roles CheckAllowData = loggedUserRoles.Where(x => x.role.code == "DA").FirstOrDefault();

            //filter list detail_ventes by user depending on profile group
            IQueryable<vente_produit_details> detailsSoldDtoList;
            if (CheckAllowData.statut == true)
            {
                detailsSoldDtoList = param.vente_produit_details.Where(x => x.vente.agence.id_entreprise == id_entreprise && x.vente.date_vente > startDate && x.vente.date_vente < endDate
                && x.id_produit_mesure == idProductMeasure);
            }
            else
            {
                detailsSoldDtoList = param.vente_produit_details.Where(x => x.vente.id_agence == id_agence && x.vente.date_vente > startDate && x.vente.date_vente < endDate
                && x.id_produit_mesure == idProductMeasure);
            }

            //filter list commandes by user depending on profile group
            IQueryable<commande_details> filteredOrderDetailsrDtoList;
            if (CheckAllowData.statut == true)
            {
                filteredOrderDetailsrDtoList = param.commande_details.Where(x => x.commande.rayon.agence.id_entreprise == id_entreprise && x.commande.date_cmde > startDate && x.commande.date_cmde < endDate
                && x.id_produit_mesure == idProductMeasure);
            }
            else
            {
                filteredOrderDetailsrDtoList = param.commande_details.Where(x => x.commande.rayon.id_agence == id_agence && x.commande.date_cmde > startDate && x.commande.date_cmde < endDate
                    && x.id_produit_mesure == idProductMeasure);
            }


            //result
            rapport_global pointPeriode = new rapport_global();
            pointPeriode.DateDebut = startDate; pointPeriode.DateFin = endDate;

            try
            {
                pointPeriode.QteVendue = detailsSoldDtoList.Sum(x => (double?)x.quantite);
                pointPeriode.TotalVentes = detailsSoldDtoList.Sum(x => (double?)(x.p_vente * x.quantite));
                pointPeriode.TotalBenefices = detailsSoldDtoList.Sum(x => (double?)(x.quantite * (x.p_vente - x.p_achat)));

                pointPeriode.QteCommandee = filteredOrderDetailsrDtoList.Sum(x => (double?)x.qte_cmde);
                pointPeriode.TotalCommandes = filteredOrderDetailsrDtoList.Sum(x => (double?)(x.qte_cmde * x.p_achat));
            }
            catch (Exception ex)
            {
                pointPeriode = new rapport_global();
                //throw;
            }

            return pointPeriode;
        }//fin GetReportByProduct

        /// <summary>
        /// Point des produits vendus classes par quantités decroissantes
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="idProfil"></param>
        /// <returns></returns>
        public static List<stock> GetMostSoldProdByQuantityReport(DateTime startDate, DateTime endDate, int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            int id_agence = MainUtil.getAgenceId(param, idProfil);
            profil loggedUser = MainUtil.getProfil(param, idProfil);

            //get profiles roles
            var loggedUserRoles = DtoParams.loadGroupRolesList(loggedUser.groupe.id);
            groupe_roles CheckAllowData = loggedUserRoles.Where(x => x.role.code == "DA").FirstOrDefault();

            //filter list ventes details by user depending on profile group
            IQueryable<vente_produit_details> filteredSoldDetailsDtoList;
            if (CheckAllowData.statut == true)
            {
                filteredSoldDetailsDtoList = param.vente_produit_details.Where(x => x.vente.agence.id_entreprise == id_entreprise && x.vente.date_vente > startDate && x.vente.date_vente < endDate);
            }
            else
            {
                filteredSoldDetailsDtoList = param.vente_produit_details.Where(x => x.vente.id_agence == id_agence && x.vente.date_vente > startDate && x.vente.date_vente < endDate);
            }

            //result
            List<stock> produitPlusVenduList = new List<stock>();
            try
            {
                //produits plus vendu en terme de quantite vendue
                var produitPlusVendusSearch = filteredSoldDetailsDtoList.ToList()
                    .GroupBy(x => x.produit_type_mesure.id)
                    .Select(grp => new { number = grp.Key, sum = grp.Sum(x => x.quantite), product = grp.FirstOrDefault<vente_produit_details>().produit_type_mesure })
                    .OrderByDescending(x => x.sum).ToList();

                foreach (var produitItem in produitPlusVendusSearch)
                {
                    stock stockItem = new stock();
                    stockItem.produit_type_mesure = produitItem.product;
                    stockItem.qte_reelle = produitItem.sum;
                    stockItem.qte_vendable = produitItem.sum;
                    produitPlusVenduList.Add(stockItem);
                }
            }
            catch (Exception ex)
            {
                return produitPlusVenduList;
            }
            return produitPlusVenduList;
        }//fin GetMostSoldProdByQuantityReport

        /// <summary>
        /// Point des produits vendus classes par chiffres d'affaires decroissants
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="idProfil"></param>
        /// <returns></returns>
        public static List<stock> GetMostSoldProdByCaReport(DateTime startDate, DateTime endDate, int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            int id_agence = MainUtil.getAgenceId(param, idProfil);
            profil loggedUser = MainUtil.getProfil(param, idProfil);

            //get profiles roles
            var loggedUserRoles = DtoParams.loadGroupRolesList(loggedUser.groupe.id);
            groupe_roles CheckAllowData = loggedUserRoles.Where(x => x.role.code == "DA").FirstOrDefault();

            //filter list ventes details by user depending on profile group
            IQueryable<vente_produit_details> filteredSoldDetailsDtoList;
            if (CheckAllowData.statut == true)
            {
                filteredSoldDetailsDtoList = param.vente_produit_details.Where(x => x.vente.agence.id_entreprise == id_entreprise && x.vente.date_vente > startDate && x.vente.date_vente < endDate);
            }
            else
            {
                filteredSoldDetailsDtoList = param.vente_produit_details.Where(x => x.vente.id_agence == id_agence && x.vente.date_vente > startDate && x.vente.date_vente < endDate);
            }

            //result
            List<stock> produitPlusCaList = new List<stock>();
            try
            {
                //produit plus CA: plus vendu en terme de chiffre daffaire
                var produitPlusCASearch = filteredSoldDetailsDtoList.ToList()
                      .GroupBy(x => x.produit_type_mesure.id)
                      .Select(grp => new { number = grp.Key, sum = grp.Sum(x => x.quantite * x.p_vente), product = grp.FirstOrDefault<vente_produit_details>().produit_type_mesure })
                      .OrderByDescending(x => x.sum).ToList();

                foreach (var produitItem in produitPlusCASearch)
                {
                    stock stockItem = new stock();
                    stockItem.produit_type_mesure = produitItem.product;
                    stockItem.qte_reelle = produitItem.sum;
                    stockItem.qte_vendable = produitItem.sum;
                    produitPlusCaList.Add(stockItem);
                }
            }
            catch (Exception)
            {
                return produitPlusCaList;
            }
            return produitPlusCaList;
        }//fin GetMostSoldProdByCaReport

        /// <summary>
        /// Point des produits vendus classes par bénéfices decroissants
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="idProfil"></param>
        /// <returns></returns>
        public static List<stock> GetMostSoldProdByProfitReport(DateTime startDate, DateTime endDate, int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            int id_agence = MainUtil.getAgenceId(param, idProfil);
            profil loggedUser = MainUtil.getProfil(param, idProfil);

            //get profiles roles
            var loggedUserRoles = DtoParams.loadGroupRolesList(loggedUser.groupe.id);
            groupe_roles CheckAllowData = loggedUserRoles.Where(x => x.role.code == "DA").FirstOrDefault();

            //filter list ventes details by user depending on profile group
            IQueryable<vente_produit_details> filteredSoldDetailsDtoList;
            if (CheckAllowData.statut == true)
            {
                filteredSoldDetailsDtoList = param.vente_produit_details.Where(x => x.vente.agence.id_entreprise == id_entreprise && x.vente.date_vente > startDate && x.vente.date_vente < endDate);
            }
            else
            {
                filteredSoldDetailsDtoList = param.vente_produit_details.Where(x => x.vente.id_agence == id_agence && x.vente.date_vente > startDate && x.vente.date_vente < endDate);
            }

            //result
            List<stock> produitPlusRentableList = new List<stock>();
            try
            {
                //produit plus rentable: en terme de benefice
                var produitPlusRentableSearch = filteredSoldDetailsDtoList.ToList()
                    .GroupBy(x => x.produit_type_mesure.id)
                    .Select(grp => new { number = grp.Key, sum = grp.Sum(x => x.quantite * (x.p_vente - x.p_achat)), product = grp.FirstOrDefault<vente_produit_details>().produit_type_mesure })
                    .OrderByDescending(x => x.sum).ToList();

                foreach (var produitItem in produitPlusRentableSearch)
                {
                    stock stockItem = new stock();
                    stockItem.produit_type_mesure = produitItem.product;
                    stockItem.qte_reelle = produitItem.sum;
                    stockItem.qte_vendable = produitItem.sum;
                    produitPlusRentableList.Add(stockItem);
                }
            }
            catch (Exception)
            {
                return produitPlusRentableList;
            }
            return produitPlusRentableList;
        }//fin GetMostSoldProdByProfitReport

        /// <summary>
        /// Point des totaux de ventes par vendeur
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="idProfil"></param>
        /// <returns></returns>
        public static List<rapport_utilisateur> GetTotalSalesByProfil(DateTime startDate, DateTime endDate, int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            int id_agence = MainUtil.getAgenceId(param, idProfil);
            profil loggedUser = MainUtil.getProfil(param, idProfil);

            //get profiles roles
            var loggedUserRoles = DtoParams.loadGroupRolesList(loggedUser.groupe.id);
            groupe_roles CheckAllowData = loggedUserRoles.Where(x => x.role.code == "DA").FirstOrDefault();

            //filter list ventes by profile depending on profile group
            IQueryable<vente_produit> filteredSoldDtoList;
            if (CheckAllowData.statut == true)
            {
                filteredSoldDtoList = param.vente_produit.Where(x => x.agence.id_entreprise == id_entreprise && x.date_vente > startDate && x.date_vente < endDate);
            }
            else
            {
                filteredSoldDtoList = param.vente_produit.Where(x => x.id_agence == id_agence && x.date_vente > startDate && x.date_vente < endDate);
            }

            //filter list ventes details by user depending on profile group
            IQueryable<vente_produit_details> filteredSoldDetailsDtoList;
            if (CheckAllowData.statut == true)
            {
                filteredSoldDetailsDtoList = param.vente_produit_details.Where(x => x.vente.agence.id_entreprise == id_entreprise && x.vente.date_vente > startDate && x.vente.date_vente < endDate);
            }
            else
            {
                filteredSoldDetailsDtoList = param.vente_produit_details.Where(x => x.vente.id_agence == id_agence && x.vente.date_vente > startDate && x.vente.date_vente < endDate);
            }

            //result
            rapport_global pointPeriode = new rapport_global();
            pointPeriode.DateDebut = startDate; pointPeriode.DateFin = endDate;

            List<rapport_utilisateur> profilsPlusCAList = new List<rapport_utilisateur>();
            try
            {
                pointPeriode.TotalVentes = filteredSoldDetailsDtoList.Sum(x => (int?)(x.quantite * (x.p_vente)));
                //vendeurs qui apportent plus de chiffre daffaires, donc qui font plus de ventes
                var profilPlusCASearch = filteredSoldDtoList.ToList()
                    .GroupBy(x => x.id_profil)
                    .Select(grp => new
                    {
                        number = grp.Key,
                        sum = grp.Sum(x => x.mt_a_payer),
                        revenue = grp.Sum(x => x.mt_a_payer - x.vente_details.Sum(y => y.p_achat * y.quantite)),
                        qty = grp.Count(),
                        percentage = pointPeriode.TotalVentes,
                        login = grp.FirstOrDefault<vente_produit>().profil.login,
                        user = grp.FirstOrDefault<vente_produit>().profil.user.nom + " " + grp.FirstOrDefault<vente_produit>().profil.user.prenoms,
                        agency = grp.FirstOrDefault<vente_produit>().profil.agence.nom,
                    })
                    .OrderByDescending(x => x.sum).ToList();

                foreach (var produitItem in profilPlusCASearch)
                {
                    rapport_utilisateur userRpt = new rapport_utilisateur();
                    userRpt.login = produitItem.login;
                    userRpt.identite = produitItem.user;
                    userRpt.agence = produitItem.agency;
                    userRpt.pourcentage = (produitItem.sum * 100) / (produitItem.percentage);
                    userRpt.TotalVentes = produitItem.sum;
                    userRpt.TotalBenefices = produitItem.revenue;
                    userRpt.NombreVentes = produitItem.qty;

                    profilsPlusCAList.Add(userRpt);
                }
            }
            catch (Exception)
            {
                return profilsPlusCAList;
            }
            return profilsPlusCAList;
        }//fin GetTotalSalesByProfil

        /// <summary>
        /// Point des totaux de ventes par client
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="idProfil"></param>
        /// <returns></returns>
        public static List<rapport_utilisateur> GetTotalSalesByCustomer(DateTime startDate, DateTime endDate, int idProfil)
        {
            IT_COMMERCEEntities param = ItCommerceFactory.GetEntity();
            int id_entreprise = MainUtil.getEntrepriseId(param, idProfil);
            int id_agence = MainUtil.getAgenceId(param, idProfil);
            profil loggedUser = MainUtil.getProfil(param, idProfil);

            //get profiles roles
            var loggedUserRoles = DtoParams.loadGroupRolesList(loggedUser.groupe.id);
            groupe_roles CheckAllowData = loggedUserRoles.Where(x => x.role.code == "DA").FirstOrDefault();

            //filter list ventes by profile depending on profile group
            IQueryable<vente_produit> filteredSoldDtoList;
            if (CheckAllowData.statut == true)
            {
                filteredSoldDtoList = param.vente_produit.Where(x => x.agence.id_entreprise == id_entreprise && x.date_vente > startDate && x.date_vente < endDate);
            }
            else
            {
                filteredSoldDtoList = param.vente_produit.Where(x => x.id_agence == id_agence && x.date_vente > startDate && x.date_vente < endDate);
            }
            //filteredSoldDtoList = filteredSoldDtoList.Where(x => x.id_client != null);

            //result
            List<rapport_utilisateur> profilsPlusCAList = new List<rapport_utilisateur>();
            try
            {
                //vendeurs qui apportent plus de chiffre daffaires, donc qui font plus de ventes
                var profilPlusCASearch = filteredSoldDtoList.ToList()
                    .GroupBy(x => x.id_client)
                    .Select(grp => new
                    {
                        number = grp.Key,
                        sum = grp.Sum(x => x.mt_a_payer),
                        revenue = grp.Sum(x => x.mt_a_payer - x.vente_details.Sum(y => y.p_achat * y.quantite)),
                        qty = grp.Count(),
                        login = "",
                        id_client = grp.FirstOrDefault<vente_produit>().id_client,
                        identite_client = (grp.FirstOrDefault<vente_produit>().client != null) ?
                        (grp.FirstOrDefault<vente_produit>().client.nom + " " + grp.FirstOrDefault<vente_produit>().client.prenom) : "ANONYME",
                        agency = "",
                    })
                    .OrderByDescending(x => x.sum).ToList();

                foreach (var produitItem in profilPlusCASearch)
                {
                    rapport_utilisateur userRpt = new rapport_utilisateur();
                    userRpt.login = produitItem.login;
                    userRpt.reference = produitItem.id_client;
                    userRpt.identite = produitItem.identite_client;
                    userRpt.agence = produitItem.agency;
                    userRpt.TotalVentes = produitItem.sum;
                    userRpt.TotalBenefices = produitItem.revenue;
                    userRpt.NombreVentes = produitItem.qty;

                    profilsPlusCAList.Add(userRpt);
                }
            }
            catch (Exception ex)
            {
                return profilsPlusCAList;
            }
            return profilsPlusCAList;
        }//fin GetTotalSalesByCustomer


    }
}
