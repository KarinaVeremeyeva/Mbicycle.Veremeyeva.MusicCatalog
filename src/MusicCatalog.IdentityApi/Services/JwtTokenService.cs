using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MusicCatalog.IdentityApi.Entities;
using MusicCatalog.IdentityApi.Settings;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace MusicCatalog.IdentityApi.Services
{
    /// <summary>
    /// Jwt token service
    /// </summary>
    public class JwtTokenService
    {
        /// <summary>
        /// Jwt token settings
        /// </summary>
        private readonly JwtTokenSettings _settings;

        /// <summary>
        /// Jwt token service constructor
        /// </summary>
        /// <param name="options">Options</param>
        public JwtTokenService(IOptions<JwtTokenSettings> options)
        {
            _settings = options.Value;
        }

        /// <summary>
        /// Generate a jwt-token
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="roles">Roles</param>
        /// <returns>Jwt token</returns>
        public static string GenerateToken(User user, IList<string> roles)
        {
            var userClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Login)
            };
            var roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role));
            userClaims.AddRange(roleClaims);

            var jwtToken = new JwtSecurityToken(
                issuer: JwtTokenSettings.Issuer,
                audience: JwtTokenSettings.Audience,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenSettings.Key)),
                    SecurityAlgorithms.HmacSha256),
                claims: userClaims
                );

            var encodedJwtToken = new JwtSecurityTokenHandler()
                .WriteToken(jwtToken);

            return encodedJwtToken;
        }

        /// <summary>
        /// Validates jwt token
        /// </summary>
        /// <param name="token">Jwt token</param>
        /// <returns></returns>
        public bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _settings.JwtIssuer,
                ValidateAudience = true,
                ValidAudience = _settings.JwtAudience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.JwtSecretKey)),
                ValidateLifetime = false
            };

            try
            {
                tokenHandler.ValidateToken(token, validationParameters, out _);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
