using System.Collections.Generic;

namespace MusicCatalog.IdentityApi.Services.Interfaces
{
    /// <summary>
    /// Jwt token service
    /// </summary>
    public interface IJwtTokenService
    {
        /// <summary>
        /// Generate a jwt-token
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="roles">Roles</param>
        /// <returns>Jwt token</returns>
        string GenerateJwtToken(string email, IList<string> roles);

        /// <summary>
        /// Validates jwt token
        /// </summary>
        /// <param name="token">Token</param>
        /// <returns>Is token valid</returns>
        bool ValidateToken(string token);
    }
}
