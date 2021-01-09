using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NS.WebApp.MVC.Models;

namespace NS.WebApp.MVC.Controllers
{
    public class IdentityController : Controller
    {
        
        [HttpGet("new-account")]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost("new-account")]
        public async Task<IActionResult> Register(UserRegisterModel userRegisterModel)
        {
            if (!ModelState.IsValid)
                return View(userRegisterModel);
            
            //API Register

            if (false)
                return View(userRegisterModel);

            //Realizar login

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("login")]
        public async Task<IActionResult> Login()
        {
            return View();
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginModel userLoginModel)
        {
            if (!ModelState.IsValid)
                return View(userLoginModel);
            
            //API Register

            if (false)
                return View(userLoginModel);

            //Realizar login

            return RedirectToAction("Index", "Home");
        }
        
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}