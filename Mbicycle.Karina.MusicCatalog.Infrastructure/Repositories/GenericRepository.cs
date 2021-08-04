using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mbicycle.Karina.MusicCatalog.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected MusicContext context;
        protected DbSet<T> dbSet;

        public GenericRepository(MusicContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public virtual void Create(T entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(int id)
        {
            dbSet.Find(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public virtual T GetById(int id)
        {
            return dbSet.Find(id);
        }

        public virtual void Update(T entity)
        {
            dbSet.Update(entity);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
