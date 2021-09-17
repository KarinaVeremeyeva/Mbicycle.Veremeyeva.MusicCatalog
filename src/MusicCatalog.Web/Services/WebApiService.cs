using MusicCatalog.IdentityApi.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MusicCatalog.Web.Services
{
    /// <summary>
    /// Service for calling web api
    /// </summary>
    public class WebApiService : IWebApiService
    {
        /// <summary>
        /// Http client
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Web api path
        /// </summary>
        private const string WebApiPath = "api/User";

        /// <summary>
        /// WebApiService constructor
        /// </summary>
        /// <param name="httpClient">http client</param>
        public WebApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <inheritdoc cref="IWebApiService.RegisterUser(RegisterModel)"/>
        public async Task<HttpResponseMessage> RegisterUser(RegisterModel model)
        {
            return await _httpClient.PostAsJsonAsync($"{WebApiPath}/register", model);
        }

        /// <inheritdoc cref="IWebApiService.LoginUser(LoginModel)"/>
        public async Task<HttpResponseMessage> LoginUser(LoginModel model)
        {
            return await _httpClient.PostAsJsonAsync($"{WebApiPath}/login", model);
        }

        /// <inheritdoc cref="IWebApiService.LogoutUser"/>
        public async Task<HttpResponseMessage> LogoutUser()
        {
            return await _httpClient.GetAsync($"{WebApiPath}/logout");
        }

        /// <inheritdoc cref="IWebApiService.GetUser(string)"/>
        public async Task<UserModel> GetUser(string id)
        {
            return await _httpClient.GetFromJsonAsync<UserModel>($"{WebApiPath}/{id}");
        }

        /// <inheritdoc cref="IWebApiService.GetUsers"/>
        public async Task<List<UserModel>> GetUsers()
        {
            return await _httpClient.GetFromJsonAsync<List<UserModel>>($"{WebApiPath}");
        }

        /// <inheritdoc cref="IWebApiService.UpdateUser(UserModel)"/>
        public async Task<HttpResponseMessage> UpdateUser(UserModel model)
        {
            return await _httpClient.PutAsJsonAsync($"{WebApiPath}/{model.Id}", model);
        }

        /// <inheritdoc cref="IWebApiService.DeleteUser(string)"/>
        public async Task<HttpResponseMessage> DeleteUser(string id)
        {
            return await _httpClient.DeleteAsync($"{WebApiPath}/{id}");
        }

        /// <inheritdoc cref="IWebApiService.GetRoles"/>
        public async Task<List<string>> GetRoles()
        {
            return await _httpClient.GetFromJsonAsync<List<string>>($"{WebApiPath}/getAllRoles"); ;
        }
    }
}
