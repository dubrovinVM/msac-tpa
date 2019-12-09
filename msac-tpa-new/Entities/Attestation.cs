using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace msac_tpa_new.Entities
{
    public class Attestation
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "IssueDate", ResourceType = typeof(@Resources.Models))]
        public DateTime IssueDate { get; set; }

        [Display(Name = "OrderFilePath", ResourceType = typeof(@Resources.Models))]
        public string OrderFilePath { get; set; }

        [Display(Name = "DescisionFilePath", ResourceType = typeof(@Resources.Models))]
        public string DescisionFilePath { get; set; }
        [Required]
        [Display(Name = "Address", ResourceType = typeof(@Resources.Models))]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Comission", ResourceType = typeof(@Resources.Models))]
        public int ComissionId { get; set; }
        [Display(Name = "Comission", ResourceType = typeof(@Resources.Models))]
        public Comission Comission { get; set; }
        [Required]
        [Display(Name = "RegionOrg", ResourceType = typeof(@Resources.Models))]
        public int RegionId { get; set; }
        [Display(Name = "RegionOrg", ResourceType = typeof(@Resources.Models))]
        public Region Region { get; set; }

        public IList<AttestationUserBelt> AttestationUserBelts { get; set; }

        public Attestation()
        {
            AttestationUserBelts = new List<AttestationUserBelt>();
        }
    }
}
