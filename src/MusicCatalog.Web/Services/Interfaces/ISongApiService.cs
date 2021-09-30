using MusicCatalog.BusinessLogic.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MusicCatalog.Web.Services.Interfaces
{
    /// <summary>
    /// Typed client to get songs from api
    /// </summary>
    public interface ISongApiService
    {
        /// <summary>
        /// Gets all songs
        /// </summary>
        /// <returns>Songs</returns>
        Task<IEnumerable<SongDto>> GetSongs();

        /// <summary>
        /// Gets a song by id
        /// </summary>
        /// <param name="id">Song id</param>
        /// <returns>Song</returns>
        Task<SongDto> GetSongById(int id);

        /// <summary>
        /// Creates a song
        /// </summary>
        /// <param name="song">Song</param>
        /// <returns>HttpResponseMessage</returns>
        Task<HttpResponseMessage> CreateSong(SongDto song);

        /// <summary>
        /// Updates a song
        /// </summary>
        /// <param name="song">Song</param>
        /// <returns>HttpResponseMessage</returns>
        Task<HttpResponseMessage> UpdateSong(SongDto song);

        /// <summary>
        /// Deletes a song
        /// </summary>
        /// <param name="id">Song id</param>
        /// <returns>HttpResponseMessage</returns>
        Task<HttpResponseMessage> DeleteSong(int id);
    }
}
