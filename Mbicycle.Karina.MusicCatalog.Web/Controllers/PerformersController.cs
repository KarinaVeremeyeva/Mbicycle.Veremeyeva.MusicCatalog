using Mbicycle.Karina.MusicCatalog.Domain;
using Mbicycle.Karina.MusicCatalog.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Mbicycle.Karina.MusicCatalog.Web.Controllers
{
    public class PerformersController : Controller
    {
        private readonly MusicContext context;
        private readonly UnitOfWork unitOfWork;

        public PerformersController(MusicContext context)
        {
            this.context = context;
            unitOfWork = new UnitOfWork(context);
        }

        public ActionResult Index()
        {
            var performers = unitOfWork.Performers.GetAll();

            return View(performers);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Performer performer)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Performers.Create(performer);
                unitOfWork.Save();

                return RedirectToAction("Index");
            }

            return View(performer);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var performer = unitOfWork.Performers.GetById(id);

            if (performer == null)
            {
                return NotFound();
            }

            return View(performer);
        }

        [HttpPost]
        public ActionResult Edit(Performer performer)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Performers.Update(performer);
                unitOfWork.Save();

                return RedirectToAction("Index");
            }

            return View(performer);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var performerToDelete = unitOfWork.Performers.GetById(id);

            if (performerToDelete == null)
            {
                return NotFound();
            }

            return View(performerToDelete);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            unitOfWork.Performers.Delete(id);
            unitOfWork.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
