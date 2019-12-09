using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace msac_tpa_new.Components
{
    public class TimerViewComponent
    {
        public string Invoke()
        {
            return $"{DateTime.Now.ToString("hh:mm:ss")}";
        }
    }
}
