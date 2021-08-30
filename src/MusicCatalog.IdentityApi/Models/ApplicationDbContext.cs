using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MusicCatalog.IdentityApi.Entities;

namespace MusicCatalog.IdentityApi.Models
{
    /// <summary>
    /// Database context for identity
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext
    {
        /// <summary>
        /// Database context for identity
        /// </summary>
        /// <param name="options">Database options</param>
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
