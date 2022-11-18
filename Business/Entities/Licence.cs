using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItCommerce.DTO.ModelDesign;

namespace ItCommerce.Business.Entities
{
    public class Licence
    {


        public int id { get; set; }
        public Company company { get; set; }
        [Required(ErrorMessage ="The company data are required")]
        public User admin { get; set; }
        [Required(ErrorMessage ="The admin data are required")]
        public string code { get; set; }
        public string key { get; set; }
        public bool isActive { get; set; }
        public DateTime activationDate { get; set; }
        public DateTime expiryDate { get; set; }

        private DateTime _now;
        public DateTime currentDate { get { return _now; } set { _now = value; } }
        public int activationCost { get; set; }
        public string module { get; set; }
        public string act { get; set; }

        public licence loadDto()
        {
            licence _dto = new licence()
            {
                id = this.id,
                code = this.code,
                cle = this.key,
                id_entreprise = (this.company != null) ? this.company.id : 0,
                est_active = this.isActive,
                date_activation = this.activationDate,
                date_expiration = this.expiryDate,
                montant_paye = this.activationCost,
            };
            return _dto;
        }

        public static Licence Create(licence _dbo)
        {
            return new Licence()
            {
                id = _dbo.id,
                code = _dbo.code,
                key = _dbo.cle,
                company = (_dbo.entreprise != null) ? Company.Create(_dbo.entreprise) : null,
                isActive = _dbo.est_active,
                activationDate = _dbo.date_activation,
                expiryDate = _dbo.date_expiration,
                activationCost = _dbo.montant_paye,
            };
        }

        public static List<Licence> CreateFromList(List<licence> _dboList)
        {
            List<Licence> _list = new List<Licence>();
            foreach (licence item in _dboList)
            {
                Licence _businessObject = Licence.Create(item);
                _list.Add(_businessObject);
            }
            return _list;
        }

        public override string ToString()
        {
            string formatted = string.Format("{0};{1};{2};{3};{4};{5}", this.key, this.isActive, this.activationDate.ToShortDateString(),
                this.currentDate.ToString("yyyy-MM-dd HH:mm tt"), this.expiryDate.ToShortDateString(), this.module);
            return formatted.ToString();
        }

        public static Licence Extract(string formatted)
        {
            try
            {
                if (formatted == null || formatted == string.Empty) return null;

                string[] splitted = formatted.Split(';');
                if(splitted.Length < 6) return null;

                Licence newLicence = new Licence();
                newLicence.key = splitted[0];
                newLicence.isActive = (splitted[1].ToLower() == "true") ? true : false;

                newLicence.activationDate = DateTime.Parse(splitted[2]);

                newLicence.currentDate = DateTime.Parse(splitted[3]);
                newLicence.expiryDate = DateTime.Parse(splitted[4]);
                newLicence.module = splitted[5];

                return newLicence;
            }
            catch (Exception ex)
            {
                throw ex;
                //return null;
            }
        }

    }
}
