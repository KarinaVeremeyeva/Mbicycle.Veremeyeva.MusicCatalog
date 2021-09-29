using Microsoft.AspNetCore.Http;
using MusicCatalog.BusinessLogic.Models;
using MusicCatalog.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MusicCatalog.Web.Services
{
    /// <summary>
    /// Service for managing albums from api
    /// </summary>
    public class AlbumApiService : IAlbumApiService
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
        /// Album path
        /// </summary>
        private const string AlbumPath = "api/Albums";

        /// <summary>
        /// Jwt token key
        /// </summary>
        private const string JwtTokenKey = "secret_jwt_key";

        /// <summary>
        /// AlbumApiService constructor
        /// </summary>
        /// <param name="httpClient">Http client</param>
        /// <param name="httpContextAccessor">Http context accessor</param>
        public AlbumApiService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <inheritdoc cref="IAlbumApiService.CreateAlbum(AlbumDto)"/>
        public async Task<HttpResponseMessage> CreateAlbum(AlbumDto album)
        {
            var token = _httpContextAccessor.HttpContext.Request.Cookies[JwtTokenKey];
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return await _httpClient.PostAsJsonAsync($"{AlbumPath}", album);
        }

        /// <inheritdoc cref="IAlbumApiService.DeleteAlbum(int)"/>
        public async Task<HttpResponseMessage> DeleteAlbum(int id)
        {
            return await _httpClient.DeleteAsync($"{AlbumPath}/{id}");
        }

        /// <inheritdoc cref="IAlbumApiService.GetAlbumById(int)"/>
        public async Task<AlbumDto> GetAlbumById(int id)
        {
            return await _httpClient.GetFromJsonAsync<AlbumDto>($"{AlbumPath}/{id}");
        }

        /// <inheritdoc cref="IAlbumApiService.GetAlbums"/>
        public async Task<IEnumerable<AlbumDto>> GetAlbums()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<AlbumDto>>($"{AlbumPath}");
        }

        /// <inheritdoc cref="IAlbumApiService.UpdateAlbum(AlbumDto)"/>
        public async Task<HttpResponseMessage> UpdateAlbum(AlbumDto album)
        {
            return await _httpClient.PutAsJsonAsync($"{AlbumPath}/{album.AlbumId}", album);
        }
    }
}
