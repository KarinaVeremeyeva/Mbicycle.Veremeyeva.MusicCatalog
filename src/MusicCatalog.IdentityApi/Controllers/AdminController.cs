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
        /// Get all users
        /// </summary>
        /// <returns>Users</returns>
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetUsers()
        {
            var users = _userManager.Users.ToList();
            var usersList = new List<string>();

            foreach (var userEmail in users)
            {
                usersList.Add(userEmail.Email);
            }

            return Ok(usersList); ;
        }

        /// <summary>
        /// Get a user by id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>User</returns>
        [HttpGet("{id}")]
        public async Task<IdentityUser> Get(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            return user;
        }

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>IActionResult</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);    
            if (user != null)
            {
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return Ok(result);
                }
            }

            return BadRequest();
        }

        /// <summary>
        /// Delete a user
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
    }
}
