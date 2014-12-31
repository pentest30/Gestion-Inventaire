﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DAL;

namespace BLL
{
    public class Repository<T> : IRepository<T> where T : class 
    {
        private readonly GmStoreContext _dbContext;
        public readonly DbSet<T> Set;

        public Repository()
        {
            _dbContext =ContextSingleton.Instance;
            Set = _dbContext.Set<T>();
        }

        public T SelectById(object id)
        {
            return Set.Find(id);
        }

        public void Delete(object id)
        {
            var item = Set.Find(id);
           // Set.Attach(item);
            if (item== null) return;
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
        public T FindSingle(Func<T, bool> predicate)
        {
            return Set.FirstOrDefault(predicate);
        }
        public IQueryable<T> GetAllLazyLoad( params Expression<Func<T, object>>[] children)
        {
            children.ToList().ForEach(x => Set.Include(x).Load());
            return Set;
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

        public void Dispose()
        {
            _dbContext.Dispose();

        }

        public void RetrunId(T item)
        {
           _dbContext.Entry(item).GetDatabaseValues();
            
        }

      
    }
}
