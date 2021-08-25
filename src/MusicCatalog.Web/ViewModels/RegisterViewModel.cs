using System.ComponentModel.DataAnnotations;

namespace MusicCatalog.Web.ViewModels
{
    /// <summary>
    /// View model for registration of new user
    /// </summary>
    public class RegisterViewModel
    {
        /// <summary>
        /// Email
        /// </summary>
        [Required(ErrorMessage = "EmailRequired")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Year of birth
        /// </summary>
        [Required(ErrorMessage = "YearOfBirthRequired")]
        [Display(Name = "Year of birth")]
        [Range(1900, 2021, ErrorMessage ="YearRange")]
        public int? YearOfBirth { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Required(ErrorMessage = "PasswordRequired")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "PasswordLength")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Confirm password
        /// </summary>
        [Required(ErrorMessage = "PasswordConfirmRequired")]
        [Compare("Password", ErrorMessage = "PasswordMismatch")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string PasswordConfirm { get; set; }
    }
}
