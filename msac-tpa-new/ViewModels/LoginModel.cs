using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace msac_tpa_new.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Error),ErrorMessageResourceName = "NoEmail" ) ]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Error), ErrorMessageResourceName = "NoPassword")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
