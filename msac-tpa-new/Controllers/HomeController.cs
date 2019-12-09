using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using msac_tpa_new.EF;
using Microsoft.AspNetCore.Mvc;
using msac_tpa_new.Models;
using Microsoft.AspNetCore.Authorization;

namespace msac_tpa_new.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public HomeController()
        {
        }
        public IActionResult Index()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
