using System.Threading.Tasks;
using NS.Identity.API.Models;

namespace NS.Identity.API.Services
{
    public interface IJwtService
    {
        Task<UserResponseLoginModel> GenerateJwt(string email);
    }
}