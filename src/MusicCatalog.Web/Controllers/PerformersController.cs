using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicCatalog.BusinessLogic.Models;
using MusicCatalog.Web.Services.Interfaces;
using MusicCatalog.Web.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicCatalog.Web.Controllers
{
    /// <summary>
    /// Performers controller
    /// </summary>
    [Authorize(Roles = "admin, user, manager")]
    public class PerformersController : Controller
    {
        /// <summary>
        /// Performer api service
        /// </summary>
        private readonly IPerformerApiService _performerApiService;

        /// <summary>
        /// Mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Performers controlller
        /// </summary>
        /// <param name="performerApiService">Performer api service</param>
        /// <param name="mapper">Mapper</param>
        public PerformersController(IPerformerApiService performerApiService, IMapper mapper)
        {
            _performerApiService = performerApiService;
            _mapper = mapper;
        }

        /// <summary>
        /// Displays a list of performers
        /// </summary>
        /// <returns>View with performers</returns>
        public async Task<ActionResult> Index()
        {
            var performer = await _performerApiService.GetPerformers();
            var performerViewModels = _mapper.Map<List<PerformerViewModel>>(performer);

            return View(performerViewModels);
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
        public async Task<ActionResult> Create(PerformerViewModel performerViewModel)
        {
            if (ModelState.IsValid)
            {
                var performer = _mapper.Map<PerformerDto>(performerViewModel);
                var response = await _performerApiService.CreatePerformer(performer);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(performerViewModel);
        }

        /// <summary>
        /// Get-request for editing performer
        /// </summary>
        /// <param name="id">Performer id</param>
        /// <returns>ViewResult</returns>
        public async Task<ActionResult> Edit(int id)
        {
            var performerToUpdate = await _performerApiService.GetPerformerById(id);

            if (performerToUpdate == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var performer = _mapper.Map<PerformerViewModel>(performerToUpdate);

            return View(performer);
        }

        /// <summary>
        /// Post-request for editing performer
        /// </summary>
        /// <param name="performerViewModel">Performer</param>
        /// <returns>ViewResult</returns>
        [HttpPost]
        public async Task<ActionResult> Edit(PerformerViewModel performerViewModel)
        {
            if (ModelState.IsValid)
            {
                var performer = _mapper.Map<PerformerDto>(performerViewModel);
                var response = await _performerApiService.UpdatePerformer(performer);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View();
        }

        /// <summary>
        /// Get-request for deleting performer
        /// </summary>
        /// <param name="id">Performer id</param>
        /// <returns>ViewResult</returns>
        public async Task<ActionResult> Delete(int id)
        {
            var performerToDelete = await _performerApiService.GetPerformerById(id);
            var performer = _mapper.Map<PerformerViewModel>(performerToDelete);

            if (performerToDelete == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(performer);
        }

        /// <summary>
        /// Post-request for deleting performer
        /// </summary>
        /// <param name="id">Performer id</param>
        /// <returns>ViewResult</returns>
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var response = await _performerApiService.DeletePerformer(id);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();
        }
    }
}
