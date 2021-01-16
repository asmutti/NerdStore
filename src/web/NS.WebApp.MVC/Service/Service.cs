using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using NS.WebApp.MVC.Extensions;

namespace NS.WebApp.MVC.Service
{
    public abstract class Service
    {
        protected StringContent GetContent(object data)
        {
            return new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
        }

        protected async Task<T> DeserializeObjectResponse<T>(HttpResponseMessage responseMessage)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                IgnoreNullValues = true,
            };
            
            return JsonSerializer.Deserialize<T>(await responseMessage.Content.ReadAsStringAsync(), options);
        }
        
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