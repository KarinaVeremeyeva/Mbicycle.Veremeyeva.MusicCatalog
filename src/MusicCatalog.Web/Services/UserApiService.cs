using MusicCatalog.IdentityApi.Models;
using MusicCatalog.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MusicCatalog.Web.Services
{
    /// <summary>
    /// Service for managing users from api
    /// </summary>
    public class UserApiService : IUserApiService
    {
        /// <summary>
        /// Http client
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Web api path
        /// </summary>
        private const string WebApiPath = "api/Users";

        /// <summary>
        /// WebApiService constructor
        /// </summary>
        /// <param name="httpClient">Http client</param>
        public UserApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <inheritdoc cref="IUserApiService.RegisterUser(RegisterModel)"/>
        public async Task<HttpResponseMessage> RegisterUser(RegisterModel model)
        {
            return await _httpClient.PostAsJsonAsync($"{WebApiPath}/register", model);
        }

        /// <inheritdoc cref="IUserApiService.LoginUser(LoginModel)"/>
        public async Task<HttpResponseMessage> LoginUser(LoginModel model)
        {
            return await _httpClient.PostAsJsonAsync($"{WebApiPath}/login", model);
        }

        /// <inheritdoc cref="IUserApiService.LogoutUser"/>
        public async Task<HttpResponseMessage> LogoutUser()
        {
            return await _httpClient.GetAsync($"{WebApiPath}/logout");
        }

        /// <inheritdoc cref="IUserApiService.GetUser(string)"/>
        public async Task<UserModel> GetUser(string id)
        {
            return await _httpClient.GetFromJsonAsync<UserModel>($"{WebApiPath}/{id}");
        }

        /// <inheritdoc cref="IUserApiService.GetUsers"/>
        public async Task<IEnumerable<UserModel>> GetUsers()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<UserModel>>($"{WebApiPath}");
        }

        /// <inheritdoc cref="IUserApiService.UpdateUser(UserModel)"/>
        public async Task<HttpResponseMessage> UpdateUser(UserModel model)
        {
            return await _httpClient.PutAsJsonAsync($"{WebApiPath}/{model.Id}", model);
        }

        /// <inheritdoc cref="IUserApiService.DeleteUser(string)"/>
        public async Task<HttpResponseMessage> DeleteUser(string id)
        {
            return await _httpClient.DeleteAsync($"{WebApiPath}/{id}");
        }

        /// <inheritdoc cref="IUserApiService.GetRoles"/>
        public async Task<List<string>> GetRoles()
        {
            return await _httpClient.GetFromJsonAsync<List<string>>($"{WebApiPath}/getAllRoles"); ;
        }
    }
}
