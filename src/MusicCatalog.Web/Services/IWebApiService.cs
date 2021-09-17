using MusicCatalog.IdentityApi.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MusicCatalog.Web.Services
{
    /// <summary>
    /// Typed client service for calling web api
    /// </summary>
    public interface IWebApiService
    {
        /// <summary>
        /// Register user
        /// </summary>
        /// <param name="model">User</param>
        /// <returns>HttpResponseMessage</returns>
        Task<HttpResponseMessage> RegisterUser(RegisterModel model);

        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="model"></param>
        /// <returns>HttpResponseMessage</returns>
        Task<HttpResponseMessage> LoginUser(LoginModel model);

        /// <summary>
        /// Logout user
        /// </summary>
        /// <returns>HttpResponseMessage</returns>
        Task<HttpResponseMessage> LogoutUser();

        /// <summary>
        /// Gets all users
        /// </summary>
        /// <returns></returns>
        Task<List<UserModel>> GetUsers();

        /// <summary>
        /// Gets user by id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>Users</returns>
        Task<UserModel> GetUser(string id);

        /// <summary>
        /// Updates user
        /// </summary>
        /// <param name="model">User</param>
        /// <returns>HttpResponseMessage</returns>
        Task<HttpResponseMessage> UpdateUser(UserModel model);

        /// <summary>
        /// Deletes user
        /// </summary>
        /// <param name="id"></param>
        /// <returns>HttpResponseMessage</returns>
        Task<HttpResponseMessage> DeleteUser(string id);

        /// <summary>
        /// Gets all roles
        /// </summary>
        /// <returns>Roles</returns>
        Task<List<string>> GetRoles();
    }
}
