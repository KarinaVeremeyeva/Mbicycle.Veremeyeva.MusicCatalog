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

            return View("Create", album);
        }

        [HttpPost]
        public IActionResult Edit(int? id)
        {
            var albumToUpdate = db.Albums.FirstOrDefault(album => album.AlbumId == id);

            if (ModelState.IsValid)
            {
                db.Entry(albumToUpdate).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(albumToUpdate);
        }

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            var albumToDelete = db.Albums.FirstOrDefault(album => album.AlbumId == id);

            if (id != null)
            {
                db.Albums.Remove(albumToDelete);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return NotFound();
        }
    }
}
