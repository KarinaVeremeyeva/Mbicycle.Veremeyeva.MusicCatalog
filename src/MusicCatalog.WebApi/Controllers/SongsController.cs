using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicCatalog.BusinessLogic.Interfaces;
using MusicCatalog.BusinessLogic.Models;
using System.Collections.Generic;

namespace MusicCatalog.WebApi.Controllers
{
    /// <summary>
    /// Songs controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SongsController : ControllerBase
    {
        /// <summary>
        /// Songs service
        /// </summary>
        private readonly ISongsService _songsService;

        /// <summary>
        /// Songs controller constructor
        /// </summary>
        /// <param name="songsService">Songs service</param>
        public SongsController(ISongsService songsService)
        {
            _songsService = songsService;
        }

        /// <summary>
        /// Gets songs list
        /// </summary>
        /// <returns>Songs</returns>
        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<SongDto> GetSongs()
        {
            var songs = _songsService.GetSongs();

            return songs;
        }

        /// <summary>
        /// Gets a song by id
        /// </summary>
        /// <param name="id">Song id</param>
        /// <returns>Song</returns>
        [HttpGet("{id}")]
        public SongDto GetSong(int id)
        {
            var song = _songsService.GetSongById(id);

            return song;
        }

        /// <summary>
        /// Creates a new song
        /// </summary>
        /// <param name="song">Song</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        public IActionResult CreateSong([FromBody] SongDto song)
        {
            if (ModelState.IsValid)
            {
                _songsService.CreateSong(song);

                return Ok();
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Updates specified song
        /// </summary>
        /// <param name="song">Song</param>
        /// <returns>IActionResult</returns>
        [HttpPut("{id}")]
        public IActionResult UpdateSong([FromBody] SongDto song)
        {
            if (ModelState.IsValid)
            {
                _songsService.UpdateSong(song);

                return Ok();
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Deletes a song
        /// </summary>
        /// <param name="id">Song id</param>
        /// <returns>IActionResult</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteSong(int id)
        {
            var song = _songsService.GetSongById(id);

            if (song != null)
            {
                _songsService.DeleteSong(id);

                return Ok();
            }

            return BadRequest();
        }
    }
}
