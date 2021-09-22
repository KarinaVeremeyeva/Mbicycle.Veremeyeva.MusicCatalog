using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MusicCatalog.BusinessLogic.Models;
using MusicCatalog.Web.Services.Interfaces;
using MusicCatalog.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MusicCatalog.Web.Controllers
{
    /// <summary>
    /// Home controller
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Songs api service
        /// </summary>
        private readonly ISongApiService _songApiService;

        /// <summary>
        /// Mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Home controller
        /// </summary>
        /// <param name="songApiService">Song api service</param>
        /// <param name="mapper">Mapper</param>
        public HomeController(ISongApiService songApiService, IMapper mapper)
        {
            _songApiService = songApiService;
            _mapper = mapper;
        }

        /// <summary>
        /// Set up language
        /// </summary>
        /// <param name="culture">Culture</param>
        /// <param name="returnUrl">Return url</param>
        /// <returns>ViewResult</returns>
        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }

        /// <summary>
        /// Displays a list of songs filtering by searchString
        /// </summary>
        /// <param name="searchString">String to search</param>
        /// <returns>View with a songs list</returns>
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var songs = await _songApiService.GetSongs();

            if (!string.IsNullOrEmpty(searchString))
            {
                songs = songs.Where(s => s.Name.Contains(searchString));
            }

            var songViewModels = _mapper.Map<List<SongViewModel>>(songs);

            return View(songViewModels);
        }

        /// <summary>
        /// Get-request for creating song
        /// </summary>
        /// <returns>IActionResult</returns>
        [Authorize(Roles = "admin, manager")]
        public IActionResult Create()
        {
            PopulateGenresDropDownList();
            PopulatePerformersDropDownList();
            PopulateAlbumsDropDownList();

            return View();
        }

        /// <summary>
        /// Post-request for creating song
        /// </summary>
        /// <param name="songViewModel">Song</param>
        /// <returns>ViewResult</returns>
        [HttpPost]
        [Authorize(Roles = "admin, manager")]
        public async Task<IActionResult> Create(SongViewModel songViewModel)
        {
            var song = _mapper.Map<SongDto>(songViewModel);

            if (ModelState.IsValid)
            {
                var response = await _songApiService.CreateSong(song);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            PopulateGenresDropDownList(song.GenreId);
            PopulatePerformersDropDownList(song.PerformerId);
            PopulateAlbumsDropDownList(song.AlbumId);

            return View(songViewModel);
        }

        /// <summary>
        /// Get-request for editing song
        /// </summary>
        /// <param name="id">Song id</param>
        /// <returns>ViewResult</returns>
        [Authorize(Roles = "admin, manager")]
        public async Task<IActionResult> Edit(int id)
        {
            var songToUpdate = await _songApiService.GetSongById(id);
            var song = _mapper.Map<SongViewModel>(songToUpdate);

            if (songToUpdate == null)
            {
                return RedirectToAction("Error", "Home");
            }
            PopulateGenresDropDownList(songToUpdate.GenreId);
            PopulatePerformersDropDownList(songToUpdate.PerformerId);
            PopulateAlbumsDropDownList(songToUpdate.AlbumId);

            return View(song);
        }

        /// <summary>
        /// Post-request for editing song
        /// </summary>
        /// <param name="songViewModel">Song</param>
        /// <returns>ViewResult</returns>
        [HttpPost]
        [Authorize(Roles = "admin, manager")]
        public async Task<IActionResult> Edit(SongViewModel songViewModel)
        {
            var song = _mapper.Map<SongDto>(songViewModel);

            if (ModelState.IsValid)
            {
                var response = await _songApiService.UpdateSong(song);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            PopulateGenresDropDownList(song.GenreId);
            PopulatePerformersDropDownList(song.PerformerId);
            PopulateAlbumsDropDownList(song.AlbumId);

            return View();
        }

        /// <summary>
        /// Get-request for deleting song
        /// </summary>
        /// <param name="id">Song id</param>
        /// <returns>ViewResult</returns>
        [Authorize(Roles = "admin, manager")]
        public IActionResult Delete(int id)
        {
            var songToDelete = _songApiService.GetSongById(id);
            var song = _mapper.Map<SongViewModel>(songToDelete);

            if (songToDelete == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(song);
        }

        /// <summary>
        /// Post-request for deleting song
        /// </summary>
        /// <param name="id">Song id</param>
        /// <returns>ViewResult</returns>
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "admin, manager")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _songApiService.DeleteSong(id);

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Populates a genres dropdown list
        /// </summary>
        /// <param name="selectedGenre">Selected genre</param>
        private void PopulateGenresDropDownList(object selectedGenre = null)
        {
            //var genres = _genresService.GetGenres();
            //ViewBag.GenreId = new SelectList(genres, "GenreId", "Name", selectedGenre);
        }

        /// <summary>
        /// Populates a performers dropdown list
        /// </summary>
        /// <param name="selectedPerformer">Selected performer</param>
        private void PopulatePerformersDropDownList(object selectedPerformer = null)
        {
            //var performers = _performersService.GetPerformers();
            //ViewBag.PerformerId = new SelectList(performers, "PerformerId", "Name", selectedPerformer);
        }

        /// <summary>
        /// Populates an album dropdown list
        /// </summary>
        /// <param name="selectedAlbum">Selected album</param>
        private void PopulateAlbumsDropDownList(object selectedAlbum = null)
        {
            //var albums = _albumsService.GetAlbums();
            //ViewBag.AlbumId = new SelectList(albums, "AlbumId", "Name", selectedAlbum);
        }

        /// <summary>
        /// Error of request
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
