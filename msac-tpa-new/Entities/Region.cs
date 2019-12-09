using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using msac_tpa_new.Entities.Authentication;

namespace msac_tpa_new.Entities
{
    public class Region
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IList<Comission> Comissions { get; set; }
        public IList<Sportman> Sportmans { get; set; }
        public IList<Attestation> Attestations { get; set; }
        public IList<User> Users { get; set; }

        public Region()
        {
            Comissions = new List<Comission>();
            Sportmans = new List<Sportman>();
            Users = new List<User>();
            Attestations = new List<Attestation>();
        }
    }
}
