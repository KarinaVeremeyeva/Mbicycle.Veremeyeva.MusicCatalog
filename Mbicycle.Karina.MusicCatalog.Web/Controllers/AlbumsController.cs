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

        [HttpGet]
        public IActionResult CreateAlbum()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateAlbum(Album album)
        {
            if (ModelState.IsValid)
            {
                db.Add(album);
                db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(album);
        }

        [HttpPost]
        public IActionResult EditAlbum(int? id)
        {
            var albumToUpdate = db.Albums.FirstOrDefault(album => album.AlbumId == id);

            if (id != null)
            {
                db.Entry(albumToUpdate).State = EntityState.Modified;
                db.SaveChanges();
            }

            return View(albumToUpdate);
        }

        [HttpPost]
        public IActionResult DeleteAlbum(int? id)
        {
            var albumToDelete = db.Albums.FirstOrDefault(album => album.AlbumId == id);

            if (id != null)
            {
                db.Remove(albumToDelete);
                db.SaveChanges();

                return View(albumToDelete);
            }

            return NotFound();
        }
    }
}
