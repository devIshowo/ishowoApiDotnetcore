﻿// <auto-generated />
using ItCommerce.DTO.ModelDesign;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using System;

namespace ItCommerce.Migrations
{
    [DbContext(typeof(IT_COMMERCEEntities))]
    [Migration("20180514212043_IshowoDbInit1")]
    partial class IshowoDbInit1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026");

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.agence", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("id_entreprise");

                    b.Property<string>("localisation")
                        .IsRequired();

                    b.Property<string>("nom")
                        .IsRequired();

                    b.HasKey("id");

                    b.HasIndex("id_entreprise");

                    b.ToTable("agences");
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.banque", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("adresse");

                    b.Property<string>("contact");

                    b.Property<int>("id_entreprise");

                    b.Property<string>("nom")
                        .IsRequired();

                    b.HasKey("id");

                    b.HasIndex("id_entreprise");

                    b.ToTable("banques");
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.categ_produit", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("id_entreprise");

                    b.Property<string>("nom")
                        .IsRequired();

                    b.HasKey("id");

                    b.HasIndex("id_entreprise");

                    b.ToTable("categ_produit");
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.categ_vente", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("libelle")
                        .IsRequired();

                    b.HasKey("id");

                    b.ToTable("categ_vente");
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.commande", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("date_cmde");

                    b.Property<int>("id_entreprise");

                    b.Property<int>("id_fournisseur");

                    b.Property<int>("id_profil");

                    b.Property<int>("id_rayon");

                    b.Property<int>("montant_cmde");

                    b.Property<int>("montant_sorti");

                    b.Property<string>("reference")
                        .IsRequired();

                    b.Property<string>("statut");

                    b.HasKey("id");

                    b.HasIndex("id_entreprise");

                    b.HasIndex("id_fournisseur");

                    b.HasIndex("id_profil");

                    b.HasIndex("id_rayon");

                    b.ToTable("commandes");
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.commande_details", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("date_exp");

                    b.Property<int>("id_cmde");

                    b.Property<int>("id_produit_mesure");

                    b.Property<int>("p_achat");

                    b.Property<int?>("p_vente")
                        .IsRequired();

                    b.Property<int>("qte_cmde");

                    b.Property<int?>("qte_recue");

                    b.Property<string>("reference");

                    b.Property<string>("statut");

                    b.HasKey("id");

                    b.HasIndex("id_cmde");

                    b.HasIndex("id_produit_mesure");

                    b.ToTable("commande_details");
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.compte", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("id_banque");

                    b.Property<int>("id_entreprise");

                    b.Property<int?>("id_profil");

                    b.Property<int>("id_type_compte");

                    b.Property<string>("intitule")
                        .IsRequired();

                    b.Property<string>("reference")
                        .IsRequired();

                    b.Property<int>("solde");

                    b.HasKey("id");

                    b.HasIndex("id_banque");

                    b.HasIndex("id_entreprise");

                    b.HasIndex("id_profil");

                    b.HasIndex("id_type_compte");

                    b.ToTable("comptes");
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.devi", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("date_devis");

                    b.Property<int>("id_agence");

                    b.Property<int?>("id_client");

                    b.Property<int>("id_profil");

                    b.Property<int>("montant_devis");

                    b.Property<string>("reference")
                        .IsRequired();

                    b.HasKey("id");

                    b.HasIndex("id_agence");

                    b.HasIndex("id_client");

                    b.HasIndex("id_profil");

                    b.ToTable("devis");
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.devis_details", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("id_devis");

                    b.Property<int>("id_produit_mesure");

                    b.Property<int>("id_rayon");

                    b.Property<int>("p_achat");

                    b.Property<int>("p_vente");

                    b.Property<int>("quantite");

                    b.HasKey("id");

                    b.HasIndex("id_devis");

                    b.HasIndex("id_produit_mesure");

                    b.HasIndex("id_rayon");

                    b.ToTable("devis_details");
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.entreprise", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("adresse")
                        .IsRequired();

                    b.Property<string>("email")
                        .IsRequired();

                    b.Property<string>("localisation")
                        .IsRequired();

                    b.Property<string>("logo")
                        .IsRequired();

                    b.Property<string>("raison_sociale")
                        .IsRequired();

                    b.Property<string>("secteur_activite")
                        .IsRequired();

                    b.Property<string>("telephone")
                        .IsRequired();

                    b.HasKey("id");

                    b.ToTable("entreprises");
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.fournisseur", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("adresse")
                        .IsRequired();

                    b.Property<string>("email")
                        .IsRequired();

                    b.Property<int>("id_entreprise");

                    b.Property<string>("nom")
                        .IsRequired();

