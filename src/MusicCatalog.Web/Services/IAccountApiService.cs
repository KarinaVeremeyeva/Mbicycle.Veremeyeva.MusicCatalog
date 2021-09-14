using Microsoft.AspNetCore.Identity;
using MusicCatalog.IdentityApi.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MusicCatalog.Web.Services
{
    /// <summary>
    /// Typed client for the account api
    /// </summary>
    public interface IAccountApiService
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
        /// Get all roless
        /// </summary>
        /// <returns>HttpResponseMessage</returns>
        Task<List<string>> GetRoles();

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>Identity users</returns>
        Task<List<IdentityUser>> GetUsers();

        /// <summary>
        /// Get a user by id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>IdentityUser</returns>
        Task<IdentityUser> GetUser(string id);

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="user">IdentityUser</param>
        /// <returns>HttpResponseMessage</returns>
        Task<HttpResponseMessage> PutUser(IdentityUser user);

        /// <summary>
        /// Delete user by id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>HttpResponseMessage</returns>
        Task<HttpResponseMessage> DeleteUser(string id);
    }
}
