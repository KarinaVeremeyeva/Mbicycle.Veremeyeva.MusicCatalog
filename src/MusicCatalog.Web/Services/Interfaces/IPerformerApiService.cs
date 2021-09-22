using MusicCatalog.BusinessLogic.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MusicCatalog.Web.Services.Interfaces
{
    /// <summary>
    /// Typed client to get performers from api
    /// </summary>
    public interface IPerformerApiService
    {
        /// <summary>
        /// Gets all performers
        /// </summary>
        /// <returns>Performers</returns>
        Task<IEnumerable<PerformerDto>> GetPerformers();

        /// <summary>
        /// Gets a performer by id
        /// </summary>
        /// <param name="id">Performer id</param>
        /// <returns>Performer</returns>
        Task<PerformerDto> GetPerformerById(int id);

        /// <summary>
        /// Creates a performer
        /// </summary>
        /// <param name="performer">Performer</param>
        /// <returns>HttpResponseMessage</returns>
        Task<HttpResponseMessage> CreatePerformer(PerformerDto performer);

        /// <summary>
        /// Updates a performer
        /// </summary>
        /// <param name="performer">Performer</param>
        /// <returns>HttpResponseMessage</returns>
        Task<HttpResponseMessage> UpdatePerformer(PerformerDto performer);

        /// <summary>
        /// Deletes a performer
        /// </summary>
        /// <param name="id"></param>
        /// <returns>HttpResponseMessage</returns>
        Task<HttpResponseMessage> DeletePerformer(int id);
    }
}
