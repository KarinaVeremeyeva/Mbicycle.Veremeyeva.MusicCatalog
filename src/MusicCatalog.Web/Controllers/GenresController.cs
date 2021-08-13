using Microsoft.AspNetCore.Mvc;
using MusicCatalog.DataAccess.Entities;
using MusicCatalog.Services;

namespace MusicCatalog.Web.Controllers
{
    /// <summary>
    /// Genres controller
    /// </summary>
    public class GenresController : Controller
    {
        /// <summary>
        /// Genre service
        /// </summary>
        private readonly GenresService _genresService;

        public GenresController(GenresService genresService)
        {
            _genresService = genresService;
        }

        /// <summary>
        /// Displays a list of genres
        /// </summary>
        /// <returns>View with genres</returns>
        public ActionResult Index()
        {
            var genres = _genresService.GetGenres();

            return View(genres);
        }

        /// <summary>
        /// Get-request for creating genre
        /// </summary>
        /// <returns>ViewResult</returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Post-request for creating genre
        /// </summary>
        /// <param name="genre">Genre</param>
        /// <returns>ViewResult</returns>
        [HttpPost]
        public ActionResult Create(Genre genre)
        {
            if (ModelState.IsValid)
            {
                _genresService.CreateGenre(genre);
                _genresService.Save();

                return RedirectToAction(nameof(Index));
            }

            return View(genre);
        }

        /// <summary>
        /// Get-request for editing genre
        /// </summary>
        /// <param name="id">Genre id</param>
        /// <returns>ViewResult</returns>
        public ActionResult Edit(int id)
        {
            var genreToUpdate = _genresService.GetGenreById(id);

            if (genreToUpdate == null)
            {
                return NotFound();
            }

            return View(genreToUpdate);
        }

        /// <summary>
        /// Post-request for editing genre
        /// </summary>
        /// <param name="genre">Genre</param>
        /// <returns>ViewResult</returns>
        [HttpPost]
        public ActionResult Edit(Genre genre)
        {
            if (ModelState.IsValid)
            {
                _genresService.UpdateGenre(genre);
                _genresService.Save();

                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        /// <summary>
        /// Get-request for deleting genre
        /// </summary>
        /// <param name="id">Genre id</param>
        /// <returns>ViewResult</returns>
        public ActionResult Delete(int id)
        {
            var genreToDelete = _genresService.GetGenreById(id);

            if (genreToDelete == null)
            {
                return NotFound();
            }

            return View(genreToDelete);
        }

        /// <summary>
        /// Post-request for deleting genre
        /// </summary>
        /// <param name="id">Genre id</param>
        /// <returns>ViewResult</returns>
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _genresService.DeleteGenre(id);
            _genresService.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}
