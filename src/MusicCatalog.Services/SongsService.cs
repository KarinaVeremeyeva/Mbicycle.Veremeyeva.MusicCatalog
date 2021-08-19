using MusicCatalog.DataAccess;
using MusicCatalog.DataAccess.Entities;
using MusicCatalog.Services.Interfaces;
using System.Collections;

namespace MusicCatalog.Services
{
    /// <summary>
    /// An implementation of the songs service
    /// </summary>
    public class SongsService : ISongsService
    {
        /// <inheritdoc cref="IRepository{T}"/>
        private readonly IRepository<Song> _songRpository;

        public SongsService(IRepository<Song> repository)
        {
            _songRpository = repository;
        }

        /// <inheritdoc cref="ISongsService.CreateSong(Song)"/>
        public void CreateSong(Song song)
        {
            _songRpository.Create(song);
        }

        /// <inheritdoc cref="ISongsService.DeleteSong(int)"/>
        public void DeleteSong(int songId)
        {
            _songRpository.Delete(songId);
        }

        /// <inheritdoc cref="ISongsService.GetSongById(int)"/>
        public Song GetSongById(int songId)
        {
            return _songRpository.GetById(songId);
        }

        /// <inheritdoc cref="ISongsService.GetSongs"/>
        public IEnumerable GetSongs()
        {
            return _songRpository.GetAll();
        }

        /// <inheritdoc cref="ISongsService.UpdateSong(Song)"/>
        public void UpdateSong(Song song)
        {
            _songRpository.Update(song);
        }
    }
}
