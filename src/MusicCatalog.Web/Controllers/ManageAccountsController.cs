using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MusicCatalog.Web.Services;
using MusicCatalog.Web.ViewModels;
using System.Collections.Generic;
using System.Net.Http.Json;
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
        private readonly AccountApiClient _accountApiClient;

        /// <summary>
        /// Mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Manage accounts controller constructor
        /// </summary>
        /// <param name="accountApiClient">Account api client</param>
        /// <param name="mapper">Mapper</param>
        public ManageAccountsController(AccountApiClient accountApiClient, IMapper mapper)
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
            var response = await _accountApiClient.Client
                .GetFromJsonAsync<List<IdentityUser>>("api/Admin");

            var users = _mapper.Map<List<UserViewModel>>(response);

            return View(users);
        }

        /// <summary>
        /// Updates user bu id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>IActionResult</returns>
        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var userToUpdate = await _accountApiClient.Client
                .GetFromJsonAsync<IdentityUser>($"api/Admin/{id}");

            if (userToUpdate == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var user = _mapper.Map<UserViewModel>(userToUpdate);

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
                var user = _mapper.Map<IdentityUser>(model);
                var response = await _accountApiClient.Client
                    .PutAsJsonAsync($"api/Admin/{user.Id}", user);

                if (response.IsSuccessStatusCode)
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
            var userToDelete = await _accountApiClient.Client
                .GetFromJsonAsync<IdentityUser>($"api/Admin/{id}");

            if (userToDelete == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var user = _mapper.Map<UserViewModel>(userToDelete);

            return View(user);
        }

        /// <summary>
        ///  Deletes a user
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>IActionResult</returns>
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Error", "Home");
            }

            var response = await _accountApiClient.Client.DeleteAsync($"api/Admin/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();
        }
    }
}
