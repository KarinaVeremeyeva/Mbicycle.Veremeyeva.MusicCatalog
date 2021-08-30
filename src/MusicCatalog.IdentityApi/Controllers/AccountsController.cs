using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MusicCatalog.IdentityApi.Entities;
using MusicCatalog.IdentityApi.Models;
using MusicCatalog.IdentityApi.Services;
using System.Collections.Generic;
using System.Security.Claims;
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
        /// <param name="user">RegisterUser</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        [Route("Register")]
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
                    await _userManager.AddToRoleAsync(user, "Admin");
                }
                var roles = await _userManager.GetRolesAsync(user);
                //return Ok(_jwtTokenService.GenerateToken(user, roles));
            }

            return BadRequest(result.Errors);
        }

        /// <summary>
        /// User log in
        /// </summary>
        /// <param name="user">RegisterUser</param>
        /// <returns>IActionResult</returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody]User user)
        {
            if (!ModelState.IsValid || user == null)
            {
                return new BadRequestObjectResult(new { Message = "Login failed" });
            }

            var identityUser = await _userManager.FindByNameAsync(user.Login);
            var result = _userManager.PasswordHasher.VerifyHashedPassword(
                identityUser, identityUser.PasswordHash, user.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                return new BadRequestObjectResult(new { Message = "Login failed" });
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, identityUser.Email),
                new Claim(ClaimTypes.Name, identityUser.UserName)
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            return Ok(new { Message = "Logged in" });
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
    }
}
