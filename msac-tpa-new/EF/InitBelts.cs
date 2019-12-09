using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using msac_tpa_new.EF;
using msac_tpa_new.Entities;

namespace msac_tpa_new.EF
{
    public static class InitBeltData
    {
        public static void Initialize(SportmenContext context)
        {
            if (!context.Belts.Any())
            {
                context.Belts.AddRange(
                    new Belt {Name = "Білий пояс" },
                    new Belt {Name = "Білий пояс 1-го ступеню" },
                    new Belt {Name = "Жовтий пояс" },
                    new Belt {Name = "Жовтий пояс 1-го ступеню" },
                    new Belt {Name = "Оранжевий пояс" },
                    new Belt {Name = "Оранжевий пояс 1-го ступеню" },
                    new Belt {Name = "Синій пояс" },
                    new Belt {Name = "Синій пояс 1-го ступеню" },
                    new Belt {Name = "Зелений пояс" },
                    new Belt { Name = "Зелений пояс 1-го ступеню" },
                    new Belt { Name = "Коричневий пояс" },
                    new Belt { Name = "Коричневий пояс 1-го ступеню " }
                );
                context.SaveChanges();
                context.Belts.AddRange(
                    new Belt {Name = "Чорний пояс 1-й дан"},
                    new Belt {Name = "Чорний пояс 2-й дан"},
                    new Belt {Name = "Чорний пояс 3-й дан"},
                    new Belt {Name = "Чорний пояс 4-й дан"},
                    new Belt {Name = "Чорний пояс 5-й дан"},
                    new Belt {Name = "Чорний пояс 6-й дан"},
                    new Belt {Name = "Чорний пояс 7-й дан"},
                    new Belt {Name = "Почесний Чорний пояс 1-й дан"},
                    new Belt {Name = "Почесний Чорний пояс 2-й дан" },
                    new Belt {Name = "Почесний Чорний пояс 3-й дан" }
                    );
                context.SaveChanges();

            }
            
        }
    }

}


