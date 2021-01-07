using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NS.Identity.API.Extensions;
using NS.Identity.API.Models;

namespace NS.Identity.API.Services
{
    public class JwtService : IJwtService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;

        public JwtService(UserManager<IdentityUser> userManager, IOptions<AppSettings> appSettings)
        {
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }

        public async Task<UserResponseLoginModel> GenerateJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);

            var identityClaims = await GetUserClaims(claims, user);
            var encodedToken = EncodeToken(identityClaims);

            var response = GetResponseToken(encodedToken, user, claims);

            return response;
        }
        
        private async Task<ClaimsIdentity> GetUserClaims (ICollection<Claim> claims, IdentityUser user) 
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), 
                ClaimValueTypes.Integer64));

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            return identityClaims;
        }
        
        private static long ToUnixEpochDate(DateTime dateTime)
            => (long) Math.Round((dateTime.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                .TotalSeconds);

        private string EncodeToken(ClaimsIdentity identityClaims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.ValidAt,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpirationHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            return encodedToken;
        }

        private UserResponseLoginModel GetResponseToken(string encodedToken, IdentityUser user, ICollection<Claim> claims)
        {
            var response = new UserResponseLoginModel
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpirationHours).TotalSeconds,
                UserToken = new UserTokenModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(c => new UserClaimModel
                    {
                        Type = c.Type,
                        Value = c.Value
                    })
                }
            };

            return response;
        }
    }
}