﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MusicCatalog.Web.Services;
using MusicCatalog.Web.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicCatalog.Web.Controllers
{
    /// <summary>
    /// Controller for accounts managing
    /// </summary>
    [Authorize(Roles = "admin")]
    public class ManageAccountsController : Controller
    {
        /// <summary>
        /// Account api client
        /// </summary>
        private readonly IAccountApiService _accountApiClient;

        /// <summary>
        /// Mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Manage accounts controller constructor
        /// </summary>
        /// <param name="accountApiClient">Account api client</param>
        /// <param name="mapper">Mapper</param>
        public ManageAccountsController(IAccountApiService accountApiClient, IMapper mapper)
        {
            _accountApiClient = accountApiClient;
            _mapper = mapper;
        }

        /// <summary>
        /// Displays a list of users emails
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _accountApiClient.GetUsers();
            var userViewModels = _mapper.Map<List<UserViewModel>>(users);

            return View(userViewModels);
        }

        /// <summary>
        /// Updates user bu id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>IActionResult</returns>
        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var userToUpdate = await _accountApiClient.GetUser(id);

            if (userToUpdate == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var user = _mapper.Map<UserViewModel>(userToUpdate);

            return View(user);
        }

        /// <summary>
        /// Updates user
        /// </summary>
        /// <param name="model">UserViewModel</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        public async Task<IActionResult> Update([FromForm] UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<IdentityUser>(model);
                var response = await _accountApiClient.PutUser(user);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError(string.Empty, "Wrong user details");
            }

            return View(model);
        }

        /// <summary>
        /// Deletes a user by id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>IActionResult</returns>
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var userToDelete = await _accountApiClient.GetUser(id);

            if (userToDelete == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var user = _mapper.Map<UserViewModel>(userToDelete);

            return View(user);
        }

        /// <summary>
        /// Deletes a user
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Error", "Home");
            }

            var response = await _accountApiClient.DeleteUser(id);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        /// <summary>
        /// Changes role by user id
        /// </summary>
        /// <param name="id">User is</param>
        /// <returns>IActionResult</returns>
        [HttpGet]
        public async Task<IActionResult> ChangeRole(string id)
        {
            var userToUpdate = await _accountApiClient.GetUser(id);
            var currentUserRole = await _accountApiClient.GetUserRole(userToUpdate.Id);

            if (userToUpdate == null)
            {
                return RedirectToAction("Error", "Home");
            }
            var user = new UserViewModel
            {
                Id = userToUpdate.Id,
                Email = userToUpdate.Email,
                Role = currentUserRole,
                ExistingRoles = await GetAllRoles()
            };
            //var user = _mapper.Map<UserViewModel>(userToUpdate);

            return View(user);
        }

        /// <summary>
        /// Changes user's role
        /// </summary>
        /// <param name="model">User model</param>
        /// <param name="role">Role</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        public async Task<IActionResult> ChangeRole(string id, [FromForm] string role)
        {
            var user = await _accountApiClient.GetUser(id);
            var currentUserRole = await _accountApiClient.GetUserRole(id);

            var model = new UserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Role = currentUserRole,
                ExistingRoles = await GetAllRoles()
            };

            if (ModelState.IsValid)
            {
                //var user = _mapper.Map<IdentityUser>(model);
                //var response = await _accountApiClient.ChangeRole(user, role);
                var response = await _accountApiClient.ChangeRole(user.Id, role);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            ModelState.AddModelError(string.Empty, "Wrong user details");
            //model.ExistingRoles = await GetAllRoles();
            return View(model);
        }

        /// <summary>
        /// Gets all roles from the identity api
        /// </summary>
        /// <returns>Roles</returns>
        private async Task<IEnumerable<SelectListItem>> GetAllRoles()
        {
            var roles = await _accountApiClient.GetRoles();
            var items = roles
                .Select(role => new SelectListItem { Text = role, Value = role })
                .ToList();

            return items;
        }
    }
}
