using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using the_wall.Models;

namespace the_wall.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("wall")]
        public IActionResult Index()
        {
            return View("Wall");
        }
    }
}
