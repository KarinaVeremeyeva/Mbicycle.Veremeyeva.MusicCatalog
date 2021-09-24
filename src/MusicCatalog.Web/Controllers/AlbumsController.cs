using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicCatalog.BusinessLogic.Models;
using MusicCatalog.Web.Services.Interfaces;
using MusicCatalog.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MusicCatalog.Web.Controllers
{
    /// <summary>
    /// Albums controller
    /// </summary>
    [Authorize(Roles = "admin, user, manager")]
    public class AlbumsController : Controller
    {
        /// <summary>
        /// Album api service
        /// </summary>
        private readonly IAlbumApiService _albumApiService;

        /// <summary>
        /// Mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Jwt token key
        /// </summary>
        private const string JwtTokenKey = "secret_jwt_key";

        /// <summary>
        /// Authorization header name
        /// </summary>
        private const string Authorization = "Authorization";

        /// <summary>
        /// Albums controller constructor
        /// </summary>
        /// <param name="albumApiService">Album api service</param>
        /// <param name="mapper">Mapper</param>
        public AlbumsController(IAlbumApiService albumApiService, IMapper mapper)
        {
            _albumApiService = albumApiService;
            _mapper = mapper;
        }

        /// <summary>
        ///  Displays a list of albums
        /// </summary>
        /// <returns>View with a album list</returns>
        public async Task<ActionResult> Index()
        {
            var albums = await _albumApiService.GetAlbums();
            var albumViewModels = _mapper.Map<List<AlbumViewModel>>(albums);

            return View(albumViewModels);
        }

        /// <summary>
        /// Get-request for creating album
        /// </summary>
        /// <returns>ViewResult</returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Post-request for creating album
        /// </summary>
        /// <param name="albumViewModel">Album</param>
        /// <returns>ViewResult</returns>
        [HttpPost]
        public async Task<ActionResult> Create(AlbumViewModel albumViewModel)
        {
            if (ModelState.IsValid)
            {
                var album = _mapper.Map<AlbumDto>(albumViewModel);

                if (!HttpContext.Request.Cookies.ContainsKey(JwtTokenKey))
                {
                    return RedirectToAction("Login", "Accounts");
                }

                var token = HttpContext.Request.Cookies[JwtTokenKey];
                HttpContext.Response.Headers.Add(Authorization, "Bearer " + token);

                var response = await _albumApiService.CreateAlbum(album);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(albumViewModel);
        }

        /// <summary>
        /// Get-request for editing album
        /// </summary>
        /// <param name="id">Album id</param>
        /// <returns>ViewResult</returns>
        public async Task<ActionResult> Edit(int id)
        {
            var albumToUpdate = await _albumApiService.GetAlbumById(id);

            if (albumToUpdate == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var album = _mapper.Map<AlbumViewModel>(albumToUpdate);

            return View(album);
        }

        /// <summary>
        /// Post-request for editing album
        /// </summary>
        /// <param name="albumViewModel">Album</param>
        /// <returns>ViewResult</returns>
        [HttpPost]
        public async Task<ActionResult> Edit(AlbumViewModel albumViewModel)
        {
            if (ModelState.IsValid)
            {
                var album = _mapper.Map<AlbumDto>(albumViewModel);
                var response = await _albumApiService.UpdateAlbum(album);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View();
        }

        /// <summary>
        /// Get-request for deleting album
        /// </summary>
        /// <param name="id">Album id</param>
        /// <returns>ViewResult</returns>
        public async Task<ActionResult> Delete(int id)
        {
            var albumToDelete = await _albumApiService.GetAlbumById(id);

            if (albumToDelete == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var album = _mapper.Map<AlbumViewModel>(albumToDelete);

            return View(album);
        }

        /// <summary>
        /// Post-request for deleting album
        /// </summary>
        /// <param name="id">Album id</param>
        /// <returns>ViewResult</returns>
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var response = await _albumApiService.DeleteAlbum(id);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();
        }
    }
}
