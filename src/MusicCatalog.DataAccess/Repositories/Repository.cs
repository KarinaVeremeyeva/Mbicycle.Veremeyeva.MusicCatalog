using System.Collections.Generic;

namespace MusicCatalog.DataAccess.Repositories
{
    /// <summary>
    /// An abstract generic repository for crud
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        public Repository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        /// <summary>
        /// Connection string for database
        /// </summary>
        protected string ConnectionString { get; }

        /// <inheritdoc cref="IRepository{T}.Create(T)"/>
        public abstract void Create(T entity);

        /// <inheritdoc cref="IRepository{T}.Delete(int)"/>
        public abstract void Delete(int id);

        /// <inheritdoc cref="IRepository{T}.GetAll"/>
        public abstract IEnumerable<T> GetAll();

        /// <inheritdoc cref="IRepository{T}.GetById(int)"/>
        public abstract T GetById(int id);

        /// <inheritdoc cref="IRepository{T}.Update(T)"/>
        public abstract void Update(T entity);
    }
}
