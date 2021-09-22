using MusicCatalog.BusinessLogic.Models;
using MusicCatalog.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MusicCatalog.Web.Services
{
    /// <summary>
    /// Service for managing performers from api
    /// </summary>
    public class PerformerApiService : IPerformerApiService
    {
        /// <summary>
        /// Http client
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Performer path
        /// </summary>
        private const string PerformerPath = "api/Performers";

        /// <summary>
        /// PerformerApiService constructor
        /// </summary>
        /// <param name="httpClient">Http client</param>
        public PerformerApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <inheritdoc cref="IPerformerApiService.CreatePerformer(PerformerDto)"/>
        public async Task<HttpResponseMessage> CreatePerformer(PerformerDto performer)
        {
            return await _httpClient.PostAsJsonAsync($"{PerformerPath}", performer);
        }

        /// <inheritdoc cref="IPerformerApiService.DeletePerformer(int)"/>
        public async Task<HttpResponseMessage> DeletePerformer(int id)
        {
            return await _httpClient.DeleteAsync($"{PerformerPath}/{id}");
        }

        /// <inheritdoc cref="IPerformerApiService.GetPerformers"/>
        public async Task<IEnumerable<PerformerDto>> GetPerformers()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<PerformerDto>>($"{PerformerPath}");
        }

        /// <inheritdoc cref="IPerformerApiService.GetPerformerById(int)"/>
        public async Task<PerformerDto> GetPerformerById(int id)
        {
            return await _httpClient.GetFromJsonAsync<PerformerDto>($"{PerformerPath}/{id}");
        }

        /// <inheritdoc cref="IPerformerApiService.UpdatePerformer(PerformerDto)"/>
        public async Task<HttpResponseMessage> UpdatePerformer(PerformerDto performer)
        {
            return await _httpClient.PutAsJsonAsync($"{PerformerPath}/{performer.PerformerId}", performer);
        }
    }
}
