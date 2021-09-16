namespace MusicCatalog.IdentityApi.Models
{
    /// <summary>
    /// Login model for a user
    /// </summary>
    public class LoginViewModel
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
        /// Remember user
        /// </summary>
        public bool RememberMe { get; set; }
    }
}
