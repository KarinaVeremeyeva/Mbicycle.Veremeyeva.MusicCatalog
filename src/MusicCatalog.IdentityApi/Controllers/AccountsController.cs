using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MusicCatalog.IdentityApi.Models;
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
        private readonly SignInManager<IdentityUser> _signInManager;

        /// <summary>
        /// Users manager
        /// </summary>
        private readonly UserManager<IdentityUser> _userManager;

        /// <summary>
        /// Roles manager
        /// </summary>
        private readonly RoleManager<IdentityRole> _roleManager;

        /// <summary>
        /// Accounts controller constructor
        /// </summary>
        /// <param name="signInManager">Users sign in manager</param>
        /// <param name="userManager">Users manager</param>
        /// <param name="roleManager">Roles manager</param>
        public AccountsController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        /// <summary>
        /// Register new user
        /// </summary>
        /// <param name="user">RegisterUser</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterUser user)
        {
            if (!ModelState.IsValid || user == null)
            {
                return new BadRequestObjectResult(new { Message = "Registration of user failed" });
            }

            var identityUser = new IdentityUser()
            {
                Email = user.Email,
            };
            var result = await _userManager.CreateAsync(identityUser, user.Password);

            if (!result.Succeeded)
            {
                var modelStates = new ModelStateDictionary();

                foreach (IdentityError error in result.Errors)
                {
                    modelStates.AddModelError(error.Code, error.Description);
                }

                return new BadRequestObjectResult(new
                {
                    Message = "Registration failed",
                    Errors = modelStates
                });
            }

            return Ok(new { Message = "Registration was successful"} );
        }
 
        /// <summary>
        /// User log in
        /// </summary>
        /// <param name="user">RegisterUser</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody]RegisterUser user)
        {
            if (!ModelState.IsValid || user == null)
            {
                return new BadRequestObjectResult(new { Message = "Login failed" });
            }

            var identityUser = await _userManager.FindByNameAsync(user.Email);
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
