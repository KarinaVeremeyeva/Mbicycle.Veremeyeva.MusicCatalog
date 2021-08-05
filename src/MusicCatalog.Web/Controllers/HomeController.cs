using MusicCatalog.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MusicCatalog.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly MusicContext db;

        public HomeController(MusicContext context)
        {
            this.db = context;
        }

        public ActionResult Index()
        {
            var songs = db.Songs
                .Include(song => song.Performer)
                .Include(song => song.Genre).ToList();

            return View(songs);
        }
    }
}