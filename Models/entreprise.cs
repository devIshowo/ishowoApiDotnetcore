using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ItCommerce.DTO.ModelDesign
{
    using System;
    using System.Collections.Generic;
    
    public class entreprise
    {
        public entreprise()
        {
            this.agences = new List<agence>();
            this.banques = new List<banque>();
            this.categ_produit = new List<categ_produit>();
            this.commandes = new List<commande>();
            this.comptes = new List<compte>();
            this.fournisseurs = new List<fournisseur>();
            this.licences = new List<licence>();
            this.mouvement_compte = new List<mouvement_compte>();
            this.produits = new List<produit>();
            this.type_mesure = new List<type_mesure>();
            this.users = new List<utilisateur>();
            this.logs = new List<log>();
        }
    
        public int id { get; set; }
        [Required]
        public string raison_sociale { get; set; }
        [Required]
        public string telephone { get; set; }
        [Required]
        public string adresse { get; set; }
        [Required]
        public string localisation { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string secteur_activite { get; set; }
        [Required]
        public string logo { get; set; }
    
        public  virtual ICollection<agence> agences { get; set; }
        public  virtual ICollection<banque> banques { get; set; }
        public  virtual ICollection<categ_produit> categ_produit { get; set; }
        public  virtual ICollection<commande> commandes { get; set; }
        public  virtual ICollection<compte> comptes { get; set; }
        public  virtual ICollection<fournisseur> fournisseurs { get; set; }
        public  virtual ICollection<licence> licences { get; set; }
        public  virtual ICollection<mouvement_compte> mouvement_compte { get; set; }
        public  virtual ICollection<produit> produits { get; set; }
        public  virtual ICollection<type_mesure> type_mesure { get; set; }
        public  virtual ICollection<utilisateur> users { get; set; }
        public  virtual ICollection<log> logs { get; set; }
    }
}
