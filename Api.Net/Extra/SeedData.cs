
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItCommerce.DTO.ModelDesign;

namespace ItCommerce.Api.Net.Extra
{
    public class SeedData
    {


        public static void Initialize(IT_COMMERCEEntities _context)
            {
                _context.Database.EnsureCreated();

                //les types de compte
                if (!_context.type_compte.Any())
                {
                    _context.type_compte.Add(new type_compte() { nom = "BANCAIRE" });
                    _context.type_compte.Add(new type_compte() { nom = "INTERNE" });
                    _context.type_compte.Add(new type_compte() { nom = "CAISSE" });
                    _context.SaveChanges();
                }

                //les groupes
                if (!_context.groupes.Any())
                {
                    _context.groupes.Add(new groupe() { nom = "ADMIN", code = "ADMIN" });
                    _context.groupes.Add(new groupe() { nom = "CAISSE", code = "CAISSE"  });
                    _context.groupes.Add(new groupe() { nom = "COMMANDES", code = "COMMANDES"  });
                    _context.SaveChanges();
                }

                //les categories de vente
                if (!_context.categ_vente.Any())
                {
                    _context.categ_vente.Add(new categ_vente() { libelle = "ORDINAIRE" });
                    _context.SaveChanges();
                }
            }
    }
}



