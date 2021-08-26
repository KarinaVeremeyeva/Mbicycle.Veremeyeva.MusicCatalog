using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicCatalog.BusinessLogic.Interfaces;
using MusicCatalog.BusinessLogic.Models;
using MusicCatalog.Web.ViewModels;
using System.Collections.Generic;

namespace MusicCatalog.Web.Controllers
{
    /// <summary>
    /// Albums controller
    /// </summary>
    public class AlbumsController : Controller
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
        /// Albums controller
        /// </summary>
        /// <param name="albumsService">Album service</param>
        /// <param name="mapper">Mapper</param>
        public AlbumsController(IAlbumsService albumsService, IMapper mapper)
        {
            _albumsService = albumsService;
            _mapper = mapper;
        }

        /// <summary>
        ///  Displays a list of albums
        /// </summary>
        /// <returns>View with a album list</returns>
        public ActionResult Index()
        {
            var albumModels = _albumsService.GetAlbums();
            var albums = _mapper.Map<List<AlbumViewModel>>(albumModels);

            return View(albums);
        }

        /// <summary>
        /// Get-request for creating album
        /// </summary>
        /// <returns>ViewResult</returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Post-request for creating album
        /// </summary>
        /// <param name="albumViewModel">Album</param>
        /// <returns>ViewResult</returns>
        [HttpPost]
        public ActionResult Create(AlbumViewModel albumViewModel)
        {
            var album = _mapper.Map<AlbumDto>(albumViewModel);

            if (ModelState.IsValid)
            {
                _albumsService.CreateAlbum(album);

                return RedirectToAction(nameof(Index));
            }

            return View(albumViewModel);
        }

        /// <summary>
        /// Get-request for editing album
        /// </summary>
        /// <param name="id">Album id</param>
        /// <returns>ViewResult</returns>
        public ActionResult Edit(int id)
        {
            var albumToUpdate = _albumsService.GetAlbumById(id);
            var album = _mapper.Map<AlbumViewModel>(albumToUpdate);

            if (albumToUpdate == null)
            {
                return NotFound();
            }

            return View(album);
        }

        /// <summary>
        /// Post-request for editing album
        /// </summary>
        /// <param name="albumViewModel">Album</param>
        /// <returns>ViewResult</returns>
        [HttpPost]
        public ActionResult Edit(AlbumViewModel albumViewModel)
        {
            var album = _mapper.Map<AlbumDto>(albumViewModel);

            if (ModelState.IsValid)
            {
                _albumsService.UpdateAlbum(album);

                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        /// <summary>
        /// Get-request for deleting album
        /// </summary>
        /// <param name="id">Album id</param>
        /// <returns>ViewResult</returns>
        public ActionResult Delete(int id)
        {
            var albumToDelete = _albumsService.GetAlbumById(id);
            var album = _mapper.Map<AlbumViewModel>(albumToDelete);

            if (albumToDelete == null)
            {
                return NotFound();
            }

            return View(album);
        }

        /// <summary>
        /// Post-request for deleting album
        /// </summary>
        /// <param name="id">Album id</param>
        /// <returns>ViewResult</returns>
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _albumsService.DeleteAlbum(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
