using System.ComponentModel.DataAnnotations;

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
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// User role
        /// </summary>
        [Required]
        public string Role { get; set; }
    }
}
