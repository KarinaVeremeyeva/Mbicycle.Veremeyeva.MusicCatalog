using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MusicCatalog.IdentityApi.Entities;
using MusicCatalog.IdentityApi.Models;
using MusicCatalog.IdentityApi.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicCatalog.IdentityApi.Controllers
{
    /// <summary>
    /// Accounts controller
    /// </summary>
    [Route("/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        /// <summary>
        /// Users sign in manager
        /// </summary>
        private readonly SignInManager<User> _signInManager;

        /// <summary>
        /// Users manager
        /// </summary>
        private readonly UserManager<User> _userManager;

        /// <summary>
        /// Roles manager
        /// </summary>
        private readonly RoleManager<IdentityRole> _roleManager;

        /// <summary>
        /// Jwt token service
        /// </summary>
        private readonly JwtTokenService _jwtTokenService;

        /// <summary>
        /// Accounts controller constructor
        /// </summary>
        /// <param name="signInManager">Users sign in manager</param>
        /// <param name="userManager">Users manager</param>
        /// <param name="roleManager">Roles manager</param>
        /// <param name="jwtTokenService">Jwt token service</param>
        public AccountsController(
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            JwtTokenService jwtTokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenService = jwtTokenService;
        }

        /// <summary>
        /// Register new user
        /// </summary>
        /// <param name="model">RegisterModel</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid || model == null)
            {
                return new BadRequestObjectResult(new { Message = "Registration of user failed" });
            }

            var user = new User()
            {
                Login = model.Login,
            };
            var result = await _userManager.CreateAsync(user, user.Password);

            if (result.Succeeded)
            {
                if (user.Login == "Admin")
                {
                    await CreateAdminRoleIfNotExist();
                    await _userManager.AddToRoleAsync(user, "Admin");
                }
                var roles = await _userManager.GetRolesAsync(user);
                
                return Ok(_jwtTokenService.GenerateJwtToken(user, roles));
            }

            return BadRequest(result.Errors);
        }

        /// <summary>
        /// User log in
        /// </summary>
        /// <param name="model">RegisterModel</param>
        /// <returns>IActionResult</returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody]RegisterModel model)
        {
            if (!ModelState.IsValid || model == null)
            {
                return new BadRequestObjectResult(new { Message = "Login failed" });
            }

            var result = await _signInManager.PasswordSignInAsync(
                model.Login, model.Password, false, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(model.Login);

                return Ok(_jwtTokenService.GenerateJwtToken(user, new List<string>()));
            }

            return Forbid();
        }

        /// <summary>
        /// Log out of the user
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Ok(new { Message = "Logged out" });
        }


        [AllowAnonymous]
        [HttpGet("validate")]
        public IActionResult Validate(string token)
        {
            if (_jwtTokenService.ValidateToken(token))
            {
                return Ok();
            }

            return Forbid();
        }

        /// <summary>
        /// Creates admin role, if role doesn't exist
        /// </summary>
        /// <returns>Tas</returns>
        private async Task CreateAdminRoleIfNotExist()
        {
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }
        }
    }
}
