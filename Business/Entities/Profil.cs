using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItCommerce.DTO.ModelDesign;
using ItCommerce.Business.Actions;

namespace ItCommerce.Business.Entities
{
    public class Profil
    {
        public int id { get; set; }
        public string login { get; set; }
        public string code { get; set; }
        public User user { get; set; }
        public Group group { get; set; }
        public Agency agency { get; set; }
        public Profil agent { get; set; }
        public DateTime? date_of_creation { get; set; }


        public static Profil Create(profil _dbo)
        {
            Licence _lastLicence = OfficeAuth.getLastLicence();
            if(_lastLicence!= null)  _lastLicence.key = string.Empty;

            Profil newProfil = new Profil()
            {
                id = _dbo.id,
                login = _dbo.login,
                code = _dbo.pwd,
                user = (_dbo.user != null)? User.Create(_dbo.user): null,
                agency = (_dbo.agence != null)? Agency.Create(_dbo.agence): null,
                group = (_dbo.groupe != null)? Group.Create(_dbo.groupe): null,
                date_of_creation = _dbo.date_creation
            };
            if (newProfil.agency != null  && newProfil.agency.company != null)
            {
                newProfil.agency.company.currentLicence = _lastLicence;
                newProfil.agency.company.logo = (newProfil.agency.company.logo != null)? newProfil.agency.company.logo : "logo.png";
            }

            return newProfil;
        }

        public static Profil CreateSimple(profil _dbo)
        {
            //Licence _lastLicence = OfficeAuth.getLastLicence();
            //if (_lastLicence != null) _lastLicence.key = string.Empty;

            Profil newProfil = new Profil()
            {
                id = _dbo.id,
                login = _dbo.login,
                code = _dbo.pwd,
                user = (_dbo.user != null) ? User.Create(_dbo.user) : null,
                agency = (_dbo.agence != null) ? Agency.Create(_dbo.agence) : null,
                group = (_dbo.groupe != null) ? Group.Create(_dbo.groupe) : null,
                date_of_creation = _dbo.date_creation,
            };
            if (newProfil.agency != null && newProfil.agency.company != null)
            {
                newProfil.agency.company.currentLicence = new Licence();
                newProfil.agency.company.logo = (newProfil.agency.company.logo != null) ? newProfil.agency.company.logo : "logo.png";
            }

            return newProfil;
        }

        public profil loadDto()
        {
            profil _dto = new profil()
            {
                id = this.id,
                login = this.login,
                pwd = this.code,
                id_user = (this.user != null)? this.user.id: 0,
                id_groupe = (this.group != null) ? this.group.id : 0,
                id_agence = (this.agency != null) ? this.agency.id : 0,
                date_creation = this.date_of_creation,
            };
            return _dto;
        }

        public static List<Profil> CreateFromList(List<profil> _dboList)
        {
            List<Profil> _list = new List<Profil>();
            foreach (profil item in _dboList)
            {
                Profil _businessObject = Profil.CreateSimple(item);
                _list.Add(_businessObject);
            }
            return _list;
        }

    }
}
