using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BLL
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> SelectAll();
        T SelectById(object id);
        void Insert(T item);
        void Update(T item);
        void Delete(object id);
        void Save();
        IEnumerable<T> Find(Func<T, bool> predicate);
        IEnumerable<T> GetAllLazyLoad( params Expression<Func<T, object>>[] children);

    }
}
