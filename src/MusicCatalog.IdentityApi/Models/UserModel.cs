using System.ComponentModel.DataAnnotations;

namespace MusicCatalog.IdentityApi.Models
{
    /// <summary>
    /// User model
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// User id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// User email
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// User role
        /// </summary>
        [Required]
        public string Role { get; set; }
    }
}
