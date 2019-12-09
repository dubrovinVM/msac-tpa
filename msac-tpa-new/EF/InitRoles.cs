using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using msac_tpa_new.BusinessLogic;
using msac_tpa_new.Entities;
using msac_tpa_new.Entities.Authentication;

namespace msac_tpa_new.EF
{
    public class InitRoles
    {
        public static void Initialize(SportmenContext context)
        {
            if (!context.Roles.Any())
            {
                string adminRoleName = "admin";
                string headerRoleName = "header";
                string userRoleName = "user";

                Role adminRole = new Role { Name = adminRoleName, Description = "Адміністратор"};
                Role headerRole = new Role { Name = headerRoleName, Description = "Голова Відокремленого Підрозділу" };
                Role userRole = new Role { Name = userRoleName, Description = "Користувач" };

                context.Roles.AddRange(adminRole, headerRole, userRole);
                context.SaveChanges();
            }
           
        }
    }
}
