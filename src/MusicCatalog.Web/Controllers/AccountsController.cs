using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MusicCatalog.IdentityApi.Models;
using MusicCatalog.Web.Services.Interfaces;
using MusicCatalog.Web.ViewModels;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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
        /// Web api service
        /// </summary>
        private readonly IUserApiService _webApiService;

        /// <summary>
        /// Mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Authentication type
        /// </summary>
        private const string AuthenticationType = "UserInfo";

        /// <summary>
        /// Jwt token key
        /// </summary>
        private const string JwtTokenKey = "secret_jwt_key";

        /// <summary>
        /// Authorization header name
        /// </summary>
        private const string Authorization = "Authorization";

        /// <summary>
        /// Authorization roles header name
        /// </summary>
        private const string AuthorizationRoles = "Authorization-roles";

        /// <summary>
        /// Accounts controller constructor
        /// </summary>
        /// <param name="webApiService">Web api service</param>
        /// <param name="mapper">Mapper</param>
        public AccountsController(IUserApiService webApiService, IMapper mapper)
        {
            _webApiService = webApiService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get-request to register new user
        /// </summary>
        /// <returns>IActionResult</returns>
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
            var user = _mapper.Map<RegisterModel>(model);
            var response = await _webApiService.RegisterUser(user);

            if (ModelState.IsValid)
            {
                if (response.IsSuccessStatusCode
                && response.Headers.Contains(Authorization)
                && response.Headers.Contains(AuthorizationRoles))
                {
                    // get token from a header
                    var token = response.Headers.GetValues(Authorization).ToArray()[0];
                    var roles = response.Headers.GetValues(AuthorizationRoles).ToArray()[0];

                    AuthorizeHandle(token, roles);

                    return RedirectToAction("Index", "Home");
                }
            }
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
            var user = _mapper.Map<LoginModel>(model);
            var response = await _webApiService.LoginUser(user);

            if (ModelState.IsValid)
            {
                if (response.IsSuccessStatusCode
                    && response.Headers.Contains(Authorization)
                    && response.Headers.Contains(AuthorizationRoles))
                {
                    // get token from header
                    var token = response.Headers.GetValues(Authorization).ToArray()[0];
                    var roles = response.Headers.GetValues(AuthorizationRoles).ToArray()[0];

                    AuthorizeHandle(token, roles);

                    return RedirectToAction("Index", "Home");
                }
            }

            return View(model);
        }

        /// <summary>
        /// Log out of the user
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            var response = await _webApiService.LogoutUser();

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
            HttpContext.Response.Cookies.Append(JwtTokenKey, token, new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict
            });

            var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var roleNames = roles.Split(',');

            // take claims of user from token, write it to the http context
            var claimsIdentity = new ClaimsIdentity(decodedToken.Claims, AuthenticationType,
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
            var roles = await _webApiService.GetRoles();
            var items = roles
                .Select(role => new SelectListItem { Text = role, Value = role })
                .ToList();

            return items;
        }
    }
}
