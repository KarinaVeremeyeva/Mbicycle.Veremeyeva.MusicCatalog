using MusicCatalog.Domain;
using MusicCatalog.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MusicCatalog.Web.Controllers
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
                .Include(album => album.Song)
                //.Select(song => song.ReleaseDate.ToShortDateString())
                .ToList();

            return View(albums);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Album album)
        {
            if (ModelState.IsValid)
            {
                db.Albums.Add(album);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(album);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var albumToUpdate = db.Albums.FirstOrDefault(album => album.AlbumId == id);

            if (albumToUpdate == null)
            {
                return NotFound();
            }

            return View(albumToUpdate);
        }

        [HttpPost]
        public IActionResult Edit(Album album)
        {
            if (ModelState.IsValid)
            {
                db.Entry(album).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(album);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var albumToDelete = db.Albums.FirstOrDefault(album => album.AlbumId == id);

            if (albumToDelete == null)
            {
                return NotFound();
            }

            return View(albumToDelete);
        }

        [HttpPost, ActionName("Delete")]

        public IActionResult DeleteConfirmed(int id)
        {
            var albumToDelete = db.Albums.FirstOrDefault(album => album.AlbumId == id);

            db.Albums.Remove(albumToDelete);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
