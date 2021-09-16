using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MusicCatalog.IdentityApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicCatalog.IdentityApi.Controllers
{
    /// <summary>
    /// Controller with crud operations for a user
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        /// <summary>
        /// User manager
        /// </summary>
        private readonly UserManager<IdentityUser> _userManager;

        /// <summary>
        /// Admin controller constructor
        /// </summary>
        /// <param name="userManager">User manager</param>
        public AdminController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Gets all users
        /// </summary>
        /// <returns>Users</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetUsersAsync()
        {
            var users = _userManager.Users.ToList();
            var usersWithRole = new List<UserModel>();
            foreach (var user in users)
            {
                usersWithRole.Add(new UserModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Role = await GetUserRole(user.Id)
                });
            }

            return Ok(usersWithRole);
        }

        /// <summary>
        /// Gets a user by id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>User</returns>
        [HttpGet("{id}")]
        public async Task<UserModel> Get(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var userWithRole = new UserModel
            {
                Id = user.Id,
                Email = user.Email,
                Role = await GetUserRole(user.Id)
            };

            return userWithRole;
        }

        /// <summary>
        /// Updates a user
        /// </summary>
        /// <param name="model">User model</param>
        /// <returns>IActionResult</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] UserModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.Email = model.Email;
                    user.UserName = model.Email;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return Ok(result);
                    }
                }
            }
           
            return BadRequest(ModelState);
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

            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return Ok(result);
                }
            }

            return BadRequest();
        }

        /// <summary>
        /// Changes user's role
        /// </summary>
        /// <param name="id">User id</param>
        /// <param name="role">Role</param>
        /// <returns>IActionResult</returns>
        [HttpPut("update-role/{id}")]
        public async Task<IActionResult> UpdateRole(string id, [FromBody] string role)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(role))
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var result = await _userManager.RemoveFromRolesAsync(user, userRoles);
                await _userManager.AddToRoleAsync(user, role);

                if (result.Succeeded)
                {
                    return Ok(result);
                }
            }

            return BadRequest();
        }

        /// <summary>
        /// Gets current user's role
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>Role name</returns>
        [HttpGet("role/{id}")]
        public async Task<string> GetUserRole(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.ToList().FirstOrDefault();

            return role;
        }
    }
}
