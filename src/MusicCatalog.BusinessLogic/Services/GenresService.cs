using AutoMapper;
using MusicCatalog.BusinessLogic.Interfaces;
using MusicCatalog.BusinessLogic.Models;
using MusicCatalog.DataAccess;
using MusicCatalog.DataAccess.Entities;
using System.Collections.Generic;

namespace MusicCatalog.BusinessLogic.Services
{
    /// <summary>
    /// An implementation of the genres service
    /// </summary>
    public class GenresService : IGenresService
    {
        /// <inheritdoc cref="IRepository{T}"/>
        private readonly IRepository<Genre> _genreRepository;

        /// <summary>
        /// Mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Genres service constructor
        /// </summary>
        /// <param name="repository">Albums repository</param>
        /// <param name="mapper">Mapper</param>
        public GenresService(IRepository<Genre> repository, IMapper mapper)
        {
            _genreRepository = repository;
            _mapper = mapper;
        }

        /// <inheritdoc cref="IGenresService.CreateGenre(GenreDto)"/>
        public void CreateGenre(GenreDto genreModel)
        {
            var genre = _mapper.Map<Genre>(genreModel);

            _genreRepository.Create(genre);
        }

        /// <inheritdoc cref="IGenresService.GetGenreById(int)"/>
        public GenreDto GetGenreById(int genreId)
        {
            var genre = _genreRepository.GetById(genreId);

            return _mapper.Map<GenreDto>(genre);
        }

        /// <inheritdoc cref="IGenresService.GetGenres"/>
        public IEnumerable<GenreDto> GetGenres()
        {
            var genres = _genreRepository.GetAll();

            return _mapper.Map<List<GenreDto>>(genres);
        }

        /// <inheritdoc cref="IGenresService.UpdateGenre(GenreDto)"/>
        public void UpdateGenre(GenreDto genreModel)
        {
            var genre = _mapper.Map<Genre>(genreModel);

            _genreRepository.Update(genre);
        }

        /// <inheritdoc cref="IGenresService.DeleteGenre(int)"/>
        public void DeleteGenre(int genreId)
        {
            _genreRepository.Delete(genreId);
        }
    }
}
