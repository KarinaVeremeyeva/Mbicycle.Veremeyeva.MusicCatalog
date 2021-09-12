using Microsoft.AspNetCore.Identity;
using MusicCatalog.IdentityApi.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MusicCatalog.Web.Services
{
    /// <summary>
    /// Implementation of typed client service
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
        private const string UsersPath = "api/Users";

        /// <summary>
        /// Admin path
        /// </summary>
        private const string AdminPath = "api/Admin";

        /// <summary>
        /// AccountApiClient constructor
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
        public async Task<List<IdentityUser>> GetUsers()
        {
            return await _httpClient.GetFromJsonAsync<List<IdentityUser>>($"{AdminPath}");
        }

        /// <inheritdoc cref="IAccountApiService.GetUser(string)"/>
        public async Task<IdentityUser> GetUser(string id)
        {
            return await _httpClient.GetFromJsonAsync<IdentityUser>($"{AdminPath}/{id}");
        }

        /// <inheritdoc cref="IAccountApiService.PutUser(IdentityUser)"/>
        public async Task<HttpResponseMessage> PutUser(IdentityUser user)
        {
            return await _httpClient.PutAsJsonAsync($"{AdminPath}/{user.Id}", user);
        }

        /// <inheritdoc cref="IAccountApiService.DeleteUser(string)"/>
        public async Task<HttpResponseMessage> DeleteUser(string id)
        {
            return await _httpClient.DeleteAsync($"{AdminPath}/{id}");
        }
    }
}
