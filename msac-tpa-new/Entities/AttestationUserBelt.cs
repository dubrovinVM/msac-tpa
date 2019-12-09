using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace msac_tpa_new.Entities
{
    public class AttestationUserBelt
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int AttestationId { get; set; }
        public Attestation Attestation { get; set; }

        public int SportmanId { get; set; }
        public Sportman Sportman { get; set; }

        public int BeltId { get; set; }
        public Belt Belt { get; set; }
    }
}
