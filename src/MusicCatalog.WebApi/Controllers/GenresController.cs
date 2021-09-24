using Microsoft.AspNetCore.Mvc;
using MusicCatalog.BusinessLogic.Interfaces;
using MusicCatalog.BusinessLogic.Models;
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
        /// Genres controller constructor
        /// </summary>
        /// <param name="genresService">Genres service</param>
        public GenresController(IGenresService genresService)
        {
            _genresService = genresService;
        }

        /// <summary>
        /// Gets genres list
        /// </summary>
        /// <returns>Genres</returns>
        [HttpGet]
        public IEnumerable<GenreDto> GetGenres()
        {
            var genres = _genresService.GetGenres();

            return genres;
        }

        /// <summary>
        /// Gets a genre by id
        /// </summary>
        /// <param name="id">Genre id</param>
        /// <returns>Genre</returns>
        [HttpGet("{id}")]
        public GenreDto GetGenre(int id)
        {
            var genre = _genresService.GetGenreById(id);

            return genre;
        }

        /// <summary>
        /// Creates a new genre
        /// </summary>
        /// <param name="genre">Genre</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        public IActionResult CreateGenre([FromBody] GenreDto genre)
        {
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
        /// <param name="genre">Genre</param>
        /// <returns>IActionResult</returns>
        [HttpPut("{id}")]
        public IActionResult UpdateGenre([FromBody] GenreDto genre)
        {
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
