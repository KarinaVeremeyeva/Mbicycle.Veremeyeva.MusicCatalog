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
        public ActionResult<IEnumerable<IdentityUser>> GetUsers()
        {
            var users = _userManager.Users.ToList();

            return Ok(users);
        }

        /// <summary>
        /// Gets a user by id
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
    }
}
