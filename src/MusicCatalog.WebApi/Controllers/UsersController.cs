using Microsoft.AspNetCore.Mvc;
using MusicCatalog.BusinessLogic.Interfaces;
using MusicCatalog.IdentityApi.Models;
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
        private const string AuthorizationRoles = "AuthorizationRoles";

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
        /// <param name="model">User</param>
        /// <returns>IActionResult</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            var user = new RegisterModel
            {
                Email = model.Email,
                Role = model.Role,
                Password = model.Password
            };
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
        /// <param name="model">User</param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            var user = new LoginModel
            {
                Email = model.Email,
                Password = model.Password
            };
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
        public async Task<UserViewModel> Get(string id)
        {
            var user = await _accountApiService.GetUser(id);
            var userWithRole = new UserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Role = await _accountApiService.GetUserRole(user.Id)
            };

            return userWithRole;
        }

        /// <summary>
        /// Gets all users
        /// </summary>
        /// <returns>Users</returns>
        [HttpGet]
        public async Task<IEnumerable<UserViewModel>> GetUsers()
        {
            var users = await _accountApiService.GetUsers();
            var usersWithRole = new List<UserViewModel>();
            foreach (var user in users)
            {
                usersWithRole.Add(new UserViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Role = await _accountApiService.GetUserRole(user.Id)
                });
            }

            return usersWithRole;
        }

        /// <summary>
        /// Updates a user
        /// </summary>
        /// <param name="model">User</param>
        /// <returns>IActionResult</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] UserViewModel model)
        {
            var user = new UserModel
            {
                Id = model.Id,
                Email = model.Email,
                Role = await _accountApiService.GetUserRole(model.Id)
            };

            var updateUserResponse = await _accountApiService.PutUser(user);
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
    }
}
