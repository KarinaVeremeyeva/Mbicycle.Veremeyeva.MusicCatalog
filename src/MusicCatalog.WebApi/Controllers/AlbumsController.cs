using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicCatalog.BusinessLogic.Interfaces;
using MusicCatalog.BusinessLogic.Models;
using MusicCatalog.Web.ViewModels;
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
        /// Mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Albums controller constructor
        /// </summary>
        /// <param name="albumsService">Album service</param>
        /// <param name="mapper">Mapper</param>
        public AlbumsController(IAlbumsService albumsService, IMapper mapper)
        {
            _albumsService = albumsService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets albums list
        /// </summary>
        /// <returns>Albums</returns>
        [HttpGet]
        public IEnumerable<AlbumViewModel> GetAlbums()
        {
            var albumModels = _albumsService.GetAlbums();
            var albums = _mapper.Map<List<AlbumViewModel>>(albumModels);

            return albums;
        }

        /// <summary>
        /// Gets an album by id
        /// </summary>
        /// <param name="id">Album id</param>
        /// <returns>Album</returns>
        [HttpGet("{id}")]
        public AlbumViewModel GetAlbum(int id)
        {
            var albumModel = _albumsService.GetAlbumById(id);
            var album = _mapper.Map<AlbumViewModel>(albumModel);

            return album;
        }

        /// <summary>
        /// Creates a new album
        /// </summary>
        /// <param name="albumViewModel">Album</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        public IActionResult CreateAlbum([FromBody] AlbumViewModel albumViewModel)
        {
            var album = _mapper.Map<AlbumDto>(albumViewModel);

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
        /// <param name="albumViewModel">Album</param>
        /// <returns>IActionResult</returns>
        [HttpPut("{id}")]
        public IActionResult UpdateAlbum([FromBody] AlbumViewModel albumViewModel)
        {
            var album = _mapper.Map<AlbumDto>(albumViewModel);

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
