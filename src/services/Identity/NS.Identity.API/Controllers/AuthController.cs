using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NS.Identity.API.Models;
using NS.Identity.API.Services;

namespace NS.Identity.API.Controllers
{
    [Route("api/identiy")]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IJwtService _jwtService;

        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager,
            IJwtService jwtService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtService = jwtService;
        }

        [HttpPost("register-account")]
        public async Task<ActionResult> Register(UserRegisterViewModel userRegisterViewModel)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var newUser = new IdentityUser()
            {
                UserName = userRegisterViewModel.Email,
                Email = userRegisterViewModel.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(newUser, userRegisterViewModel.Password);

            foreach (var error in result.Errors)
            {
                AddError(error.Description);
            }

            return CustomResponse();

        }

        [HttpPost("auth")]
        public async Task<ActionResult> Login(UserLoginViewModel userLoginViewModel)
        {
            if (!ModelState.IsValid)
                return CustomResponse();

            var result =
                await _signInManager.PasswordSignInAsync(userLoginViewModel.Email, userLoginViewModel.Password, false,
                    true);

            if (result.Succeeded)
            {
                var jwt = await _jwtService.GenerateJwt(userLoginViewModel.Email);
                return CustomResponse(jwt);
            }

            if (result.IsLockedOut)
            {
                AddError("User is temporarily blocked for too many wrong attempts to login.");
                return CustomResponse();
            }

            AddError("User or password is invalid.");

            return CustomResponse();
        }
    }
}