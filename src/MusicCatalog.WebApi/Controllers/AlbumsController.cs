using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicCatalog.BusinessLogic.Interfaces;
using MusicCatalog.BusinessLogic.Models;
using System.Collections.Generic;

namespace MusicCatalog.WebApi.Controllers
{
    /// <summary>
    /// Albums controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        /// <summary>
        /// Albums service
        /// </summary>
        private readonly IAlbumsService _albumsService;

        /// <summary>
        /// Albums controller constructor
        /// </summary>
        /// <param name="albumsService">Album service</param>
        public AlbumsController(IAlbumsService albumsService)
        {
            _albumsService = albumsService;
        }

        /// <summary>
        /// Gets albums list
        /// </summary>
        /// <returns>Albums</returns>
        [HttpGet]
        public IEnumerable<AlbumDto> GetAlbums()
        {
            var albums = _albumsService.GetAlbums();

            return albums;
        }

        /// <summary>
        /// Gets an album by id
        /// </summary>
        /// <param name="id">Album id</param>
        /// <returns>Album</returns>
        [HttpGet("{id}")]
        public AlbumDto GetAlbum(int id)
        {
            var album = _albumsService.GetAlbumById(id);

            return album;
        }

        /// <summary>
        /// Creates a new album
        /// </summary>
        /// <param name="album">Album</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        [Authorize]
        public IActionResult CreateAlbum([FromBody] AlbumDto album)
        {
            if (ModelState.IsValid)
            {
                _albumsService.CreateAlbum(album);

                return Ok();
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Updates specified album
        /// </summary>
        /// <param name="album">Album</param>
        /// <returns>IActionResult</returns>
        [HttpPut("{id}")]
        public IActionResult UpdateAlbum([FromBody] AlbumDto album)
        {
            if (ModelState.IsValid)
            {
                _albumsService.UpdateAlbum(album);

                return Ok();
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Deletes album by id
        /// </summary>
        /// <param name="id">Album id</param>
        /// <returns>IActionResult</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteAlbum(int id)
        {
            var album = _albumsService.GetAlbumById(id);

            if (album != null)
            {
                _albumsService.DeleteAlbum(id);

                return Ok();
            }

            return BadRequest();
        }
    }
}
