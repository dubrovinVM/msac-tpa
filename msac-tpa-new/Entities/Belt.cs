using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace msac_tpa_new.Entities
{
    public class Belt
    {
        public int Id { get; set; }
        [Display(Name = "Belt", ResourceType = typeof(@Resources.Models))]
        public string Name { get; set; }

        public IList<AttestationUserBelt> AttestationUserBelts { get; set; }

        public Belt()
        {
            AttestationUserBelts = new List<AttestationUserBelt>();
        }
    }
}
