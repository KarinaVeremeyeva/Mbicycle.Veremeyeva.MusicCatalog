using System.Collections.Generic;

namespace Mbicycle.Karina.MusicCatalog.Infrastructure.Repositories
{
    public interface IGenericRepository<T> where T: class
    {
        void Create(T entity);
        void Update(T entity);
        void Delete(int id);
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Save();
    }
}
