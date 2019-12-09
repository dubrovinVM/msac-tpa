using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using msac_tpa_new.Entities;

namespace msac_tpa_new.ViewModels
{
    public class SportmenViewModel
    {
        public IEnumerable<Sportman> Sportmen { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