                    b.Property<int?>("solde");

                    b.Property<string>("telephone")
                        .IsRequired();

                    b.HasKey("id");

                    b.HasIndex("id_entreprise");

                    b.ToTable("fournisseurs");
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.groupe", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("code")
                        .IsRequired();

                    b.Property<string>("nom")
                        .IsRequired();

                    b.HasKey("id");

                    b.ToTable("groupes");
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.licence", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("cle")
                        .IsRequired();

                    b.Property<string>("code")
                        .IsRequired();

                    b.Property<DateTime>("date_activation");

                    b.Property<DateTime>("date_expiration");

                    b.Property<bool>("est_active");

                    b.Property<int>("id_entreprise");

                    b.Property<int>("montant_paye");

                    b.HasKey("id");

                    b.HasIndex("id_entreprise");

                    b.ToTable("licences");
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.log", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("actor")
                        .IsRequired();

                    b.Property<string>("categorie")
                        .IsRequired();

                    b.Property<DateTime>("date_log");

                    b.Property<string>("desc")
                        .IsRequired();

                    b.Property<int>("id_entreprise");

                    b.HasKey("id");

                    b.HasIndex("id_entreprise");

                    b.ToTable("logs");
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.lot", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("date_creation");

                    b.Property<DateTime?>("date_expiration");

                    b.Property<DateTime>("date_lot");

                    b.Property<string>("reference")
                        .IsRequired();

                    b.HasKey("id");

                    b.ToTable("lots");
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.mouvement_compte", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("credit");

                    b.Property<DateTime>("date_mvt");

                    b.Property<int>("debit");

                    b.Property<int>("id_compte");

                    b.Property<int>("id_entreprise");

                    b.Property<int>("id_profil");

                    b.Property<string>("motif")
                        .IsRequired();

                    b.Property<string>("numero_piece")
                        .IsRequired();

                    b.Property<int>("solde");

                    b.HasKey("id");

                    b.HasIndex("id_compte");

                    b.HasIndex("id_entreprise");

                    b.HasIndex("id_profil");

                    b.ToTable("mouvement_compte");
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.objectif_vente", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("date_debut");

                    b.Property<DateTime>("date_fin");

                    b.Property<bool>("est_atteint");

                    b.Property<int>("id_agence");

                    b.Property<int>("id_profil");

                    b.Property<int>("montant_atteint");

                    b.Property<int>("montant_objectif");

                    b.HasKey("id");

                    b.HasIndex("id_agence");

                    b.HasIndex("id_profil");

                    b.ToTable("objectif_vente");
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.produit", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("code_fabricant");

                    b.Property<string>("code_interne");

                    b.Property<string>("description")
                        .IsRequired();

                    b.Property<int>("id_categ");

                    b.Property<int>("id_entreprise");

                    b.Property<string>("image");

                    b.Property<string>("nom")
                        .IsRequired();

                    b.HasKey("id");

                    b.HasIndex("id_categ");

                    b.HasIndex("id_entreprise");

                    b.ToTable("produits");
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.produit_corresp_mesure", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("id_produit_mesure_enfant");

                    b.Property<int>("id_produit_mesure_parent");

                    b.Property<int>("quantite");

                    b.HasKey("id");

                    b.HasIndex("id_produit_mesure_enfant");

                    b.HasIndex("id_produit_mesure_parent");

                    b.ToTable("produit_corresp_mesure");
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.produit_type_mesure", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("id_produit");

                    b.Property<int>("id_type_mesure");

                    b.Property<int>("quantite");

                    b.Property<string>("reference");

                    b.HasKey("id");

                    b.HasIndex("id_produit");

                    b.HasIndex("id_type_mesure");

                    b.ToTable("produit_type_mesure");
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.profil", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("id_agence");

                    b.Property<int>("id_groupe");

                    b.Property<int>("id_user");

                    b.Property<string>("login")
                        .IsRequired();

                    b.Property<string>("pwd")
                        .IsRequired();

                    b.HasKey("id");

                    b.HasIndex("id_agence");

                    b.HasIndex("id_groupe");

                    b.HasIndex("id_user");

                    b.ToTable("profils");
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.rayon", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("id_agence");

                    b.Property<bool?>("is_magasin");

                    b.Property<string>("nom")
                        .IsRequired();

                    b.HasKey("id");

                    b.HasIndex("id_agence");

                    b.ToTable("rayons");
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.stock", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("date_exp");

                    b.Property<DateTime>("date_modif");

                    b.Property<int>("id_produit_mesure");

                    b.Property<int>("id_rayon");

                    b.Property<int>("p_achat");

