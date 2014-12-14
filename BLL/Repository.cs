using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL;

namespace BLL
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly GMStoreContext _dbContext;
        private readonly DbSet<T> _set;

        public Repository(GMStoreContext dbContext)
        {
            _dbContext = dbContext;
            _set = _dbContext.Set<T>();
        }

        public T SelectById(object id)
        {
            return _set.Find(id);
        }

        public void Delete(object id)
        {
            var item = _set.Find(id);
            _set.Remove(item);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return _set.Where(predicate);
        }


        public void Insert(T item)
        {
            _set.Add(item);
        }

        public void Update(T item)
        {
            _set.Attach(item);
            _dbContext.Entry(item).State = EntityState.Modified;
        }



        public IEnumerable<T> SelectAll()
        {
            return _set.ToList();
        }
    }
}
