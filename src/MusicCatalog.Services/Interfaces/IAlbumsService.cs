using MusicCatalog.DataAccess.Entities;
using System.Collections.Generic;

namespace MusicCatalog.Services.Interfaces
{
    /// <summary>
    /// An album service
    /// </summary>
    public interface IAlbumsService
    {
        /// <summary>
        /// Creates an album
        /// </summary>
        /// <param name="album">New album</param>
        void CreateAlbum(Album album);

        /// <summary>
        /// Updates the album
        /// </summary>
        /// <param name="album">Album to update</param>
        void UpdateAlbum(Album album);

        /// <summary>
        /// Deletes the album by id
        /// </summary>
        /// <param name="albumId">Album id</param>
        void DeleteAlbum(int albumId);

        /// <summary>
        /// Returns the album by id
        /// </summary>
        /// <param name="albumId">Album id</param>
        /// <returns>Album</returns>
        Album GetAlbumById(int albumId);

        /// <summary>
        /// Returns all albums by id
        /// </summary>
        /// <returns>Albums</returns>
        IEnumerable<Album> GetAlbums();
    }
}
