using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicCatalog.BusinessLogic.Interfaces;
using MusicCatalog.BusinessLogic.Models;
using MusicCatalog.Web.ViewModels;
using System.Collections.Generic;

namespace MusicCatalog.WebApi.Controllers
{
    /// <summary>
    /// Genres controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        /// <summary>
        /// Genre service
        /// </summary>
        private readonly IGenresService _genresService;

        /// <summary>
        /// Mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Genres controller constructor
        /// </summary>
        /// <param name="genresService">Genres service</param>
        /// <param name="mapper">Mapper</param>
        public GenresController(IGenresService genresService, IMapper mapper)
        {
            _genresService = genresService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets genres list
        /// </summary>
        /// <returns>Genres</returns>
        [HttpGet]
        public IEnumerable<GenreViewModel> GetGenres()
        {
            var genreModels = _genresService.GetGenres();
            var genres = _mapper.Map<List<GenreViewModel>>(genreModels);

            return genres;
        }

        /// <summary>
        /// Gets a genre by id
        /// </summary>
        /// <param name="id">Genre id</param>
        /// <returns>Genre</returns>
        [HttpGet("{id}")]
        public GenreViewModel GetGenre(int id)
        {
            var genreModel = _genresService.GetGenreById(id);
            var genre = _mapper.Map<GenreViewModel>(genreModel);

            return genre;
        }

        /// <summary>
        /// Creates a new genre
        /// </summary>
        /// <param name="genreViewModel">Genre</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        public IActionResult CreateGenre([FromBody] GenreViewModel genreViewModel)
        {
            var genre = _mapper.Map<GenreDto>(genreViewModel);

            if (ModelState.IsValid)
            {
                _genresService.CreateGenre(genre);

                return Ok();
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Updates specified genre
        /// </summary>
        /// <param name="genreViewModel">Genre</param>
        /// <returns>IActionResult</returns>
        [HttpPut("{id}")]
        public IActionResult UpdateGenre([FromBody] GenreViewModel genreViewModel)
        {
            var genre = _mapper.Map<GenreDto>(genreViewModel);

            if (ModelState.IsValid)
            {
                _genresService.UpdateGenre(genre);

                return Ok();
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Deletes genre by id
        /// </summary>
        /// <param name="id">Genre id</param>
        /// <returns>IActionResult</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteGenre(int id)
        {
            var genre = _genresService.GetGenreById(id);

            if (genre != null)
            {
                _genresService.DeleteGenre(id);
                return Ok();
            }

            return BadRequest();
        }
    }
}
