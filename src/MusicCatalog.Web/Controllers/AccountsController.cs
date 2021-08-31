using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicCatalog.Web.ViewModels;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MusicCatalog.Web.Controllers
{
    /// <summary>
    /// Accounts controller for UI
    /// </summary>
    [Route("[controller]")]
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
        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Post-request to register new user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            var client = _clientFactory.CreateClient();   
            client.BaseAddress = new Uri("http://localhost:2563");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            
            var content = new StringContent(
                JsonConvert.SerializeObject(model), 
                Encoding.UTF8, 
                "application/json");

            var result = client.PostAsync("accounts/register", content).Result;

            if (result.IsSuccessStatusCode)
            {
                var token = await result.Content.ReadAsStringAsync();
                HttpContext.Response.Cookies.Append("secret_jwt_key", token, new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict
                });

                return Ok();
            }

            return Forbid();
        }

        /// <summary>
        /// Get-request for log in of user
        /// </summary>
        /// <returns></returns>
        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Post-request for log in of user
        /// </summary>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginViewModel model)
        {
            var client = _clientFactory.CreateClient();
            client.BaseAddress = new Uri("http://localhost:2563");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var content = new StringContent(
               JsonConvert.SerializeObject(model),
               Encoding.UTF8,
               "application/json");

            var result = await client.PostAsync("accounts/login", content);

            if (result.IsSuccessStatusCode)
            {
                var token = await result.Content.ReadAsStringAsync();
                HttpContext.Response.Cookies.Append("secret_jwt_key", token, new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict
                });

                return Ok();
            }

            return Forbid();
        }
    }
}
