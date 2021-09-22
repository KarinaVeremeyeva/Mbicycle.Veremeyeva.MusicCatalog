using Microsoft.AspNetCore.Mvc;
using MusicCatalog.BusinessLogic.Interfaces;
using MusicCatalog.BusinessLogic.Models;
using System.Collections.Generic;

namespace MusicCatalog.WebApi.Controllers
{
    /// <summary>
    /// Performers controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PerformersController : ControllerBase
    {
        /// <summary>
        /// Performer service
        /// </summary>
        private readonly IPerformersService _performersService;

        /// <summary>
        /// Performers controlller constructor
        /// </summary>
        /// <param name="performersService">Performer service</param>
        public PerformersController(IPerformersService performersService)
        {
            _performersService = performersService;
        }

        /// <summary>
        /// Gets performers list
        /// </summary>
        /// <returns>Performers</returns>
        [HttpGet]
        public IEnumerable<PerformerDto> GetPerformers()
        {
            var performers = _performersService.GetPerformers();

            return performers;
        }

        /// <summary>
        /// Gets a performer by id
        /// </summary>
        /// <returns>Performers</returns>
        [HttpGet("{id}")]
        public PerformerDto GetPerformer(int id)
        {
            var performer = _performersService.GetPerformerById(id);

            return performer;
        }

        /// <summary>
        /// Creates a new performer
        /// </summary>
        /// <param name="performer">Performer</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        public IActionResult CreatePerformer([FromBody] PerformerDto performer)
        {
            if (ModelState.IsValid)
            {
                _performersService.CreatePerformer(performer);

                return Ok();
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Updates specified performer
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpPut("{id}")]
        public IActionResult UpdatePerformer([FromBody] PerformerDto performer)
        {
            if (ModelState.IsValid)
            {
                _performersService.UpdatePerformer(performer);

                return Ok();
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Deletes performer by id
        /// </summary>
        /// <param name="id">Performer id</param>
        /// <returns>IActionResult</returns>
        [HttpDelete("{id}")]
        public IActionResult DeletePerformer(int id)
        {
            var performer = _performersService.GetPerformerById(id);

            if (performer != null)
            {
                _performersService.DeletePerformer(id);

                return Ok();
            }

            return BadRequest();
        }
    }
}
