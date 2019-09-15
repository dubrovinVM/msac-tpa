using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace msac_tpa.DAL.Entities
{
    public class Region
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IList<Comission> Comissions { get; set; }
        public IList<Sportman> Sportmans { get; set; }

        public Region()
        {
            Comissions = new List<Comission>();
            Sportmans = new List<Sportman>();
        }
    }
}
