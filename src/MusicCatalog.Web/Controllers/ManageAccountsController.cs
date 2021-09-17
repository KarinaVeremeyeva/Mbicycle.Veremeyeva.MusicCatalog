using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MusicCatalog.IdentityApi.Models;
using MusicCatalog.Web.Services;
using MusicCatalog.Web.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicCatalog.Web.Controllers
{
    /// <summary>
    /// Controller for accounts managing
    /// </summary>
    [Authorize(Roles = "admin")]
    public class ManageAccountsController : Controller
    {
        /// <summary>
        /// Web api client
        /// </summary>
        private readonly IWebApiService _webApiService;

        /// <summary>
        /// Mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Manage accounts controller constructor
        /// </summary>
        /// <param name="webApiService">Web api service</param>
        /// <param name="mapper">Mapper</param>
        public ManageAccountsController(IWebApiService webApiService, IMapper mapper)
        {
            _webApiService = webApiService;
            _mapper = mapper;
        }

        /// <summary>
        /// Displays a list of users emails
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _webApiService.GetUsers();
            var userViewModels = _mapper.Map<List<UserViewModel>>(users);

            return View(userViewModels);
        }

        /// <summary>
        /// Updates user by id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>IActionResult</returns>
        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var userToUpdate = await _webApiService.GetUser(id);

            if (userToUpdate == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var user = _mapper.Map<UserViewModel>(userToUpdate);
            user.ExistingRoles = await GetAllRoles();

            return View(user);
        }

        /// <summary>
        /// Updates user
        /// </summary>
        /// <param name="model">UserViewModel</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        public async Task<IActionResult> Update([FromForm] UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<UserModel>(model);
                var updateUserResponse = await _webApiService.UpdateUser(user);

                if (updateUserResponse.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }

        /// <summary>
        /// Deletes a user by id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>IActionResult</returns>
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var userToDelete = await _webApiService.GetUser(id);

            if (userToDelete == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var user = _mapper.Map<UserViewModel>(userToDelete);

            return View(user);
        }

        /// <summary>
        /// Deletes a user
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Error", "Home");
            }

            var response = await _webApiService.DeleteUser(id);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        /// <summary>
        /// Gets all roles from the identity api
        /// </summary>
        /// <returns>Roles</returns>
        private async Task<IEnumerable<SelectListItem>> GetAllRoles()
        {
            var roles = await _webApiService.GetRoles();
            var items = roles
                .Select(role => new SelectListItem { Text = role, Value = role })
                .ToList();

            return items;
        }
    }
}
