using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NS.WebApp.MVC.Extensions;
using NS.WebApp.MVC.Models;

namespace NS.WebApp.MVC.Service
{
    public class AuthService : Service, IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.AuthUrl);
            _httpClient = httpClient;
        }

        public async Task<UserLoginResponseModel> Login(UserLoginModel userLoginModel)
        {
            var loginContent = GetContent(userLoginModel);

            var response = await _httpClient.PostAsync($"/api/identity/auth", loginContent);

            if (!HandleResponseErrors(response))
            {
                return new UserLoginResponseModel
                {
                    ResponseResult = await DeserializeObjectResponse<ResponseResultModel>(response)
                };
            }

            return await DeserializeObjectResponse<UserLoginResponseModel>(response);
        }

        public async Task<UserLoginResponseModel> Register(UserRegisterModel userRegisterModel)
        {
            var registerContent = GetContent(userRegisterModel);

            var response = await _httpClient.PostAsync($"/api/identity/register-account", registerContent);
            
            if (!HandleResponseErrors(response))
            {
                return new UserLoginResponseModel
                {
                    ResponseResult = await DeserializeObjectResponse<ResponseResultModel>(response)
                };
            }

            return await DeserializeObjectResponse<UserLoginResponseModel>(response);
        }
    }
}