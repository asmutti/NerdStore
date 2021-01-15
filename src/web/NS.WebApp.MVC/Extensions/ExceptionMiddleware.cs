using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NS.WebApp.MVC.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (CustomHttpRequestException ex)
            {
                HandleRequestExceptionAsync(httpContext, ex);
            }
        }

        private static void HandleRequestExceptionAsync(HttpContext httpContext, CustomHttpRequestException httpRequestException)
        {
            if (httpRequestException.HttpStatusCode == HttpStatusCode.Unauthorized)
            {
                httpContext.Response.Redirect($"/login?returnUrl={httpContext.Request.Path}");
                return;
            }

            httpContext.Response.StatusCode = (int) httpRequestException.HttpStatusCode;
        }
        
    }
}