using Microsoft.AspNetCore.Identity;

namespace MusicCatalog.IdentityApi.Entities
{
    /// <summary>
    /// User details for authorization
    /// </summary>
    public class User : IdentityUser
    {
        /// <summary>
        /// User's login
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
    }
}
