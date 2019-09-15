using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using msac_tpa_new.Entities;

namespace msac_tpa.DAL.Entities
{
    public class Sportman
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Name", ResourceType = typeof(@Resources.Models))]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Surname", ResourceType = typeof(@Resources.Models))]
        public string Surname { get; set; }
        [Required]
        [Display(Name = "LastName", ResourceType = typeof(@Resources.Models))]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Birthday", ResourceType = typeof(@Resources.Models))]
        public DateTime BirthDay { get; set; }

        [Display(Name = "AvatarFilePath", ResourceType = typeof(@Resources.Models))]
        public string AvatarFilePath { get; set; }

        [Required]
        [Display(Name = "Region", ResourceType = typeof(@Resources.Models))]
        public int RegionId { get; set; }
        [Display(Name = "Region", ResourceType = typeof(@Resources.Models))]
        public Region Region { get; set; }

        [NotMapped]
        public string Fullname => string.Format($"{Surname} {Name} {LastName}");

        public IList<AttestationUserBelt> AttestationUserBelts { get; set; }
        public IList<SportmanComission> SportmanComissions { get; set; }

        public Sportman()
        {
            AttestationUserBelts = new List<AttestationUserBelt>();
            SportmanComissions = new List<SportmanComission>();
        }
    }
}
