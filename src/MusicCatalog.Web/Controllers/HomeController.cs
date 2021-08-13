using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicCatalog.DataAccess;
using System.Linq;

namespace MusicCatalog.Web.Controllers
{
    /// <summary>
    /// Home controller
    /// </summary>
    public class HomeController : Controller
    {
        /// <inheritdoc cref="MusicContext"/>
        private readonly MusicContext _context;

        public HomeController(MusicContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Displays a list of songs
        /// </summary>
        /// <returns>View with a songs list</returns>
        public IActionResult Index()
        {
            var songs = _context.Songs
                .Include(song => song.Performer)
                .Include(song => song.Genre).ToList();

            return View(songs);
        }
    }
}
