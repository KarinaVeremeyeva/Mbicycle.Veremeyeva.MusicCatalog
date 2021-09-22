using MusicCatalog.BusinessLogic.Models;
using MusicCatalog.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MusicCatalog.Web.Services
{
    /// <summary>
    /// Service for managing songs from api
    /// </summary>
    public class SongApiService : ISongApiService
    {
        /// <summary>
        /// Http client
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Song path
        /// </summary>
        private const string SongPath = "api/Songs";

        /// <summary>
        /// SongApiService constructor
        /// </summary>
        /// <param name="httpClient">Http client</param>
        public SongApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <inheritdoc cref="ISongApiService.CreateSong(SongDto)"/>
        public async Task<HttpResponseMessage> CreateSong(SongDto song)
        {
            return await _httpClient.PostAsJsonAsync($"{SongPath}", song);
        }

        /// <inheritdoc cref="ISongApiService.DeleteSong(int)"/>
        public async Task<HttpResponseMessage> DeleteSong(int id)
        {
            return await _httpClient.DeleteAsync($"{SongPath}/{id}");
        }

        /// <inheritdoc cref="ISongApiService.GetSongById(int)"/>
        public async Task<SongDto> GetSongById(int id)
        {
            return await _httpClient.GetFromJsonAsync<SongDto>($"{SongPath}//{id}");
        }

        /// <inheritdoc cref="ISongApiService.GetSongs"/>
        public async Task<IEnumerable<SongDto>> GetSongs()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<SongDto>>($"{SongPath}");
        }

        /// <inheritdoc cref="ISongApiService.UpdateSong(SongDto)"/>
        public async Task<HttpResponseMessage> UpdateSong(SongDto song)
        {
            return await _httpClient.PutAsJsonAsync($"{SongPath}", song);
        }
    }
}
