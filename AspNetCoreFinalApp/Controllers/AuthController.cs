using AspNetCoreFinalApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AspNetCoreFinalApp.Controllers
{
    [Authorize]
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return Redirect("Home");
        }
        [AllowAnonymous]
        public IActionResult Login(string ReturnUrl) 
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(AuthViewModal model) 
        {
            if (!ModelState.IsValid) 
            {
                return View(model);
            }

            List<Claim> claims = new List<Claim> {
                new Claim("claimName", "claimValue")
            };
            var claimIdentity = new ClaimsIdentity(claims, "Cookie");
            var claimsPrincipal = new ClaimsPrincipal(claimIdentity);
            await HttpContext.SignInAsync(claimsPrincipal);

            return Redirect(model.ReturnUrl);
        }
    }
}
