using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DAL;
using GM.Entity.Models;
using Microsoft.Practices.Unity;

namespace BLL
{
   public class StockService:IRepository<PieceMagasin>
   {
       readonly static UnityContainer Container = new UnityContainer();
       private static EntityFactory<PieceMagasin> _factory;
       private static GmStoreContext _db;

       public StockService(GmStoreContext db)
       {
           _db = db;
           Container.RegisterInstance(new EntityFactory<PieceMagasin>(_db));
           _factory = Container.Resolve<EntityFactory<PieceMagasin>>();
           Container.RegisterInstance(new Repository<PieceMagasin>(_db));
       }

       public IEnumerable<PieceMagasin> SelectAll()
       {
           return _db.PieceMagasins;
       }

       public PieceMagasin SelectById(object id)
       {
           return _db.PieceMagasins.FirstOrDefault(a => a.Id == (long)id);
       }

       public void Insert(PieceMagasin item)
       {
           _db.PieceMagasins.Add(item);
       }

     

       public void Update(PieceMagasin item)
       {
           _db.PieceMagasins.Attach(item);
           _db.Entry(item).State = EntityState.Modified;
       }

       public void Delete(object id)
       {
           var item = _db.PieceMagasins.Find(id);
           _db.PieceMagasins.Attach(item);
           _db.Entry(item).State = EntityState.Deleted;
       }

       public void Save()
       {
           _db.SaveChanges();
       }

       public IEnumerable<PieceMagasin> Find(Func<PieceMagasin, bool> predicate)
       {
          return _db.PieceMagasins.Where(predicate);
       }

       public IQueryable<PieceMagasin> GetAllLazyLoad(params Expression<Func<PieceMagasin, object>>[] children)
       {
           children.ToList().ForEach(x => _db.PieceMagasins.Include(x).Load());
           return _db.PieceMagasins;
       }
       public PieceMagasin Create()
       {
           return _factory.Create();
       }
   }
}
