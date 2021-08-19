using Microsoft.AspNetCore.Mvc;
using MusicCatalog.DataAccess.Entities;
using MusicCatalog.Services.Interfaces;

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

        public AlbumsController(IAlbumsService albumsService)
        {
            _albumsService = albumsService;
        }

        /// <summary>
        ///  Displays a list of albums
        /// </summary>
        /// <returns>View with a album list</returns>
        public ActionResult Index()
        {
            var albums = _albumsService.GetAlbums();

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
        /// <param name="album">Album</param>
        /// <returns>ViewResult</returns>
        [HttpPost]
        public ActionResult Create(Album album)
        {
            if (ModelState.IsValid)
            {
                _albumsService.CreateAlbum(album);

                return RedirectToAction(nameof(Index));
            }

            return View(album);
        }

        /// <summary>
        /// Get-request for editing album
        /// </summary>
        /// <param name="id">Album id</param>
        /// <returns>ViewResult</returns>
        public ActionResult Edit(int id)
        {
            var albumToUpdate = _albumsService.GetAlbumById(id);

            if (albumToUpdate == null)
            {
                return NotFound();
            }

            return View(albumToUpdate);
        }

        /// <summary>
        /// Post-request for editing album
        /// </summary>
        /// <param name="album">Album</param>
        /// <returns>ViewResult</returns>
        [HttpPost]
        public ActionResult Edit(Album album)
        {
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

            if (albumToDelete == null)
            {
                return NotFound();
            }

            return View(albumToDelete);
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
