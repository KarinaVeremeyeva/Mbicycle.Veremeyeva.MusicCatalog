using System;
using System.Collections.Generic;

namespace MusicCatalog.DataAccess
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public Repository(string connectionStr)
        {
            ConnectionStr = connectionStr;
        }

        protected string ConnectionStr { get; }

        public void Create(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public T GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
