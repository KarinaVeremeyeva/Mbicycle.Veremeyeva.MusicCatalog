using System.ComponentModel.DataAnnotations;

namespace MusicCatalog.Web.ViewModels
{
    /// <summary>
    /// View model for user authorization
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Email
        /// </summary>
        [Required(ErrorMessage = "EmailRequired")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Required(ErrorMessage = "PasswordRequired")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "PasswordLength")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Remember data
        /// </summary>
        [Display(Name = "Remember?")]
        public bool RememberMe { get; set; }

        /// <summary>
        /// Return url
        /// </summary>
        public string ReturnUrl { get; set; }
    }
}