                    b.Property<int>("p_vente");

                    b.Property<int>("qte_reelle");

                    b.Property<int>("qte_vendable");

                    b.Property<string>("reference");

                    b.HasKey("id");

                    b.HasIndex("id_produit_mesure");

                    b.HasIndex("id_rayon");

                    b.ToTable("stocks");
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.stock_archives", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("date_modif");

                    b.Property<int>("id_produit_mesure");

                    b.Property<int>("id_rayon");

                    b.Property<int>("p_achat");

                    b.Property<int>("p_vente");

                    b.Property<int>("quantite");

                    b.HasKey("id");

                    b.HasIndex("id_produit_mesure");

                    b.HasIndex("id_rayon");

                    b.ToTable("stock_archives");
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.transfert", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("date_transfert");

                    b.Property<int>("id_destination");

                    b.Property<int>("id_profil");

                    b.Property<int>("id_source");

                    b.Property<string>("reference")
                        .IsRequired();

                    b.HasKey("id");

                    b.HasIndex("id_destination");

                    b.HasIndex("id_profil");

                    b.HasIndex("id_source");

                    b.ToTable("transferts");
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.transfert_details", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("id_produit_mesure");

                    b.Property<int>("id_transfert");

                    b.Property<int>("p_achat");

                    b.Property<int>("p_vente");

                    b.Property<int>("quantite");

                    b.HasKey("id");

                    b.HasIndex("id_produit_mesure");

                    b.HasIndex("id_transfert");

                    b.ToTable("transfert_details");
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.type_compte", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("nom")
                        .IsRequired();

                    b.HasKey("id");

                    b.ToTable("type_compte");
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.type_mesure", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("id_entreprise");

                    b.Property<string>("nom")
                        .IsRequired();

                    b.HasKey("id");

                    b.HasIndex("id_entreprise");

                    b.ToTable("type_mesure");
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.user", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("adresse")
                        .IsRequired();

                    b.Property<string>("email")
                        .IsRequired();

                    b.Property<int>("id_entreprise");

                    b.Property<bool>("is_customer");

                    b.Property<string>("nom")
                        .IsRequired();

                    b.Property<string>("prenoms")
                        .IsRequired();

                    b.Property<int?>("solde");

                    b.Property<string>("telephone")
                        .IsRequired();

                    b.HasKey("id");

                    b.HasIndex("id_entreprise");

                    b.ToTable("user");
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.vente", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool?>("avec_facture");

                    b.Property<DateTime>("date_vente");

                    b.Property<int>("id_agence");

                    b.Property<int>("id_categ_vente");

                    b.Property<int?>("id_client");

                    b.Property<int>("id_profil");

                    b.Property<int>("montant_recu");

                    b.Property<int>("montant_vente");

                    b.Property<string>("reference")
                        .IsRequired();

                    b.Property<int>("reliquat");

                    b.HasKey("id");

                    b.HasIndex("id_agence");

                    b.HasIndex("id_categ_vente");

                    b.HasIndex("id_client");

                    b.HasIndex("id_profil");

                    b.ToTable("vente");
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.vente_details", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("id_produit_mesure");

                    b.Property<int>("id_rayon");

                    b.Property<int>("id_vente");

                    b.Property<int>("p_achat");

                    b.Property<int>("p_vente");

                    b.Property<int>("quantite");

                    b.HasKey("id");

                    b.HasIndex("id_produit_mesure");

                    b.HasIndex("id_rayon");

                    b.HasIndex("id_vente");

