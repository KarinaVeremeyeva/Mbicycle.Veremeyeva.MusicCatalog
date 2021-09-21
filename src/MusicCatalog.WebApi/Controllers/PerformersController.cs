using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicCatalog.BusinessLogic.Interfaces;
using MusicCatalog.BusinessLogic.Models;
using MusicCatalog.Web.ViewModels;
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
        /// Mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Performers controlller constructor
        /// </summary>
        /// <param name="performersService">Performer service</param>
        /// <param name="mapper">Mapper</param>
        public PerformersController(IPerformersService performersService, IMapper mapper)
        {
            _performersService = performersService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets performers list
        /// </summary>
        /// <returns>Performers</returns>
        [HttpGet]
        public IEnumerable<PerformerViewModel> GetPerformers()
        {
            var performerModels = _performersService.GetPerformers();
            var performers = _mapper.Map<List<PerformerViewModel>>(performerModels);

            return performers;
        }

        /// <summary>
        /// Gets a performer by id
        /// </summary>
        /// <returns>Performers</returns>
        [HttpGet("{id}")]
        public PerformerViewModel GetPerformer(int id)
        {
            var performerModel = _performersService.GetPerformerById(id);
            var performer = _mapper.Map<PerformerViewModel>(performerModel);

            return performer;
        }

        /// <summary>
        /// Creates a new performer
        /// </summary>
        /// <param name="performerViewModel">Performer</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        public IActionResult CreatePerformer([FromBody] PerformerViewModel performerViewModel)
        {
            var performer = _mapper.Map<PerformerDto>(performerViewModel);

            if (ModelState.IsValid)
            {
                _performersService.CreatePerformer(performer);

                return Ok();
            }

            return BadRequest(ModelState); ;
        }

        /// <summary>
        /// Updates specified performer
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpPut("{id}")]
        public IActionResult UpdatePerformer([FromBody] PerformerViewModel performerViewModel)
        {
            var performer = _mapper.Map<PerformerDto>(performerViewModel);

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
