using Mbicycle.Karina.MusicCatalog.Domain;
using Mbicycle.Karina.MusicCatalog.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Mbicycle.Karina.MusicCatalog.Web.Controllers
{
    public class GenresController : Controller
    {
        private IGenreRepository genreRepository;

        public GenresController(IGenreRepository genreRepository)
        {
            this.genreRepository = genreRepository;
        }

        public ActionResult Index()
        {
            return View(genreRepository.GetAll());
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
                genreRepository.Create(genre);
                genreRepository.Save();

                return RedirectToAction("Index");
            }

            return View(genre);
        }

        [HttpPost]
        public IActionResult Edit(int id)
        {
            var genreToUpdate = genreRepository.GetById(id);

            if (ModelState.IsValid)
            {
                genreRepository.Update(genreToUpdate);
                genreRepository.Save();

                return RedirectToAction("Index");
            }

            return View(genreToUpdate);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var genreToDelete = genreRepository.GetById(id);

            if (genreToDelete == null)
            {
                return NotFound();
            }

            return View(genreToDelete);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            genreRepository.Delete(id);
            genreRepository.Save();

            return RedirectToAction("Index");
        }
    }
}
