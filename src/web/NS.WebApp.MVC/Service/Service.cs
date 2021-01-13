using System;
using System.Net.Http;
using NS.WebApp.MVC.Extensions;

namespace NS.WebApp.MVC.Service
{
    public abstract class Service
    {
        protected bool HandleResponseErrors(HttpResponseMessage response)
        {
            switch ((int) response.StatusCode)
            {
                case 401:
                    
                case 403:
                    
                case 404:
                    
                case 500:
                    throw new CustomHttpRequestException(response.StatusCode);
                
                case 400:
                    return false;
            }

            response.EnsureSuccessStatusCode();

            return true;
        }
    }
}