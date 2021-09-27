using MusicCatalog.BusinessLogic.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MusicCatalog.Web.Services.Interfaces
{
    /// <summary>
    /// Typed client to get albums from api
    /// </summary>
    public interface IAlbumApiService
    {
        /// <summary>
        /// Gets all albums
        /// </summary>
        /// <returns>Albums</returns>
        Task<IEnumerable<AlbumDto>> GetAlbums();

        /// <summary>
        /// Gets an album by id
        /// </summary>
        /// <param name="id">Album id</param>
        /// <returns>Album</returns>
        Task<AlbumDto> GetAlbumById(int id);

        /// <summary>
        /// Creates an album
        /// </summary>
        /// <param name="album">Album</param>
        /// <returns>HttpResponseMessage</returns>
        Task<HttpResponseMessage> CreateAlbum(AlbumDto album);

        Task<HttpResponseMessage> CreateAlbum(AlbumDto album, string token);

        /// <summary>
        /// Updates an album
        /// </summary>
        /// <param name="album">Album</param>
        /// <returns>HttpResponseMessage</returns>
        Task<HttpResponseMessage> UpdateAlbum(AlbumDto album);

        /// <summary>
        /// Deletes an album
        /// </summary>
        /// <param name="id">Album id</param>
        /// <returns>HttpResponseMessage</returns>
        Task<HttpResponseMessage> DeleteAlbum(int id);
    }
}
