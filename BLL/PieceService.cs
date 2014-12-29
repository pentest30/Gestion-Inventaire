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
   public class PieceService:IRepository<Piece>
   {
       private readonly GmStoreContext _context;
       private readonly UnityContainer _container = new UnityContainer() ;
       private readonly EntityFactory<Piece> _factory;
       


       public PieceService()
       {
           _context = ContextSingleton.Instance;
           _container.RegisterInstance(new EntityFactory<Piece>());
           _factory = _container.Resolve<EntityFactory<Piece>>();
           
       }

       public Piece Create()
       {
           return _factory.Create();
       }

       public IEnumerable<Piece> SelectAll()
       {
           return _context.Pieces;
       }

       public Piece SelectById(object id)
       {
           return _context.Pieces.Find(id);
       }

       public void Insert(Piece item)
       {
           
           _context.Pieces.Attach(item);
           _context.Pieces.Add(item);
       }

       public void Update(Piece item)
       {
           _context.Pieces.Attach(item);
           _context.Entry(item).State = EntityState.Modified;
       }

       public void Delete(object id)
       {
           var item = _context.Pieces.Find(id);
           _context.Pieces.Attach(item);
           _context.Entry(item).State = EntityState.Deleted;

       }

       public void Save()
       {
           _context.SaveChanges();
       }

       public IEnumerable<Piece> Find(Func<Piece, bool> predicate)
       {
           return _context.Pieces.Where(predicate);
       }

       public IQueryable<Piece> GetAllLazyLoad(params Expression<Func<Piece, object>>[] children)
       {
           children.ToList().ForEach(x => _context.Pieces.Include(x).Load());
           return _context.Pieces;
       }

       private bool _disposed = false;

       protected virtual void Dispose(bool disposing)
       {
           if (!this._disposed)
           {
               if (disposing)
               {
                   _context.Dispose();
               }
           }
           this._disposed = true;
       }
       public void Dispose()
       {
           Dispose(true);
           GC.SuppressFinalize(this);
       }
   }
}
