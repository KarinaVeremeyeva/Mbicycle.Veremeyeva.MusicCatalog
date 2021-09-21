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
        /// Jwt token service
        /// </summary>
        private readonly JwtTokenService _jwtTokenService;

        /// <summary>
        /// User service
        /// </summary>
        private readonly IUserService _userService;

        /// <summary>
        /// Authorization header name
        /// </summary>
        private const string Authorization = "Authorization";

        /// <summary>
        /// Authorization roles header name
        /// </summary>
        private const string AuthorizationRoles = "Authorization-roles";

        /// <summary>
        /// Users controller constructor
        /// </summary>
        /// <param name="jwtTokenService">Jwt token service</param>
        /// <param name="userService">User service</param>
        public UsersController(JwtTokenService jwtTokenService, IUserService userService)
        {
            _jwtTokenService = jwtTokenService;
            _userService = userService;
        }

        /// <summary>
        /// Registers new user and returns the jwt token
        /// </summary>
        /// <param name="model">RegisterModel</param>
        /// <returns>IActionResult</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var user = new IdentityUser()
            {
                Email = model.Email,
                UserName = model.Email
            };
            var result = await _userService.CreateAsync(model);

            if (result == null)
            {
                return BadRequest(result.Errors);
            }

            var roles = await _userService.GetUserRoles(user);

            Response.Headers.Add(Authorization, _jwtTokenService.GenerateJwtToken(user, roles));
            Response.Headers.Add(AuthorizationRoles, roles.ToArray());

            return Ok();
        }

        /// <summary>
        /// User log in
        /// </summary>
        /// <param name="model">RegisterModel</param>
        /// <returns>IActionResult</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = new IdentityUser()
            {
                Email = model.Email,
                UserName = model.Email
            };
            var result = await _userService.AuthenticateAsync(model);
            if (!result.Succeeded)
            {
                return BadRequest(new { errorMessage = "Invalid email or password" });
            }

            var roles = await _userService.GetUserRoles(user);

            // add token to header of response
            Response.Headers.Add(Authorization, _jwtTokenService.GenerateJwtToken(user, roles));
            Response.Headers.Add(AuthorizationRoles, roles.ToArray());

            return Ok();
        }

        /// <summary>
        /// Log out of the user
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await _userService.LogoutAsync();

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
        /// <returns>Role names</returns>
        [HttpGet("getAllRoles")]
        public ActionResult<IEnumerable<string>> GetAllRoles()
        {
            var roleNames = _userService.GetAllRoles();

            return Ok(roleNames);
        }
    }
}
