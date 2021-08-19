using Microsoft.AspNetCore.Mvc;
using MusicCatalog.DataAccess.Entities;
using MusicCatalog.Services.Interfaces;

namespace MusicCatalog.Web.Controllers
{
    /// <summary>
    /// Home controller
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Songs service
        /// </summary>
        private readonly ISongsService _songsService;

        public HomeController(ISongsService songsService)
        {
            _songsService = songsService;
        }

        /// <summary>
        /// Displays a list of songs
        /// </summary>
        /// <returns>View with a songs list</returns>
        public ActionResult Index()
        {
            var songs = _songsService.GetSongs();

            return View(songs);
        }

        /// <summary>
        /// Get-request for creating song
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Post-request for creating song
        /// </summary>
        /// <param name="song">Song</param>
        /// <returns>ViewResult</returns>
        [HttpPost]
        public ActionResult Create(Song song)
        {
            if (ModelState.IsValid)
            {
                _songsService.CreateSong(song);

                return RedirectToAction(nameof(Index));
            }

            return View(song);
        }
    }
}
