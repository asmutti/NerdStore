using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using NS.WebApp.MVC.Extensions;
using NS.WebApp.MVC.Service;

namespace NS.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddHttpClient<IAuthService, AuthService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();
        }
    }
}