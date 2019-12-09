using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using msac_tpa_new.BusinessLogic;
using msac_tpa_new.EF;
using msac_tpa_new.Entities;
using msac_tpa_new.Entities.Authentication;

namespace msac_tpa_new.EF
{
    public class InitSportmenData
    {
        public static void Initialize(SportmenContext context)
        {
            if (!context.SportMans.Any())
            {
                context.SportMans.AddRange(
                    new Sportman {Name = "Віктор", Surname = "Дубровін", LastName = "Михайлович", BirthDay = DateTime.Parse("1986-04-30 00:00:00.9520252"), RegionId=18, AvatarFilePath = "Dubrovіn.jpg" },
                    new Sportman {Name = "Володимир", Surname = "Герасименко", LastName = "Михайлович", BirthDay = DateTime.Parse("1975-01-04 00:00:00.9520252"), RegionId=18, AvatarFilePath = "Gerasimenko.jpg" },
                    new Sportman {Name = "Андрій", Surname = "Бурьян", LastName = "Володимирович", BirthDay = DateTime.Parse("1976-02-04 00:00:00.9520252"), RegionId=18, AvatarFilePath = "Buryan.jpg" },
                    new Sportman {Name = "Андрій", Surname = "Савельєв", LastName = "Станіславович", BirthDay = DateTime.Parse("1975-05-17 00:00:00.9520252"), RegionId=18, AvatarFilePath = "Savelev.jpeg" },
                    new Sportman {Name = "Сергій", Surname = "Костів", LastName = "Федорович", BirthDay = DateTime.Parse("1986-12-12 00:00:00.9520252"), RegionId=18, AvatarFilePath = "Kostіv.jpg" },
                    new Sportman {Name = "Володимир", Surname = "Купрін", LastName = "Михайлович", BirthDay = DateTime.Parse("1983-01-20 00:00:00.9520252"), RegionId=18, AvatarFilePath = "Kuprіn.jpg" },
                    new Sportman {Name = "Максим", Surname = "Пономаренко ", LastName = "Олександрович", BirthDay = DateTime.Parse("1987-01-15 00:00:00.9520252"), RegionId=18, AvatarFilePath = "Ponomarenko.jpg" }
                );
                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                string adminEmail = "vites@outlook.com";
                string adminPassword = Hashing.GetHash("30041986");

                User adminUser = new User { Email = adminEmail, RegionId = 1, Password = adminPassword, RoleId = context.Roles.FirstOrDefault(a => a.Name.Equals("admin"))?.Id };
                context.Users.Add(adminUser);
                context.SaveChanges();
            }
        }
    }
}
