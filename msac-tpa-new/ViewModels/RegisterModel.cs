using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using msac_tpa_new.Entities;

namespace msac_tpa_new.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Error), ErrorMessageResourceName = "NoEmail")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Error), ErrorMessageResourceName = "NoPassword")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessageResourceType = typeof(Resources.Error), ErrorMessageResourceName = "NoPasswordMatch")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Region", ResourceType = typeof(@Resources.Models))]
        public int RegionId { get; set; }
        [Display(Name = "Region", ResourceType = typeof(@Resources.Models))]
        public Region Region { get; set; }

        [Display(Name = "Role", ResourceType = typeof(@Resources.Models))]
        public int RoleId { get; set; }
    
    }
}
