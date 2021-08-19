using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MusicCatalog.DataAccess.Repositories.EFRepositories
{
    /// <summary>
    /// A generic repository for crud using entity framework
    /// </summary>
    /// <typeparam name="T">Entity</typeparam>
    public class EFRepository<T> : IRepository<T> where T : class
    {
        /// <summary>
        /// A database context
        /// </summary>
        protected MusicContext context;

        /// <summary>
        /// Represents the collection of all entities in the context
        /// </summary>
        protected DbSet<T> dbSet;

        public EFRepository(MusicContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        /// <inheritdoc cref="IRepository{T}.Create(T)"/>
        public virtual void Create(T entity)
        {
            dbSet.Add(entity);
            context.SaveChanges();
        }

        /// <inheritdoc cref="IRepository{T}.Update(T)"/>
        public virtual void Update(T entity)
        {
            dbSet.Update(entity);
            context.SaveChanges();
        }

        /// <inheritdoc cref="IRepository{T}.Delete(int)"/>
        public virtual void Delete(int id)
        {
            var genreToDelete = GetById(id);

            dbSet.Remove(genreToDelete);
            context.SaveChanges();

        }

        /// <inheritdoc cref="IRepository{T}.GetAll"/>
        public virtual IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        /// <inheritdoc cref="IRepository{T}.GetById(int)"/>
        public virtual T GetById(int id)
        {
            return dbSet.Find(id);
        }
    }
}
