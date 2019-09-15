using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using msac_tpa.DAL.Entities;

namespace msac_tpa_new.Entities
{
    public class SportmanComission
    {
        public int ComissionId { get; set; }
        public Comission Comission { get; set; }

        public int SportmanId { get; set; }
        public Sportman Sportman { get; set; }
    }
}
