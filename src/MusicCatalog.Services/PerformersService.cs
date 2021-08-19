using MusicCatalog.DataAccess;
using MusicCatalog.DataAccess.Entities;
using MusicCatalog.Services.Interfaces;
using System.Collections;

namespace MusicCatalog.Services
{
    /// <summary>
    /// An implementation of the performers service
    /// </summary>
    public class PerformersService : IPerformersService
    {
        /// <inheritdoc cref="IRepository{T}"/>
        private readonly IRepository<Performer> _performerRepository;

        public PerformersService(IRepository<Performer> repository)
        {
            _performerRepository = repository;
        }

        /// <inheritdoc cref="IPerformersService.CreatePerformer(Performer)"/>
        public void CreatePerformer(Performer performer)
        {
            _performerRepository.Create(performer);
        }

        /// <inheritdoc cref="IPerformersService.GetPerformerById(int)"/>
        public Performer GetPerformerById(int performerId)
        {
            return _performerRepository.GetById(performerId);
        }

        /// <inheritdoc cref="IPerformersService.GetPerformers"/>
        public IEnumerable GetPerformers()
        {
            return _performerRepository.GetAll();
        }

        /// <inheritdoc cref="IPerformersService.UpdatePerformer(Performer)"/>
        public void UpdatePerformer(Performer performer)
        {
            _performerRepository.Update(performer);
        }

        /// <inheritdoc cref="IPerformersService.DeletePerformer(int)"/>
        public void DeletePerformer(int performerId)
        {
            _performerRepository.Delete(performerId);
        }
    }
}
