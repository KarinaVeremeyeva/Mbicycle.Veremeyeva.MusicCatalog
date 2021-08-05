using Mbicycle.Karina.MusicCatalog.Domain;
using Mbicycle.Karina.MusicCatalog.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Mbicycle.Karina.MusicCatalog.Web.Controllers
{
    public class GenresController : Controller
    {
        private readonly MusicContext context;
        private readonly UnitOfWork unitOfWork;

        public GenresController(MusicContext context)
        {
            this.context = context;
            unitOfWork = new UnitOfWork(context);
        }

        public ActionResult Index()
        {
            var genres = unitOfWork.Genres.GetAll();

            return View(genres);
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
                unitOfWork.Genres.Create(genre);
                unitOfWork.Save();

                return RedirectToAction("Index");
            }

            return View(genre);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var genreToUpdate = unitOfWork.Genres.GetById(id);

            if (genreToUpdate == null)
            {
                return NotFound();
            }

            return View(genreToUpdate);
        }

        [HttpPost]
        public IActionResult Edit(Genre genre)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Genres.Update(genre);
                unitOfWork.Save();

                return RedirectToAction("Index");
            }

            return View(genre);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var genreToDelete = unitOfWork.Genres.GetById(id);

            if (genreToDelete == null)
            {
                return NotFound();
            }

            return View(genreToDelete);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            unitOfWork.Genres.Delete(id);
            unitOfWork.Save();

            return RedirectToAction("Index");
        }
    }
}
