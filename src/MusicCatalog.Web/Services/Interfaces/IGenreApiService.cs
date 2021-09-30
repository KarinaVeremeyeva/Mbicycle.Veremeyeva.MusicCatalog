using MusicCatalog.BusinessLogic.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MusicCatalog.Web.Services.Interfaces
{
    /// <summary>
    /// Typed client to get genres from api
    /// </summary>
    public interface IGenreApiService
    {
        /// <summary>
        /// Gets all genres
        /// </summary>
        /// <returns>Genres</returns>
        Task<IEnumerable<GenreDto>> GetGenres();

        /// <summary>
        /// Gets a genre by id
        /// </summary>
        /// <param name="id">Genre id</param>
        /// <returns>Genre</returns>
        Task<GenreDto> GetGenreById(int id);

        /// <summary>
        /// Creates a genre
        /// </summary>
        /// <param name="genre">Genre</param>
        /// <returns>HttpResponseMessage</returns>
        Task<HttpResponseMessage> CreateGenre(GenreDto genre);

        /// <summary>
        /// Updates a genre
        /// </summary>
        /// <param name="genre">Genre</param>
        /// <returns>HttpResponseMessage</returns>
        Task<HttpResponseMessage> UpdateGenre(GenreDto genre);

        /// <summary>
        /// Deletes a genre
        /// </summary>
        /// <param name="id">Genre id</param>
        /// <returns>HttpResponseMessage</returns>
        Task<HttpResponseMessage> DeleteGenre(int id);
    }
}
