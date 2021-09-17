using Microsoft.AspNetCore.Identity;
using MusicCatalog.IdentityApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicCatalog.IdentityApi.Services
{
    /// <summary>
    /// Implementation of user service
    /// </summary>
    public class UserService : IUserService
    {
        /// <summary>
        /// Users sign in manager
        /// </summary>
        private readonly SignInManager<IdentityUser> _signInManager;

        /// <summary>
        /// Users manager
        /// </summary>
        private readonly UserManager<IdentityUser> _userManager;

        /// <summary>
        /// Roles manager
        /// </summary>
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        /// <inheritdoc cref="IUserService.AuthenticateAsync(string, string)"/>
        public async Task<SignInResult> AuthenticateAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var result = await _signInManager.PasswordSignInAsync(
                user.Email, password, isPersistent: false, lockoutOnFailure: false);

            return result;
        }

        /// <inheritdoc cref="IUserService.CreateAsync(RegisterModel, string)"/>
        public async Task<IdentityResult> CreateAsync(RegisterModel model, string password)
        {
            var user = new IdentityUser()
            {
                Email = model.Email,
                UserName = model.Email
            };

            if (!string.IsNullOrEmpty(model.Role))
            {
                await _userManager.AddToRoleAsync(user, model.Role);
            }

            var result = await _userManager.CreateAsync(user, password);
            await _signInManager.SignInAsync(user, isPersistent: false);

            return result;
        }

        /// <inheritdoc cref="IUserService.DeleteUser(string)"/>
        public async Task DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
        }

        /// <inheritdoc cref="IUserService.LogoutAsync"/>
        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        /// <inheritdoc cref="IUserService.UpdateUser(UserModel)"/>
        public async Task UpdateUser(UserModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user != null)
            {
                user.Email = model.Email;
                user.UserName = model.Email;

                await _userManager.UpdateAsync(user);
            }
        }

        /// <inheritdoc cref="IUserService.UpdateRole(string, string)"/>
        public async Task UpdateRole(string id, string role)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                await _userManager.RemoveFromRolesAsync(user, userRoles);             
                await _userManager.AddToRoleAsync(user, role);
            }
        }

        /// <inheritdoc cref="IUserService.GetUser(string)"/>
        public async Task<UserModel> GetUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var userWithRole = new UserModel
            {
                Id = user.Id,
                Email = user.Email,
                Role = await GetUserRole(user.Id)
            };

            return userWithRole;
        }

        /// <inheritdoc cref="IUserService.GetUsersAsync"/>
        public async Task<IEnumerable<UserModel>> GetUsersAsync()
        {
            var users = _userManager.Users.ToList();
            var usersWithRole = new List<UserModel>();
            foreach (var user in users)
            {
                usersWithRole.Add(new UserModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Role = await GetUserRole(user.Id)
                });
            }

            return usersWithRole;
        }

        /// <inheritdoc cref="IUserService.GetUserRole(string)"/>
        public async Task<string> GetUserRole(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.ToList().FirstOrDefault();

            return role;
        }

        /// <inheritdoc cref="IUserService.GetAllRoles"/>
        public IEnumerable<string> GetAllRoles()
        {
            var roles = _roleManager.Roles.ToList();
            var roleNames = roles.Select(role => role.Name);

            return roleNames;
        }
    }
}
