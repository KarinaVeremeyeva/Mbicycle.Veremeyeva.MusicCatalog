using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MusicCatalog.Web.Models
{
    /// <summary>
    /// Database context for identity
    /// </summary>
    public class ApplicationContext : IdentityDbContext<User>
    {
        /// <summary>
        /// Database context for identity
        /// </summary>
        /// <param name="options">Database options</param>
        public ApplicationContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
