using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using MusicCatalog.DataAccess.Entities;
using MusicCatalog.Services.Interfaces;
using System;
using System.Collections;
using System.Linq;

namespace MusicCatalog.Web.Controllers
{
    /// <summary>
    /// Home controller
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Songs service
        /// </summary>
        private readonly ISongsService _songsService;

        public HomeController(ISongsService songsService)
        {
            _songsService = songsService;
        }

        /// <summary>
        /// Set up language
        /// </summary>
        /// <param name="culture">Culture</param>
        /// <param name="returnUrl">Return url</param>
        /// <returns></returns>
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
        public IActionResult Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var songs = _songsService.GetSongs();

            if (!string.IsNullOrEmpty(searchString))
            {
                songs = songs.Where(s => s.Name.Contains(searchString));
            }

            return View(songs);
        }

        /// <summary>
        /// Get-request for creating song
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Post-request for creating song
        /// </summary>
        /// <param name="song">Song</param>
        /// <returns>ViewResult</returns>
        [HttpPost]
        public IActionResult Create(Song song)
        {
            if (ModelState.IsValid)
            {
                _songsService.CreateSong(song);

                return RedirectToAction(nameof(Index));
            }

            return View(song);
        }

        /// <summary>
        /// Get-request for editing song
        /// </summary>
        /// <param name="id">Song id</param>
        /// <returns>ViewResult</returns>
        public IActionResult Edit(int id)
        {
            var songToUpdate = _songsService.GetSongById(id);

            if (songToUpdate == null)
            {
                return NotFound();
            }

            return View(songToUpdate);
        }

        /// <summary>
        /// Post-request for editing song
        /// </summary>
        /// <param name="song">Song</param>
        /// <returns>ViewResult</returns>
        [HttpPost]
        public IActionResult Edit(Song song)
        {
            if (ModelState.IsValid)
            {
                _songsService.UpdateSong(song);

                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        /// <summary>
        /// Get-request for deleting song
        /// </summary>
        /// <param name="id">Song id</param>
        /// <returns>ViewResult</returns>
        public IActionResult Delete(int id)
        {
            var songToDelete = _songsService.GetSongById(id);

            if (songToDelete == null)
            {
                return NotFound();
            }

            return View(songToDelete);
        }

        /// <summary>
        /// Post-request for deleting song
        /// </summary>
        /// <param name="id">Song id</param>
        /// <returns>ViewResult</returns>
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _songsService.DeleteSong(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
