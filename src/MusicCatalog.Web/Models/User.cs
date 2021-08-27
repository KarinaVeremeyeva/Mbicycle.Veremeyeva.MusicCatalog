using Microsoft.AspNetCore.Identity;

namespace MusicCatalog.Web.Models
{
    /// <summary>
    /// Represents user
    /// </summary>
    public class User : IdentityUser
    {
        /// <summary>
        /// Year of birth
        /// </summary>
        public int YearOfBirth { get; set; }
    }
}
