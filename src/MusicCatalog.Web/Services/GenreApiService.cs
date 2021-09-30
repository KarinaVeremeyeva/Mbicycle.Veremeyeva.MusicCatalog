using MusicCatalog.BusinessLogic.Models;
using MusicCatalog.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MusicCatalog.Web.Services
{
    /// <summary>
    /// Service for managing genres from api
    /// </summary>
    public class GenreApiService : IGenreApiService
    {
        /// <summary>
        /// Http client
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Genre path
        /// </summary>
        private const string GenrePath = "api/Genres";

        /// <summary>
        /// GenreApiService constructor
        /// </summary>
        /// <param name="httpClient">Http client</param>
        public GenreApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <inheritdoc cref="IGenreApiService.CreateGenre(GenreDto)"/>
        public async Task<HttpResponseMessage> CreateGenre(GenreDto genre)
        {
            return await _httpClient.PostAsJsonAsync($"{GenrePath}", genre);
        }

        /// <inheritdoc cref="IGenreApiService.DeleteGenre(int)"/>
        public async Task<HttpResponseMessage> DeleteGenre(int id)
        {
            return await _httpClient.DeleteAsync($"{GenrePath}/{id}");
        }

        /// <inheritdoc cref="IGenreApiService.GetGenreById(int)"/>
        public async Task<GenreDto> GetGenreById(int id)
        {
            return await _httpClient.GetFromJsonAsync<GenreDto>($"{GenrePath}/{id}");
        }

        /// <inheritdoc cref="IGenreApiService.GetGenres"/>
        public async Task<IEnumerable<GenreDto>> GetGenres()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<GenreDto>>($"{GenrePath}");
        }

        /// <inheritdoc cref="IGenreApiService.UpdateGenre(GenreDto)"/>
        public async Task<HttpResponseMessage> UpdateGenre(GenreDto genre)
        {
            return await _httpClient.PutAsJsonAsync($"{GenrePath}/{genre.GenreId}", genre);
        }
    }
}
