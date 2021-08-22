using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MusicCatalog.DataAccess.Entities;
using MusicCatalog.Services.Interfaces;
using System;
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

        private readonly IGenresService _genresService;
        private readonly IPerformersService _performersService;
        private readonly IAlbumsService _albumsService;

        public HomeController(ISongsService songsService, IGenresService genresService,
                            IPerformersService performersService, IAlbumsService albumsService)
        {
            _songsService = songsService;
            _genresService = genresService;
            _performersService = performersService;
            _albumsService = albumsService;
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
            PopulateGenresDropDownList();
            PopulatePerformersDropDownList();
            PopulateAlbumsDropDownList();

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

            PopulateGenresDropDownList(song.GenreId);
            PopulatePerformersDropDownList(song.PerformerId);
            PopulateAlbumsDropDownList(song.AlbumId);

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

            PopulateGenresDropDownList(songToUpdate.GenreId);
            PopulatePerformersDropDownList(songToUpdate.PerformerId);
            PopulateAlbumsDropDownList(songToUpdate.AlbumId);

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

        private void PopulateGenresDropDownList(object selectedGenre = null)
        {
            var genres = _genresService.GetGenres();
            ViewBag.GenreId = new SelectList(genres, "GenreId", "Name", selectedGenre);
        }

        private void PopulatePerformersDropDownList(object selectedPerformer = null)
        {
            var performers = _performersService.GetPerformers();
            ViewBag.PerformerId = new SelectList(performers, "PerformerId", "Name", selectedPerformer);
        }

        private void PopulateAlbumsDropDownList(object selectedAlbum = null)
        {
            var albums = _albumsService.GetAlbums();
            ViewBag.AlbumId = new SelectList(albums, "AlbumId", "Name", selectedAlbum);
        }
    }
}
