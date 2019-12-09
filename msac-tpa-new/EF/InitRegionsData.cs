using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using msac_tpa_new.EF;
using msac_tpa_new.Entities;

namespace msac_tpa_new.EF
{
    public class InitRegionsData
    {
        public static void Initialize(SportmenContext context)
        {
            if (!context.Regions.Any())
            {
                context.Regions.AddRange(
                    new Region() { Name = "Вінницька область" },
                    new Region() { Name = "Волинська область" },
                    new Region() { Name = "Дніпропетровська область" },
                    new Region() { Name = "Донецька область" },
                    new Region() { Name = "Житомирська область" },
                    new Region() { Name = "Закарпатська область" },
                    new Region() { Name = "Запорізька область" },
                    new Region() { Name = "Івано-Франківська область" },
                    new Region() { Name = "Київ" },
                    new Region() { Name = "Київська область" },
                    new Region() { Name = "Кіровоградська область" },
                    new Region() { Name = "Луганська область" },
                    new Region() { Name = "Львівська область" },
                    new Region() { Name = "Миколаївська область" },
                    new Region() { Name = "Одеська область" },
                    new Region() { Name = "Полтавська область" },
                    new Region() { Name = "Республіка Крим" },
                    new Region() { Name = "Рівненська область" },
                    new Region() { Name = "Севастополь" },
                    new Region() { Name = "Сумська область" },
                    new Region() { Name = "Тернопільська область" },
                    new Region() { Name = "Харківська область" },
                    new Region() { Name = "Херсонська область" },
                    new Region() { Name = "Хмельницька область" },
                    new Region() { Name = "Черкаська область" },
                    new Region() { Name = "Чернівецька область" },
                    new Region() { Name = "Чернігівськa" },
                    new Region() { Name = "Національна федерація" });
            }
            context.SaveChanges();
        }
    }
}
