using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicCatalog.Web.ViewModels
{
    /// <summary>
    /// User view model
    /// </summary>
    public class UserViewModel
    {
        /// <summary>
        /// User id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// User email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// User's role
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Existing roles list
        /// </summary>
        public IEnumerable<SelectListItem> ExistingRoles { get; set; }
    }
}
