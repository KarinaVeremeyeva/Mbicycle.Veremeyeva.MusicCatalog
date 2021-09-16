using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        /// <summary>
        /// User's role
        /// </summary>
        [NotMapped]
        [Required(ErrorMessage = "RoleRequired")]
        public string Role { get; set; }

        /// <summary>
        /// Existing roles list
        /// </summary>
        [NotMapped]
        public IEnumerable<SelectListItem> ExistingRoles { get; set; }
    }
}
