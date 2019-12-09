using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace msac_tpa_new.Entities.Authentication
{
    public class User
    {
        public int Id { get; set; }
        [Display(Name = "Email", ResourceType = typeof(@Resources.Models))]
        public string Email { get; set; }
        [Display(Name = "Password", ResourceType = typeof(@Resources.Models))]
        public string Password { get; set; }

        [Display(Name = "Role", ResourceType = typeof(@Resources.Models))]
        public int? RoleId { get; set; }
        public Role Role { get; set; }

        [Display(Name = "Region", ResourceType = typeof(@Resources.Models))]
        public int RegionId { get; set; }
        [Display(Name = "Region", ResourceType = typeof(@Resources.Models))]
        public Region Region { get; set; }
    }

    public class Role
    {
        public int Id { get; set; }
        [Display(Name = "Role", ResourceType = typeof(@Resources.Models))]
        public string Name { get; set; }
        [Display(Name = "Role", ResourceType = typeof(@Resources.Models))]
        public string Description { get; set; }
        public List<User> Users { get; set; }
        public Role()
        {
            Users = new List<User>();
        }
    }
}
