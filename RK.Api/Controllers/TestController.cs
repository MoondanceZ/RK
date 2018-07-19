using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RK.Infrastructure;

namespace RK.Api.Controllers
{    
    [AllowAnonymous]
    [Route("[controller]")]
    public class TestController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Json(ReturnStatus.Success("test success"));
        }
    }
}