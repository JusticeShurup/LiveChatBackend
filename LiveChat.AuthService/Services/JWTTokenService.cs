 using LiveChat.AuthService.Models;
using LiveChat.AuthService.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LiveChat.AuthService.Services
{
    public class JWTTokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

        public JWTTokenService(IConfiguration configuration) 
        {
            _configuration = configuration;
            _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        }

        public string GenerateAccessToken(ApplicationUser user, List<Claim> claims)
        {
            var token = new JwtSecurityToken
                (
                issuer: _configuration["JwtToken:Issuer"]!,
                audience: _configuration["JwtToken:Audience"]!,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddSeconds(double.Parse(_configuration["JwtToken:AccessTokenLifetimeSeconds"]!)),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtToken:SecretKey"]!)), SecurityAlgorithms.HmacSha256)
                );

            return _jwtSecurityTokenHandler.WriteToken(token)!;
        }

        public string GenerateRefreshToken(ApplicationUser user, List<Claim> claims)
        {
            var token = new JwtSecurityToken
                (
                issuer: _configuration["JwtToken:Issuer"]!,
                audience: _configuration["JwtToken:Audience"]!,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddDays(double.Parse(_configuration["JwtToken:RefreshTokenLifetimeDays"]!)),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtToken:SecretKey"]!)), SecurityAlgorithms.HmacSha256)
                );

            return _jwtSecurityTokenHandler.WriteToken(token)!;
        }

        public IEnumerable<Claim> GetClaims(string token)
        {
            JwtSecurityToken jwtToken = _jwtSecurityTokenHandler.ReadJwtToken(token);
            
            return jwtToken.Claims;
        }
    }
}
