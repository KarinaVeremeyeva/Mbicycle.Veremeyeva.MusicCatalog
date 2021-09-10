using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicCatalog.IdentityApi.Models
{
    /// <summary>
    /// Initializer for the identity database
    /// </summary>
    public static class IdentityDataInitializer
    {
        /// <summary>
        /// Calls SeedRoles() and SeedUsers()
        /// </summary>
        /// <param name="userManager">UserManager</param>
        /// <param name="roleManager">RoleManager</param>
        /// <returns></returns>
        public static async Task SeedData(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            await SeedRoles(roleManager);
            await SeedUsers(userManager);
        }
        /// <summary>
        /// Initializes the identity database with initial roles
        /// </summary>
        /// <param name="roleManager">RoleManager</param>
        /// <returns></returns>
        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (roleManager.Roles.Any())
            {
                return;
            }

            var roleNames = new string[] { "admin", "user", "quest" };
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }

        /// <summary>
        /// Initializes the identity database with initial users
        /// </summary>
        /// <param name="userManager">UserManager</param>
        /// <returns></returns>
        public static async Task SeedUsers(UserManager<IdentityUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var users = new List<IdentityUser>()
                {
                    new IdentityUser { UserName = "User1", Email = "user1@mail" },
                    new IdentityUser { UserName = "User2", Email = "user2@mail" },
                    new IdentityUser { UserName = "Admin", Email = "admin@email" }
                };

                foreach (var user in users)
                {
                    var result = await userManager.CreateAsync(user, "123456qw");

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(users[0], "user");
                        await userManager.AddToRoleAsync(users[1], "user");
                        await userManager.AddToRoleAsync(users[2], "admin");
                    }
                }
            }
          
        }
    }
}
