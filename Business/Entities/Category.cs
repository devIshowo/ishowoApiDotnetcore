using ItCommerce.DTO.ModelDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItCommerce.Business.Entities
{
    public class Category
    {
        public int id { get; set; }
        public string name { get; set; }
        public Profil agent { get; set; }

        public categ_produit loadDto()
        {
            categ_produit _dto = new categ_produit()
            {
                id = this.id,
                nom = this.name,
            };
            return _dto;
        }

        public static Category Create(categ_produit _dbo)
        {
            return new Category()
            {
                id = _dbo.id,
                name = _dbo.nom,
            };
        }

        public static List<Category> CreateFromList(List<categ_produit> _dboList)
        {
            List<Category> _list = new List<Category>();
            foreach (categ_produit item in _dboList)
            {
                Category _businessObject = Category.Create(item);
                _list.Add(_businessObject);
            }
            return _list;
        }
    }
}
