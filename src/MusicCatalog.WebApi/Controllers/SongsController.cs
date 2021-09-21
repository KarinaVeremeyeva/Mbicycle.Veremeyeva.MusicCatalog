using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicCatalog.BusinessLogic.Interfaces;
using MusicCatalog.BusinessLogic.Models;
using MusicCatalog.Web.ViewModels;
using System.Collections.Generic;

namespace MusicCatalog.WebApi.Controllers
{
    /// <summary>
    /// Songs controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        /// <summary>
        /// Songs service
        /// </summary>
        private readonly ISongsService _songsService;

        /// <summary>
        /// Mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Songs controller constructor
        /// </summary>
        /// <param name="songsService">Songs service</param>
        /// <param name="mapper">Mapper</param>
        public SongsController(ISongsService songsService, IMapper mapper)
        {
            _songsService = songsService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets songs list
        /// </summary>
        /// <returns>Songs</returns>
        [HttpGet]
        public IEnumerable<SongViewModel> GetSongs()
        {
            var songModels = _songsService.GetSongs();
            var songs = _mapper.Map<List<SongViewModel>>(songModels);

            return songs;
        }

        /// <summary>
        /// Gets a song by id
        /// </summary>
        /// <param name="id">Song id</param>
        /// <returns>Song</returns>
        [HttpGet("{id}")]
        public SongViewModel GetSong(int id)
        {
            var song = _songsService.GetSongById(id);
            var songViewModel = _mapper.Map<SongViewModel>(song);

            return songViewModel;
        }

        /// <summary>
        /// Creates a new song
        /// </summary>
        /// <param name="songViewModel">Song</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        public IActionResult CreateSong([FromBody] SongViewModel songViewModel)
        {
            var song = _mapper.Map<SongDto>(songViewModel);

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
        /// <param name="songViewModel">Song</param>
        /// <returns>IActionResult</returns>
        [HttpPut("{id}")]
        public IActionResult UpdateSong([FromBody] SongViewModel songViewModel)
        {
            var song = _mapper.Map<SongDto>(songViewModel);

            if (ModelState.IsValid)
            {
                _songsService.CreateSong(song);

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
