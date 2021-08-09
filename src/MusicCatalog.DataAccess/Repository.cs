using System.Collections.Generic;

namespace MusicCatalog.DataAccess
{
    public abstract class Repository<T> where T : class
    {
        public Repository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        protected string ConnectionString { get; }

        public abstract void Create(T entity);

        public abstract void Delete(int id);

        public abstract IEnumerable<T> GetAll();

        public abstract T GetById(int id);

        public abstract void Update(T entity);
    }
}
