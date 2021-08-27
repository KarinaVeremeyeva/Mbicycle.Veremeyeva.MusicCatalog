namespace MusicCatalog.IdentityApi.Models
{
    /// <summary>
    /// User details for authorization
    /// </summary>
    public class RegisterUser
    {
        /// <summary>
        /// User's email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
    }
}
