namespace MusicCatalog.IdentityApi.Models
{
    /// <summary>
    /// Register model for a user
    /// </summary>
    public class RegisterModel
    {
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// User role
        /// </summary>
        public string Role { get; set; }
    }
}
