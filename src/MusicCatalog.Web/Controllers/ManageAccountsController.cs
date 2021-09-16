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
        /// Account api client
        /// </summary>
        private readonly IAccountApiService _accountApiClient;

        /// <summary>
        /// Mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Manage accounts controller constructor
        /// </summary>
        /// <param name="accountApiClient">Account api client</param>
        /// <param name="mapper">Mapper</param>
        public ManageAccountsController(IAccountApiService accountApiClient, IMapper mapper)
        {
            _accountApiClient = accountApiClient;
            _mapper = mapper;
        }

        /// <summary>
        /// Displays a list of users emails
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _accountApiClient.GetUsers();
            var userViewModels = _mapper.Map<List<UserViewModel>>(users);

            return View(userViewModels);
        }

        /// <summary>
        /// Updates user bu id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>IActionResult</returns>
        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var userToUpdate = await _accountApiClient.GetUser(id);

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
                var updateUserResponse = await _accountApiClient.PutUser(user);
                var updateRoleResponse = await _accountApiClient.UpdateRole(user.Id, user.Role);

                if (updateUserResponse.IsSuccessStatusCode
                    && updateRoleResponse.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError(string.Empty, "Wrong user details");
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
            var userToDelete = await _accountApiClient.GetUser(id);

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

            var response = await _accountApiClient.DeleteUser(id);

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
            var roles = await _accountApiClient.GetRoles();
            var items = roles
                .Select(role => new SelectListItem { Text = role, Value = role })
                .ToList();

            return items;
        }
    }
}
