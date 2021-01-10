using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NS.WebApp.MVC.Models;
using NS.WebApp.MVC.Service;

namespace NS.WebApp.MVC.Controllers
{
    public class IdentityController : Controller
    {
        private readonly IAuthService _authService;

        public IdentityController(IAuthService authService)
        {
            _authService = authService;
        }

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
            var response = await _authService.Register(userRegisterModel);

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

            var response = await _authService.Login(userLoginModel);

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