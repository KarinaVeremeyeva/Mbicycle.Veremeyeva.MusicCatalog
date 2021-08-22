﻿using Microsoft.AspNetCore.Mvc;
using MusicCatalog.DataAccess.Entities;
using MusicCatalog.Services.Interfaces;

namespace MusicCatalog.Web.Controllers
{
    /// <summary>
    /// Performers controller
    /// </summary>
    public class PerformersController : Controller
    {
        /// <summary>
        /// Performer service
        /// </summary>
        private readonly IPerformersService _performersService;

        public PerformersController(IPerformersService performersService)
        {
            _performersService = performersService;
        }

        /// <summary>
        /// Displays a list of performers
        /// </summary>
        /// <returns>View with performers</returns>
        public ActionResult Index()
        {
            var performers = _performersService.GetPerformers();

            return View(performers);
        }

        /// <summary>
        /// Get-request for creating performer
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Post-request for creating performer
        /// </summary>
        /// <param name="performer">Performer</param>
        /// <returns>ViewResult</returns>
        [HttpPost]
        public ActionResult Create(Performer performer)
        {
            if (ModelState.IsValid)
            {
                _performersService.CreatePerformer(performer);

                return RedirectToAction(nameof(Index));
            }

            return View(performer);
        }

        /// <summary>
        /// Get-request for editing performer
        /// </summary>
        /// <param name="id">Performer id</param>
        /// <returns>ViewResult</returns>
        public ActionResult Edit(int id)
        {
            var performerToUpdate = _performersService.GetPerformerById(id);

            if (performerToUpdate == null)
            {
                return NotFound();
            }

            return View(performerToUpdate);
        }

        /// <summary>
        /// Post-request for editing performer
        /// </summary>
        /// <param name="performer">Performer</param>
        /// <returns>ViewResult</returns>
        [HttpPost]
        public ActionResult Edit(Performer performer)
        {
            if (ModelState.IsValid)
            {
                _performersService.UpdatePerformer(performer);

                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        /// <summary>
        /// Get-request for deleting performer
        /// </summary>
        /// <param name="id">Performer id</param>
        /// <returns>ViewResult</returns>
        public ActionResult Delete(int id)
        {
            var performerToDelete = _performersService.GetPerformerById(id);

            if (performerToDelete == null)
            {
                return NotFound();
            }

            return View(performerToDelete);
        }

        /// <summary>
        /// Post-request for deleting performer
        /// </summary>
        /// <param name="id">Performer id</param>
        /// <returns>ViewResult</returns>
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _performersService.DeletePerformer(id);

            return RedirectToAction(nameof(Index));
        }
    }
}