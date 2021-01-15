using System;
using System.Security.Claims;

namespace NS.WebApp.MVC.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal == null)
            {
                throw new ArgumentException(nameof(claimsPrincipal));
            }

            var claim = claimsPrincipal.FindFirst("sub");
            return claim?.Value;
        }

        public static string GetUserEmail(this ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal == null)
            {
                throw new ArgumentException(nameof(claimsPrincipal));
            }

            var claim = claimsPrincipal.FindFirst("email");
            return claim?.Value;
        }

        public static string GetUserToken(this ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal == null)
            {
                throw new ArgumentException(nameof(claimsPrincipal));
            }

            var claim = claimsPrincipal.FindFirst("Jwt");
            return claim?.Value;
        }
    }
}