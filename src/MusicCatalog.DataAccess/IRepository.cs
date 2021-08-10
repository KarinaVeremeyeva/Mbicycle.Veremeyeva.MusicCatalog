using System.Collections.Generic;

namespace MusicCatalog.DataAccess
{
    /// <summary>
    /// A repository for crud
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T: class
    {
        /// <summary>
        /// Creates entity
        /// </summary>
        /// <param name="entity"></param>
        void Create(T entity);

        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);

        /// <summary>
        /// Deletes entity
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);

        /// <summary>
        /// Returns all entities
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Returns entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById(int id);
    }
}
