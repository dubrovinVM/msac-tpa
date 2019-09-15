using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace msac_tpa.DAL.Entities
{
    public class Attestation
    {
        public int Id { get; set; }
        public DateTime IssueDate { get; set; }
        public string OrderFilePath { get; set; }
        public string DecisionFilePath { get; set; }

        public int ComissionId { get; set; }
        public Comission Comission { get; set; }

        public IList<AttestationUserBelt> AttestationUserBelts { get; set; }

        public Attestation()
        {
            AttestationUserBelts = new List<AttestationUserBelt>();
        }
    }
}
