using Microsoft.AspNetCore.Http;
using MusicCatalog.IdentityApi.Models;
using MusicCatalog.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
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
        /// Provides access to the http context
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// User path
        /// </summary>
        private const string UserPath = "api/User";

        /// <summary>
        /// Admin path
        /// </summary>
        private const string AdminPath = "api/Admin";

        /// <summary>
        /// Jwt token key
        /// </summary>
        private const string JwtTokenKey = "secret_jwt_key";

        /// <summary>
        /// AccountApiService constructor
        /// </summary>
        /// <param name="client">Http client</param>
        /// <param name="httpContextAccessor">Http context accessor</param>
        public UserApiService(HttpClient client, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = client;
            _httpContextAccessor = httpContextAccessor;
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
            var token = _httpContextAccessor.HttpContext.Request.Cookies[JwtTokenKey];
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return await _httpClient.GetFromJsonAsync<List<UserModel>>($"{AdminPath}");
        }

        /// <inheritdoc cref="IUserApiService.GetUser(string)"/>
        public async Task<UserModel> GetUser(string id)
        {
            var token = _httpContextAccessor.HttpContext.Request.Cookies[JwtTokenKey];
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return await _httpClient.GetFromJsonAsync<UserModel>($"{AdminPath}/{id}");
        }

        /// <inheritdoc cref="IUserApiService.UpdateUser(UserModel)"/>
        public async Task<HttpResponseMessage> UpdateUser(UserModel user)
        {
            var token = _httpContextAccessor.HttpContext.Request.Cookies[JwtTokenKey];
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return await _httpClient.PutAsJsonAsync($"{AdminPath}/{user.Id}", user);
        }

        /// <inheritdoc cref="IUserApiService.DeleteUser(string)"/>
        public async Task<HttpResponseMessage> DeleteUser(string id)
        {
            var token = _httpContextAccessor.HttpContext.Request.Cookies[JwtTokenKey];
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return await _httpClient.DeleteAsync($"{AdminPath}/{id}");
        }

        /// <inheritdoc cref="IUserApiService.GetUserRole(string)"/>
        public async Task<string> GetUserRole(string id)
        {
            var token = _httpContextAccessor.HttpContext.Request.Cookies[JwtTokenKey];
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return await _httpClient.GetFromJsonAsync<string>($"{AdminPath}/role/{id}");
        }
    }
}
