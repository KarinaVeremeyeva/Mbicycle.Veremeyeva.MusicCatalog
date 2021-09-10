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
        private readonly HttpClient _client;

        /// <summary>
        /// AccountApiClient constructor
        /// </summary>
        /// <param name="client">Http client</param>
        public AccountApiService(HttpClient client)
        {
            _client = client;
        }

        /// <inheritdoc cref="IAccountApiService.RegisterUser(RegisterModel)"/>
        public async Task<HttpResponseMessage> RegisterUser(RegisterModel model)
        {
            return await _client.PostAsJsonAsync("api/Users/register", model);
        }

        /// <inheritdoc cref="IAccountApiService.LoginUser(LoginModel)"/>
        public async Task<HttpResponseMessage> LoginUser(LoginModel model)
        {
            return await _client.PostAsJsonAsync("api/Users/login", model);
        }

        /// <inheritdoc cref="IAccountApiService.LogoutUser"/>
        public async Task<HttpResponseMessage> LogoutUser()
        {
            return await _client.GetAsync("api/Users/logout");
        }

        /// <inheritdoc cref="IAccountApiService.GetRoles"/>
        public async Task<List<string>> GetRoles()
        {
            var roles = await _client.GetFromJsonAsync<List<string>>("api/Users/getAllRoles");     
            
            return roles;
        }

        /// <inheritdoc cref="IAccountApiService.GetUsers"/>
        public async Task<List<IdentityUser>> GetUsers()
        {
            var users = await _client.GetFromJsonAsync<List<IdentityUser>>("api/Admin");          
            
            return users;
        }

        /// <inheritdoc cref="IAccountApiService.GetUser(string)"/>
        public async Task<IdentityUser> GetUser(string id)
        {
            var user = await _client.GetFromJsonAsync<IdentityUser>($"api/Admin/{id}");      
            
            return user;
        }

        /// <inheritdoc cref="IAccountApiService.PutUser(IdentityUser)"/>
        public async Task<HttpResponseMessage> PutUser(IdentityUser user)
        {
            return await _client.PutAsJsonAsync($"api/Admin/{user.Id}", user);
        }

        /// <inheritdoc cref="IAccountApiService.DeleteUser(string)"/>
        public async Task<HttpResponseMessage> DeleteUser(string id)
        {
           return await _client.DeleteAsync($"api/Admin/{id}");
        }
    }
}
