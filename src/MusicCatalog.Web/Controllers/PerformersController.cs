using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicCatalog.BusinessLogic.Interfaces;
using MusicCatalog.BusinessLogic.Models;
using MusicCatalog.Web.ViewModels;
using System;
using System.Collections.Generic;

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

        /// <summary>
        /// Mapper
        /// </summary>
        private readonly IMapper _mapper;

        public PerformersController(IPerformersService performersService, IMapper mapper)
        {
            _performersService = performersService;
            _mapper = mapper;
        }

        /// <summary>
        /// Displays a list of performers
        /// </summary>
        /// <returns>View with performers</returns>
        public ActionResult Index()
        {
            var performerModels = _performersService.GetPerformers();
            var performers = _mapper.Map<List<PerformerViewModel>>(performerModels);

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
        /// <param name="performerViewModel">Performer</param>
        /// <returns>ViewResult</returns>
        [HttpPost]
        public ActionResult Create(PerformerViewModel performerViewModel)
        {
            var performer = _mapper.Map<PerformerDto>(performerViewModel);

            if (ModelState.IsValid)
            {
                _performersService.CreatePerformer(performer);

                return RedirectToAction(nameof(Index));
            }

            return View(performerViewModel);
        }

        /// <summary>
        /// Get-request for editing performer
        /// </summary>
        /// <param name="id">Performer id</param>
        /// <returns>ViewResult</returns>
        public ActionResult Edit(int id)
        {
            var performerToUpdate = _performersService.GetPerformerById(id);
            var performer = _mapper.Map<PerformerViewModel>(performerToUpdate);

            if (performerToUpdate == null)
            {
                return NotFound();
            }

            return View(performer);
        }

        /// <summary>
        /// Post-request for editing performer
        /// </summary>
        /// <param name="performerViewModel">Performer</param>
        /// <returns>ViewResult</returns>
        [HttpPost]
        public ActionResult Edit(PerformerViewModel performerViewModel)
        {
            var performer = _mapper.Map<PerformerDto>(performerViewModel);

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
            var performer = _mapper.Map<PerformerViewModel>(performerToDelete);

            if (performerToDelete == null)
            {
                return NotFound();
            }

            return View(performer);
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
