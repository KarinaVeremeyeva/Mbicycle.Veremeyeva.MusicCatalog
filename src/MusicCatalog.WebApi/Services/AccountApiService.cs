using MusicCatalog.IdentityApi.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MusicCatalog.WebApi.Services
{
    /// <summary>
    /// Implementation of typed client service for calling identity api
    /// </summary>
    public class AccountApiService : IAccountApiService
    {
        /// <summary>
        /// Http client
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Users path
        /// </summary>
        private const string UsersPath = "api/User";

        /// <summary>
        /// Admin path
        /// </summary>
        private const string AdminPath = "api/Admin";

        /// <summary>
        /// AccountApiService constructor
        /// </summary>
        /// <param name="client">Http client</param>
        public AccountApiService(HttpClient client)
        {
            _httpClient = client;
        }

        /// <inheritdoc cref="IAccountApiService.RegisterUser(RegisterModel)"/>
        public async Task<HttpResponseMessage> RegisterUser(RegisterModel model)
        {
            return await _httpClient.PostAsJsonAsync($"{UsersPath}/register", model);
        }

        /// <inheritdoc cref="IAccountApiService.LoginUser(LoginModel)"/>
        public async Task<HttpResponseMessage> LoginUser(LoginModel model)
        {
            return await _httpClient.PostAsJsonAsync($"{UsersPath}/login", model);
        }

        /// <inheritdoc cref="IAccountApiService.LogoutUser"/>
        public async Task<HttpResponseMessage> LogoutUser()
        {
            return await _httpClient.GetAsync($"{UsersPath}/logout");
        }

        /// <inheritdoc cref="IAccountApiService.GetRoles"/>
        public async Task<List<string>> GetRoles()
        {
            return await _httpClient.GetFromJsonAsync<List<string>>($"{UsersPath}/getAllRoles"); ;
        }

        /// <inheritdoc cref="IAccountApiService.GetUsers"/>
        public async Task<List<UserModel>> GetUsers()
        {
            return await _httpClient.GetFromJsonAsync<List<UserModel>>($"{AdminPath}");
        }

        /// <inheritdoc cref="IAccountApiService.GetUser(string)"/>
        public async Task<UserModel> GetUser(string id)
        {
            return await _httpClient.GetFromJsonAsync<UserModel>($"{AdminPath}/{id}");
        }

        /// <inheritdoc cref="IAccountApiService.UpdateUser(UserModel)"/>
        public async Task<HttpResponseMessage> UpdateUser(UserModel user)
        {
            return await _httpClient.PutAsJsonAsync($"{AdminPath}/{user.Id}", user);
        }

        /// <inheritdoc cref="IAccountApiService.DeleteUser(string)"/>
        public async Task<HttpResponseMessage> DeleteUser(string id)
        {
            return await _httpClient.DeleteAsync($"{AdminPath}/{id}");
        }

        /// <inheritdoc cref="IAccountApiService.UpdateRole(string, string)"/>
        public async Task<HttpResponseMessage> UpdateRole(string id, string role)
        {
            return await _httpClient.PutAsJsonAsync($"{AdminPath}/update-role/{id}", role);
        }

        /// <inheritdoc cref="IAccountApiService.GetUserRole(string)"/>
        public async Task<string> GetUserRole(string id)
        {
            return await _httpClient.GetFromJsonAsync<string>($"{AdminPath}/role/{id}");
        }
    }
}
