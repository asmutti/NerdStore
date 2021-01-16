using System;
using System.Net;

namespace NS.WebApp.MVC.Extensions
{
    public class CustomHttpRequestException : Exception
    {
        public HttpStatusCode HttpStatusCode;

        public CustomHttpRequestException() {}

        public CustomHttpRequestException(string message, Exception innerException) : base(message, innerException) {}

        public CustomHttpRequestException(HttpStatusCode httpStatusCode)
        {
            HttpStatusCode = httpStatusCode;
        }
    }
}