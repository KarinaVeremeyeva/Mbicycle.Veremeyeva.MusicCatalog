using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace MusicCatalog.IdentityApi.Models
{
    /// <summary>
    /// Initializer for the identity database
    /// </summary>
    public static class RoleInitializer
    {
        /// <summary>
        /// Initialize the identity database with initial roles
        /// </summary>
        /// <param name="roleManager">RoleManager</param>
        /// <returns></returns>
        public static async Task InitializeWithRoles(RoleManager<IdentityRole> roleManager)
        {
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }

            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }

            if (await roleManager.FindByNameAsync("quest") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("quest"));
            }
        }
    }
}
