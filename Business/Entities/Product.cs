using ItCommerce.DTO.ModelDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItCommerce.Business.Entities
{
    public class Product
    {
        public int id { get; set; }
        public string name { get; set; }
        public string reference { get; set; }
        public Category category { get; set; }
        public List<ProdMeasureType> measure_types { get; set; }
        public List<MeasureAssociation> measure_associations { get; set; }
        public Profil agent { get; set; }



        public static Product Create(produit _dbo)
        {
            //get corresp measures
            List<produit_corresp_mesure> associationsList = new List<produit_corresp_mesure>();
            List<produit_type_mesure> prodTypMeasList = _dbo.produit_type_mesure.ToList<produit_type_mesure>();
            foreach (produit_type_mesure item in prodTypMeasList)
            {
                if(item.produit_corresp_mesure_parents != null){
                    associationsList.AddRange(item.produit_corresp_mesure_parents);
                }
                if(item.produit_corresp_mesure_enfants != null){
                    associationsList.AddRange(item.produit_corresp_mesure_enfants);
                }
            }
            associationsList = associationsList.Distinct<produit_corresp_mesure>().ToList<produit_corresp_mesure>();

            //create product
            return new Product()
            {
                id = _dbo.id,
                name = _dbo.nom,
                reference = _dbo.code_fabricant,
                category = (_dbo.categ_produit != null) ? Category.Create(_dbo.categ_produit) : null,
                measure_types = Product.GetMeasureTypes(_dbo.produit_type_mesure),
                measure_associations = Product.GetMeasureAssociation(associationsList),
            };
        }

        public static Product CreateLight(produit _dbo)
        {
            //create product
            return new Product()
            {
                id = _dbo.id,
                name = _dbo.nom,
                reference = _dbo.code_fabricant,
                category = (_dbo.categ_produit != null) ? Category.Create(_dbo.categ_produit) : null,
            };
        }


        public static List<ProdMeasureType> GetMeasureTypes(ICollection<produit_type_mesure> prodTM)
        {
            List<ProdMeasureType> result = new List<ProdMeasureType>();
            if (prodTM == null) return result;
            foreach (var item in prodTM)
            {
                var newMesureType = ProdMeasureType.Create(item);               
                result.Add(newMesureType);
            }
            return result;
        }//fin GetMeasureTypes

        public static List<MeasureAssociation> GetMeasureAssociation(ICollection<produit_corresp_mesure> prodCM)
        {
            List<MeasureAssociation> result = new List<MeasureAssociation>();
            if (prodCM == null) return result;
            foreach (var item in prodCM)
            {
                var newMesureAssoc = new MeasureAssociation()
                {
                    id = item.id,
                    retail = ProdMeasureType.Create(item.produit_type_mesure_enfant),
                    quantity = item.quantite,
                    bulk = ProdMeasureType.Create(item.produit_type_mesure_parent)
                };
                result.Add(newMesureAssoc);
            }
            return result;
        }//fin GetMeasureAssociation

        public produit loadDto()
        {
            produit _dto = new produit()
            {
                id = this.id,
                nom = this.name,
                description = "",
                code_fabricant = this.reference,
                code_interne = this.reference,
                id_categ = (this.category != null) ? this.category.id : 0,
            };
            //types mesure
            if(this.measure_types != null)
            {
                _dto.produit_type_mesure = new List<produit_type_mesure>();
                foreach (var item in this.measure_types)
                {
                    produit_type_mesure pTM = new produit_type_mesure();
                    pTM.id_produit = this.id;
                    pTM.id_type_mesure = (item.measure_type != null) ? item.measure_type.id : 0;
                    _dto.produit_type_mesure.Add(pTM);
                }
            }

            //correspondances mesures
            if (this.measure_associations != null)
            {
                _dto.produit_corresp_mesure = new List<produit_corresp_mesure>();
                foreach (var item in this.measure_associations)
                {
                    produit_corresp_mesure pCM = new produit_corresp_mesure();
                    pCM.id_produit_mesure_enfant = item.retail.id;
                    pCM.quantite = item.quantity;
                    pCM.id_produit_mesure_parent = item.bulk.id;
                    pCM.produit_type_mesure_enfant =  item.retail.loadDto();
                    pCM.produit_type_mesure_parent = item.bulk.loadDto();
                    _dto.produit_corresp_mesure.Add(pCM);
                }
            }

            return _dto;
         }

        public List<produit_corresp_mesure> loadAssociationDto()
        {
                List<produit_corresp_mesure> result = new List<produit_corresp_mesure>();
                foreach (var item in this.measure_associations)
                {
                    produit_corresp_mesure pCM = new produit_corresp_mesure();
                    pCM.id = item.id;
                    pCM.id_produit_mesure_parent = (item.bulk != null)? item.bulk.id : 0;
                    pCM.id_produit_mesure_enfant = (item.retail != null) ? item.retail.id : 0;
                    pCM.quantite = item.quantity;
                    result.Add(pCM);
                }
            return result;
        }

        public static List<Product> CreateFromList(List<produit> _dboList)
        {
            List<Product> _list = new List<Product>();
            foreach (produit item in _dboList)
            {
                Product _businessObject = Product.Create(item);
                _list.Add(_businessObject);
            }
            return _list;
        }


    }
}
