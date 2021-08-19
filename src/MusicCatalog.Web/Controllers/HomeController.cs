using Microsoft.AspNetCore.Mvc;
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
    }
}
