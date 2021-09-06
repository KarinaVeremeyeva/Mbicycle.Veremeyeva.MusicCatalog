using Microsoft.AspNetCore.Authorization;
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
            var userEmails = await client.GetFromJsonAsync<List<string>>("api/Admin");

            return View(userEmails);
        }

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="model">User model</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        public async Task<IActionResult> Update([FromForm] UserViewModel model)
        {
            var client = _clientFactory.CreateClient("client");
            var response = await client.PostAsJsonAsync($"api/Admin/{model.Id}", model);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();
        }
    }
}
