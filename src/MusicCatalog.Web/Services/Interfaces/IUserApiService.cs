using MusicCatalog.IdentityApi.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MusicCatalog.Web.Services.Interfaces
{
    /// <summary>
    /// Typed client to get users from identity api
    /// </summary>
    public interface IUserApiService
    {
        /// <summary>
        /// Register user
        /// </summary>
        /// <param name="model">RegisterModel</param>
        /// <returns>HttpResponseMessage</returns>
        Task<HttpResponseMessage> RegisterUser(RegisterModel model);

        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="model">LoginModel</param>
        /// <returns>HttpResponseMessage</returns>
        Task<HttpResponseMessage> LoginUser(LoginModel model);

        /// <summary>
        /// Logout user
        /// </summary>
        /// <returns>HttpResponseMessage</returns>
        Task<HttpResponseMessage> LogoutUser();

        /// <summary>
        /// Get all roles
        /// </summary>
        /// <returns>HttpResponseMessage</returns>
        Task<List<string>> GetRoles();

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>Users</returns>
        Task<List<UserModel>> GetUsers();

        /// <summary>
        /// Get a user by id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>User</returns>
        Task<UserModel> GetUser(string id);

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="user">User</param>
        /// <returns>HttpResponseMessage</returns>
        Task<HttpResponseMessage> UpdateUser(UserModel user);

        /// <summary>
        /// Delete user by id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>HttpResponseMessage</returns>
        Task<HttpResponseMessage> DeleteUser(string id);

        /// <summary>
        /// Get user role
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>Role name</returns>
        Task<string> GetUserRole(string id);
    }
}