                    b.ToTable("vente_details");
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.agence", b =>
                {
                    b.HasOne("ItCommerce.DTO.ModelDesign.entreprise", "entreprise")
                        .WithMany("agences")
                        .HasForeignKey("id_entreprise")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.banque", b =>
                {
                    b.HasOne("ItCommerce.DTO.ModelDesign.entreprise", "entreprise")
                        .WithMany("banques")
                        .HasForeignKey("id_entreprise")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.categ_produit", b =>
                {
                    b.HasOne("ItCommerce.DTO.ModelDesign.entreprise", "entreprise")
                        .WithMany("categ_produit")
                        .HasForeignKey("id_entreprise")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.commande", b =>
                {
                    b.HasOne("ItCommerce.DTO.ModelDesign.entreprise", "entreprise")
                        .WithMany("commandes")
                        .HasForeignKey("id_entreprise")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ItCommerce.DTO.ModelDesign.fournisseur", "fournisseur")
                        .WithMany("commandes")
                        .HasForeignKey("id_fournisseur")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ItCommerce.DTO.ModelDesign.profil", "profil")
                        .WithMany("commandes")
                        .HasForeignKey("id_profil")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ItCommerce.DTO.ModelDesign.rayon", "rayon")
                        .WithMany("commandes")
                        .HasForeignKey("id_rayon")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.commande_details", b =>
                {
                    b.HasOne("ItCommerce.DTO.ModelDesign.commande", "commande")
                        .WithMany("commande_details")
                        .HasForeignKey("id_cmde")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ItCommerce.DTO.ModelDesign.produit_type_mesure", "produit_type_mesure")
                        .WithMany("commande_details")
                        .HasForeignKey("id_produit_mesure")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.compte", b =>
                {
                    b.HasOne("ItCommerce.DTO.ModelDesign.banque", "banque")
                        .WithMany("comptes")
                        .HasForeignKey("id_banque");

                    b.HasOne("ItCommerce.DTO.ModelDesign.entreprise", "entreprise")
                        .WithMany("comptes")
                        .HasForeignKey("id_entreprise")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ItCommerce.DTO.ModelDesign.profil", "profil")
                        .WithMany("comptes")
                        .HasForeignKey("id_profil");

                    b.HasOne("ItCommerce.DTO.ModelDesign.type_compte", "type_compte")
                        .WithMany("comptes")
                        .HasForeignKey("id_type_compte")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.devi", b =>
                {
                    b.HasOne("ItCommerce.DTO.ModelDesign.agence", "agence")
                        .WithMany("devis")
                        .HasForeignKey("id_agence")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ItCommerce.DTO.ModelDesign.user", "user")
                        .WithMany("devis")
                        .HasForeignKey("id_client");

                    b.HasOne("ItCommerce.DTO.ModelDesign.profil", "profil")
                        .WithMany("devis")
                        .HasForeignKey("id_profil")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.devis_details", b =>
                {
                    b.HasOne("ItCommerce.DTO.ModelDesign.devi", "devi")
                        .WithMany("devis_details")
                        .HasForeignKey("id_devis")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ItCommerce.DTO.ModelDesign.produit_type_mesure", "produit_type_mesure")
                        .WithMany("devis_details")
                        .HasForeignKey("id_produit_mesure")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ItCommerce.DTO.ModelDesign.rayon", "rayon")
                        .WithMany("devis_details")
                        .HasForeignKey("id_rayon")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.fournisseur", b =>
                {
                    b.HasOne("ItCommerce.DTO.ModelDesign.entreprise", "entreprise")
                        .WithMany("fournisseurs")
                        .HasForeignKey("id_entreprise")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.licence", b =>
                {
                    b.HasOne("ItCommerce.DTO.ModelDesign.entreprise", "entreprise")
                        .WithMany("licences")
                        .HasForeignKey("id_entreprise")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.log", b =>
                {
                    b.HasOne("ItCommerce.DTO.ModelDesign.entreprise", "entreprise")
                        .WithMany("logs")
                        .HasForeignKey("id_entreprise")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.mouvement_compte", b =>
                {
                    b.HasOne("ItCommerce.DTO.ModelDesign.compte", "compte")
                        .WithMany("mouvement_compte")
                        .HasForeignKey("id_compte")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ItCommerce.DTO.ModelDesign.entreprise", "entreprise")
                        .WithMany("mouvement_compte")
                        .HasForeignKey("id_entreprise")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ItCommerce.DTO.ModelDesign.profil", "profil")
                        .WithMany("mouvement_compte")
                        .HasForeignKey("id_profil")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.objectif_vente", b =>
                {
                    b.HasOne("ItCommerce.DTO.ModelDesign.agence", "agence")
                        .WithMany("objectif_vente")
                        .HasForeignKey("id_agence")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ItCommerce.DTO.ModelDesign.profil", "profil")
                        .WithMany("objectif_vente")
                        .HasForeignKey("id_profil")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.produit", b =>
                {
                    b.HasOne("ItCommerce.DTO.ModelDesign.categ_produit", "categ_produit")
                        .WithMany("produits")
                        .HasForeignKey("id_categ")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ItCommerce.DTO.ModelDesign.entreprise", "entreprise")
                        .WithMany("produits")
                        .HasForeignKey("id_entreprise")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.produit_corresp_mesure", b =>
                {
                    b.HasOne("ItCommerce.DTO.ModelDesign.produit_type_mesure", "produit_type_mesure_enfant")
                        .WithMany("produit_corresp_mesure_enfants")
                        .HasForeignKey("id_produit_mesure_enfant")
                        .HasConstraintName("FK_prod_crp_mes_prod_type_mes_id_prod_mes_enfant")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ItCommerce.DTO.ModelDesign.produit_type_mesure", "produit_type_mesure_parent")
                        .WithMany("produit_corresp_mesure_parents")
                        .HasForeignKey("id_produit_mesure_parent")
                        .HasConstraintName("FK_prod_crp_mes_prod_type_mes_id_prod_mes_parent")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.produit_type_mesure", b =>
                {
                    b.HasOne("ItCommerce.DTO.ModelDesign.produit", "produit")
                        .WithMany("produit_type_mesure")
                        .HasForeignKey("id_produit")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ItCommerce.DTO.ModelDesign.type_mesure", "type_mesure")
                        .WithMany("produit_type_mesure")
                        .HasForeignKey("id_type_mesure")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.profil", b =>
                {
                    b.HasOne("ItCommerce.DTO.ModelDesign.agence", "agence")
                        .WithMany("profils")
                        .HasForeignKey("id_agence")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ItCommerce.DTO.ModelDesign.groupe", "groupe")
                        .WithMany("profils")
                        .HasForeignKey("id_groupe")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ItCommerce.DTO.ModelDesign.user", "user")
                        .WithMany("profils")
                        .HasForeignKey("id_user")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.rayon", b =>
                {
                    b.HasOne("ItCommerce.DTO.ModelDesign.agence", "agence")
                        .WithMany("rayons")
                        .HasForeignKey("id_agence")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.stock", b =>
                {
                    b.HasOne("ItCommerce.DTO.ModelDesign.produit_type_mesure", "produit_type_mesure")
                        .WithMany("stocks")
                        .HasForeignKey("id_produit_mesure")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ItCommerce.DTO.ModelDesign.rayon", "rayon")
                        .WithMany("stocks")
                        .HasForeignKey("id_rayon")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.stock_archives", b =>
                {
                    b.HasOne("ItCommerce.DTO.ModelDesign.produit_type_mesure", "produit_type_mesure")
                        .WithMany("stock_archives")
                        .HasForeignKey("id_produit_mesure")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ItCommerce.DTO.ModelDesign.rayon", "rayon")
                        .WithMany("stock_archives")
                        .HasForeignKey("id_rayon")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.transfert", b =>
                {
                    b.HasOne("ItCommerce.DTO.ModelDesign.rayon", "rayon_destination")
                        .WithMany()
                        .HasForeignKey("id_destination")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ItCommerce.DTO.ModelDesign.profil", "profil")
                        .WithMany("transferts")
                        .HasForeignKey("id_profil")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ItCommerce.DTO.ModelDesign.rayon", "rayon_source")
                        .WithMany()
                        .HasForeignKey("id_source")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.transfert_details", b =>
                {
                    b.HasOne("ItCommerce.DTO.ModelDesign.produit_type_mesure", "produit_type_mesure")
                        .WithMany("transfert_details")
                        .HasForeignKey("id_produit_mesure")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ItCommerce.DTO.ModelDesign.transfert", "transfert")
                        .WithMany("transfert_details")
                        .HasForeignKey("id_transfert")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.type_mesure", b =>
                {
                    b.HasOne("ItCommerce.DTO.ModelDesign.entreprise", "entreprise")
                        .WithMany("type_mesure")
                        .HasForeignKey("id_entreprise")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.user", b =>
                {
                    b.HasOne("ItCommerce.DTO.ModelDesign.entreprise", "entreprise")
                        .WithMany("users")
                        .HasForeignKey("id_entreprise")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.vente", b =>
                {
                    b.HasOne("ItCommerce.DTO.ModelDesign.agence", "agence")
                        .WithMany("ventes")
                        .HasForeignKey("id_agence")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ItCommerce.DTO.ModelDesign.categ_vente", "categ_vente")
                        .WithMany("ventes")
                        .HasForeignKey("id_categ_vente")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ItCommerce.DTO.ModelDesign.user", "user")
                        .WithMany("ventes")
                        .HasForeignKey("id_client");

                    b.HasOne("ItCommerce.DTO.ModelDesign.profil", "profil")
                        .WithMany("ventes")
                        .HasForeignKey("id_profil")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ItCommerce.DTO.ModelDesign.vente_details", b =>
                {
                    b.HasOne("ItCommerce.DTO.ModelDesign.produit_type_mesure", "produit_type_mesure")
                        .WithMany("vente_details")
                        .HasForeignKey("id_produit_mesure")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ItCommerce.DTO.ModelDesign.rayon", "rayon")
                        .WithMany("vente_details")
                        .HasForeignKey("id_rayon")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ItCommerce.DTO.ModelDesign.vente", "vente")
                        .WithMany("vente_details")
                        .HasForeignKey("id_vente")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
