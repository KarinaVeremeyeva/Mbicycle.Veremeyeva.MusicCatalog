using MusicCatalog.DataAccess;
using MusicCatalog.DataAccess.Entities;
using System;
using System.Collections;

namespace MusicCatalog.Services
{
    /// <summary>
    /// An implementation of the genres service
    /// </summary>
    public class GenresService : IGenresService, IDisposable
    {
        /// <inheritdoc cref="MusicContext"/>
        private readonly MusicContext _context;

        /// <inheritdoc cref="IRepository{T}"/>
        private readonly IRepository<Genre> _genreRepository;

        /// <summary>
        /// A dispose value
        /// </summary>
        private bool _disposed = false;

        public GenresService(MusicContext context, IRepository<Genre> repository)
        {
            _genreRepository = repository;
            _context = context;
        }

        /// <inheritdoc cref="IGenresService.CreateGenre(Genre)"/>
        public void CreateGenre(Genre genre)
        {
            _genreRepository.Create(genre);
        }

        /// <inheritdoc cref="IGenresService.GetGenreById(int)"/>
        public Genre GetGenreById(int genreId)
        {
            return _genreRepository.GetById(genreId);
        }

        /// <inheritdoc cref="IGenresService.GetGenres"/>
        public IEnumerable GetGenres()
        {
            return _genreRepository.GetAll();
        }

        /// <inheritdoc cref="IGenresService.UpdateGenre(Genre)"/>
        public void UpdateGenre(Genre genre)
        {
            _genreRepository.Update(genre);
        }

        /// <inheritdoc cref="IGenresService.DeleteGenre(int)"/>
        public void DeleteGenre(int genreId)
        {
            _genreRepository.Delete(genreId);
        }

        /// <inheritdoc cref="IGenresService.Save"/>
        public void Save()
        {
            _context.SaveChanges();
        }

        /// <summary>
        /// Implements dispose method
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        /// <summary>
        /// Clears resources
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
