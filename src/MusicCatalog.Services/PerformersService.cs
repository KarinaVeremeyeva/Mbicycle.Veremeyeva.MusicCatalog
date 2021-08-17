using MusicCatalog.DataAccess;
using MusicCatalog.DataAccess.Entities;
using System;
using System.Collections;

namespace MusicCatalog.Services
{
    /// <summary>
    /// An implementation of the performers service
    /// </summary>
    public class PerformersService : IPerformersService, IDisposable
    {
        /// <inheritdoc cref="MusicContext"/>
        private readonly MusicContext _context;

        /// <inheritdoc cref="IRepository{T}"/>
        private readonly IRepository<Performer> _performerRepository;

        /// <summary>
        /// A dispose value
        /// </summary>
        private bool _disposed = false;

        public PerformersService(MusicContext context, IRepository<Performer> repository)
        {
            _performerRepository = repository;
            _context = context;
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

        /// <inheritdoc cref="IPerformersService.Save"/>
        public void Save()
        {
            _context.SaveChanges();
        }

        /// <summary>
        /// Implements dispose method
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        /// <summary>
        /// Clears resources
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
