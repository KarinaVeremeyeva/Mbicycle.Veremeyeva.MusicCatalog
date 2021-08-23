using Microsoft.AspNetCore.Identity;

namespace MusicCatalog.Web.Models
{
    /// <summary>
    /// Represents user
    /// </summary>
    public class User : IdentityUser
    {
        public string Name { get; set; }
    }
}
