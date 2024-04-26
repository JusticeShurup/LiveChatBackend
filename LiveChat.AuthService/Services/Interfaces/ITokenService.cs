using LiveChat.AuthService.Models;
using System.Security.Claims;

namespace LiveChat.AuthService.Services.Interfaces
{
    public interface ITokenService
    {
        public string GenerateAccessToken(ApplicationUser user, List<Claim> claims);
        public string GenerateRefreshToken(ApplicationUser user, List<Claim> claims);
        public IEnumerable<Claim> GetClaims(string token);
    }
}
