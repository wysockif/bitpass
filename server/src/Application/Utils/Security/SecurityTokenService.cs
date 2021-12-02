using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Settings;
using Microsoft.IdentityModel.Tokens;

namespace Application.Utils.Security
{
    public interface ISecurityTokenService
    {
        string GenerateAccessTokenForUser(long userId, string username);
    }
    
    public class SecurityTokenService : ISecurityTokenService
    {
        private readonly ApplicationSettings _applicationSettings;

        public SecurityTokenService(ApplicationSettings applicationSettings)
        {
            _applicationSettings = applicationSettings;
        }

        public string GenerateAccessTokenForUser(long userId, string username)
        {
            var claims = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Name, username)
            });

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_applicationSettings.AccessToken.Key));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiresAt = DateTime.Now.AddHours(_applicationSettings.AccessToken.ExpiryTimeInHours);

            var tokenDescriptor = new SecurityTokenDescriptor
                { Subject = claims, Expires = expiresAt, SigningCredentials = signingCredentials };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}