using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace msac_tpa.DAL.Entities
{
    public class AttestationUserBelt
    {
        [Key]
        public int Id { get; set; }

        public int AttestationId { get; set; }
        public Attestation Attestation { get; set; }

        public int SportManId { get; set; }
        public Sportman SportMan { get; set; }

        public int BeltId { get; set; }
        public Belt Belt { get; set; }
    }
}
