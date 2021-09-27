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
        /// <summary>
        /// Creates new user
        /// </summary>
        /// <param name="model">User</param>
        /// <returns>IdentityResult</returns>
        Task<IdentityResult> CreateAsync(RegisterModel model);

        /// <summary>
        /// Authenticates user
        /// </summary>
        /// <param name="model">User</param>
        /// <returns>SignInResult</returns>
        Task<SignInResult> AuthenticateAsync(LoginModel model);

        /// <summary>
        /// Logout of user
        /// </summary>
        /// <returns></returns>
        Task LogoutAsync();

        /// <summary>
        /// Deletes user by id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>IdentityResult</returns>
        Task<IdentityResult> DeleteUser(string id);

        /// <summary>
        /// Updates user
        /// </summary>
        /// <param name="model">user</param>
        /// <returns>IdentityResult</returns>
        Task<IdentityResult> UpdateUser(UserModel model);

        /// <summary>
        /// Gets user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User</returns>
        Task<UserModel> GetUser(string id);

        /// <summary>
        /// Gets all users
        /// </summary>
        /// <returns>Users</returns>
        Task<IEnumerable<UserModel>> GetUsersAsync();

        /// <summary>
        /// Gets current user role
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>Role</returns>
        Task<string> GetUserRole(string id);

        /// <summary>
        /// Gets all roles
        /// </summary>
        /// <returns>Roles</returns>
        IEnumerable<string> GetAllRoles();
        
        /// <summary>
        /// Gets user roles
        /// </summary>
        /// <param name="email">User email</param>
        /// <returns>Roles</returns>
        Task<IList<string>> GetUserRoles(string email);
    }
}
