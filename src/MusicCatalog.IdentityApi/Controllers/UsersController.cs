using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MusicCatalog.IdentityApi.Models;
using MusicCatalog.IdentityApi.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicCatalog.IdentityApi.Controllers
{
    /// <summary>
    /// Users controller of the identity api
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
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
        /// Jwt token service
        /// </summary>
        private readonly JwtTokenService _jwtTokenService;

        /// <summary>
        /// Users controller constructor
        /// </summary>
        /// <param name="signInManager">Users sign in manager</param>
        /// <param name="userManager">Users manager</param>
        /// <param name="roleManager">Roles manager</param>
        /// <param name="jwtTokenService">Jwt token service</param>
        public UsersController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            JwtTokenService jwtTokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenService = jwtTokenService;
        }

        /// <summary>
        /// Registers new user and returns the jwt token
        /// </summary>
        /// <param name="model">RegisterModel</param>
        /// <returns>IActionResult</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] UserModel model)
        {
            if (!ModelState.IsValid || model == null)
            {
                return new BadRequestObjectResult(new { Message = "Registration of user failed" });
            }

            var user = new IdentityUser()
            {
                Email = model.Email,
                UserName = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var roles = await _userManager.GetRolesAsync(user);

                await _signInManager.SignInAsync(user, isPersistent: false);

                return Ok(_jwtTokenService.GenerateJwtToken(user, roles));
            }

            return BadRequest(result.Errors);
        }

        /// <summary>
        /// User log in
        /// </summary>
        /// <param name="model">RegisterModel</param>
        /// <returns>IActionResult</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] UserModel model)
        {
            if (!ModelState.IsValid || model == null)
            {
                return new BadRequestObjectResult(new { Message = "Login failed" });
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                var roles = await _userManager.GetRolesAsync(user);
                //var claims = _userManager.GetClaimsAsync(user);

                return Ok(_jwtTokenService.GenerateJwtToken(user, roles));
            }

            return Forbid();
        }

        /// <summary>
        /// Log out of the user
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return Ok(new { Message = "Logged out" });
        }

        /// <summary>
        /// Validates the token
        /// </summary>
        /// <param name="token">Jwt token</param>
        /// <returns>IActionResult</returns>
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
        /// Gets all role names
        /// </summary>
        /// <returns></returns>
        [HttpGet("getAllRoles")]
        public ActionResult<IEnumerable<string>> GetAllRoles()
        {
            var roles = _roleManager.Roles.ToList();
            var roleNames = new List<string>();
            
            foreach (var role in roles)
            {
                roleNames.Add(role.Name);
            }

            return roleNames;
        }
    }
}
