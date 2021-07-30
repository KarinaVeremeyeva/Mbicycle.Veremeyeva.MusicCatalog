using Mbicycle.Karina.MusicCatalog.Domain;
using Mbicycle.Karina.MusicCatalog.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Mbicycle.Karina.MusicCatalog.Web.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly MusicContext db;

        public AlbumsController(MusicContext context)
        {
            this.db = context;
        }

        public ActionResult Index()
        {
            var albums = db.Albums
                .Include(album => album.Song).ToList();

            return View(albums);
        }

        public ActionResult CreateAlbum(Album album)
        {
            if (ModelState.IsValid)
            {
                db.Add(album);
                db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(album);
        }
    }
}
