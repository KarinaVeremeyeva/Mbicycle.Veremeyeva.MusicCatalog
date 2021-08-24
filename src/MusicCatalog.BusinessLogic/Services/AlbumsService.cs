using AutoMapper;
using MusicCatalog.BusinessLogic.Interfaces;
using MusicCatalog.BusinessLogic.Models;
using MusicCatalog.DataAccess;
using MusicCatalog.DataAccess.Entities;
using System.Collections.Generic;

namespace MusicCatalog.BusinessLogic.Services
{
    /// <summary>
    /// An implementation of the album service
    /// </summary>
    public class AlbumsService : IAlbumsService
    {
        /// <inheritdoc cref="IRepository{T}"/>
        private readonly IRepository<Album> _albumRepository;

        /// <summary>
        /// Mapper
        /// </summary>
        private readonly IMapper _mapper;

        public AlbumsService(IRepository<Album> repository, IMapper mapper)
        {
            _albumRepository = repository;
            _mapper = mapper;
        }

        /// <inheritdoc cref="IAlbumsService.CreateAlbum(AlbumDto)"/>
        public void CreateAlbum(AlbumDto albumModel)
        {
            var album = _mapper.Map<Album>(albumModel);

            _albumRepository.Create(album);
        }

        /// <inheritdoc cref="IAlbumsService.DeleteAlbum(int)"/>
        public void DeleteAlbum(int albumId)
        {
            _albumRepository.Delete(albumId);
        }

        /// <inheritdoc cref="IAlbumsService.GetAlbumById(int)"/>
        public AlbumDto GetAlbumById(int albumId)
        {
            var album = _albumRepository.GetById(albumId);

            return _mapper.Map<AlbumDto>(album);
        }

        /// <inheritdoc cref="IAlbumsService.GetAlbums"/>
        public IEnumerable<AlbumDto> GetAlbums()
        {
            var albums = _albumRepository.GetAll();

            return _mapper.Map<List<AlbumDto>>(albums);
        }

        /// <inheritdoc cref="IAlbumsService.UpdateAlbum(AlbumDto)"/>
        public void UpdateAlbum(AlbumDto albumModel)
        {
            var album = _mapper.Map<Album>(albumModel);

            _albumRepository.Update(album);
        }
    }
}
