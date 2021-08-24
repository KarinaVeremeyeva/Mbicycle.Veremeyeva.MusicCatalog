using Microsoft.AspNetCore.Identity;

namespace MusicCatalog.Web.Models
{
    /// <summary>
    /// Represents user
    /// </summary>
    public class User : IdentityUser
    {
        public int YearOfBirth { get; set; }
    }
}
