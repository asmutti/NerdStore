using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using NS.WebApp.MVC.Models;

namespace NS.WebApp.MVC.Service
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserLoginResponseModel> Login(UserLoginModel userLoginModel)
        {
            var loginContent = new StringContent(
                JsonSerializer.Serialize(userLoginModel), 
                Encoding.UTF8, 
                "application/json");

            var response = await _httpClient.PostAsync("https://localhost:5001/api/identity/auth", loginContent);

            return JsonSerializer.Deserialize<UserLoginResponseModel>(await response.Content.ReadAsStringAsync());
        }

        public async Task<string> Register(UserRegisterModel userRegisterModel)
        {
            var registerContent = new StringContent(
                JsonSerializer.Serialize(userRegisterModel), 
                Encoding.UTF8, 
                "application/json");

            var response = await _httpClient.PostAsync("https://localhost:44340/api/identity/new-account", registerContent);

            return JsonSerializer.Deserialize<string>(await response.Content.ReadAsStringAsync());
        }
    }
}