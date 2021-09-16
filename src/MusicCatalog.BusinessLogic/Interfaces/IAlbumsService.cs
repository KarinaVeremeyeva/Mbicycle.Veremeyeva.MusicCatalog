using MusicCatalog.BusinessLogic.Models;
using System.Collections.Generic;

namespace MusicCatalog.BusinessLogic.Interfaces
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
        void CreateAlbum(AlbumDto album);

        /// <summary>
        /// Updates the album
        /// </summary>
        /// <param name="album">Album to update</param>
        void UpdateAlbum(AlbumDto album);

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
        AlbumDto GetAlbumById(int albumId);

        /// <summary>
        /// Returns all albums
        /// </summary>
        /// <returns>Albums</returns>
        IEnumerable<AlbumDto> GetAlbums();
    }
}
