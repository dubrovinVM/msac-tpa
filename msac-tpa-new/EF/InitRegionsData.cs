using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using msac_tpa.DAL.EF;
using msac_tpa.DAL.Entities;

namespace msac_tpa_new.EF
{
    public class InitRegionsData
    {
        public static void Initialize(SportmenContext context)
        {
            if (!context.Regions.Any())
            {
                context.Regions.AddRange(
                    new Region() { Name = "Вінницька" },
                    new Region() { Name = "Волинська" },
                    new Region() { Name = "Дніпропетровська" },
                    new Region() { Name = "Донецька" },
                    new Region() { Name = "Житомирська" },
                    new Region() { Name = "Закарпатська" },
                    new Region() { Name = "Запорізька" },
                    new Region() { Name = "Івано-Франківська" },
                    new Region() { Name = "Київ" },
                    new Region() { Name = "Київська" },
                    new Region() { Name = "Кіровоградська" },
                    new Region() { Name = "Луганська" },
                    new Region() { Name = "Львівська" },
                    new Region() { Name = "Миколаївська" },
                    new Region() { Name = "Одеська" },
                    new Region() { Name = "Полтавська" },
                    new Region() { Name = "Республіка Крим" },
                    new Region() { Name = "Рівненська" },
                    new Region() { Name = "Севастополь" },
                    new Region() { Name = "Сумська" },
                    new Region() { Name = "Тернопільська" },
                    new Region() { Name = "Харківська" },
                    new Region() { Name = "Херсонська" },
                    new Region() { Name = "Хмельницька" },
                    new Region() { Name = "Черкаська" },
                    new Region() { Name = "Чернівецька" },
                    new Region() { Name = "Чернігівськa" });
            }
            context.SaveChanges();
        }
    }
}
