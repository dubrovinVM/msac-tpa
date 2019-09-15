using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using msac_tpa.DAL.EF;
using msac_tpa.DAL.Entities;

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
                    new Sportman {Name = "Володимир", Surname = "Герасименко", LastName = "Михайлович", BirthDay = DateTime.Now.AddYears(-33), RegionId=18, AvatarFilePath = "Gerasimenko.jpg" },
                    new Sportman {Name = "Андрій", Surname = "Бурьян", LastName = "Михайлович", BirthDay = DateTime.Now.AddYears(-33), RegionId=18, AvatarFilePath = "Bur'yan.jpg" },
                    new Sportman {Name = "Андрій", Surname = "Савельєв", LastName = "Михайлович", BirthDay = DateTime.Now.AddYears(-33), RegionId=18, AvatarFilePath = "Cavel'єv.jpeg" },
                    new Sportman {Name = "Сергій", Surname = "Костів", LastName = "Михайлович", BirthDay = DateTime.Now.AddYears(-33), RegionId=18, AvatarFilePath = "Kostіv.jpg" },
                    new Sportman {Name = "Володимир", Surname = "Купрін", LastName = "Михайлович", BirthDay = DateTime.Now.AddYears(-33), RegionId=18, AvatarFilePath = "Kuprіn.jpg" },
                    new Sportman {Name = "Влад", Surname = "Откидач", LastName = "Михайлович", BirthDay = DateTime.Now.AddYears(-33), RegionId=22, AvatarFilePath = "Otkidach.jpg" }
                );
                context.SaveChanges();
            }
        }
    }
}
