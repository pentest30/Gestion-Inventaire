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
        public readonly DbSet<T> Set;

        public Repository(GMStoreContext dbContext)
        {
            _dbContext = dbContext;
            Set = _dbContext.Set<T>();
        }

        public T SelectById(object id)
        {
            return Set.Find(id);
        }

        public void Delete(object id)
        {
            var item = Set.Find(id);
            Set.Remove(item);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return Set.Where(predicate);
        }


        public void Insert(T item)
        {
            Set.Add(item);
        }

        public void Update(T item)
        {
            Set.Attach(item);
            _dbContext.Entry(item).State = EntityState.Modified;
        }



        public IEnumerable<T> SelectAll()
        {
            return Set.ToList();
        }
    }
}
