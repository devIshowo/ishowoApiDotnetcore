using ItCommerce.Business.Entities;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Class
    {
        [Required(ErrorMessage = "The company data are required")]
        [StringLength(5)]
        public string company { get; set; }
        [Required(ErrorMessage = "The admin data are required")]
        public string admin { get; set; } 
    }
}
