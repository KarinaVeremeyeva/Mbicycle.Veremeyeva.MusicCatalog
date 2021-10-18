using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicCatalog.IdentityApi.Models;
using MusicCatalog.IdentityApi.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicCatalog.IdentityApi.Controllers
{
    /// <summary>
    /// Controller with crud operations for a user
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class AdminController : ControllerBase
    {
        /// <summary>
        /// User service
        /// </summary>
        private readonly IUserService _userService;

        /// <summary>
        /// Admin controller constructor
        /// </summary>
        /// <param name="userService">User service</param>
        public AdminController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Gets all users
        /// </summary>
        /// <returns>Users</returns>
        [HttpGet]
        public async Task<IEnumerable<UserModel>> GetUsersAsync()
        {
            var users = await _userService.GetUsersAsync();

            return users;
        }

        /// <summary>
        /// Gets a user by id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>User</returns>
        [HttpGet("{id}")]
        public async Task<UserModel> Get(string id)
        {
            var user = await _userService.GetUser(id);
            
            return user;
        }

        /// <summary>
        /// Updates a user
        /// </summary>
        /// <param name="model">User model</param>
        /// <returns>IActionResult</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] UserModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var result = await _userService.UpdateUser(model);
            if (result.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result.Errors);
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

            var result = await _userService.DeleteUser(id);
            if (result.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result.Errors);
        }

        /// <summary>
        /// Gets current user's role
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>Role name</returns>
        [HttpGet("role/{id}")]
        public async Task<string> GetUserRole(string id)
        {
            var role = await _userService.GetUserRole(id);

            return role;
        }
    }
}
