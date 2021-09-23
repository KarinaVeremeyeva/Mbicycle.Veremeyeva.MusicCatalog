using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicCatalog.IdentityApi.Models;
using MusicCatalog.WebApi.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicCatalog.WebApi.Controllers
{
    /// <summary>
    /// Users controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        /// <summary>
        /// Account api client
        /// </summary>
        private readonly IAccountApiService _accountApiService;

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
        /// <param name="accountApiService">Account api service</param>
        public UsersController(IAccountApiService accountApiService)
        {
            _accountApiService = accountApiService;
        }

        /// <summary>
        /// Registers new user
        /// </summary>
        /// <param name="user">User</param>
        /// <returns>IActionResult</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel user)
        {
            var response = await _accountApiService.RegisterUser(user);

            if (response.IsSuccessStatusCode
                && response.Headers.Contains(Authorization)
                && response.Headers.Contains(AuthorizationRoles))
            {
                var token = response.Headers.GetValues(Authorization).ToArray()[0];
                var roles = response.Headers.GetValues(AuthorizationRoles).ToArray()[0];

                Response.Headers.Add(Authorization, token);
                Response.Headers.Add(AuthorizationRoles, roles);

                return Ok();
            }

            return Forbid();
        }

        /// <summary>
        /// User log in
        /// </summary>
        /// <param name="user">User</param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel user)
        {
            var response = await _accountApiService.LoginUser(user);

            if (response.IsSuccessStatusCode
                && response.Headers.Contains(Authorization)
                && response.Headers.Contains(AuthorizationRoles))
            {
                var token = response.Headers.GetValues(Authorization).ToArray()[0];
                var roles = response.Headers.GetValues(AuthorizationRoles).ToArray()[0];

                Response.Headers.Add(Authorization, token);
                Response.Headers.Add(AuthorizationRoles, roles);

                return Ok();
            }

            return BadRequest(new { errorText = "Invalid username or password." });
        }

        /// <summary>
        /// Log out of the user
        /// </summary>
        /// <returns></returns>
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            var response = await _accountApiService.LogoutUser();

            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }

            return Forbid();
        }

        /// <summary>
        /// Gets a user by id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>User</returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<UserModel> Get(string id)
        {
            var user = await _accountApiService.GetUser(id);

            return user;
        }

        /// <summary>
        /// Gets all users
        /// </summary>
        /// <returns>Users</returns>
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IEnumerable<UserModel>> GetUsers()
        {
            var users = await _accountApiService.GetUsers();

            return users;
        }

        /// <summary>
        /// Updates a user
        /// </summary>
        /// <param name="user">User</param>
        /// <returns>IActionResult</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update([FromBody] UserModel user)
        {
            var updateUserResponse = await _accountApiService.UpdateUser(user);
            var updateRoleResponse = await _accountApiService.UpdateRole(user.Id, user.Role);

            if (updateUserResponse.IsSuccessStatusCode
                && updateRoleResponse.IsSuccessStatusCode)
            {
                return Ok();
            }

            return BadRequest();
        }

        /// <summary>
        /// Deletes a user
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>IActionResult</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await _accountApiService.GetUser(id);
            if (user != null)
            {
                var result = await _accountApiService.DeleteUser(user.Id);
                if (result.IsSuccessStatusCode)
                {
                    return Ok(result);
                }
            }

            return BadRequest();
        }

        /// <summary>
        /// Gets all roles from the identity api
        /// </summary>
        /// <returns>Roles</returns>
        [HttpGet("getAllRoles")]
        public async Task<ActionResult<IEnumerable<string>>> GetAllRoles()
        {
            var roles = await _accountApiService.GetRoles();

            return Ok(roles);
        }
    }
}
