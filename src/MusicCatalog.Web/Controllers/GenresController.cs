using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicCatalog.BusinessLogic.Interfaces;
using MusicCatalog.BusinessLogic.Models;
using MusicCatalog.Web.ViewModels;
using System.Collections.Generic;

namespace MusicCatalog.Web.Controllers
{
    /// <summary>
    /// Genres controller
    /// </summary>
    public class GenresController : Controller
    {
        /// <summary>
        /// Genre service
        /// </summary>
        private readonly IGenresService _genresService;

        /// <summary>
        /// Mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Genres controller
        /// </summary>
        /// <param name="genresService">Genres service</param>
        /// <param name="mapper">Mapper</param>
        public GenresController(IGenresService genresService, IMapper mapper)
        {
            _genresService = genresService;
            _mapper = mapper;
        }

        /// <summary>
        /// Displays a list of genres
        /// </summary>
        /// <returns>View with genres</returns>
        public ActionResult Index()
        {
            var genreModels = _genresService.GetGenres();
            var genres = _mapper.Map<List<GenreViewModel>>(genreModels);

            return View(genres);
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
        public ActionResult Create(GenreViewModel genreViewModel)
        {
            var genre = _mapper.Map<GenreDto>(genreViewModel);

            if (ModelState.IsValid)
            {
                _genresService.CreateGenre(genre);

                return RedirectToAction(nameof(Index));
            }

            return View(genreViewModel);
        }

        /// <summary>
        /// Get-request for editing genre
        /// </summary>
        /// <param name="id">Genre id</param>
        /// <returns>ViewResult</returns>
        public ActionResult Edit(int id)
        {
            var genreToUpdate = _genresService.GetGenreById(id);
            var genre = _mapper.Map<GenreViewModel>(genreToUpdate);

            if (genreToUpdate == null)
            {
                return NotFound();
            }

            return View(genre);
        }

        /// <summary>
        /// Post-request for editing genre
        /// </summary>
        /// <param name="genreViewModel">Genre</param>
        /// <returns>ViewResult</returns>
        [HttpPost]
        public ActionResult Edit(GenreViewModel genreViewModel)
        {
            var genre = _mapper.Map<GenreDto>(genreViewModel);

            if (ModelState.IsValid)
            {
                _genresService.UpdateGenre(genre);

                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        /// <summary>
        /// Get-request for deleting genre
        /// </summary>
        /// <param name="id">Genre id</param>
        /// <returns>ViewResult</returns>
        public ActionResult Delete(int id)
        {
            var genreToDelete = _genresService.GetGenreById(id);
            var genre = _mapper.Map<GenreViewModel>(genreToDelete);

            if (genreToDelete == null)
            {
                return NotFound();
            }

            return View(genre);
        }

        /// <summary>
        /// Post-request for deleting genre
        /// </summary>
        /// <param name="id">Genre id</param>
        /// <returns>ViewResult</returns>
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _genresService.DeleteGenre(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
