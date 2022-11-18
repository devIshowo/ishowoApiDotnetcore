using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ASP.NETCoreWithEFCore.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categ_vente",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    libelle = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categ_vente", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "entreprises",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    raison_sociale = table.Column<string>(nullable: false),
                    telephone = table.Column<string>(nullable: false),
                    adresse = table.Column<string>(nullable: false),
                    localisation = table.Column<string>(nullable: false),
                    email = table.Column<string>(nullable: false),
                    secteur_activite = table.Column<string>(nullable: false),
                    logo = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entreprises", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "groupes",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    nom = table.Column<string>(nullable: false),
                    code = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_groupes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "lots",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    reference = table.Column<string>(nullable: false),
                    date_lot = table.Column<DateTime>(nullable: false),
                    date_creation = table.Column<DateTime>(nullable: true),
                    date_expiration = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lots", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "type_compte",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    nom = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_type_compte", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "agences",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    nom = table.Column<string>(nullable: false),
                    localisation = table.Column<string>(nullable: false),
                    id_entreprise = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_agences", x => x.id);
                    table.ForeignKey(
                        name: "FK_agences_entreprises_id_entreprise",
                        column: x => x.id_entreprise,
                        principalTable: "entreprises",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "banques",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    nom = table.Column<string>(nullable: false),
                    adresse = table.Column<string>(nullable: true),
                    contact = table.Column<string>(nullable: true),
                    id_entreprise = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_banques", x => x.id);
                    table.ForeignKey(
                        name: "FK_banques_entreprises_id_entreprise",
                        column: x => x.id_entreprise,
                        principalTable: "entreprises",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "categ_produit",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    nom = table.Column<string>(nullable: false),
                    id_entreprise = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categ_produit", x => x.id);
                    table.ForeignKey(
                        name: "FK_categ_produit_entreprises_id_entreprise",
                        column: x => x.id_entreprise,
                        principalTable: "entreprises",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fournisseurs",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    nom = table.Column<string>(nullable: false),
                    telephone = table.Column<string>(nullable: false),
                    email = table.Column<string>(nullable: false),
                    adresse = table.Column<string>(nullable: false),
                    id_entreprise = table.Column<int>(nullable: false),
                    solde = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fournisseurs", x => x.id);
                    table.ForeignKey(
                        name: "FK_fournisseurs_entreprises_id_entreprise",
                        column: x => x.id_entreprise,
                        principalTable: "entreprises",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "licences",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    id_entreprise = table.Column<int>(nullable: false),
                    code = table.Column<string>(nullable: false),
                    cle = table.Column<string>(nullable: false),
                    est_active = table.Column<bool>(nullable: false),
                    date_activation = table.Column<DateTime>(nullable: false),
                    date_expiration = table.Column<DateTime>(nullable: false),
                    montant_paye = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_licences", x => x.id);
                    table.ForeignKey(
                        name: "FK_licences_entreprises_id_entreprise",
                        column: x => x.id_entreprise,
                        principalTable: "entreprises",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "logs",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    date_log = table.Column<DateTime>(nullable: false),
                    categorie = table.Column<string>(nullable: false),
                    id_entreprise = table.Column<int>(nullable: false),
                    actor = table.Column<string>(nullable: false),
                    desc = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_logs", x => x.id);
                    table.ForeignKey(
                        name: "FK_logs_entreprises_id_entreprise",
                        column: x => x.id_entreprise,
                        principalTable: "entreprises",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "type_mesure",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    nom = table.Column<string>(nullable: false),
                    id_entreprise = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_type_mesure", x => x.id);
                    table.ForeignKey(
                        name: "FK_type_mesure_entreprises_id_entreprise",
                        column: x => x.id_entreprise,
                        principalTable: "entreprises",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    nom = table.Column<string>(nullable: false),
                    prenoms = table.Column<string>(nullable: false),
                    telephone = table.Column<string>(nullable: false),
                    adresse = table.Column<string>(nullable: false),
                    email = table.Column<string>(nullable: false),
                    id_entreprise = table.Column<int>(nullable: false),
                    is_customer = table.Column<bool>(nullable: false),
                    solde = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                    table.ForeignKey(
                        name: "FK_users_entreprises_id_entreprise",
                        column: x => x.id_entreprise,
                        principalTable: "entreprises",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "rayons",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    nom = table.Column<string>(nullable: false),
                    is_magasin = table.Column<bool>(nullable: true),
                    id_agence = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rayons", x => x.id);
                    table.ForeignKey(
                        name: "FK_rayons_agences_id_agence",
                        column: x => x.id_agence,
                        principalTable: "agences",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "produits",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    nom = table.Column<string>(nullable: false),
                    id_categ = table.Column<int>(nullable: false),
                    id_entreprise = table.Column<int>(nullable: false),
                    description = table.Column<string>(nullable: false),
                    image = table.Column<string>(nullable: true),
                    code_interne = table.Column<string>(nullable: true),
                    code_fabricant = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_produits", x => x.id);
                    table.ForeignKey(
                        name: "FK_produits_categ_produit_id_categ",
                        column: x => x.id_categ,
                        principalTable: "categ_produit",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_produits_entreprises_id_entreprise",
                        column: x => x.id_entreprise,
                        principalTable: "entreprises",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "profils",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    login = table.Column<string>(nullable: false),
                    pwd = table.Column<string>(nullable: false),
                    id_user = table.Column<int>(nullable: false),
                    id_groupe = table.Column<int>(nullable: false),
                    id_agence = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_profils", x => x.id);
                    table.ForeignKey(
                        name: "FK_profils_agences_id_agence",
                        column: x => x.id_agence,
                        principalTable: "agences",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_profils_groupes_id_groupe",
                        column: x => x.id_groupe,
                        principalTable: "groupes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_profils_users_id_user",
                        column: x => x.id_user,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "produit_type_mesure",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    id_produit = table.Column<int>(nullable: false),
                    id_type_mesure = table.Column<int>(nullable: false),
                    quantite = table.Column<int>(nullable: false),
                    reference = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_produit_type_mesure", x => x.id);
                    table.ForeignKey(
                        name: "FK_produit_type_mesure_produits_id_produit",
                        column: x => x.id_produit,
                        principalTable: "produits",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_produit_type_mesure_type_mesure_id_type_mesure",
                        column: x => x.id_type_mesure,
                        principalTable: "type_mesure",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "commandes",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    date_cmde = table.Column<DateTime>(nullable: false),
                    id_fournisseur = table.Column<int>(nullable: false),
                    statut = table.Column<string>(nullable: true),
                    id_entreprise = table.Column<int>(nullable: false),
                    id_profil = table.Column<int>(nullable: false),
                    id_rayon = table.Column<int>(nullable: false),
                    reference = table.Column<string>(nullable: false),
                    montant_cmde = table.Column<int>(nullable: false),
                    montant_sorti = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_commandes", x => x.id);
                    table.ForeignKey(
                        name: "FK_commandes_entreprises_id_entreprise",
                        column: x => x.id_entreprise,
                        principalTable: "entreprises",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_commandes_fournisseurs_id_fournisseur",
                        column: x => x.id_fournisseur,
                        principalTable: "fournisseurs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_commandes_profils_id_profil",
                        column: x => x.id_profil,
                        principalTable: "profils",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_commandes_rayons_id_rayon",
                        column: x => x.id_rayon,
                        principalTable: "rayons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comptes",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    intitule = table.Column<string>(nullable: false),
                    reference = table.Column<string>(nullable: false),
                    solde = table.Column<int>(nullable: false),
                    id_type_compte = table.Column<int>(nullable: false),
                    id_banque = table.Column<int>(nullable: true),
                    id_profil = table.Column<int>(nullable: true),
                    id_entreprise = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comptes", x => x.id);
                    table.ForeignKey(
                        name: "FK_comptes_banques_id_banque",
                        column: x => x.id_banque,
                        principalTable: "banques",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_comptes_entreprises_id_entreprise",
                        column: x => x.id_entreprise,
                        principalTable: "entreprises",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_comptes_profils_id_profil",
                        column: x => x.id_profil,
                        principalTable: "profils",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_comptes_type_compte_id_type_compte",
                        column: x => x.id_type_compte,
                        principalTable: "type_compte",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "devis",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    date_devis = table.Column<DateTime>(nullable: false),
                    montant_devis = table.Column<int>(nullable: false),
                    reference = table.Column<string>(nullable: false),
                    id_agence = table.Column<int>(nullable: false),
                    id_client = table.Column<int>(nullable: true),
                    id_profil = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_devis", x => x.id);
                    table.ForeignKey(
                        name: "FK_devis_agences_id_agence",
                        column: x => x.id_agence,
                        principalTable: "agences",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_devis_users_id_client",
                        column: x => x.id_client,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_devis_profils_id_profil",
                        column: x => x.id_profil,
                        principalTable: "profils",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "objectif_vente",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    id_profil = table.Column<int>(nullable: false),
                    date_debut = table.Column<DateTime>(nullable: false),
                    date_fin = table.Column<DateTime>(nullable: false),
                    montant_objectif = table.Column<int>(nullable: false),
                    montant_atteint = table.Column<int>(nullable: false),
                    est_atteint = table.Column<bool>(nullable: false),
                    id_agence = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_objectif_vente", x => x.id);
                    table.ForeignKey(
                        name: "FK_objectif_vente_agences_id_agence",
                        column: x => x.id_agence,
                        principalTable: "agences",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_objectif_vente_profils_id_profil",
                        column: x => x.id_profil,
                        principalTable: "profils",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "transferts",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    date_transfert = table.Column<DateTime>(nullable: false),
                    id_source = table.Column<int>(nullable: false),
                    id_destination = table.Column<int>(nullable: false),
                    id_profil = table.Column<int>(nullable: false),
                    reference = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transferts", x => x.id);
                    table.ForeignKey(
                        name: "FK_transferts_rayons_id_destination",
                        column: x => x.id_destination,
                        principalTable: "rayons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_transferts_profils_id_profil",
                        column: x => x.id_profil,
                        principalTable: "profils",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_transferts_rayons_id_source",
                        column: x => x.id_source,
                        principalTable: "rayons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ventes",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    date_vente = table.Column<DateTime>(nullable: false),
                    montant_vente = table.Column<int>(nullable: false),
                    montant_recu = table.Column<int>(nullable: false),
                    reliquat = table.Column<int>(nullable: false),
                    id_categ_vente = table.Column<int>(nullable: false),
                    id_agence = table.Column<int>(nullable: false),
                    id_client = table.Column<int>(nullable: true),
                    id_profil = table.Column<int>(nullable: false),
                    avec_facture = table.Column<bool>(nullable: true),
                    reference = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ventes", x => x.id);
                    table.ForeignKey(
                        name: "FK_ventes_agences_id_agence",
                        column: x => x.id_agence,
                        principalTable: "agences",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ventes_categ_vente_id_categ_vente",
                        column: x => x.id_categ_vente,
                        principalTable: "categ_vente",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ventes_users_id_client",
                        column: x => x.id_client,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ventes_profils_id_profil",
                        column: x => x.id_profil,
                        principalTable: "profils",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "produit_corresp_mesure",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    id_produit_mesure_parent = table.Column<int>(nullable: false),
                    id_produit_mesure_enfant = table.Column<int>(nullable: false),
                    quantite = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_produit_corresp_mesure", x => x.id);
                    table.ForeignKey(
                        name: "FK_prod_crp_mes_prod_type_mes_id_prod_mes_enfant",
                        column: x => x.id_produit_mesure_enfant,
                        principalTable: "produit_type_mesure",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_prod_crp_mes_prod_type_mes_id_prod_mes_parent",
                        column: x => x.id_produit_mesure_parent,
                        principalTable: "produit_type_mesure",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "stock_archives",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    id_produit_mesure = table.Column<int>(nullable: false),
                    id_rayon = table.Column<int>(nullable: false),
                    quantite = table.Column<int>(nullable: false),
                    p_vente = table.Column<int>(nullable: false),
                    p_achat = table.Column<int>(nullable: false),
                    date_modif = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stock_archives", x => x.id);
                    table.ForeignKey(
                        name: "FK_stock_archives_produit_type_mesure_id_produit_mesure",
                        column: x => x.id_produit_mesure,
                        principalTable: "produit_type_mesure",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_stock_archives_rayons_id_rayon",
                        column: x => x.id_rayon,
                        principalTable: "rayons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "stocks",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    id_produit_mesure = table.Column<int>(nullable: false),
                    id_rayon = table.Column<int>(nullable: false),
                    qte_vendable = table.Column<int>(nullable: false),
                    qte_reelle = table.Column<int>(nullable: false),
                    date_modif = table.Column<DateTime>(nullable: false),
                    p_vente = table.Column<int>(nullable: false),
                    p_achat = table.Column<int>(nullable: false),
                    reference = table.Column<string>(nullable: true),
                    date_exp = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stocks", x => x.id);
                    table.ForeignKey(
                        name: "FK_stocks_produit_type_mesure_id_produit_mesure",
                        column: x => x.id_produit_mesure,
                        principalTable: "produit_type_mesure",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_stocks_rayons_id_rayon",
                        column: x => x.id_rayon,
                        principalTable: "rayons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "commande_details",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    qte_cmde = table.Column<int>(nullable: false),
                    qte_recue = table.Column<int>(nullable: true),
                    statut = table.Column<string>(nullable: true),
                    id_cmde = table.Column<int>(nullable: false),
                    p_achat = table.Column<int>(nullable: false),
                    id_produit_mesure = table.Column<int>(nullable: false),
                    p_vente = table.Column<int>(nullable: false),
                    reference = table.Column<string>(nullable: true),
                    date_exp = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_commande_details", x => x.id);
                    table.ForeignKey(
                        name: "FK_commande_details_commandes_id_cmde",
                        column: x => x.id_cmde,
                        principalTable: "commandes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_commande_details_produit_type_mesure_id_produit_mesure",
                        column: x => x.id_produit_mesure,
                        principalTable: "produit_type_mesure",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mouvement_compte",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    id_compte = table.Column<int>(nullable: false),
                    credit = table.Column<int>(nullable: false),
                    debit = table.Column<int>(nullable: false),
                    solde = table.Column<int>(nullable: false),
                    numero_piece = table.Column<string>(nullable: false),
                    date_mvt = table.Column<DateTime>(nullable: false),
                    motif = table.Column<string>(nullable: false),
                    id_profil = table.Column<int>(nullable: false),
                    id_entreprise = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mouvement_compte", x => x.id);
                    table.ForeignKey(
                        name: "FK_mouvement_compte_comptes_id_compte",
                        column: x => x.id_compte,
                        principalTable: "comptes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mouvement_compte_entreprises_id_entreprise",
                        column: x => x.id_entreprise,
                        principalTable: "entreprises",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mouvement_compte_profils_id_profil",
                        column: x => x.id_profil,
                        principalTable: "profils",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "devis_details",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    quantite = table.Column<int>(nullable: false),
                    id_devis = table.Column<int>(nullable: false),
                    p_achat = table.Column<int>(nullable: false),
                    p_vente = table.Column<int>(nullable: false),
                    id_produit_mesure = table.Column<int>(nullable: false),
                    id_rayon = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_devis_details", x => x.id);
                    table.ForeignKey(
                        name: "FK_devis_details_devis_id_devis",
                        column: x => x.id_devis,
                        principalTable: "devis",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_devis_details_produit_type_mesure_id_produit_mesure",
                        column: x => x.id_produit_mesure,
                        principalTable: "produit_type_mesure",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_devis_details_rayons_id_rayon",
                        column: x => x.id_rayon,
                        principalTable: "rayons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "transfert_details",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    id_produit_mesure = table.Column<int>(nullable: false),
                    id_transfert = table.Column<int>(nullable: false),
                    quantite = table.Column<int>(nullable: false),
                    p_achat = table.Column<int>(nullable: false),
                    p_vente = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transfert_details", x => x.id);
                    table.ForeignKey(
                        name: "FK_transfert_details_produit_type_mesure_id_produit_mesure",
                        column: x => x.id_produit_mesure,
                        principalTable: "produit_type_mesure",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_transfert_details_transferts_id_transfert",
                        column: x => x.id_transfert,
                        principalTable: "transferts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "vente_details",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    quantite = table.Column<int>(nullable: false),
                    id_vente = table.Column<int>(nullable: false),
                    p_achat = table.Column<int>(nullable: false),
                    p_vente = table.Column<int>(nullable: false),
                    id_produit_mesure = table.Column<int>(nullable: false),
                    id_rayon = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vente_details", x => x.id);
                    table.ForeignKey(
                        name: "FK_vente_details_produit_type_mesure_id_produit_mesure",
                        column: x => x.id_produit_mesure,
                        principalTable: "produit_type_mesure",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_vente_details_rayons_id_rayon",
                        column: x => x.id_rayon,
                        principalTable: "rayons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_vente_details_ventes_id_vente",
                        column: x => x.id_vente,
                        principalTable: "ventes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_agences_id_entreprise",
                table: "agences",
                column: "id_entreprise");

            migrationBuilder.CreateIndex(
                name: "IX_banques_id_entreprise",
                table: "banques",
                column: "id_entreprise");

            migrationBuilder.CreateIndex(
                name: "IX_categ_produit_id_entreprise",
                table: "categ_produit",
                column: "id_entreprise");

            migrationBuilder.CreateIndex(
                name: "IX_commande_details_id_cmde",
                table: "commande_details",
                column: "id_cmde");

            migrationBuilder.CreateIndex(
                name: "IX_commande_details_id_produit_mesure",
                table: "commande_details",
                column: "id_produit_mesure");

            migrationBuilder.CreateIndex(
                name: "IX_commandes_id_entreprise",
                table: "commandes",
                column: "id_entreprise");

            migrationBuilder.CreateIndex(
                name: "IX_commandes_id_fournisseur",
                table: "commandes",
                column: "id_fournisseur");

            migrationBuilder.CreateIndex(
                name: "IX_commandes_id_profil",
                table: "commandes",
                column: "id_profil");

            migrationBuilder.CreateIndex(
                name: "IX_commandes_id_rayon",
                table: "commandes",
                column: "id_rayon");

            migrationBuilder.CreateIndex(
                name: "IX_comptes_id_banque",
                table: "comptes",
                column: "id_banque");

            migrationBuilder.CreateIndex(
                name: "IX_comptes_id_entreprise",
                table: "comptes",
                column: "id_entreprise");

            migrationBuilder.CreateIndex(
                name: "IX_comptes_id_profil",
                table: "comptes",
                column: "id_profil");

            migrationBuilder.CreateIndex(
                name: "IX_comptes_id_type_compte",
                table: "comptes",
                column: "id_type_compte");

            migrationBuilder.CreateIndex(
                name: "IX_devis_id_agence",
                table: "devis",
                column: "id_agence");

            migrationBuilder.CreateIndex(
                name: "IX_devis_id_client",
                table: "devis",
                column: "id_client");

            migrationBuilder.CreateIndex(
                name: "IX_devis_id_profil",
                table: "devis",
                column: "id_profil");

            migrationBuilder.CreateIndex(
                name: "IX_devis_details_id_devis",
                table: "devis_details",
                column: "id_devis");

            migrationBuilder.CreateIndex(
                name: "IX_devis_details_id_produit_mesure",
                table: "devis_details",
                column: "id_produit_mesure");

            migrationBuilder.CreateIndex(
                name: "IX_devis_details_id_rayon",
                table: "devis_details",
                column: "id_rayon");

            migrationBuilder.CreateIndex(
                name: "IX_fournisseurs_id_entreprise",
                table: "fournisseurs",
                column: "id_entreprise");

            migrationBuilder.CreateIndex(
                name: "IX_licences_id_entreprise",
                table: "licences",
                column: "id_entreprise");

            migrationBuilder.CreateIndex(
                name: "IX_logs_id_entreprise",
                table: "logs",
                column: "id_entreprise");

            migrationBuilder.CreateIndex(
                name: "IX_mouvement_compte_id_compte",
                table: "mouvement_compte",
                column: "id_compte");

            migrationBuilder.CreateIndex(
                name: "IX_mouvement_compte_id_entreprise",
                table: "mouvement_compte",
                column: "id_entreprise");

            migrationBuilder.CreateIndex(
                name: "IX_mouvement_compte_id_profil",
                table: "mouvement_compte",
                column: "id_profil");

            migrationBuilder.CreateIndex(
                name: "IX_objectif_vente_id_agence",
                table: "objectif_vente",
                column: "id_agence");

            migrationBuilder.CreateIndex(
                name: "IX_objectif_vente_id_profil",
                table: "objectif_vente",
                column: "id_profil");

            migrationBuilder.CreateIndex(
                name: "IX_produit_corresp_mesure_id_produit_mesure_enfant",
                table: "produit_corresp_mesure",
                column: "id_produit_mesure_enfant");

            migrationBuilder.CreateIndex(
                name: "IX_produit_corresp_mesure_id_produit_mesure_parent",
                table: "produit_corresp_mesure",
                column: "id_produit_mesure_parent");

            migrationBuilder.CreateIndex(
                name: "IX_produit_type_mesure_id_produit",
                table: "produit_type_mesure",
                column: "id_produit");

            migrationBuilder.CreateIndex(
                name: "IX_produit_type_mesure_id_type_mesure",
                table: "produit_type_mesure",
                column: "id_type_mesure");

            migrationBuilder.CreateIndex(
                name: "IX_produits_id_categ",
                table: "produits",
                column: "id_categ");

            migrationBuilder.CreateIndex(
                name: "IX_produits_id_entreprise",
                table: "produits",
                column: "id_entreprise");

            migrationBuilder.CreateIndex(
                name: "IX_profils_id_agence",
                table: "profils",
                column: "id_agence");

            migrationBuilder.CreateIndex(
                name: "IX_profils_id_groupe",
                table: "profils",
                column: "id_groupe");

            migrationBuilder.CreateIndex(
                name: "IX_profils_id_user",
                table: "profils",
                column: "id_user");

            migrationBuilder.CreateIndex(
                name: "IX_rayons_id_agence",
                table: "rayons",
                column: "id_agence");

            migrationBuilder.CreateIndex(
                name: "IX_stock_archives_id_produit_mesure",
                table: "stock_archives",
                column: "id_produit_mesure");

            migrationBuilder.CreateIndex(
                name: "IX_stock_archives_id_rayon",
                table: "stock_archives",
                column: "id_rayon");

            migrationBuilder.CreateIndex(
                name: "IX_stocks_id_produit_mesure",
                table: "stocks",
                column: "id_produit_mesure");

            migrationBuilder.CreateIndex(
                name: "IX_stocks_id_rayon",
                table: "stocks",
                column: "id_rayon");

            migrationBuilder.CreateIndex(
                name: "IX_transfert_details_id_produit_mesure",
                table: "transfert_details",
                column: "id_produit_mesure");

            migrationBuilder.CreateIndex(
                name: "IX_transfert_details_id_transfert",
                table: "transfert_details",
                column: "id_transfert");

            migrationBuilder.CreateIndex(
                name: "IX_transferts_id_destination",
                table: "transferts",
                column: "id_destination");

            migrationBuilder.CreateIndex(
                name: "IX_transferts_id_profil",
                table: "transferts",
                column: "id_profil");

            migrationBuilder.CreateIndex(
                name: "IX_transferts_id_source",
                table: "transferts",
                column: "id_source");

            migrationBuilder.CreateIndex(
                name: "IX_type_mesure_id_entreprise",
                table: "type_mesure",
                column: "id_entreprise");

            migrationBuilder.CreateIndex(
                name: "IX_users_id_entreprise",
                table: "users",
                column: "id_entreprise");

            migrationBuilder.CreateIndex(
                name: "IX_vente_details_id_produit_mesure",
                table: "vente_details",
                column: "id_produit_mesure");

            migrationBuilder.CreateIndex(
                name: "IX_vente_details_id_rayon",
                table: "vente_details",
                column: "id_rayon");

            migrationBuilder.CreateIndex(
                name: "IX_vente_details_id_vente",
                table: "vente_details",
                column: "id_vente");

            migrationBuilder.CreateIndex(
                name: "IX_ventes_id_agence",
                table: "ventes",
                column: "id_agence");

            migrationBuilder.CreateIndex(
                name: "IX_ventes_id_categ_vente",
                table: "ventes",
                column: "id_categ_vente");

            migrationBuilder.CreateIndex(
                name: "IX_ventes_id_client",
                table: "ventes",
                column: "id_client");

            migrationBuilder.CreateIndex(
                name: "IX_ventes_id_profil",
                table: "ventes",
                column: "id_profil");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "commande_details");

            migrationBuilder.DropTable(
                name: "devis_details");

            migrationBuilder.DropTable(
                name: "licences");

            migrationBuilder.DropTable(
                name: "logs");

            migrationBuilder.DropTable(
                name: "lots");

            migrationBuilder.DropTable(
                name: "mouvement_compte");

            migrationBuilder.DropTable(
                name: "objectif_vente");

            migrationBuilder.DropTable(
                name: "produit_corresp_mesure");

            migrationBuilder.DropTable(
                name: "stock_archives");

            migrationBuilder.DropTable(
                name: "stocks");

            migrationBuilder.DropTable(
                name: "transfert_details");

            migrationBuilder.DropTable(
                name: "vente_details");

            migrationBuilder.DropTable(
                name: "commandes");

            migrationBuilder.DropTable(
                name: "devis");

            migrationBuilder.DropTable(
                name: "comptes");

            migrationBuilder.DropTable(
                name: "transferts");

            migrationBuilder.DropTable(
                name: "produit_type_mesure");

            migrationBuilder.DropTable(
                name: "ventes");

            migrationBuilder.DropTable(
                name: "fournisseurs");

            migrationBuilder.DropTable(
                name: "banques");

            migrationBuilder.DropTable(
                name: "type_compte");

            migrationBuilder.DropTable(
                name: "rayons");

            migrationBuilder.DropTable(
                name: "produits");

            migrationBuilder.DropTable(
                name: "type_mesure");

            migrationBuilder.DropTable(
                name: "categ_vente");

            migrationBuilder.DropTable(
                name: "profils");

            migrationBuilder.DropTable(
                name: "categ_produit");

            migrationBuilder.DropTable(
                name: "agences");

            migrationBuilder.DropTable(
                name: "groupes");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "entreprises");
        }
    }
}
