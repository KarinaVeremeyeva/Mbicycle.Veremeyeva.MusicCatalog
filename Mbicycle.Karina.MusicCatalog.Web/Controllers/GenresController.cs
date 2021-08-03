using Mbicycle.Karina.MusicCatalog.Domain;
using Mbicycle.Karina.MusicCatalog.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Mbicycle.Karina.MusicCatalog.Web.Controllers
{
    public class GenresController : Controller
    {
        private readonly MusicContext db;

        public GenresController(MusicContext context)
        {
            this.db = context;
        }

        public ActionResult Index()
        {
            return View(db.Genres.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Genre genre)
        {
            if (ModelState.IsValid)
            {
                db.Genres.Add(genre);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(genre);
        }

        [HttpPost]
        public IActionResult Edit(int? id)
        {
            var genreToUpdate = db.Genres.FirstOrDefault(genre => genre.GenreId == id);

            if (ModelState.IsValid)
            {
                db.Entry(genreToUpdate).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(genreToUpdate);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genreToDelete = db.Genres.FirstOrDefault(genre => genre.GenreId == id);

            if (genreToDelete == null)
            {
                return NotFound();
            }

            return View(genreToDelete);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int? id)
        {
            var genreToDelete = db.Genres.FirstOrDefault(genre => genre.GenreId == id);

            db.Genres.Remove(genreToDelete);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
