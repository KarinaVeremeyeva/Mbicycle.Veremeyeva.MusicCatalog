using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MusicCatalog.Web.ViewModels;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
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
        /// Http client factory for creating http client instances
        /// </summary>
        private readonly IHttpClientFactory _clientFactory;

        /// <summary>
        /// Accounts controller constructor
        /// </summary>
        /// <param name="clientFactory"></param>
        public AccountsController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        /// <summary>
        /// Get-request to register new user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Post-request to register new user
        /// </summary>
        /// <param name="model">RegisterViewModel</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterViewModel model)
        {
            var client = _clientFactory.CreateClient("client");           
            var result = client.PostAsJsonAsync("Users/register", model).Result;

            if (result.IsSuccessStatusCode)
            {
                var token = await result.Content.ReadAsStringAsync();
                HttpContext.Response.Cookies.Append("secret_jwt_key", token, new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict
                });

                return RedirectToAction("Index", "Home");
            }

            return Forbid();
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
            var client = _clientFactory.CreateClient("client");
            var result = client.PostAsJsonAsync("Users/login", model).Result;

            if (result.IsSuccessStatusCode)
            {
                var token = await result.Content.ReadAsStringAsync();
                HttpContext.Response.Cookies.Append("secret_jwt_key", token, new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict
                });

                return RedirectToAction("Index", "Home");
            }

            return Forbid();
        }

        /// <summary>
        /// Log out of the user
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(JwtBearerDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Authorizes user by token and role
        /// </summary>
        /// <param name="token">Jwt token</param>
        /// <param name="roles">Roles of user</param>
        private async void AuthorizeHandle(string token, string roles)
        {
            HttpContext.Response.Cookies.Append("secret_jwt_key", token, new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict
            });

            var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var rolesArray = roles.Split(',');

            var claimsIdentity = new ClaimsIdentity(decodedToken.Claims, "UserInfo",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            HttpContext.User = new GenericPrincipal(claimsIdentity, rolesArray);

            await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, HttpContext.User);
        }

        /// <summary>
        /// Gets all roles
        /// </summary>
        /// <returns></returns>
        private async Task<IEnumerable<SelectListItem>> GetAllRoles()
        {
            var client = _clientFactory.CreateClient("client");
            var roles = await client.GetFromJsonAsync<List<string>>("Users/login/getAllRoles");
            var items = new List<SelectListItem>();

            foreach (var role in roles)
            {
                items.Add(new SelectListItem { Text = role, Value = role });
            }

            return items;
        }
    }
}
