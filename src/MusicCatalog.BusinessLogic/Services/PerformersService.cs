using AutoMapper;
using MusicCatalog.BusinessLogic.Interfaces;
using MusicCatalog.BusinessLogic.Models;
using MusicCatalog.DataAccess;
using MusicCatalog.DataAccess.Entities;
using System.Collections.Generic;

namespace MusicCatalog.BusinessLogic.Services
{
    /// <summary>
    /// An implementation of the performers service
    /// </summary>
    public class PerformersService : IPerformersService
    {
        /// <inheritdoc cref="IRepository{T}"/>
        private readonly IRepository<Performer> _performerRepository;

        /// <summary>
        /// Mapper
        /// </summary>
        private readonly IMapper _mapper;

        public PerformersService(IRepository<Performer> repository, IMapper mapper)
        {
            _performerRepository = repository;
            _mapper = mapper;
        }

        /// <inheritdoc cref="IPerformersService.CreatePerformer(PerformerDto)"/>
        public void CreatePerformer(PerformerDto performerModel)
        {
            var performer = _mapper.Map<Performer>(performerModel);

            _performerRepository.Create(performer);
        }

        /// <inheritdoc cref="IPerformersService.GetPerformerById(int)"/>
        public PerformerDto GetPerformerById(int performerId)
        {
            var performer = _performerRepository.GetById(performerId);

            return _mapper.Map<PerformerDto>(performer);
        }

        /// <inheritdoc cref="IPerformersService.GetPerformers"/>
        public IEnumerable<PerformerDto> GetPerformers()
        {
            var performers = _performerRepository.GetAll();

            return _mapper.Map<List<PerformerDto>>(performers);
        }

        /// <inheritdoc cref="IPerformersService.UpdatePerformer(PerformerDto)"/>
        public void UpdatePerformer(PerformerDto performerModel)
        {
            var performer = _mapper.Map<Performer>(performerModel);

            _performerRepository.Update(performer);
        }

        /// <inheritdoc cref="IPerformersService.DeletePerformer(int)"/>
        public void DeletePerformer(int performerId)
        {
            _performerRepository.Delete(performerId);
        }
    }
}
