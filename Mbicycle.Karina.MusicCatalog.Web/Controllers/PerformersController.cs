﻿using Mbicycle.Karina.MusicCatalog.Domain;
using Mbicycle.Karina.MusicCatalog.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Mbicycle.Karina.MusicCatalog.Web.Controllers
{
    public class PerformersController : Controller
    {
        private readonly UnitOfWork unitOfWork;

        public PerformersController()
        {
            unitOfWork = new UnitOfWork();
        }

        public ActionResult Index()
        {
            var performers = unitOfWork.Performers.GetAll();

            return View(performers);
        }

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

        public ActionResult Delete(int id)
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