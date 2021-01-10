using System.Threading.Tasks;
using NS.WebApp.MVC.Models;

namespace NS.WebApp.MVC.Service
{
    public interface IAuthService
    {
        Task<UserLoginResponseModel> Login(UserLoginModel userLoginModel);

        Task<string> Register(UserRegisterModel userRegisterModel);
    }
}