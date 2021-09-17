using Microsoft.AspNetCore.Identity;
using MusicCatalog.IdentityApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicCatalog.IdentityApi.Services
{
    /// <summary>
    /// User service
    /// </summary>
    public interface IUserService
    {
        Task<IdentityResult> CreateAsync(RegisterModel model, string password);

        /// <summary>
        ///  Authenticate user
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="password">password</param>
        /// <returns>SignInResult</returns>
        Task<SignInResult> AuthenticateAsync(string email, string password);

        /// <summary>
        /// Logout
        /// </summary>
        /// <returns></returns>
        Task LogoutAsync();

        /// <summary>
        /// Delete user by id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns></returns>
        Task DeleteUser(string id);

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="model">user</param>
        /// <returns></returns>
        Task UpdateUser(UserModel model);

        /// <summary>
        /// Update role
        /// </summary>
        /// <param name="id"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        Task UpdateRole(string id, string role);

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User</returns>
        Task<UserModel> GetUser(string id);

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>Users</returns>
        Task<IEnumerable<UserModel>> GetUsersAsync();

        /// <summary>
        /// Get current user role
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>Role</returns>
        Task<string> GetUserRole(string id);

        /// <summary>
        /// Gets all roles
        /// </summary>
        /// <returns>Roles</returns>
        IEnumerable<string> GetAllRoles();
    }
}
