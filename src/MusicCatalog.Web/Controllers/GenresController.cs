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
    /// Genres controller
    /// </summary>
    [Authorize(Roles = "admin, user, manager")]
    public class GenresController : Controller
    {
        /// <summary>
        /// Genre api service
        /// </summary>
        private readonly IGenreApiService _genreApiService;

        /// <summary>
        /// Mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Genres controller
        /// </summary>
        /// <param name="genreApiService">Genres service</param>
        /// <param name="mapper">Mapper</param>
        public GenresController(IGenreApiService genreApiService, IMapper mapper)
        {
            _genreApiService = genreApiService;
            _mapper = mapper;
        }

        /// <summary>
        /// Displays a list of genres
        /// </summary>
        /// <returns>View with genres</returns>
        public async Task<ActionResult> Index()
        {
            var genres = await _genreApiService.GetGenres();
            var genreViewModels = _mapper.Map<List<GenreViewModel>>(genres);

            return View(genreViewModels);
        }

        /// <summary>
        /// Get-request for creating genre
        /// </summary>
        /// <returns>ViewResult</returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Post-request for creating genre
        /// </summary>
        /// <param name="genreViewModel">Genre</param>
        /// <returns>ViewResult</returns>
        [HttpPost]
        public async Task<ActionResult> Create(GenreViewModel genreViewModel)
        {
            if (ModelState.IsValid)
            {
                var genre = _mapper.Map<GenreDto>(genreViewModel);
                var response = await _genreApiService.CreateGenre(genre);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(genreViewModel);
        }

        /// <summary>
        /// Get-request for editing genre
        /// </summary>
        /// <param name="id">Genre id</param>
        /// <returns>ViewResult</returns>
        public async Task<ActionResult> Edit(int id)
        {
            var genreToUpdate = await _genreApiService.GetGenreById(id);

            if (genreToUpdate == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var genre = _mapper.Map<GenreViewModel>(genreToUpdate);

            return View(genre);
        }

        /// <summary>
        /// Post-request for editing genre
        /// </summary>
        /// <param name="genreViewModel">Genre</param>
        /// <returns>ViewResult</returns>
        [HttpPost]
        public async Task<ActionResult> Edit(GenreViewModel genreViewModel)
        {
            if (ModelState.IsValid)
            {
                var genre = _mapper.Map<GenreDto>(genreViewModel);
                var response = await _genreApiService.UpdateGenre(genre);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View();
        }

        /// <summary>
        /// Get-request for deleting genre
        /// </summary>
        /// <param name="id">Genre id</param>
        /// <returns>ViewResult</returns>
        public async Task<ActionResult> Delete(int id)
        {
            var genreToDelete = await _genreApiService.GetGenreById(id);

            if (genreToDelete == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var genre = _mapper.Map<GenreViewModel>(genreToDelete);

            return View(genre);
        }

        /// <summary>
        /// Post-request for deleting genre
        /// </summary>
        /// <param name="id">Genre id</param>
        /// <returns>ViewResult</returns>
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var response = await _genreApiService.DeleteGenre(id);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();
        }
    }
}
