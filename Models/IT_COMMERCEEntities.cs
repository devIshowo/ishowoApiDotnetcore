using System;
using Microsoft.EntityFrameworkCore;


namespace ItCommerce.DTO.ModelDesign
{
    public class IT_COMMERCEEntities : DbContext
    {
        public IT_COMMERCEEntities(DbContextOptions<IT_COMMERCEEntities> dbcontextoption)
            : base(dbcontextoption)
        {
            //dbcontextoption.ContextType.
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //correspondances de mesures
            modelBuilder.Entity<produit_corresp_mesure>().HasOne(a => a.produit_type_mesure_parent)
                        .WithMany(b => b.produit_corresp_mesure_parents).HasForeignKey(c => c.id_produit_mesure_parent)
                        .HasConstraintName("FK_prod_crp_mes_prod_type_mes_id_prod_mes_parent");
            modelBuilder.Entity<produit_corresp_mesure>().HasOne(a => a.produit_type_mesure_enfant)
                        .WithMany(b => b.produit_corresp_mesure_enfants).HasForeignKey(c => c.id_produit_mesure_enfant)
                        .HasConstraintName("FK_prod_crp_mes_prod_type_mes_id_prod_mes_enfant");

        }

        public DbSet<agence> agences { get; set; }
        public DbSet<banque> banques { get; set; }
        public DbSet<categ_produit> categ_produit { get; set; }
        public DbSet<categ_vente> categ_vente { get; set; }
        public DbSet<commande> commandes { get; set; }
        public DbSet<commande_details> commande_details { get; set; }
        public DbSet<compte> comptes { get; set; }

        public DbSet<entreprise> entreprises { get; set; }
        public DbSet<fournisseur> fournisseurs { get; set; }
        public DbSet<groupe> groupes { get; set; }
        public DbSet<groupe_roles> groupe_roles { get; set; }
        public DbSet<role> roles { get; set; }
        public DbSet<licence> licences { get; set; }
        public DbSet<log> logs { get; set; }
        public DbSet<client> clients { get; set; }


        public DbSet<lot> lots { get; set; }
        public DbSet<mouvement_compte> mouvement_compte { get; set; }
        public DbSet<objectif_vente> objectif_vente { get; set; }
        public DbSet<produit> produits { get; set; }
        public DbSet<produit_corresp_mesure> produit_corresp_mesure { get; set; }
        public DbSet<produit_type_mesure> produit_type_mesure { get; set; }
        public DbSet<produit_stock_limit> produit_stock_limit { get; set; }
        public DbSet<profil> profils { get; set; }

        public DbSet<rayon> rayons { get; set; }
        public DbSet<stock> stocks { get; set; }
        public DbSet<stock_archives> stock_archives { get; set; }
        public DbSet<transfert> transferts { get; set; }
        public DbSet<transfert_details> transfert_details { get; set; }
        public DbSet<type_compte> type_compte { get; set; }
        public DbSet<type_mesure> type_mesure { get; set; }


        public DbSet<utilisateur> users { get; set; }
        public DbSet<service> services { get; set; }
        public DbSet<vente_produit> vente_produit { get; set; }
        public DbSet<facture> factures { get; set; }
        public DbSet<facture_avoir> factures_avoir { get; set; }
        public DbSet<vente_service> vente_service { get; set; }
        public DbSet<param_mecef> param_mecef { get; set; }
        public DbSet<vente_produit_details> vente_produit_details { get; set; }
        public DbSet<vente_service_details> vente_service_details { get; set; }

    }
}
