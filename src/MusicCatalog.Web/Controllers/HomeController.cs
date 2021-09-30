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
        /// Song api service
        /// </summary>
        private readonly ISongApiService _songApiService;

        /// <summary>
        /// Genre api service
        /// </summary>
        private readonly IGenreApiService _genreApiService;

        /// <summary>
        /// Performer api service
        /// </summary>
        private readonly IPerformerApiService _performerApiService;

        /// <summary>
        /// Album service
        /// </summary>
        private readonly IAlbumApiService _albumApiService;

        /// <summary>
        /// Mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Home controller constructor
        /// </summary>
        /// <param name="songApiService">Song api service</param>
        /// <param name="genreApiService">Genre api service</param>
        /// <param name="performerApiService">Performer api service</param>
        /// <param name="albumApiService">Album api service</param>
        /// <param name="mapper">Mapper</param>
        public HomeController(
            ISongApiService songApiService,
            IGenreApiService genreApiService,
            IPerformerApiService performerApiService,
            IAlbumApiService albumApiService,
            IMapper mapper)
        {
            _songApiService = songApiService;
            _genreApiService = genreApiService;
            _performerApiService = performerApiService;
            _albumApiService = albumApiService;
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
        public async Task<IActionResult> Create()
        {
            await PopulateGenresDropDownListAsync();
            await PopulatePerformersDropDownListAsync();
            await PopulateAlbumsDropDownListAsync();

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
            await PopulateGenresDropDownListAsync(song.GenreId);
            await PopulatePerformersDropDownListAsync(song.PerformerId);
            await PopulateAlbumsDropDownListAsync(song.AlbumId);

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
            await PopulateGenresDropDownListAsync(songToUpdate.GenreId);
            await PopulatePerformersDropDownListAsync(songToUpdate.PerformerId);
            await PopulateAlbumsDropDownListAsync(songToUpdate.AlbumId);

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
            await PopulateGenresDropDownListAsync(song.GenreId);
            await PopulatePerformersDropDownListAsync(song.PerformerId);
            await PopulateAlbumsDropDownListAsync(song.AlbumId);

            return View();
        }

        /// <summary>
        /// Get-request for deleting song
        /// </summary>
        /// <param name="id">Song id</param>
        /// <returns>ViewResult</returns>
        [Authorize(Roles = "admin, manager")]
        public async Task<IActionResult> Delete(int id)
        {
            var songToDelete = await _songApiService.GetSongById(id);
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
        private async Task PopulateGenresDropDownListAsync(object selectedGenre = null)
        {
            var genres = await _genreApiService.GetGenres();

            ViewBag.GenreId = new SelectList(genres, "GenreId", "Name", selectedGenre);
        }

        /// <summary>
        /// Populates a performers dropdown list
        /// </summary>
        /// <param name="selectedPerformer">Selected performer</param>
        private async Task PopulatePerformersDropDownListAsync(object selectedPerformer = null)
        {
            var performers = await _performerApiService.GetPerformers();

            ViewBag.PerformerId = new SelectList(performers, "PerformerId", "Name", selectedPerformer);
        }

        /// <summary>
        /// Populates an album dropdown list
        /// </summary>
        /// <param name="selectedAlbum">Selected album</param>
        private async Task PopulateAlbumsDropDownListAsync(object selectedAlbum = null)
        {
            var albums = await _albumApiService.GetAlbums();

            ViewBag.AlbumId = new SelectList(albums, "AlbumId", "Name", selectedAlbum);
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
