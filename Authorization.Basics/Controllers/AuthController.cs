using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authorization.Basics.Controllers
{
    [Authorize]
    public class AuthController : Controller
    {
        public IActionResult Index() 
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult Login()
        {

            return View();
        }
    }
}
