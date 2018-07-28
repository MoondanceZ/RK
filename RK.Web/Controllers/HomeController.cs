using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RK.Infrastructure;

namespace RK.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {            
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
