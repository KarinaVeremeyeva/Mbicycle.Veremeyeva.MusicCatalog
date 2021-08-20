using MusicCatalog.DataAccess;
using MusicCatalog.DataAccess.Entities;
using MusicCatalog.Services.Interfaces;
using System.Collections.Generic;

namespace MusicCatalog.Services
{
    /// <summary>
    /// An implementation of the genres service
    /// </summary>
    public class GenresService : IGenresService
    {
        /// <inheritdoc cref="IRepository{T}"/>
        private readonly IRepository<Genre> _genreRepository;

        public GenresService(IRepository<Genre> repository)
        {
            _genreRepository = repository;
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
        public IEnumerable<Genre> GetGenres()
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
    }
}
