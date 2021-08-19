using MusicCatalog.DataAccess;
using MusicCatalog.DataAccess.Entities;
using MusicCatalog.Services.Interfaces;
using System.Collections;

namespace MusicCatalog.Services
{
    /// <summary>
    /// An implementation of the album service
    /// </summary>
    public class AlbumsService : IAlbumsService
    {
        /// <inheritdoc cref="IRepository{T}"/>
        private readonly IRepository<Album> _albumRepository;

        public AlbumsService(IRepository<Album> repository)
        {
            _albumRepository = repository;
        }

        /// <inheritdoc cref="IAlbumsService.CreateAlbum(Album)"/>
        public void CreateAlbum(Album album)
        {
            _albumRepository.Create(album);
        }

        /// <inheritdoc cref="IAlbumsService.DeleteAlbum(int)"/>
        public void DeleteAlbum(int albumId)
        {
            _albumRepository.Delete(albumId);
        }

        /// <inheritdoc cref="IAlbumsService.GetAlbumById(int)"/>
        public Album GetAlbumById(int albumId)
        {
            return _albumRepository.GetById(albumId);
        }

        /// <inheritdoc cref="IAlbumsService.GetAlbums"/>
        public IEnumerable GetAlbums()
        {
            return _albumRepository.GetAll();
        }

        /// <inheritdoc cref="IAlbumsService.UpdateAlbum(Album)"/>
        public void UpdateAlbum(Album album)
        {
            _albumRepository.Update(album);
        }
    }
}
