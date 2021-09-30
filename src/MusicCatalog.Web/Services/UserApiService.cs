using MusicCatalog.IdentityApi.Models;
using MusicCatalog.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MusicCatalog.Web.Services
{
    /// <summary>
    /// Implementation of typed client service for managing users from identity api
    /// </summary>
    public class UserApiService : IUserApiService
    {
        /// <summary>
        /// Http client
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// User path
        /// </summary>
        private const string UserPath = "api/User";

        /// <summary>
        /// Admin path
        /// </summary>
        private const string AdminPath = "api/Admin";

        /// <summary>
        /// AccountApiService constructor
        /// </summary>
        /// <param name="client">Http client</param>
        public UserApiService(HttpClient client)
        {
            _httpClient = client;
        }

        /// <inheritdoc cref="IUserApiService.RegisterUser(RegisterModel)"/>
        public async Task<HttpResponseMessage> RegisterUser(RegisterModel model)
        {
            return await _httpClient.PostAsJsonAsync($"{UserPath}/register", model);
        }

        /// <inheritdoc cref="IUserApiService.LoginUser(LoginModel)"/>
        public async Task<HttpResponseMessage> LoginUser(LoginModel model)
        {
            return await _httpClient.PostAsJsonAsync($"{UserPath}/login", model);
        }

        /// <inheritdoc cref="IUserApiService.LogoutUser"/>
        public async Task<HttpResponseMessage> LogoutUser()
        {
            return await _httpClient.GetAsync($"{UserPath}/logout");
        }

        /// <inheritdoc cref="IUserApiService.GetRoles"/>
        public async Task<List<string>> GetRoles()
        {
            return await _httpClient.GetFromJsonAsync<List<string>>($"{UserPath}/getAllRoles"); ;
        }

        /// <inheritdoc cref="IUserApiService.GetUsers"/>
        public async Task<List<UserModel>> GetUsers()
        {
            return await _httpClient.GetFromJsonAsync<List<UserModel>>($"{AdminPath}");
        }

        /// <inheritdoc cref="IUserApiService.GetUser(string)"/>
        public async Task<UserModel> GetUser(string id)
        {
            return await _httpClient.GetFromJsonAsync<UserModel>($"{AdminPath}/{id}");
        }

        /// <inheritdoc cref="IUserApiService.UpdateUser(UserModel)"/>
        public async Task<HttpResponseMessage> UpdateUser(UserModel user)
        {
            return await _httpClient.PutAsJsonAsync($"{AdminPath}/{user.Id}", user);
        }

        /// <inheritdoc cref="IUserApiService.DeleteUser(string)"/>
        public async Task<HttpResponseMessage> DeleteUser(string id)
        {
            return await _httpClient.DeleteAsync($"{AdminPath}/{id}");
        }

        /// <inheritdoc cref="IUserApiService.GetUserRole(string)"/>
        public async Task<string> GetUserRole(string id)
        {
            return await _httpClient.GetFromJsonAsync<string>($"{AdminPath}/role/{id}");
        }
    }
}
