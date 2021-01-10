using Microsoft.Extensions.DependencyInjection;
using NS.WebApp.MVC.Service;

namespace NS.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddHttpClient<IAuthService, AuthService>();
        }
    }
}