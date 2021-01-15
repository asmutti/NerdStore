using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using NS.WebApp.MVC.Models;
using NS.WebApp.MVC.Service;

namespace NS.WebApp.MVC.Controllers
{
    public class IdentityController : MainController
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
            var userLoginResponseModel = await _authService.Register(userRegisterModel);

            if (ResponseHasErrors(userLoginResponseModel.ResponseResult))
                return View(userRegisterModel);

            //Realizar login
            await DoLogin(userLoginResponseModel);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("login")]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginModel userLoginModel, string returnUrl = null)
        {
            if (!ModelState.IsValid)
                return View(userLoginModel);
             
            //API Register
            var userLoginResponseModel = await _authService.Login(userLoginModel);

            if (ResponseHasErrors(userLoginResponseModel.ResponseResult))
                return View(userLoginModel);

            await DoLogin(userLoginResponseModel);

            return string.IsNullOrEmpty(returnUrl) 
                ? RedirectToAction("Index", "Home") 
                : RedirectToAction(returnUrl);
        }
        
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            
            return RedirectToAction("Index", "Home");
        }

        private async Task DoLogin(UserLoginResponseModel userLoginResponseModel)
        {
            var token = GetJwtSecurityToken(userLoginResponseModel.AccessToken);

            var claims = new List<Claim> {new Claim("Jwt", userLoginResponseModel.AccessToken)};
            claims.AddRange(token.Claims);

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.Now.AddMinutes(60),
                IsPersistent = true
            };

            claims.AddRange(token.Claims);
            
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), 
                authProperties);
        }

        private static JwtSecurityToken GetJwtSecurityToken(string jwtToken)
        {
            return new JwtSecurityTokenHandler().ReadJwtToken(jwtToken);
        }
    }
}