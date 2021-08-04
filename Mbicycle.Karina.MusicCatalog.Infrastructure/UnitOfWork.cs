using Mbicycle.Karina.MusicCatalog.Infrastructure.Repositories;
using System;

namespace Mbicycle.Karina.MusicCatalog.Infrastructure
{
    public class UnitOfWork : IDisposable
    {
        private readonly MusicContext _context;

        private AlbumRepository _albumRepository;
        private GenreRepository _genreRepository;
        private PerformerRepository _performerRepository;
        private SongRepository _songRepository;

        private bool disposed;

        public UnitOfWork(MusicContext context)
        {
            _context = context;
        }

        public AlbumRepository Albums
        {
            get
            {
                if (_albumRepository == null)
                {
                    _albumRepository = new AlbumRepository(_context);
                }

                return _albumRepository;
            }
        }

        public GenreRepository Genres
        {
            get
            {
                if (_genreRepository == null)
                {
                    _genreRepository = new GenreRepository(_context);
                }

                return _genreRepository;
            }
        }

        public PerformerRepository Performers 
        {
            get 
            { 
                if (_performerRepository == null)
                {
                    _performerRepository = new PerformerRepository(_context);
                }

                return _performerRepository;
            }
        }

        public SongRepository SongRepository
        {
            get
            {
                if (_songRepository == null)
                {
                    _songRepository = new SongRepository(_context);
                }

                return _songRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
