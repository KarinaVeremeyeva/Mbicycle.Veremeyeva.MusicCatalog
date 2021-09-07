using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MusicCatalog.Web.ViewModels;
using System.Collections.Generic;
using System.Net.Http;
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
        /// Http client factory for creating http client instances
        /// </summary>
        private readonly IHttpClientFactory _clientFactory;

        /// <summary>
        /// Manage accounts controller constructor
        /// </summary>
        /// <param name="clientFactory">Http client factory</param>
        public ManageAccountsController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        /// <summary>
        /// Displays a list of users emails
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = _clientFactory.CreateClient("client");
            var response = await client.GetFromJsonAsync<List<IdentityUser>>("api/Admin");
            
            var users = new List<UserViewModel>();
            foreach (var identityUser in response)
            {
                users.Add(new UserViewModel { Id = identityUser.Id, Email = identityUser.Email });
            }

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
            var client = _clientFactory.CreateClient("client");
            var userToUpdate = await client.GetFromJsonAsync<IdentityUser>($"api/Admin/{id}");

            if (userToUpdate == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var user = new UserViewModel { Id = userToUpdate.Id, Email = userToUpdate.Email };

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
            var client = _clientFactory.CreateClient("client");
            var response = await client.PutAsJsonAsync($"api/Admin/{model.Id}", model);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        /// <summary>
        /// Deletes a user by id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>IActionResult</returns>
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var client = _clientFactory.CreateClient("client");
            var userToDelete = await client.GetFromJsonAsync<IdentityUser>($"api/Admin/{id}");

            if (userToDelete == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var user = new UserViewModel { Id = userToDelete.Id, Email = userToDelete.Email };

            return View(user);
        }

        /// <summary>
        ///  Deletes a user
        /// </summary>
        /// <param name="model">UserViewModel</param>
        /// <returns>IActionResult</returns>
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed([FromForm] UserViewModel model)
        {
            var client = _clientFactory.CreateClient("client");
            var response = await client.DeleteAsync($"api/Admin/{model.Id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();
        }
    }
}
