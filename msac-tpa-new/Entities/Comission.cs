using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using msac_tpa_new.Entities;

namespace msac_tpa_new.Entities
{
    public class Comission
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Title", ResourceType = typeof(@Resources.Models))]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Region", ResourceType = typeof(@Resources.Models))]
        public int RegionId { get; set; }
        [Display(Name = "Region", ResourceType = typeof(@Resources.Models))]
        public Region Region { get; set; }

        [Display(Name = "MembersOfComission", ResourceType = typeof(@Resources.Models))]
        public ICollection<SportmanComission> SportmanComissions { get; set; } 

        public ICollection<Attestation> Attestations { get; set; }

        public Comission()
        {
            SportmanComissions = new List<SportmanComission>();
            Attestations = new List<Attestation>();
        }
    }
}
