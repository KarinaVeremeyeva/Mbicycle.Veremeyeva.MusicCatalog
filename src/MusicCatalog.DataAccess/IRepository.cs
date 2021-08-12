using System.Collections.Generic;

namespace MusicCatalog.DataAccess
{
    /// <summary>
    /// A repository for crud
    /// </summary>
    /// <typeparam name="T">Entity</typeparam>
    public interface IRepository<T> where T: class
    {
        /// <summary>
        /// Creates entity
        /// </summary>
        /// <param name="entity">New entity</param>
        void Create(T entity);

        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="entity">Entity to update</param>
        void Update(T entity);

        /// <summary>
        /// Deletes entity
        /// </summary>
        /// <param name="id">Entity id to delete</param>
        void Delete(int id);

        /// <summary>
        /// Returns all entities
        /// </summary>
        /// <returns>Entities</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Returns entity by id
        /// </summary>
        /// <param name="id">Entity id</param>
        /// <returns>Entity</returns>
        T GetById(int id);
    }
}
