using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Application.Settings;
using Microsoft.IdentityModel.Tokens;

namespace Application.Utils.Security
{
    public interface ISecurityTokenService
    {
        AccessToken GenerateAccessTokenForUser(long userId, string username);
        RefreshToken GenerateRefreshTokenForUser(long userId, string username);
        long? GetUserIdFromRefreshToken(string refreshToken);
        Guid? GetTokenGuidFromRefreshToken(string refreshToken);
        long? GetExpirationTimestampFromRefreshToken(string refreshToken);
    }

    public record AccessToken(string Token, long ExpirationTimestamp);
    public record RefreshToken(string Token, Guid TokenGuid, long ExpirationTimestamp);

    public class SecurityTokenService : ISecurityTokenService
    {
        private readonly ApplicationSettings _applicationSettings;

        public SecurityTokenService(ApplicationSettings applicationSettings)
        {
            _applicationSettings = applicationSettings;
        }

        public AccessToken GenerateAccessTokenForUser(long userId, string username)
        {
            var claims = new ClaimsIdentity(new[]
            {
                new Claim("type", "access"),
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
            return new AccessToken(tokenHandler.WriteToken(token), ((DateTimeOffset)expiresAt).ToUnixTimeSeconds());
        }

        public RefreshToken GenerateRefreshTokenForUser(long userId, string username)
        {
            var jti = Guid.NewGuid();
            var claims = new ClaimsIdentity(new[]
            {
                new Claim("type", "refresh"),
                new Claim(JwtRegisteredClaimNames.Jti, jti.ToString()),
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Name, username)
            });

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_applicationSettings.RefreshToken.Key));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiresAt = DateTime.Now.AddHours(_applicationSettings.RefreshToken.ExpiryTimeInHours);

            var tokenDescriptor = new SecurityTokenDescriptor
                { Subject = claims, Expires = expiresAt, SigningCredentials = signingCredentials };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new RefreshToken(tokenHandler.WriteToken(token), jti, ((DateTimeOffset)expiresAt).ToUnixTimeSeconds());
        }

        public long? GetUserIdFromRefreshToken(string refreshToken)
        {
            if (ValidateRefreshToken(refreshToken, out var claimsPrincipal))
            {
                return null;
            }

            var claim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (claim == null || !long.TryParse(claim.Value, out long userId))
            {
                return null;
            }

            return userId;
        }

        public Guid? GetTokenGuidFromRefreshToken(string refreshToken)
        {
            if (ValidateRefreshToken(refreshToken, out var claimsPrincipal))
            {
                return null;
            }

            var claim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);
            if (claim == null || !Guid.TryParse(claim.Value, out var tokenId))
            {
                return null;
            }

            return tokenId;
        }

        public long? GetExpirationTimestampFromRefreshToken(string refreshToken)
        {
            if (ValidateRefreshToken(refreshToken, out var claimsPrincipal))
            {
                return null;
            }

            var claim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Exp);
            if (claim == null || !long.TryParse(claim.Value, out var expiredAt))
            {
                return null;
            }

            return expiredAt;
        }

        private bool ValidateRefreshToken(string token, out ClaimsPrincipal claimsPrincipal)
        {
            claimsPrincipal = ReadAndValidateToken(token);
            if (claimsPrincipal == null)
            {
                return true;
            }

            var claimsArray = claimsPrincipal.Claims.ToArray();
            var isTokenTypeCorrect = claimsArray
                .FirstOrDefault(ca => ca.Type.Equals("type"))?
                .Value == "refresh";

            if (!isTokenTypeCorrect)
            {
                return true;
            }

            return false;
        }

        private ClaimsPrincipal? ReadAndValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                return tokenHandler.ValidateToken(token, new TokenValidationParameters()
                {
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_applicationSettings.RefreshToken.Key)),
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out _);
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}