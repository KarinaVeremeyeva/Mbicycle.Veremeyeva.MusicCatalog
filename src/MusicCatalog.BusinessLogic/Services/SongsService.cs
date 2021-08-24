using AutoMapper;
using MusicCatalog.BusinessLogic.Interfaces;
using MusicCatalog.BusinessLogic.Models;
using MusicCatalog.DataAccess;
using MusicCatalog.DataAccess.Entities;
using System.Collections.Generic;

namespace MusicCatalog.BusinessLogic.Services
{
    /// <summary>
    /// An implementation of the songs service
    /// </summary>
    public class SongsService : ISongsService
    {
        /// <inheritdoc cref="IRepository{T}"/>
        private readonly IRepository<Song> _songRpository;

        /// <summary>
        /// Mapper
        /// </summary>
        private readonly IMapper _mapper;

        public SongsService(IRepository<Song> repository, IMapper mapper)
        {
            _songRpository = repository;
            _mapper = mapper;
        }

        /// <inheritdoc cref="ISongsService.CreateSong(SongDto)"/>
        public void CreateSong(SongDto songModel)
        {
            var song = _mapper.Map<Song>(songModel);

            _songRpository.Create(song);
        }

        /// <inheritdoc cref="ISongsService.DeleteSong(int)"/>
        public void DeleteSong(int songId)
        {
            _songRpository.Delete(songId);
        }

        /// <inheritdoc cref="ISongsService.GetSongById(int)"/>
        public SongDto GetSongById(int songId)
        {
            var song = _songRpository.GetById(songId);

            return _mapper.Map<SongDto>(song);
        }

        /// <inheritdoc cref="ISongsService.GetSongs"/>
        public IEnumerable<SongDto> GetSongs()
        {
            var songs = _songRpository.GetAll();

            return _mapper.Map<List<SongDto>>(songs);
        }

        /// <inheritdoc cref="ISongsService.UpdateSong(SongDto)"/>
        public void UpdateSong(SongDto songModel)
        {
            var song = _mapper.Map<Song>(songModel);

            _songRpository.Update(song);
        }
    }
}
