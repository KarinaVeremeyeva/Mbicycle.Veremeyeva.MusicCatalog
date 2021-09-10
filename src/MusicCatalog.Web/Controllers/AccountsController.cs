using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MusicCatalog.Web.Services;
using MusicCatalog.Web.ViewModels;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace MusicCatalog.Web.Controllers
{
    /// <summary>
    /// Accounts controller for UI
    /// </summary>
    public class AccountsController : Controller
    {
        /// <summary>
        /// Account api client
        /// </summary>
        private readonly AccountApiClient _accountApiClient;

        /// <summary>
        /// Accounts controller constructor
        /// </summary>
        /// <param name="accountApiClient">Account api client</param>
        public AccountsController(AccountApiClient accountApiClient)
        {
            _accountApiClient = accountApiClient;
        }

        /// <summary>
        /// Get-request to register new user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult>  Register()
        {
            return View(new RegisterViewModel { ExistingRoles = await GetAllRoles() });
        }

        /// <summary>
        /// Post-request to register new user
        /// </summary>
        /// <param name="model">RegisterViewModel</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterViewModel model)
        {
            var response = await _accountApiClient.Client.PostAsJsonAsync("api/Users/register", model);

            if (response.IsSuccessStatusCode
                && response.Headers.Contains("Authorization")
                && response.Headers.Contains("AuthorizationRoles"))
            {
                // get token from a header
                var token = response.Headers.GetValues("Authorization").ToArray()[0];
                var roles = response.Headers.GetValues("AuthorizationRoles").ToArray()[0];

                AuthorizeHandle(token, roles);

                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError(string.Empty, "Wrong user details");
            model.ExistingRoles = await GetAllRoles();

            return View(model);
        }

        /// <summary>
        /// Get-request for log in of user
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        } 

        /// <summary>
        /// Post-request for log in of user
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginViewModel model)
        {
            var response = await _accountApiClient.Client.PostAsJsonAsync("api/Users/login", model);

            if (response.IsSuccessStatusCode
                && response.Headers.Contains("Authorization")
                && response.Headers.Contains("AuthorizationRoles"))
            {
                // get token from header
                var token = response.Headers.GetValues("Authorization").ToArray()[0];
                var roles = response.Headers.GetValues("AuthorizationRoles").ToArray()[0];

                AuthorizeHandle(token, roles);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Wrong user details");

            return View(model);
        }

        /// <summary>
        /// Log out of the user
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            var response = await _accountApiClient.Client.GetAsync("api/Users/logout");

            if (response.IsSuccessStatusCode)
            {
                await HttpContext.SignOutAsync(JwtBearerDefaults.AuthenticationScheme);

                return RedirectToAction("Index", "Home");
            }

            return Forbid();
        }

        /// <summary>
        /// Authorizes user by token and role
        /// </summary>
        /// <param name="token">Jwt token</param>
        /// <param name="roles">Roles</param>
        private async void AuthorizeHandle(string token, string roles)
        {
            HttpContext.Response.Cookies.Append("secret_jwt_key", token, new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict
            });

            var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var roleNames = roles.Split(',');

            // take claims of user from token, write it to the http context
            var claimsIdentity = new ClaimsIdentity(decodedToken.Claims, "UserInfo",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            
            HttpContext.User = new GenericPrincipal(claimsIdentity, roleNames);

            await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, HttpContext.User);
        }

        /// <summary>
        /// Gets all roles from the identity api
        /// </summary>
        /// <returns>Roles</returns>
        private async Task<IEnumerable<SelectListItem>> GetAllRoles()
        {
            var roles = await _accountApiClient.Client
                .GetFromJsonAsync<List<string>>("api/Users/getAllRoles");
            var items = new List<SelectListItem>();

            foreach (var role in roles)
            {
                items.Add(new SelectListItem { Text = role, Value = role });
            }

            return items;
        }
    }
}
