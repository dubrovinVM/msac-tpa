using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace msac_tpa.DAL.Entities
{
    public class Belt
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IList<AttestationUserBelt> AttestationUserBelts { get; set; }

        public Belt()
        {
            AttestationUserBelts = new List<AttestationUserBelt>();
        }
    }
}
