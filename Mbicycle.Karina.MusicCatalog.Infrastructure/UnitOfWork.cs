using Mbicycle.Karina.MusicCatalog.Infrastructure.Repositories;
using System;

namespace Mbicycle.Karina.MusicCatalog.Infrastructure
{
    public class UnitOfWork : IDisposable
    {
        private readonly MusicContext db = new MusicContext();

        private AlbumRepository albumRepository;
        private GenreRepository genreRepository;
        private PerformerRepository performerRepository;
        private SongRepository songRepository;

        private bool disposed;

        public AlbumRepository Albums
        {
            get
            {
                if (albumRepository == null)
                {
                    albumRepository = new AlbumRepository(db);
                }

                return albumRepository;
            }
        }

        public GenreRepository Genres
        {
            get
            {
                if (genreRepository == null)
                {
                    genreRepository = new GenreRepository(db);
                }

                return genreRepository;
            }
        }

        public PerformerRepository Performers 
        {
            get 
            { 
                if (performerRepository == null)
                {
                    performerRepository = new PerformerRepository(db);
                }

                return performerRepository;
            }
        }

        public SongRepository SongRepository
        {
            get
            {
                if (songRepository == null)
                {
                    songRepository = new SongRepository(db);
                }

                return songRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
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
